using System.Drawing.Drawing2D;

namespace EasyAuthenticator.UI
{
    /// <summary>
    /// 液态玻璃面板（macOS Tahoe 风格）：取样底图的模糊材质，
    /// 叠白色磨砂蒙层 + 顶部镜面高光带 + 上亮下暗镜面描边，浅色玻璃悬浮于底图之上
    /// </summary>
    public class GlassPanel : Panel
    {
        /// <summary>材质变体（材质重量=层级：主卡片更厚，内嵌区域更沉）</summary>
        public enum Variant
        {
            /// <summary>普通卡片（59%白磨砂）</summary>
            Raised,
            /// <summary>主卡片（67%白磨砂+更强镜面边缘，更厚）</summary>
            Hero,
            /// <summary>内嵌区域（43%白+顶部内阴影，下沉感）</summary>
            Inset
        }

        /// <summary>共享材质画布（底图模糊版，由窗体启动时生成赋值）</summary>
        public static Bitmap? Material;
        /// <summary>底图原图（Cover映射计算用）</summary>
        public static Bitmap? RawImage;
        /// <summary>客户区像素 → 材质像素的缩放（含Cover缩放与材质半分辨率）</summary>
        public static float MapScale = 0.5f;
        /// <summary>Cover裁剪在材质坐标系的X偏移</summary>
        public static float MapOffsetX;
        /// <summary>Cover裁剪在材质坐标系的Y偏移</summary>
        public static float MapOffsetY;

        /// <summary>圆角半径</summary>
        public int CornerRadius { get; set; } = 16;

        private Variant variant = Variant.Raised;
        /// <summary>材质变体</summary>
        public Variant MaterialVariant
        {
            get => variant;
            set { variant = value; Invalidate(); }
        }

        /// <summary>面板自绘文本行（代替透明标签控件——透明标签周期性刷新会被父级合成路径画到兄弟控件上）</summary>
        public class TextOverlay
        {
            public Rectangle Rect;
            public string Text = "";
            public Font? Font;
            public Color Color;
            public TextFormatFlags Flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix;

            public TextOverlay(Rectangle rect, Font? font, Color color)
            {
                Rect = rect;
                Font = font;
                Color = color;
            }
        }

        private readonly List<TextOverlay> overlays = new();
        /// <summary>面板自绘文本行列表（由窗体配置）</summary>
        public List<TextOverlay> Overlays => overlays;

        //—— 可点击交互（替代玻璃按钮：hover附加白蒙层、press加深、Primary主色填充）——
        private bool clickable;
        /// <summary>是否可点击（手型光标+悬停/按压反馈）</summary>
        public bool Clickable
        {
            get => clickable;
            set
            {
                clickable = value;
                Cursor = value ? Cursors.Hand : Cursors.Default;
            }
        }

        private bool primary;
        /// <summary>主色调按钮样式（Accent填充）</summary>
        public bool Primary
        {
            get => primary;
            set { primary = value; Invalidate(); }
        }

        private bool hover;
        private bool pressed;

        protected override void OnMouseEnter(EventArgs e) { if (clickable) { hover = true; Invalidate(); } base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { if (clickable) { hover = false; pressed = false; Invalidate(); } base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { if (clickable) { pressed = true; Invalidate(); } base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { if (clickable) { pressed = false; Invalidate(); } base.OnMouseUp(e); }
        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }

        public GlassPanel()
        {
            DoubleBuffered = true;
            //关键：不用透明BackColor！透明会让WinForms给面板HWND加WS_EX_TRANSPARENT，
            //刷新时内容会被父级合成路径错位绘制（虚影）。
            //不透明HWND + 圆角外显式取样父级背景（步骤0），视觉一致且合成路径正常
            BackColor = Color.White; //会被自绘完全覆盖，仅为不透明占位
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var g = e.Graphics;
            //0) 先画父级背景（圆角路径外的矩形角落与背后内容一致，消除黑色棱角）：
            //   显式分发——InvokePaintBackground内部路径不保证走到我们的重写
            if (Parent is Form1 form1)
                form1.PaintArtworkForChild(g, Bounds);
            else if (Parent is GlassPanel glassParent)
                glassParent.PaintBackgroundForChild(g, Bounds);
            else if (Parent != null)
                InvokePaintBackground(Parent, e);
            //保存绘图状态：被透明子控件回调时，传入的Graphics带有子控件平移与裁剪，
            //必须Intersect裁剪并最终Restore，绝不能ResetClip（会炸掉兄弟控件的裁剪区域）
            var state = g.Save();
            try
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                using var path = GlassTheme.RoundedRect(rect, CornerRadius);

                //1) 基色兜底
                using (var b = new SolidBrush(GlassTheme.BgBase))
                    g.FillPath(b, path);

                //2) 取样模糊材质对应区域（磨砂玻璃本体；客户区坐标经Cover映射换算到材质坐标）
                if (Material != null)
                {
                    var src = MaterialRect();
                    src.Intersect(new Rectangle(0, 0, Material.Width, Material.Height));
                    if (src.Width > 0 && src.Height > 0)
                    {
                        g.SetClip(path, CombineMode.Intersect);
                        //材质是模糊半分辨率图，放大绘制无感知
                        g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                        g.DrawImage(Material, new Rectangle(0, 0, (int)(src.Width / MapScale), (int)(src.Height / MapScale)), src, GraphicsUnit.Pixel);
                        g.Restore(state);
                        state = g.Save();
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                    }
                }

                //3) 白色磨砂蒙层与体积处理
                switch (variant)
                {
                    case Variant.Hero:
                        using (var fill = new SolidBrush(Color.FromArgb(170, 255, 255, 255)))
                            g.FillPath(fill, path);
                        //底部内阴影（厚度）
                        using (var sh = new LinearGradientBrush(new Rectangle(0, Height - Height / 4, Width, Height / 4),
                            Color.FromArgb(0, 0, 0, 0), Color.FromArgb(28, 0, 0, 0), 90f))
                        {
                            g.SetClip(path, CombineMode.Intersect);
                            g.FillRectangle(sh, 0, Height - Height / 4, Width, Height / 4);
                        }
                        break;
                    case Variant.Inset:
                        using (var fill = new SolidBrush(Color.FromArgb(110, 255, 255, 255)))
                            g.FillPath(fill, path);
                        //顶部内阴影（下沉）
                        using (var sh = new LinearGradientBrush(new Rectangle(0, 0, Width, 9),
                            Color.FromArgb(40, 0, 0, 0), Color.FromArgb(0, 0, 0, 0), 90f))
                        {
                            g.SetClip(path, CombineMode.Intersect);
                            g.FillRectangle(sh, 0, 0, Width, 9);
                        }
                        break;
                    default:
                        using (var fill = new SolidBrush(Color.FromArgb(150, 255, 255, 255)))
                            g.FillPath(fill, path);
                        break;
                }

                //3.5) 可点击交互层：Primary主色填充 / 悬停与按压附加白蒙层
                if (primary)
                {
                    using (var b = new SolidBrush(Color.FromArgb(!Enabled ? 120 : pressed ? 245 : hover ? 235 : 215, GlassTheme.Accent)))
                        g.FillPath(b, path);
                }
                else if (clickable && Enabled)
                {
                    int a = pressed ? 56 : hover ? 28 : 0;
                    if (a > 0)
                    {
                        using var b = new SolidBrush(Color.FromArgb(a, 255, 255, 255));
                        g.FillPath(b, path);
                    }
                }

                //4) 顶部镜面高光带（液态玻璃招牌：光打在材质上沿）
                if (variant != Variant.Inset)
                {
                    int hiAlpha = variant == Variant.Hero ? 80 : 55;
                    using var hiPath = GlassTheme.RoundedRect(new Rectangle(1, 1, Width - 3, Math.Max(1, Height / 3)), Math.Max(1, CornerRadius - 1));
                    using var hi = new LinearGradientBrush(new Rectangle(0, 0, Width, Math.Max(1, Height / 3)),
                        Color.FromArgb(hiAlpha, 255, 255, 255), Color.FromArgb(0, 255, 255, 255), 90f);
                    g.SetClip(path, CombineMode.Intersect);
                    g.FillPath(hi, hiPath);
                }
            }
            finally
            {
                g.Restore(state);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using var path = GlassTheme.RoundedRect(rect, CornerRadius);

            //镜面描边：顶部亮白 → 底部淡白
            int topAlpha = variant == Variant.Hero ? 190 : variant == Variant.Inset ? 110 : 150;
            int bottomAlpha = variant == Variant.Inset ? 60 : 55;
            using var br = new LinearGradientBrush(rect,
                Color.FromArgb(topAlpha, 255, 255, 255), Color.FromArgb(bottomAlpha, 255, 255, 255), 90f);
            using var pen = new Pen(br, 1f);
            g.DrawPath(pen, path);

            //自绘文本行（动态内容不走透明标签控件）
            foreach (var line in overlays)
            {
                if (!string.IsNullOrEmpty(line.Text) && line.Font != null)
                    TextRenderer.DrawText(g, line.Text, line.Font, line.Rect, line.Color, line.Flags);
            }
        }

        /// <summary>
        /// 把本面板的背景完整绘制到子控件的Graphics上（供透明子控件取样背景；
        /// childRect为子控件在本面板坐标系的矩形）
        /// </summary>
        public void PaintBackgroundForChild(Graphics g, Rectangle childRect)
        {
            var state = g.Save();
            g.TranslateTransform(-childRect.X, -childRect.Y);
            using var pe = new PaintEventArgs(g, childRect);
            OnPaintBackground(pe);
            g.Restore(state);
        }

        /// <summary>
        /// 计算本控件在材质画布上的取样区域（窗体客户区坐标 × 映射系数 + Cover偏移）
        /// </summary>
        private Rectangle MaterialRect()
        {
            var form = FindForm();
            if (form == null)
                return new Rectangle(0, 0, Width, Height);
            var mine = PointToScreen(Point.Empty);
            var host = form.PointToScreen(Point.Empty);
            return new Rectangle(
                (int)((mine.X - host.X) * MapScale + MapOffsetX),
                (int)((mine.Y - host.Y) * MapScale + MapOffsetY),
                (int)(Width * MapScale),
                (int)(Height * MapScale));
        }
    }
}

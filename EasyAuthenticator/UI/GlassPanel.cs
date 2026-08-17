using System.Drawing.Drawing2D;

namespace EasyAuthenticator.UI
{
    /// <summary>
    /// 液体玻璃面板：按控件在窗体上的位置取样背景画布，
    /// 叠加白色微透蒙层、顶部高光与圆角描边，呈现磨砂玻璃质感
    /// </summary>
    public class GlassPanel : Panel
    {
        /// <summary>共享背景画布（由窗体启动时生成赋值）</summary>
        public static Bitmap? Artwork;

        /// <summary>圆角半径</summary>
        public int CornerRadius { get; set; } = 18;
        /// <summary>白色蒙层透明度（0-255）</summary>
        public int FillAlpha { get; set; } = 22;
        /// <summary>描边透明度（0-255）</summary>
        public int BorderAlpha { get; set; } = 46;

        public GlassPanel()
        {
            DoubleBuffered = true;
            BackColor = Color.Transparent;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using var path = GlassTheme.RoundedRect(rect, CornerRadius);

            //1) 基色兜底
            using (var b = new SolidBrush(GlassTheme.BgBase))
                g.FillPath(b, path);

            //2) 取样背景画布对应区域（“透过玻璃看背景”）
            if (Artwork != null)
            {
                var src = ArtworkRect();
                src.Intersect(new Rectangle(0, 0, Artwork.Width, Artwork.Height));
                if (src.Width > 0 && src.Height > 0)
                {
                    g.SetClip(path);
                    g.DrawImage(Artwork, new Rectangle(0, 0, src.Width, src.Height), src, GraphicsUnit.Pixel);
                    g.ResetClip();
                }
            }

            //3) 玻璃白色蒙层
            using (var fill = new SolidBrush(Color.FromArgb(FillAlpha, 255, 255, 255)))
                g.FillPath(fill, path);

            //4) 顶部高光（液体玻璃边缘反光）
            using (var hi = new LinearGradientBrush(new Rectangle(0, 0, Width, Math.Max(1, Height / 3)),
                Color.FromArgb(30, 255, 255, 255), Color.FromArgb(0, 255, 255, 255), 90f))
            {
                g.SetClip(path);
                g.FillRectangle(hi, 0, 0, Width, Math.Max(1, Height / 3));
                g.ResetClip();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using var path = GlassTheme.RoundedRect(rect, CornerRadius);
            using var pen = new Pen(Color.FromArgb(BorderAlpha, 255, 255, 255), 1f);
            g.DrawPath(pen, path);
        }

        /// <summary>
        /// 计算本控件在背景画布上的取样区域（按窗体客户区坐标偏移）
        /// </summary>
        private Rectangle ArtworkRect()
        {
            var form = FindForm();
            if (form == null)
                return new Rectangle(0, 0, Width, Height);
            var mine = PointToScreen(Point.Empty);
            var host = form.PointToScreen(Point.Empty);
            return new Rectangle(mine.X - host.X, mine.Y - host.Y, Width, Height);
        }
    }
}

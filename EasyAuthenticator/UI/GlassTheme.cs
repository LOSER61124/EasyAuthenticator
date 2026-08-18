using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace EasyAuthenticator.UI
{
    /// <summary>
    /// 液态玻璃主题（macOS Tahoe 风格）：浅色磨砂玻璃浮于彩色底图之上。
    /// 底图来自嵌入资源 EasyAuthenticator.bg.jpg，面板取样其模糊版，
    /// 叠白色半透明蒙层 + 顶部镜面高光 + 柔和投影，内容用浅色模式语义色（深色文字）。
    /// </summary>
    public static class GlassTheme
    {
        //—— 玻璃上文字（iOS label 族，Light）——
        /// <summary>一级文字 label（85%黑）</summary>
        public static readonly Color TextPrimary = Color.FromArgb(217, 0, 0, 0);
        /// <summary>二级文字 secondaryLabel（60%）</summary>
        public static readonly Color TextSecondary = Color.FromArgb(153, 60, 60, 67);
        /// <summary>三级文字 tertiaryLabel（33%）</summary>
        public static readonly Color TextTertiary = Color.FromArgb(84, 60, 60, 67);
        /// <summary>四级文字 quaternaryLabel（18%）</summary>
        public static readonly Color TextQuaternary = Color.FromArgb(46, 60, 60, 67);

        //—— 玻璃填充（白色族，磨砂蒙层用）——
        /// <summary>白色填充 43%</summary>
        public static readonly Color FillPrimary = Color.FromArgb(110, 255, 255, 255);
        /// <summary>白色填充 35%</summary>
        public static readonly Color FillSecondary = Color.FromArgb(90, 255, 255, 255);
        /// <summary>白色填充 27%</summary>
        public static readonly Color FillTertiary = Color.FromArgb(70, 255, 255, 255);
        /// <summary>白色填充 20%（进度条轨道等）</summary>
        public static readonly Color FillQuaternary = Color.FromArgb(50, 255, 255, 255);

        //—— 分隔线（浅色发丝线）——
        public static readonly Color Separator = Color.FromArgb(120, 255, 255, 255);

        //—— 强调色（Apple Light，玻璃上更鲜亮）——
        /// <summary>主色 systemBlue</summary>
        public static readonly Color Accent = Color.FromArgb(0, 122, 255);
        /// <summary>主色悬停加深</summary>
        public static readonly Color AccentHover = Color.FromArgb(0, 102, 214);
        /// <summary>危险色 systemRed</summary>
        public static readonly Color Danger = Color.FromArgb(255, 59, 48);
        /// <summary>成功色 systemGreen</summary>
        public static readonly Color Success = Color.FromArgb(52, 199, 89);

        //—— 基底 ——
        /// <summary>背景基色（底图加载失败时兜底）</summary>
        public static readonly Color BgBase = Color.FromArgb(210, 214, 224);

        //—— 不透明等效色（用于不支持透明色的属性）——
        /// <summary>表格背景（磨砂白）</summary>
        public static readonly Color GridBg = Color.FromArgb(244, 244, 248);
        /// <summary>表头背景</summary>
        public static readonly Color GridHeaderBg = Color.FromArgb(236, 236, 242);
        /// <summary>表格行分隔线（不透明，DataGridView.GridColor不接受透明色）</summary>
        public static readonly Color GridLine = Color.FromArgb(216, 216, 224);
        /// <summary>输入框内嵌底（柔和蓝灰白，避免纯白与磨砂面板生硬对比）</summary>
        public static readonly Color FieldFill = Color.FromArgb(233, 236, 241);

        private static readonly string[] FontCandidates = { "Segoe UI Variable Text", "Segoe UI", "Microsoft YaHei UI" };

        /// <summary>
        /// 主题字体（优先 Segoe UI Variable，自动回退 Segoe UI / 微软雅黑）
        /// </summary>
        public static Font Font(float size, FontStyle style = FontStyle.Regular)
        {
            foreach (var name in FontCandidates)
            {
                try
                {
                    using var ff = new FontFamily(name); //字体不存在会抛异常
                    return new Font(ff, size, style, GraphicsUnit.Point);
                }
                catch { }
            }
            return new Font(FontFamily.GenericSansSerif, size, style, GraphicsUnit.Point);
        }

        //—— 语义化字阶 ——
        /// <summary>大数字展示（Display）</summary>
        public static Font FontDisplay(float size = 38f) => Font(size);
        /// <summary>面板标题（Headline，加粗）</summary>
        public static Font FontHeadline(float size = 11.5f) => Font(size, FontStyle.Bold);
        /// <summary>正文（Body）</summary>
        public static Font FontBody(float size = 9.5f) => Font(size);
        /// <summary>说明文字（Caption）</summary>
        public static Font FontCaption(float size = 9f) => Font(size);

        /// <summary>
        /// 圆角矩形路径
        /// </summary>
        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }
            int d = radius * 2;
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 强缓出曲线（UI反馈专用，禁止ease-in）
        /// </summary>
        public static double EaseOut(double t)
        {
            t = Math.Clamp(t, 0, 1);
            double u = 1 - t;
            return 1 - u * u * u * u;
        }

        /// <summary>
        /// 从嵌入资源加载底图原图（不裁剪，Cover裁剪由窗体绘制时按实际客户区计算，任意DPI/尺寸都正确）
        /// </summary>
        public static Bitmap? LoadRawArtwork()
        {
            try
            {
                using var stream = typeof(GlassTheme).Assembly.GetManifestResourceStream("EasyAuthenticator.bg.jpg");
                if (stream != null)
                    return Image.FromStream(stream) as Bitmap;
            }
            catch { }
            return null;
        }

        /// <summary>
        /// 生成玻璃材质画布：半分辨率 + 盒式模糊 + 轻度提饱和
        /// （磨砂玻璃的本体；半分辨率下模糊等效半径翻倍且性能更好）
        /// </summary>
        public static Bitmap CreateMaterial(Bitmap artwork, int radius, float saturation, out float materialScale)
        {
            int w = Math.Max(1, artwork.Width / 2);
            int h = Math.Max(1, artwork.Height / 2);
            var small = new Bitmap(w, h);
            using (var g = Graphics.FromImage(small))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(artwork, 0, 0, w, h);
            }
            var blurred = BoxBlur(small, radius, 1);
            BoostSaturation(blurred, saturation);
            materialScale = (float)w / artwork.Width;
            return blurred;
        }

        /// <summary>
        /// 柔和投影（液态玻璃面板悬浮感）：多层外扩圆角矩形，越近越浓，向下偏移
        /// </summary>
        public static void DrawSoftShadow(Graphics g, Rectangle bounds, int radius)
        {
            var old = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            for (int i = 10; i >= 1; i--)
            {
                var r = bounds;
                r.Inflate(i, i);
                r.Offset(0, 3);
                int a = (int)(16 * (1 - (i - 1) / 10.0)); // 最近一层最浓(≈16)，向外渐淡
                using var path = RoundedRect(r, radius + i);
                using var b = new SolidBrush(Color.FromArgb(a, 0, 0, 0));
                g.FillPath(b, path);
            }
            g.SmoothingMode = old;
        }

        /// <summary>
        /// 盒式模糊（安全代码，单遍横向+纵向）
        /// </summary>
        public static Bitmap BoxBlur(Bitmap src, int radius, int iterations)
        {
            var bmp = new Bitmap(src);
            for (int it = 0; it < iterations; it++)
            {
                var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                var data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                int stride = data.Stride;
                int w = bmp.Width, h = bmp.Height;
                byte[] px = new byte[stride * h];
                Marshal.Copy(data.Scan0, px, 0, px.Length);
                byte[] tmp = (byte[])px.Clone();

                for (int y = 0; y < h; y++)
                {
                    int row = y * stride;
                    for (int x = 0; x < w; x++)
                    {
                        int r = 0, g = 0, b = 0, a = 0, count = 0;
                        for (int k = -radius; k <= radius; k++)
                        {
                            int xx = Math.Min(w - 1, Math.Max(0, x + k));
                            int i = row + xx * 4;
                            b += tmp[i]; g += tmp[i + 1]; r += tmp[i + 2]; a += tmp[i + 3]; count++;
                        }
                        int o = row + x * 4;
                        px[o] = (byte)(b / count); px[o + 1] = (byte)(g / count);
                        px[o + 2] = (byte)(r / count); px[o + 3] = (byte)(a / count);
                    }
                }
                tmp = (byte[])px.Clone();
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        long r = 0, g = 0, b = 0, a = 0; int count = 0;
                        for (int k = -radius; k <= radius; k++)
                        {
                            int yy = Math.Min(h - 1, Math.Max(0, y + k));
                            int i = yy * stride + x * 4;
                            b += tmp[i]; g += tmp[i + 1]; r += tmp[i + 2]; a += tmp[i + 3]; count++;
                        }
                        int o = y * stride + x * 4;
                        px[o] = (byte)(b / count); px[o + 1] = (byte)(g / count);
                        px[o + 2] = (byte)(r / count); px[o + 3] = (byte)(a / count);
                    }
                }
                Marshal.Copy(px, 0, data.Scan0, px.Length);
                bmp.UnlockBits(data);
            }
            return bmp;
        }

        /// <summary>
        /// 按亮度保持提升饱和度（vibrancy：模糊会洗掉颜色，必须补回饱和）
        /// </summary>
        private static void BoostSaturation(Bitmap bmp, float sat)
        {
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var data = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int stride = data.Stride;
            byte[] px = new byte[stride * bmp.Height];
            Marshal.Copy(data.Scan0, px, 0, px.Length);
            for (int i = 0; i < px.Length; i += 4)
            {
                float b = px[i], g = px[i + 1], r = px[i + 2];
                float luma = 0.0722f * b + 0.7152f * g + 0.2126f * r;
                px[i] = (byte)Math.Clamp(luma + (b - luma) * sat, 0, 255);
                px[i + 1] = (byte)Math.Clamp(luma + (g - luma) * sat, 0, 255);
                px[i + 2] = (byte)Math.Clamp(luma + (r - luma) * sat, 0, 255);
            }
            Marshal.Copy(px, 0, data.Scan0, px.Length);
            bmp.UnlockBits(data);
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int value, int size);

        /// <summary>
        /// 启用DWM深色标题栏（Windows 10 1809+，失败静默忽略）
        /// </summary>
        public static void EnableDarkTitleBar(IntPtr hwnd)
        {
            try
            {
                int dark = 1;
                DwmSetWindowAttribute(hwnd, 20, ref dark, 4); // DWMWA_USE_IMMERSIVE_DARK_MODE
            }
            catch { }
        }
    }
}

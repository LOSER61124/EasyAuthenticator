using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace EasyAuthenticator.UI
{
    /// <summary>
    /// 苹果暗色系液体玻璃主题：颜色、字体、背景画布、DWM深色标题栏
    /// </summary>
    public static class GlassTheme
    {
        //—— Apple Dark 配色规范 ——
        /// <summary>背景基色（近黑）</summary>
        public static readonly Color BgBase = Color.FromArgb(10, 10, 14);
        /// <summary>一级文字</summary>
        public static readonly Color TextPrimary = Color.FromArgb(245, 245, 247);
        /// <summary>二级文字（60%）</summary>
        public static readonly Color TextSecondary = Color.FromArgb(153, 235, 235, 245);
        /// <summary>三级文字（30%）</summary>
        public static readonly Color TextTertiary = Color.FromArgb(77, 235, 235, 245);
        /// <summary>主色 systemBlue (Dark)</summary>
        public static readonly Color Accent = Color.FromArgb(10, 132, 255);
        /// <summary>危险色 systemRed (Dark)</summary>
        public static readonly Color Danger = Color.FromArgb(255, 69, 58);
        /// <summary>输入框内嵌填充</summary>
        public static readonly Color FieldFill = Color.FromArgb(24, 24, 30);
        /// <summary>表格行分隔线（不透明，DataGridView.GridColor不接受透明色；等效7%白叠于深色底）</summary>
        public static readonly Color GridLine = Color.FromArgb(34, 34, 41);
        /// <summary>选中行</summary>
        public static readonly Color Selection = Color.FromArgb(64, 10, 132, 255);

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
        /// 生成暗色流体渐变背景画布（近黑基底 + 四团柔和光斑，类macOS深色壁纸）
        /// 画布本身没有硬边缘细节，玻璃面板取样后即呈现磨砂质感
        /// </summary>
        public static Bitmap CreateArtwork(Size size)
        {
            int w = Math.Max(1, size.Width);
            int h = Math.Max(1, size.Height);
            var bmp = new Bitmap(w, h);
            using var g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //基底：近黑微渐变
            using (var br = new LinearGradientBrush(new Rectangle(0, 0, w, h),
                Color.FromArgb(14, 14, 19), Color.FromArgb(6, 6, 10), 35f))
                g.FillRectangle(br, 0, 0, w, h);

            //流体光斑（中心实、边缘渐隐）
            DrawBlob(g, size, 0.15f, 0.05f, 0.55f, Color.FromArgb(150, 38, 44, 110)); // 靛蓝·左上
            DrawBlob(g, size, 0.85f, 0.25f, 0.50f, Color.FromArgb(120, 10, 90, 170));  // 蓝·右
            DrawBlob(g, size, 0.55f, 1.00f, 0.60f, Color.FromArgb(130, 88, 45, 130));  // 紫·下
            DrawBlob(g, size, 0.05f, 0.90f, 0.45f, Color.FromArgb(90, 12, 80, 110));   // 青·左下
            return bmp;
        }

        private static void DrawBlob(Graphics g, Size size, float cx, float cy, float r, Color color)
        {
            float radius = Math.Max(size.Width, size.Height) * r;
            var center = new PointF(size.Width * cx, size.Height * cy);
            using var path = new GraphicsPath();
            path.AddEllipse(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            using var pgb = new PathGradientBrush(path);
            pgb.CenterPoint = center;
            pgb.CenterColor = color;
            pgb.SurroundColors = new[] { Color.FromArgb(0, color.R, color.G, color.B) };
            g.FillEllipse(pgb, center.X - radius, center.Y - radius, radius * 2, radius * 2);
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

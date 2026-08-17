using System.Drawing.Drawing2D;

namespace EasyAuthenticator.UI
{
    /// <summary>
    /// 细线进度条：指示当前校验码剩余有效时间比例，低于25%变红
    /// </summary>
    public class GlassProgress : Control
    {
        private double fraction = 1;

        public GlassProgress()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
            Size = new Size(200, 4);
        }

        /// <summary>设置进度比例（0-1）</summary>
        public void SetFraction(double value)
        {
            fraction = Math.Clamp(value, 0, 1);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int radius = Height / 2;

            //轨道
            var track = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var p = GlassTheme.RoundedRect(track, radius))
            using (var b = new SolidBrush(Color.FromArgb(28, 255, 255, 255)))
                g.FillPath(b, p);

            //填充
            int w = (int)Math.Round((Width - 1) * fraction);
            if (w >= Height)
            {
                using var p = GlassTheme.RoundedRect(new Rectangle(0, 0, w, Height - 1), radius);
                using var b = new SolidBrush(fraction > 0.25 ? GlassTheme.Accent : GlassTheme.Danger);
                g.FillPath(b, p);
            }
        }
    }
}

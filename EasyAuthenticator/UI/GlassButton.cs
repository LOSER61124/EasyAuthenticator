using System.Drawing.Drawing2D;

namespace EasyAuthenticator.UI
{
    /// <summary>
    /// 液体玻璃按钮：圆角微透，含悬停/按下/禁用态；Primary=true 时为主色调填充按钮
    /// </summary>
    public class GlassButton : Button
    {
        /// <summary>是否主色调按钮（Accent填充白字）</summary>
        public bool Primary { get; set; }
        /// <summary>圆角半径</summary>
        public int CornerRadius { get; set; } = 10;

        private bool hover;
        private bool pressed;

        public GlassButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.ResizeRedraw, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Color.Transparent;
            ForeColor = GlassTheme.TextPrimary;
            Font = GlassTheme.Font(9.5f);
            Cursor = Cursors.Hand;
            DoubleBuffered = true;
        }

        protected override void OnMouseEnter(EventArgs e) { hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { hover = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs mevent) { pressed = true; Invalidate(); base.OnMouseDown(mevent); }
        protected override void OnMouseUp(MouseEventArgs mevent) { pressed = false; Invalidate(); base.OnMouseUp(mevent); }
        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using var path = GlassTheme.RoundedRect(rect, CornerRadius);

            Color fill;
            Color border;
            if (Primary)
            {
                int alpha = !Enabled ? 90 : pressed ? 255 : hover ? 225 : 200;
                fill = Color.FromArgb(alpha, GlassTheme.Accent);
                border = Color.FromArgb(120, GlassTheme.Accent);
            }
            else
            {
                int alpha = !Enabled ? 10 : pressed ? 46 : hover ? 32 : 18;
                fill = Color.FromArgb(alpha, 255, 255, 255);
                border = Color.FromArgb(46, 255, 255, 255);
            }

            using (var b = new SolidBrush(fill))
                g.FillPath(b, path);
            using (var p = new Pen(border, 1f))
                g.DrawPath(p, path);

            var textColor = Enabled ? (Primary ? Color.White : GlassTheme.TextPrimary) : GlassTheme.TextTertiary;
            TextRenderer.DrawText(g, Text, Font, rect, textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix);
        }
    }
}

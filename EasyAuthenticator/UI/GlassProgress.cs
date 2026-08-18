using System.Drawing.Drawing2D;

namespace EasyAuthenticator.UI
{
    /// <summary>
    /// 细线进度条：指示当前校验码剩余有效时间比例。
    /// 目标值每秒跳变，显示值以30ms插值平滑跟随（视觉上是连续流逝），低于25%变红
    /// </summary>
    public class GlassProgress : Control
    {
        private double target = 1;
        private double current = 1;
        private readonly System.Windows.Forms.Timer smoothTimer;

        public GlassProgress()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw, true);
            //不用透明BackColor（避免WS_EX_TRANSPARENT合成错位），不透明占位，背景显式取样父级
            BackColor = Color.White;
            Size = new Size(200, 4);

            smoothTimer = new System.Windows.Forms.Timer { Interval = 30 };
            smoothTimer.Tick += (s, e) =>
            {
                current += (target - current) * 0.25;
                if (Math.Abs(target - current) < 0.003)
                {
                    current = target;
                    smoothTimer.Stop();
                }
                Invalidate();
            };
        }

        /// <summary>
        /// 背景显式取样父级（不透明控件，需自己画父级背景才能"透"出磨砂）
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent is GlassPanel glassParent)
                glassParent.PaintBackgroundForChild(e.Graphics, Bounds);
            else if (Parent is Form1 form1)
                form1.PaintArtworkForChild(e.Graphics, Bounds);
            else if (Parent != null)
                InvokePaintBackground(Parent, e);
        }

        /// <summary>设置进度比例目标值（0-1），显示值平滑跟随</summary>
        public void SetFraction(double value)
        {
            target = Math.Clamp(value, 0, 1);
            smoothTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int radius = Height / 2;

            //轨道（白色磨砂）
            var track = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var p = GlassTheme.RoundedRect(track, radius))
            using (var b = new SolidBrush(Color.FromArgb(90, 255, 255, 255)))
                g.FillPath(b, p);

            //填充
            int w = (int)Math.Round((Width - 1) * current);
            if (w >= Height)
            {
                using var p = GlassTheme.RoundedRect(new Rectangle(0, 0, w, Height - 1), radius);
                using var b = new SolidBrush(current > 0.25 ? GlassTheme.Accent : GlassTheme.Danger);
                g.FillPath(b, p);
            }
        }
    }
}

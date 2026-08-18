namespace EasyAuthenticator.UI
{
    /// <summary>
    /// 玻璃面板上的透明标签：UserPaint自绘文字 + 显式父级背景。
    /// 绕开WinForms透明标签在自定义绘制父级上的合成路径（会产生文字虚影）
    /// </summary>
    public class GlassLabel : Label
    {
        public GlassLabel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint
                | ControlStyles.ResizeRedraw, true);
            //关键：不用透明BackColor！透明会让WinForms给标签HWND加WS_EX_TRANSPARENT，
            //周期性刷新时文本会被父级合成路径画到兄弟控件上（文字虚影）。
            //改为不透明HWND + 自绘父级背景（PaintBackgroundForChild），表面与父级磨砂无缝一致
            BackColor = Color.White; //会被父级背景完全覆盖，仅为不透明占位
        }

        /// <summary>
        /// 由标签自己画一遍父级背景（WinForms父级有WS_CLIPCHILDREN不会替子控件画背景；
        /// 注意不能留空——留空会露黑底；也不能走默认透明路径——会重复绘制）。
        /// 显式分发——InvokePaintBackground内部路径不保证走到我们的重写
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

        protected override void OnPaint(PaintEventArgs e)
        {
            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor, FlagsFromAlignment());
        }

        private TextFormatFlags FlagsFromAlignment()
        {
            var flags = TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine;
            switch (TextAlign)
            {
                case ContentAlignment.MiddleLeft:
                    flags |= TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
                    break;
                case ContentAlignment.MiddleCenter:
                    flags |= TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                    break;
                case ContentAlignment.MiddleRight:
                    flags |= TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
                    break;
                default:
                    flags |= TextFormatFlags.Left | TextFormatFlags.Top;
                    break;
            }
            return flags;
        }
    }
}

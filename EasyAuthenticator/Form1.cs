using EasyAuthenticator.Ext;
using EasyAuthenticator.Model;
using EasyAuthenticator.UI;
using System.Text;
using WinformLib;

namespace EasyAuthenticator
{
    public partial class Form1 : Form
    {
        private string key = "";
        private Bitmap? artworkRaw;

        public Form1()
        {
            InitializeComponent();
            //加载底图原图与模糊材质（Cover映射由UpdateMaterialMapping按实际客户区/DPI计算）
            artworkRaw = GlassTheme.LoadRawArtwork();
            GlassPanel.RawImage = artworkRaw;
            if (artworkRaw != null)
                GlassPanel.Material = GlassTheme.CreateMaterial(artworkRaw, 18, 1.15f, out _);
        }

        /// <summary>
        /// 启用DWM深色标题栏
        /// </summary>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            GlassTheme.EnableDarkTitleBar(Handle);
            UpdateMaterialMapping();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateMaterialMapping();
        }

        /// <summary>
        /// 计算底图原图→当前客户区的Cover映射，并换算到材质坐标系（供玻璃面板取样）
        /// </summary>
        private void UpdateMaterialMapping()
        {
            if (GlassPanel.Material == null || GlassPanel.RawImage == null || ClientSize.Width <= 0 || ClientSize.Height <= 0)
                return;
            var raw = GlassPanel.RawImage;
            float clientAspect = (float)ClientSize.Width / ClientSize.Height;
            float srcAspect = (float)raw.Width / raw.Height;
            float scale, offX = 0, offY = 0; // 原图像素/客户区像素
            if (srcAspect > clientAspect)
            {
                scale = (float)raw.Height / ClientSize.Height;
                offX = (raw.Width - ClientSize.Width * scale) / 2;
            }
            else
            {
                scale = (float)raw.Width / ClientSize.Width;
                offY = (raw.Height - ClientSize.Height * scale) / 2;
            }
            float ms = (float)GlassPanel.Material.Width / raw.Width; // 材质/原图（半分辨率≈0.5）
            GlassPanel.MapScale = ms * scale;
            GlassPanel.MapOffsetX = offX * ms;
            GlassPanel.MapOffsetY = offY * ms;
        }

        /// <summary>
        /// 底图按Cover绘制（任意窗口尺寸/DPI下比例都正确）
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (artworkRaw == null)
            {
                base.OnPaintBackground(e);
                return;
            }
            e.Graphics.Clear(GlassTheme.BgBase);
            DrawArtworkCover(e.Graphics);
        }

        /// <summary>
        /// 把底图绘制到子控件的Graphics上（供玻璃面板圆角外的角落取样；childRect为子控件在窗体坐标系的矩形）
        /// </summary>
        public void PaintArtworkForChild(Graphics g, Rectangle childRect)
        {
            if (artworkRaw == null)
                return;
            var state = g.Save();
            g.TranslateTransform(-childRect.X, -childRect.Y);
            DrawArtworkCover(g);
            g.Restore(state);
        }

        /// <summary>
        /// Cover方式绘制底图到当前客户区（调用方需保证g的原点在客户区左上角）
        /// </summary>
        private void DrawArtworkCover(Graphics g)
        {
            float clientAspect = (float)ClientSize.Width / Math.Max(1, ClientSize.Height);
            float srcAspect = (float)artworkRaw!.Width / artworkRaw.Height;
            Rectangle src;
            if (srcAspect > clientAspect)
            {
                int cw = (int)(artworkRaw.Height * clientAspect);
                src = new Rectangle((artworkRaw.Width - cw) / 2, 0, cw, artworkRaw.Height);
            }
            else
            {
                int ch = (int)(artworkRaw.Width / clientAspect);
                src = new Rectangle(0, (artworkRaw.Height - ch) / 2, artworkRaw.Width, ch);
            }
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImage(artworkRaw, new Rectangle(0, 0, ClientSize.Width, ClientSize.Height), src, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 玻璃面板下方的柔和投影（画在面板之下，营造悬浮感）
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            GlassTheme.DrawSoftShadow(e.Graphics, panelHero.Bounds, panelHero.CornerRadius);
            GlassTheme.DrawSoftShadow(e.Graphics, panelPrev.Bounds, panelPrev.CornerRadius);
            GlassTheme.DrawSoftShadow(e.Graphics, panelNext.Bounds, panelNext.CornerRadius);
            GlassTheme.DrawSoftShadow(e.Graphics, panelManage.Bounds, panelManage.CornerRadius);
        }

        private GlassPanel.TextOverlay? heroCodeOverlay;
        private GlassPanel.TextOverlay? heroCountdownOverlay;
        private GlassPanel.TextOverlay? prevCodeOverlay;
        private GlassPanel.TextOverlay? nextCodeOverlay;
        private GlassPanel.TextOverlay? chipResetOverlay;

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            key = FormExtentions.GetMachineGuid();//机器码
            this.SetCommon(new FormSettings
            {
                isExitAsk = false
            });
            //动态文本由面板自绘（透明标签周期性刷新会被父级合成路径画到兄弟控件上，产生文字虚影）
            heroCodeOverlay = new GlassPanel.TextOverlay(new Rectangle(18, 38, 406, 96), GlassTheme.FontDisplay(38f), GlassTheme.TextPrimary) { Text = "0 0 0 0 0 0" };
            heroCountdownOverlay = new GlassPanel.TextOverlay(new Rectangle(20, 144, 300, 20), GlassTheme.FontCaption(), GlassTheme.TextSecondary) { Text = "距离当前校验码过期还差00秒" };
            prevCodeOverlay = new GlassPanel.TextOverlay(new Rectangle(14, 40, 188, 40), GlassTheme.FontDisplay(18f), GlassTheme.TextSecondary) { Text = "0 0 0 0 0 0" };
            nextCodeOverlay = new GlassPanel.TextOverlay(new Rectangle(14, 40, 190, 40), GlassTheme.FontDisplay(18f), GlassTheme.TextSecondary) { Text = "0 0 0 0 0 0" };
            panelHero.Overlays.Add(heroCodeOverlay);
            panelHero.Overlays.Add(heroCountdownOverlay);
            panelPrev.Overlays.Add(prevCodeOverlay);
            panelNext.Overlays.Add(nextCodeOverlay);
            //可点击面板的文字
            const TextFormatFlags centerFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix;
            button1.Overlays.Add(new GlassPanel.TextOverlay(new Rectangle(0, 0, 172, 32), GlassTheme.FontBody(), GlassTheme.TextPrimary) { Text = "查看", Flags = centerFlags });
            chipResetOverlay = new GlassPanel.TextOverlay(new Rectangle(0, 0, 172, 32), GlassTheme.FontBody(), Color.White) { Text = "重设", Flags = centerFlags };
            button2.Overlays.Add(chipResetOverlay);
            linkExport.Overlays.Add(new GlassPanel.TextOverlay(new Rectangle(0, 0, 124, 36), GlassTheme.FontBody(), GlassTheme.TextPrimary) { Text = "导出校验码", Flags = centerFlags });
            linkManage.Overlays.Add(new GlassPanel.TextOverlay(new Rectangle(0, 0, 124, 36), GlassTheme.FontBody(), GlassTheme.TextPrimary) { Text = "密钥管理", Flags = centerFlags });
            label8_Click(sender, e);
            var isNokey = !LocalDb.Fsql.Select<PasswordInfo>().Any(x=>x.IsDelete ==0);
            if (isNokey)
            {
                chipResetOverlay!.Text = "初始设定"; button2.Invalidate();
                button1.Enabled = false;
            }
            else
            {
                //存在密码
                chipResetOverlay!.Text = "重新设定"; button2.Invalidate();
                button1.Enabled = true;

                //密码显示上去
                ShowPWDNow();

            }
            //删除按钮图标胶囊自绘
            dataGridView1.CellPainting += DataGridView1_CellPainting;
            //TOTP刷新计时器（必须用UI线程计时器，线程池计时器在非UI线程写控件会产生虚影）
            var totpTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            totpTimer.Tick += (s, e2) => StartTimes();
            totpTimer.Start();
        }

        private void ShowPWDNow()
        {
            string? pwd_aes = GetCurrentPwd();
            if (string.IsNullOrEmpty(pwd_aes))
            {
                textBox1.Text = "";
                return;
            }
            var pwd = EasyAES.AesDecrypt(key, pwd_aes);//明文

            textBox1.Text = pwd.Substring(0, 5) + "*******";
        }

        /// <summary>
        /// 定时器方法
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void StartTimes()
        {
            var res = GetCurrentPwd();
            if (!string.IsNullOrEmpty(res))
            {
                var pwdDetails = TotpHelper.GetTotpWindowCodes(EasyAES.AesDecrypt(key, res));
                heroCodeOverlay!.Text = GetSpaceShow(pwdDetails.CurrentPDW);
                prevCodeOverlay!.Text = GetSpaceShow(pwdDetails.PrePWD.ToString());
                nextCodeOverlay!.Text = GetSpaceShow(pwdDetails.NextPDW.ToString());
                heroCountdownOverlay!.Text = $"距离当前校验码过期还差{pwdDetails.RemainTime}秒";
                glassProgress1.SetFraction(pwdDetails.RemainTime / 30.0);
                //每秒全量重绘（小窗体+面板双缓冲，无闪烁成本），彻底清理缩放/合成残留
                this.Invalidate(true);
            }
        }

        private string GetSpaceShow(string currentPDW)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in currentPDW)
            {
                sb.Append(item + " ");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取当前密码(密文)
        /// </summary>
        /// <returns></returns>
        private string? GetCurrentPwd()
        {
            return LocalDb.Fsql.Select<PasswordInfo>().Where(x => x.IsDelete == 0).OrderByDescending(x => x.Createtime).First()?.Pwd;
        }

        private int small = 478;
        private int big = 890;
        private int width = 422;
        private bool issmall = false;
        private System.Windows.Forms.Timer? sizeTimer;
        private double sizeFrom, sizeTarget, sizeT;

        private void label8_Click(object sender, EventArgs e)
        {
            //窗口可见时做180ms强缓出宽度动画；初始化（Load中调用）直接到位
            SetWindowWidth(issmall ? big : small, !this.Visible);
            issmall = !issmall;
            Query();
        }

        /// <summary>
        /// 窗口宽度动画（180ms ease-out；instant=true时直接设置；尺寸按DeviceDpi缩放）
        /// </summary>
        private void SetWindowWidth(int targetWidth, bool instant)
        {
            int scaledWidth = (int)Math.Round(targetWidth * DeviceDpi / 96.0);
            int scaledHeight = (int)Math.Round(width * DeviceDpi / 96.0);
            if (instant)
            {
                this.Size = new System.Drawing.Size(scaledWidth, scaledHeight);
                return;
            }
            sizeFrom = this.Width;
            sizeTarget = scaledWidth;
            sizeHeight = scaledHeight;
            sizeT = 0;
            sizeTimer ??= new System.Windows.Forms.Timer { Interval = 15 };
            sizeTimer.Tick -= SizeTimer_Tick;
            sizeTimer.Tick += SizeTimer_Tick;
            sizeTimer.Start();
        }

        private int sizeHeight;

        private void SizeTimer_Tick(object? sender, EventArgs e)
        {
            sizeT += 15.0 / 180.0;
            if (sizeT >= 1)
            {
                sizeT = 1;
                sizeTimer!.Stop();
                //动画结束强制全量重绘，清除缩放过程中可能残留的合成残影
                this.Invalidate(true);
            }
            int w = (int)Math.Round(sizeFrom + (sizeTarget - sizeFrom) * GlassTheme.EaseOut(sizeT));
            this.Size = new System.Drawing.Size(w, sizeHeight);
        }

        private void Query()
        {
            var list = LocalDb.Fsql.Select<PasswordInfo>().Where(x => x.IsDelete == 0).OrderByDescending(x => x.Createtime).ToList();
            foreach (var item in list)
            {
                item.Pwd = EasyAES.AesDecrypt(key, item.Pwd).Substring(0, 5) + "*******";
            }
            if (list.Count != 0)
            {
                dataGridView1.SetCommonWithCell(new DataGridViewExtentions.DataDisplayEntityCell<PasswordInfo>
                {
                    DataList = list,
                    ButtonList = new List<(string ButtonName, string TitileName, int Width)>
                {
                    ("删除","操作",76),
                },
                    HeadtextList = new List<(System.Linq.Expressions.Expression<Func<PasswordInfo, object>> fields, string name, int width)>
                {
                    (x=>x.Pwd,"密钥",130),
                    (x=>x.Createtime,"创建时间",118),
                }
                });
                //BeginInvoke延迟到全部绑定事件处理后执行，确保样式压过WinformLib的最终调整
                BeginInvoke(new Action(StyleGrid));
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
            }
        }

        /// <summary>
        /// 数据表格暗色化（WinformLib绑定时会覆盖部分样式，每次刷新后重设压过它）
        /// </summary>
        private void StyleGrid()
        {
            dataGridView1.BackgroundColor = GlassTheme.GridBg;
            dataGridView1.EnableHeadersVisualStyles = false;
            //WinformLib会改表格的位置尺寸和行头设置，每次刷新后强制恢复
            dataGridView1.Location = new Point(2, 2);
            dataGridView1.Size = new Size(354, 152);
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = GlassTheme.GridBg;
            dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = GlassTheme.GridBg;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlassTheme.GridHeaderBg;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = GlassTheme.TextSecondary;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = GlassTheme.GridHeaderBg;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = GlassTheme.TextSecondary;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.BackColor = GlassTheme.GridHeaderBg;
                col.HeaderCell.Style.ForeColor = GlassTheme.TextSecondary;
                col.HeaderCell.Style.SelectionBackColor = GlassTheme.GridHeaderBg;
                col.HeaderCell.Style.SelectionForeColor = GlassTheme.TextSecondary;
                if (col is DataGridViewButtonColumn bc)
                {
                    bc.FlatStyle = FlatStyle.Flat;
                    bc.DefaultCellStyle.BackColor = GlassTheme.GridBg;
                    bc.DefaultCellStyle.ForeColor = GlassTheme.TextPrimary;
                    bc.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, GlassTheme.Accent);
                    bc.DefaultCellStyle.SelectionForeColor = GlassTheme.TextPrimary;
                }
            }
        }

        private Font? iconFont;
        private Font? gridTextFont;
        /// <summary>图标字体（Segoe Fluent Icons → Segoe MDL2 Assets 回退）</summary>
        private Font IconFont => iconFont ??= CreateIconFont();
        private Font GridTextFont => gridTextFont ??= GlassTheme.FontBody(9f);

        private static Font CreateIconFont()
        {
            foreach (var name in new[] { "Segoe Fluent Icons", "Segoe MDL2 Assets" })
            {
                try
                {
                    using var ff = new FontFamily(name); //不存在会抛异常
                    return new Font(ff, 10f, FontStyle.Regular, GraphicsUnit.Point);
                }
                catch { }
            }
            return GlassTheme.FontBody(10f);
        }

        /// <summary>
        /// 删除按钮列自绘：危险色胶囊 + 垃圾桶图标 + 文字（不改单元格值，避免影响按钮查找逻辑）
        /// </summary>
        private void DataGridView1_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            bool selected = (e.State & DataGridViewElementStates.Selected) != 0;
            var g = e.Graphics!;

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                e.Paint(e.ClipBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                var chip = new Rectangle(e.CellBounds.X + 8, e.CellBounds.Y + 5, e.CellBounds.Width - 16, e.CellBounds.Height - 11);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GlassTheme.RoundedRect(chip, 7))
                using (var b = new SolidBrush(selected ? Color.FromArgb(120, GlassTheme.Danger) : Color.FromArgb(72, GlassTheme.Danger)))
                    g.FillPath(b, path);

                const string glyph = "\uE74D"; //垃圾桶图标
                const string text = "删除";
                var gSize = TextRenderer.MeasureText(g, glyph, IconFont, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPrefix);
                var tSize = TextRenderer.MeasureText(g, text, GridTextFont, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPrefix);
                int total = gSize.Width + 4 + tSize.Width;
                int x = chip.X + (chip.Width - total) / 2;
                int cy = chip.Y + (chip.Height - Math.Max(gSize.Height, tSize.Height)) / 2;
                TextRenderer.DrawText(g, glyph, IconFont, new Point(x, cy), Color.White, TextFormatFlags.NoPrefix);
                TextRenderer.DrawText(g, text, GridTextFont, new Point(x + gSize.Width + 4, cy + 1), Color.White, TextFormatFlags.NoPrefix);
                e.Handled = true;
                return;
            }

            //文本单元格自绘（浅色玻璃上选中只是淡蓝洗色，文字保持深色，白字会隐形）
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);
            var textRect = e.CellBounds;
            textRect.Inflate(-6, 0);
            TextRenderer.DrawText(g, e.FormattedValue?.ToString() ?? "", GridTextFont, textRect,
                GlassTheme.TextPrimary,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
            e.Handled = true;
        }

        /// <summary>
        /// 设定密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //默认生成
            var defaultpwd = TotpHelper.CreateRandomBase32Secret();

            //确定要重新设定密码吗
            if (this.PopUpDialog("确定要重新设定密码吗？"))
            {
                var result = this.SetCustomizeForms(new CustomizeFormsExtentions.CustomizeFormInput
                {
                    FormTitle = "重设密码",
                    inputs = new List<CustomizeFormsExtentions.CustomizeValueInput>
                    {
                        new CustomizeFormsExtentions.CustomizeValueInput
                        {
                            Label = "请输入新密钥:",
                            DefaultValue = defaultpwd,
                        }
                    },
                    funsForm = (x) =>
                    {
                        foreach (var item in x.Controls)
                        {
                            if (item is Label)
                            {
                                (item as Label).BackColor = Color.Transparent;
                            }
                        }
                    }
                });
                if (result.Count != 0)
                {
                    LocalDb.Fsql.Insert(new PasswordInfo
                    {
                        IsDelete = 0,
                        Createtime = DateTime.Now,
                        Pwd = EasyAES.AesEncrypt(key, result["请输入新密钥:"])
                    }).ExecuteAffrows();
                    //刷新
                    //存在密码
                    chipResetOverlay!.Text = "重新设定"; button2.Invalidate();
                    button1.Enabled = true;


                    Query();
                    ShowPWDNow();
                }
            }
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var res = GetCurrentPwd();
            if(string.IsNullOrEmpty(res))
            {
                this.PopUpTips("当前没有密钥！");
                return;
            }
            var pwd = EasyAES.AesDecrypt(key, res);
            this.PopUpTips($"当前的密钥是【{pwd}】,已导出到剪切板中！");
            pwd.ToClipboard();
            textBox1.Text = pwd;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            var result = heroCodeOverlay!.Text.Replace(" ", "");
            result.ToClipboard();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var res = dataGridView1.GetCommonByButton<PasswordInfo>("删除", e);
            var now_id = LocalDb.Fsql.Select<PasswordInfo>().OrderByDescending(x => x.Createtime).First()?.Id ?? -1;
            if (res != null)
            {
                var entity = LocalDb.Fsql.Select<PasswordInfo>().Where(x => x.Id == res.Id).First();
                var isDeleteFirst = res.Id == now_id;
                var tips = isDeleteFirst ? "您是否要删除【当前密钥】？删除完成后，列表中的最新密钥会自动设为当前密钥。" : "您要删除当前密钥吗?";
                if (this.PopUpDialog(tips))
                {
                    res.Pwd = entity.Pwd;
                    res.IsDelete = 1;
                    LocalDb.Fsql.Update<PasswordInfo>().SetSource(res).ExecuteAffrows();
                }
                //刷新
                Query();
                ShowPWDNow();
            }
        }

        private int historyCount = 0;
        private void label2_Click(object sender, EventArgs e)
        {
            historyCount++;
            if (historyCount >= 10)
            {
                try
                {
                    var list = LocalDb.Fsql.Select<PasswordInfo>().ToList()
                                    .OrderByDescending(x=>x.Createtime)
                                    .Select(x => EasyAES.AesDecrypt(key, x.Pwd))
                                    .ToList();
                    string res = string.Join('\n', list);
                    res.ToClipboard();
                    this.PopUpTips("【隐藏模式】已将历史所有密钥输出到剪切板中！");
                }
                catch (Exception ex)
                {
                    this.PopUpTips($"【隐藏模式】调用失败！{ex}");
                }

            }
        }
    }
}

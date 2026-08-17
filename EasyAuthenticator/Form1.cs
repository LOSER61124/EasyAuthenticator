using Dm.util;
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
        public Form1()
        {
            InitializeComponent();
            //生成液体玻璃背景画布（1600x1000足够覆盖窗口各档尺寸）
            GlassPanel.Artwork = GlassTheme.CreateArtwork(new Size(1600, 1000));
            this.BackgroundImage = GlassPanel.Artwork;
            this.BackgroundImageLayout = ImageLayout.None;
        }

        /// <summary>
        /// 启用DWM深色标题栏
        /// </summary>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            GlassTheme.EnableDarkTitleBar(Handle);
        }

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
            label8_Click(sender, e);
            var isNokey = !LocalDb.Fsql.Select<PasswordInfo>().Any(x=>x.IsDelete ==0);
            if (isNokey)
            {
                button2.Text = "初始设定";
                button1.Enabled = false;
            }
            else
            {
                //存在密码
                button2.Text = "重新设定";
                button1.Enabled = true;

                //密码显示上去
                ShowPWDNow();

            }
            TimerExtentions.RegisterTimer("timer1", 1000, StartTimes, true);
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
                label4.Text = GetSpaceShow(pwdDetails.CurrentPDW);
                label5.Text = GetSpaceShow(pwdDetails.PrePWD.ToString());
                label6.Text = GetSpaceShow(pwdDetails.NextPDW.ToString());
                label3.Text = $"距离当前校验码过期还差{pwdDetails.RemainTime}秒";
                glassProgress1.SetFraction(pwdDetails.RemainTime / 30.0);

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
        private void label8_Click(object sender, EventArgs e)
        {
            if (issmall)
            {
                this.Size = new System.Drawing.Size(big, width);
                issmall = false;
            }
            else
            {
                this.Size = new System.Drawing.Size(small, width);
                issmall = true;
            }
            Query();
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
                    ("删除","操作",80),
                },
                    HeadtextList = new List<(System.Linq.Expressions.Expression<Func<PasswordInfo, object>> fields, string name, int width)>
                {
                    (x=>x.Pwd,"密钥",160),
                    (x=>x.Createtime,"创建时间",130),
                }
                });
                StyleGridButtons();
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
            }
        }

        /// <summary>
        /// 数据表格按钮列暗色化（删除按钮用危险色）
        /// </summary>
        private void StyleGridButtons()
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col is DataGridViewButtonColumn bc)
                {
                    bc.FlatStyle = FlatStyle.Flat;
                    bc.DefaultCellStyle.BackColor = Color.FromArgb(40, GlassTheme.Danger);
                    bc.DefaultCellStyle.ForeColor = Color.White;
                    bc.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, GlassTheme.Danger);
                    bc.DefaultCellStyle.SelectionForeColor = Color.White;
                }
            }
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
                    button2.Text = "重新设定";
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
            var result = label4.Text.replace(" ", "");
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

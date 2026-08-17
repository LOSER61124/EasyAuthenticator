using EasyAuthenticator.UI;

namespace EasyAuthenticator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Font fontCaption = GlassTheme.Font(9.5f);
            Font fontCaptionSmall = GlassTheme.Font(9f);
            Font fontCode = GlassTheme.Font(38f);
            Font fontCodeSmall = GlassTheme.Font(18f);
            Font fontTitle = GlassTheme.Font(12f, FontStyle.Bold);

            panelHero = new GlassPanel();
            lblCapCurrent = new Label();
            label4 = new Label();
            label3 = new Label();
            glassProgress1 = new GlassProgress();
            panelPrev = new GlassPanel();
            lblCapPrev = new Label();
            label5 = new Label();
            panelNext = new GlassPanel();
            lblCapNext = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            panelManage = new GlassPanel();
            lblManageTitle = new Label();
            label1 = new Label();
            panelKey = new GlassPanel();
            textBox1 = new TextBox();
            button1 = new GlassButton();
            button2 = new GlassButton();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            panelHero.SuspendLayout();
            panelPrev.SuspendLayout();
            panelNext.SuspendLayout();
            panelManage.SuspendLayout();
            panelKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            //
            // panelHero
            //
            panelHero.Controls.Add(lblCapCurrent);
            panelHero.Controls.Add(label4);
            panelHero.Controls.Add(label3);
            panelHero.Controls.Add(glassProgress1);
            panelHero.CornerRadius = 20;
            panelHero.Location = new Point(12, 12);
            panelHero.Name = "panelHero";
            panelHero.Size = new Size(442, 202);
            panelHero.TabIndex = 0;
            //
            // lblCapCurrent
            //
            lblCapCurrent.AutoSize = true;
            lblCapCurrent.BackColor = Color.Transparent;
            lblCapCurrent.Font = fontCaption;
            lblCapCurrent.ForeColor = GlassTheme.TextSecondary;
            lblCapCurrent.Location = new Point(18, 14);
            lblCapCurrent.Name = "lblCapCurrent";
            lblCapCurrent.Text = "当前校验码";
            //
            // label4
            //
            label4.BackColor = Color.Transparent;
            label4.Font = fontCode;
            label4.ForeColor = GlassTheme.TextPrimary;
            label4.Location = new Point(18, 38);
            label4.Name = "label4";
            label4.Size = new Size(406, 96);
            label4.TabIndex = 1;
            label4.Text = "0 0 0 0 0 0";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            //
            // label3
            //
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = fontCaption;
            label3.ForeColor = GlassTheme.TextSecondary;
            label3.Location = new Point(20, 144);
            label3.Name = "label3";
            label3.TabIndex = 0;
            label3.Text = "距离当前校验码过期还差00秒";
            //
            // glassProgress1
            //
            glassProgress1.Location = new Point(20, 174);
            glassProgress1.Name = "glassProgress1";
            glassProgress1.Size = new Size(402, 4);
            glassProgress1.TabIndex = 2;
            //
            // panelPrev
            //
            panelPrev.Controls.Add(lblCapPrev);
            panelPrev.Controls.Add(label5);
            panelPrev.CornerRadius = 16;
            panelPrev.Location = new Point(12, 224);
            panelPrev.Name = "panelPrev";
            panelPrev.Size = new Size(216, 108);
            panelPrev.TabIndex = 1;
            //
            // lblCapPrev
            //
            lblCapPrev.AutoSize = true;
            lblCapPrev.BackColor = Color.Transparent;
            lblCapPrev.Font = fontCaptionSmall;
            lblCapPrev.ForeColor = GlassTheme.TextSecondary;
            lblCapPrev.Location = new Point(14, 10);
            lblCapPrev.Name = "lblCapPrev";
            lblCapPrev.Text = "上个校验码";
            //
            // label5
            //
            label5.BackColor = Color.Transparent;
            label5.Font = fontCodeSmall;
            label5.ForeColor = GlassTheme.TextSecondary;
            label5.Location = new Point(14, 40);
            label5.Name = "label5";
            label5.Size = new Size(188, 40);
            label5.TabIndex = 2;
            label5.Text = "0 0 0 0 0 0";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            //
            // panelNext
            //
            panelNext.Controls.Add(lblCapNext);
            panelNext.Controls.Add(label6);
            panelNext.CornerRadius = 16;
            panelNext.Location = new Point(236, 224);
            panelNext.Name = "panelNext";
            panelNext.Size = new Size(218, 108);
            panelNext.TabIndex = 2;
            //
            // lblCapNext
            //
            lblCapNext.AutoSize = true;
            lblCapNext.BackColor = Color.Transparent;
            lblCapNext.Font = fontCaptionSmall;
            lblCapNext.ForeColor = GlassTheme.TextSecondary;
            lblCapNext.Location = new Point(14, 10);
            lblCapNext.Name = "lblCapNext";
            lblCapNext.Text = "下个校验码";
            //
            // label6
            //
            label6.BackColor = Color.Transparent;
            label6.Font = fontCodeSmall;
            label6.ForeColor = GlassTheme.TextSecondary;
            label6.Location = new Point(14, 40);
            label6.Name = "label6";
            label6.Size = new Size(190, 40);
            label6.TabIndex = 3;
            label6.Text = "0 0 0 0 0 0";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            //
            // label7
            //
            label7.Font = fontCaption;
            label7.ForeColor = GlassTheme.Accent;
            label7.Location = new Point(14, 346);
            label7.Name = "label7";
            label7.Size = new Size(120, 20);
            label7.TabIndex = 3;
            label7.Text = "导出校验码";
            label7.Cursor = Cursors.Hand;
            label7.Click += label7_Click;
            //
            // label8
            //
            label8.Font = fontCaption;
            label8.ForeColor = GlassTheme.Accent;
            label8.Location = new Point(334, 346);
            label8.Name = "label8";
            label8.Size = new Size(120, 20);
            label8.TabIndex = 4;
            label8.Text = "密钥管理";
            label8.TextAlign = ContentAlignment.MiddleRight;
            label8.Cursor = Cursors.Hand;
            label8.Click += label8_Click;
            //
            // panelManage
            //
            panelManage.Controls.Add(lblManageTitle);
            panelManage.Controls.Add(label1);
            panelManage.Controls.Add(panelKey);
            panelManage.Controls.Add(button1);
            panelManage.Controls.Add(button2);
            panelManage.Controls.Add(label2);
            panelManage.Controls.Add(dataGridView1);
            panelManage.CornerRadius = 20;
            panelManage.Location = new Point(466, 12);
            panelManage.Name = "panelManage";
            panelManage.Size = new Size(394, 356);
            panelManage.TabIndex = 5;
            //
            // lblManageTitle
            //
            lblManageTitle.AutoSize = true;
            lblManageTitle.BackColor = Color.Transparent;
            lblManageTitle.Font = fontTitle;
            lblManageTitle.ForeColor = GlassTheme.TextPrimary;
            lblManageTitle.Location = new Point(18, 14);
            lblManageTitle.Name = "lblManageTitle";
            lblManageTitle.Text = "密钥管理";
            //
            // label1
            //
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = fontCaption;
            label1.ForeColor = GlassTheme.TextSecondary;
            label1.Location = new Point(18, 52);
            label1.Name = "label1";
            label1.TabIndex = 0;
            label1.Text = "当前密钥";
            //
            // panelKey
            //
            panelKey.Controls.Add(textBox1);
            panelKey.CornerRadius = 9;
            panelKey.FillAlpha = 14;
            panelKey.Location = new Point(18, 70);
            panelKey.Name = "panelKey";
            panelKey.Size = new Size(358, 32);
            panelKey.TabIndex = 1;
            //
            // textBox1
            //
            textBox1.BackColor = GlassTheme.FieldFill;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = fontCaption;
            textBox1.ForeColor = GlassTheme.TextPrimary;
            textBox1.Location = new Point(10, 6);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(338, 20);
            textBox1.TabIndex = 1;
            //
            // button1
            //
            button1.Location = new Point(18, 114);
            button1.Name = "button1";
            button1.Size = new Size(172, 32);
            button1.TabIndex = 2;
            button1.Text = "查看";
            button1.Click += button1_Click;
            //
            // button2
            //
            button2.Location = new Point(204, 114);
            button2.Name = "button2";
            button2.Primary = true;
            button2.Size = new Size(172, 32);
            button2.TabIndex = 3;
            button2.Text = "重设";
            button2.Click += button2_Click;
            //
            // label2
            //
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = fontCaption;
            label2.ForeColor = GlassTheme.TextSecondary;
            label2.Location = new Point(18, 158);
            label2.Name = "label2";
            label2.TabIndex = 4;
            label2.Text = "历史密钥查询";
            label2.Click += label2_Click;
            //
            // dataGridView1
            //
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BackgroundColor = Color.FromArgb(16, 16, 22);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 24, 31);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = fontCaptionSmall;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = GlassTheme.TextSecondary;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(24, 24, 31);
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = GlassTheme.TextSecondary;
            dataGridView1.ColumnHeadersHeight = 30;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(16, 16, 22);
            dataGridView1.DefaultCellStyle.Font = fontCaption;
            dataGridView1.DefaultCellStyle.ForeColor = GlassTheme.TextPrimary;
            dataGridView1.DefaultCellStyle.SelectionBackColor = GlassTheme.Selection;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = GlassTheme.GridLine;
            dataGridView1.Location = new Point(18, 182);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(358, 156);
            dataGridView1.TabIndex = 5;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(8F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = GlassTheme.BgBase;
            ClientSize = new Size(872, 380);
            Controls.Add(panelManage);
            Controls.Add(panelNext);
            Controls.Add(panelPrev);
            Controls.Add(panelHero);
            Controls.Add(label7);
            Controls.Add(label8);
            DoubleBuffered = true;
            Font = GlassTheme.Font(9f);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "EasyAuthenticator";
            Load += Form1_Load;
            panelHero.ResumeLayout(false);
            panelHero.PerformLayout();
            panelPrev.ResumeLayout(false);
            panelPrev.PerformLayout();
            panelNext.ResumeLayout(false);
            panelNext.PerformLayout();
            panelManage.ResumeLayout(false);
            panelManage.PerformLayout();
            panelKey.ResumeLayout(false);
            panelKey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GlassPanel panelHero;
        private GlassPanel panelPrev;
        private GlassPanel panelNext;
        private GlassPanel panelManage;
        private GlassPanel panelKey;
        private GlassProgress glassProgress1;
        private Label lblCapCurrent;
        private Label lblCapPrev;
        private Label lblCapNext;
        private Label lblManageTitle;
        private TextBox textBox1;
        private Label label1;
        private GlassButton button2;
        private GlassButton button1;
        private DataGridView dataGridView1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
    }
}

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
            Font fontCaption = GlassTheme.FontCaption();
            Font fontCaptionSmall = GlassTheme.FontCaption(8.5f);
            Font fontCode = GlassTheme.FontDisplay(38f);
            Font fontCodeSmall = GlassTheme.FontDisplay(18f);
            Font fontTitle = GlassTheme.FontHeadline(12f);

            panelHero = new GlassPanel();
            lblCapCurrent = new GlassLabel();
            glassProgress1 = new GlassProgress();
            panelPrev = new GlassPanel();
            lblCapPrev = new GlassLabel();
            panelNext = new GlassPanel();
            lblCapNext = new GlassLabel();
            linkExport = new GlassPanel();
            linkManage = new GlassPanel();
            panelManage = new GlassPanel();
            lblManageTitle = new GlassLabel();
            label1 = new GlassLabel();
            panelKey = new GlassPanel();
            textBox1 = new TextBox();
            button1 = new GlassPanel();
            button2 = new GlassPanel();
            label2 = new GlassLabel();
            panelGrid = new GlassPanel();
            dataGridView1 = new DataGridView();
            panelHero.SuspendLayout();
            panelPrev.SuspendLayout();
            panelNext.SuspendLayout();
            panelManage.SuspendLayout();
            panelKey.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            //
            // panelHero
            //
            panelHero.Controls.Add(lblCapCurrent);
            panelHero.Controls.Add(glassProgress1);
            panelHero.CornerRadius = 20;
            panelHero.MaterialVariant = GlassPanel.Variant.Hero;
            panelHero.Location = new Point(12, 12);
            panelHero.Name = "panelHero";
            panelHero.Size = new Size(442, 202);
            panelHero.TabIndex = 0;
            //
            // lblCapCurrent
            //
            lblCapCurrent.AutoSize = true;
            lblCapCurrent.Font = fontCaption;
            lblCapCurrent.ForeColor = GlassTheme.TextSecondary;
            lblCapCurrent.Location = new Point(18, 14);
            lblCapCurrent.Name = "lblCapCurrent";
            lblCapCurrent.Text = "当前校验码";
            //
            //
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
            panelPrev.CornerRadius = 16;
            panelPrev.Location = new Point(12, 224);
            panelPrev.Name = "panelPrev";
            panelPrev.Size = new Size(216, 108);
            panelPrev.TabIndex = 1;
            //
            // lblCapPrev
            //
            lblCapPrev.AutoSize = true;
            lblCapPrev.Font = fontCaptionSmall;
            lblCapPrev.ForeColor = GlassTheme.TextSecondary;
            lblCapPrev.Location = new Point(14, 10);
            lblCapPrev.Name = "lblCapPrev";
            lblCapPrev.Text = "上个校验码";
            //
            //
            // panelNext
            //
            panelNext.Controls.Add(lblCapNext);
            panelNext.CornerRadius = 16;
            panelNext.Location = new Point(236, 224);
            panelNext.Name = "panelNext";
            panelNext.Size = new Size(218, 108);
            panelNext.TabIndex = 2;
            //
            // lblCapNext
            //
            lblCapNext.AutoSize = true;
            lblCapNext.Font = fontCaptionSmall;
            lblCapNext.ForeColor = GlassTheme.TextSecondary;
            lblCapNext.Location = new Point(14, 10);
            lblCapNext.Name = "lblCapNext";
            lblCapNext.Text = "下个校验码";
            //
            //
            // linkExport
            //
            linkExport.CornerRadius = 10;
            linkExport.Clickable = true;
            linkExport.Location = new Point(12, 340);
            linkExport.Name = "linkExport";
            linkExport.Size = new Size(124, 36);
            linkExport.TabIndex = 3;
            linkExport.Click += label7_Click;
            //
            // linkManage
            //
            linkManage.CornerRadius = 10;
            linkManage.Clickable = true;
            linkManage.Location = new Point(330, 340);
            linkManage.Name = "linkManage";
            linkManage.Size = new Size(124, 36);
            linkManage.TabIndex = 4;
            linkManage.Click += label8_Click;
            //
            // panelManage
            //
            panelManage.Controls.Add(lblManageTitle);
            panelManage.Controls.Add(label1);
            panelManage.Controls.Add(panelKey);
            panelManage.Controls.Add(button1);
            panelManage.Controls.Add(button2);
            panelManage.Controls.Add(label2);
            panelManage.Controls.Add(panelGrid);
            panelManage.CornerRadius = 20;
            panelManage.Location = new Point(466, 12);
            panelManage.Name = "panelManage";
            panelManage.Size = new Size(394, 356);
            panelManage.TabIndex = 5;
            //
            // lblManageTitle
            //
            lblManageTitle.AutoSize = true;
            lblManageTitle.Font = fontTitle;
            lblManageTitle.ForeColor = GlassTheme.TextPrimary;
            lblManageTitle.Location = new Point(18, 14);
            lblManageTitle.Name = "lblManageTitle";
            lblManageTitle.Text = "密钥管理";
            //
            // label1
            //
            label1.AutoSize = true;
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
            panelKey.MaterialVariant = GlassPanel.Variant.Inset;
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
            button1.CornerRadius = 10;
            button1.Clickable = true;
            button1.Location = new Point(18, 114);
            button1.Name = "button1";
            button1.Size = new Size(172, 32);
            button1.TabIndex = 2;
            button1.Click += button1_Click;
            //
            // button2
            //
            button2.CornerRadius = 10;
            button2.Clickable = true;
            button2.Location = new Point(204, 114);
            button2.Name = "button2";
            button2.Primary = true;
            button2.Size = new Size(172, 32);
            button2.TabIndex = 3;
            button2.Click += button2_Click;
            //
            // label2
            //
            label2.Font = fontCaption;
            label2.ForeColor = GlassTheme.TextSecondary;
            label2.Location = new Point(18, 152);
            label2.Name = "label2";
            label2.Size = new Size(140, 26);
            label2.TabIndex = 4;
            label2.Text = "历史密钥查询";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            label2.Click += label2_Click;
            //
            // panelGrid
            //
            panelGrid.Controls.Add(dataGridView1);
            panelGrid.CornerRadius = 10;
            panelGrid.MaterialVariant = GlassPanel.Variant.Inset;
            panelGrid.Location = new Point(18, 182);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(358, 156);
            panelGrid.TabIndex = 5;
            //
            // dataGridView1
            //
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.BackgroundColor = GlassTheme.GridBg;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlassTheme.GridHeaderBg;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = fontCaptionSmall;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = GlassTheme.TextSecondary;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = GlassTheme.GridHeaderBg;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = GlassTheme.TextSecondary;
            dataGridView1.ColumnHeadersHeight = 30;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.DefaultCellStyle.BackColor = GlassTheme.GridBg;
            dataGridView1.DefaultCellStyle.Font = fontCaption;
            dataGridView1.DefaultCellStyle.ForeColor = GlassTheme.TextPrimary;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, GlassTheme.Accent);
            dataGridView1.DefaultCellStyle.SelectionForeColor = GlassTheme.TextPrimary;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = GlassTheme.GridLine;
            dataGridView1.Location = new Point(2, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 32;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(354, 152);
            dataGridView1.TabIndex = 5;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = GlassTheme.BgBase;
            ClientSize = new Size(872, 380);
            Controls.Add(panelManage);
            Controls.Add(panelNext);
            Controls.Add(panelPrev);
            Controls.Add(panelHero);
            Controls.Add(linkExport);
            Controls.Add(linkManage);
            //全控件均无透明后代（面板/标签/按钮皆不透明自绘），窗体双缓冲安全，消除每秒刷新闪烁
            DoubleBuffered = true;
            Font = GlassTheme.FontBody(9f);
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
            panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GlassPanel panelHero;
        private GlassPanel panelPrev;
        private GlassPanel panelNext;
        private GlassPanel panelManage;
        private GlassPanel panelKey;
        private GlassPanel panelGrid;
        private GlassPanel linkExport;
        private GlassPanel linkManage;
        private GlassProgress glassProgress1;
        private Label lblCapCurrent;
        private Label lblCapPrev;
        private Label lblCapNext;
        private Label lblManageTitle;
        private TextBox textBox1;
        private Label label1;
        private GlassPanel button2;
        private GlassPanel button1;
        private DataGridView dataGridView1;
        private Label label2;
    }
}

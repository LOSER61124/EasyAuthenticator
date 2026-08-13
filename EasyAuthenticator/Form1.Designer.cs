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
            groupBox1 = new GroupBox();
            label8 = new Label();
            label7 = new Label();
            label4 = new Label();
            label3 = new Label();
            groupBox2 = new GroupBox();
            label5 = new Label();
            groupBox3 = new GroupBox();
            label6 = new Label();
            groupBox4 = new GroupBox();
            dataGridView1 = new DataGridView();
            label2 = new Label();
            button2 = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Font = new Font("宋体", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(12, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(442, 241);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "当前校验码";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.Blue;
            label8.Location = new Point(362, 213);
            label8.Name = "label8";
            label8.Size = new Size(71, 15);
            label8.TabIndex = 3;
            label8.Text = "密钥管理";
            label8.Click += label8_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.Blue;
            label7.Location = new Point(247, 213);
            label7.Name = "label7";
            label7.Size = new Size(87, 15);
            label7.TabIndex = 2;
            label7.Text = "导出校验码";
            label7.Click += label7_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("微软雅黑", 45F, FontStyle.Bold);
            label4.ForeColor = Color.Red;
            label4.Location = new Point(6, 67);
            label4.Name = "label4";
            label4.Size = new Size(428, 100);
            label4.TabIndex = 1;
            label4.Text = "0 0 0 0 0 0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Blue;
            label3.Location = new Point(6, 213);
            label3.Name = "label3";
            label3.Size = new Size(217, 15);
            label3.TabIndex = 0;
            label3.Text = "距离当前校验码过期还差00秒";
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.Transparent;
            groupBox2.Controls.Add(label5);
            groupBox2.Font = new Font("宋体", 9F, FontStyle.Bold);
            groupBox2.Location = new Point(12, 266);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(216, 100);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "上个校验码";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("微软雅黑", 18F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label5.ForeColor = Color.Gray;
            label5.Location = new Point(19, 36);
            label5.Name = "label5";
            label5.Size = new Size(176, 40);
            label5.TabIndex = 2;
            label5.Text = "0 0 0 0 0 0";
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.Transparent;
            groupBox3.Controls.Add(label6);
            groupBox3.Font = new Font("宋体", 9F, FontStyle.Bold);
            groupBox3.Location = new Point(234, 266);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(220, 100);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "下个校验码";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("微软雅黑", 18F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label6.ForeColor = Color.Gray;
            label6.Location = new Point(25, 36);
            label6.Name = "label6";
            label6.Size = new Size(176, 40);
            label6.TabIndex = 3;
            label6.Text = "0 0 0 0 0 0";
            // 
            // groupBox4
            // 
            groupBox4.BackColor = Color.Transparent;
            groupBox4.Controls.Add(dataGridView1);
            groupBox4.Controls.Add(label2);
            groupBox4.Controls.Add(button2);
            groupBox4.Controls.Add(button1);
            groupBox4.Controls.Add(textBox1);
            groupBox4.Controls.Add(label1);
            groupBox4.Font = new Font("宋体", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            groupBox4.Location = new Point(473, 4);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(393, 362);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "密钥管理";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(9, 124);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(379, 232);
            dataGridView1.TabIndex = 5;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 103);
            label2.Name = "label2";
            label2.Size = new Size(119, 15);
            label2.TabIndex = 4;
            label2.Text = "历史密钥查询：";
            label2.Click += label2_Click;
            // 
            // button2
            // 
            button2.Location = new Point(268, 63);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 3;
            button2.Text = "重设";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(121, 63);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 2;
            button1.Text = "查看";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(96, 23);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(304, 25);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 29);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 0;
            label1.Text = "当前密钥：";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Dblue;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(872, 374);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            DoubleBuffered = true;
            Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "EasyAuthenticator";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private TextBox textBox1;
        private Label label1;
        private Button button2;
        private Button button1;
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

namespace GoldenPigs
{
    partial class YuceTouzhuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtpTouzhushijian = new System.Windows.Forms.DateTimePicker();
            this.btnAutoTouzhu = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvTuijian = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.btnTuijian = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTuijian)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTouzhushijian
            // 
            this.dtpTouzhushijian.Location = new System.Drawing.Point(180, 28);
            this.dtpTouzhushijian.Name = "dtpTouzhushijian";
            this.dtpTouzhushijian.Size = new System.Drawing.Size(119, 21);
            this.dtpTouzhushijian.TabIndex = 0;
            // 
            // btnAutoTouzhu
            // 
            this.btnAutoTouzhu.Location = new System.Drawing.Point(790, 29);
            this.btnAutoTouzhu.Name = "btnAutoTouzhu";
            this.btnAutoTouzhu.Size = new System.Drawing.Size(75, 23);
            this.btnAutoTouzhu.TabIndex = 1;
            this.btnAutoTouzhu.Text = "自动投注";
            this.btnAutoTouzhu.UseVisualStyleBackColor = true;
            this.btnAutoTouzhu.Click += new System.EventHandler(this.btnAutoTouzhu_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox5);
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(99, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(766, 57);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "投注策略";
            // 
            // dgvTuijian
            // 
            this.dgvTuijian.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTuijian.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTuijian.Location = new System.Drawing.Point(39, 155);
            this.dgvTuijian.Name = "dgvTuijian";
            this.dgvTuijian.RowTemplate.Height = 23;
            this.dgvTuijian.Size = new System.Drawing.Size(826, 347);
            this.dgvTuijian.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "投注时间";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(17, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "随机顺序";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(130, 21);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "SP值顺序";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(243, 21);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(66, 16);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "只包含3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(356, 21);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(66, 16);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "只包含0";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(469, 21);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(84, 16);
            this.checkBox5.TabIndex = 4;
            this.checkBox5.Text = "只包含名人";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // btnTuijian
            // 
            this.btnTuijian.Location = new System.Drawing.Point(693, 29);
            this.btnTuijian.Name = "btnTuijian";
            this.btnTuijian.Size = new System.Drawing.Size(75, 23);
            this.btnTuijian.TabIndex = 5;
            this.btnTuijian.Text = "推荐";
            this.btnTuijian.UseVisualStyleBackColor = true;
            this.btnTuijian.Click += new System.EventHandler(this.btnTuijian_Click);
            // 
            // YuceTouzhuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 559);
            this.Controls.Add(this.btnTuijian);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTuijian);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAutoTouzhu);
            this.Controls.Add(this.dtpTouzhushijian);
            this.Name = "YuceTouzhuForm";
            this.Text = "预测投注中心";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTuijian)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpTouzhushijian;
        private System.Windows.Forms.Button btnAutoTouzhu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvTuijian;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnTuijian;
    }
}
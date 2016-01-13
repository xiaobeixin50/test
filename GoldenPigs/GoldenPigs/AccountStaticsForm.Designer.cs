namespace GoldenPigs
{
    partial class AccountStaticsForm
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
            this.btnChongzhi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblYue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblZongjine = new System.Windows.Forms.Label();
            this.lblZhongjiangjine = new System.Windows.Forms.Label();
            this.lblGoujiangjine = new System.Windows.Forms.Label();
            this.txtChongzhijine = new System.Windows.Forms.TextBox();
            this.dgvIncome = new System.Windows.Forms.DataGridView();
            this.lilChongzhijine = new System.Windows.Forms.LinkLabel();
            this.lilZhongjiangjine = new System.Windows.Forms.LinkLabel();
            this.lilGoujiangjine = new System.Windows.Forms.LinkLabel();
            this.lilYue = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncome)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChongzhi
            // 
            this.btnChongzhi.Location = new System.Drawing.Point(767, 54);
            this.btnChongzhi.Name = "btnChongzhi";
            this.btnChongzhi.Size = new System.Drawing.Size(75, 23);
            this.btnChongzhi.TabIndex = 0;
            this.btnChongzhi.Text = "充值";
            this.btnChongzhi.UseVisualStyleBackColor = true;
            this.btnChongzhi.Click += new System.EventHandler(this.btnChongzhi_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "余额：";
            // 
            // lblYue
            // 
            this.lblYue.AutoSize = true;
            this.lblYue.Location = new System.Drawing.Point(179, 138);
            this.lblYue.Name = "lblYue";
            this.lblYue.Size = new System.Drawing.Size(23, 12);
            this.lblYue.TabIndex = 2;
            this.lblYue.Text = "0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "充值总金额：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "中奖金额：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "购奖总金额：";
            // 
            // lblZongjine
            // 
            this.lblZongjine.AutoSize = true;
            this.lblZongjine.Location = new System.Drawing.Point(179, 42);
            this.lblZongjine.Name = "lblZongjine";
            this.lblZongjine.Size = new System.Drawing.Size(23, 12);
            this.lblZongjine.TabIndex = 6;
            this.lblZongjine.Text = "0.0";
            // 
            // lblZhongjiangjine
            // 
            this.lblZhongjiangjine.AutoSize = true;
            this.lblZhongjiangjine.Location = new System.Drawing.Point(179, 75);
            this.lblZhongjiangjine.Name = "lblZhongjiangjine";
            this.lblZhongjiangjine.Size = new System.Drawing.Size(23, 12);
            this.lblZhongjiangjine.TabIndex = 7;
            this.lblZhongjiangjine.Text = "0.0";
            // 
            // lblGoujiangjine
            // 
            this.lblGoujiangjine.AutoSize = true;
            this.lblGoujiangjine.Location = new System.Drawing.Point(179, 102);
            this.lblGoujiangjine.Name = "lblGoujiangjine";
            this.lblGoujiangjine.Size = new System.Drawing.Size(23, 12);
            this.lblGoujiangjine.TabIndex = 8;
            this.lblGoujiangjine.Text = "0.0";
            // 
            // txtChongzhijine
            // 
            this.txtChongzhijine.Location = new System.Drawing.Point(563, 54);
            this.txtChongzhijine.Name = "txtChongzhijine";
            this.txtChongzhijine.Size = new System.Drawing.Size(118, 21);
            this.txtChongzhijine.TabIndex = 9;
            this.txtChongzhijine.Text = "10000";
            // 
            // dgvIncome
            // 
            this.dgvIncome.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvIncome.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIncome.Location = new System.Drawing.Point(75, 193);
            this.dgvIncome.Name = "dgvIncome";
            this.dgvIncome.RowTemplate.Height = 23;
            this.dgvIncome.Size = new System.Drawing.Size(767, 298);
            this.dgvIncome.TabIndex = 10;
            // 
            // lilChongzhijine
            // 
            this.lilChongzhijine.AutoSize = true;
            this.lilChongzhijine.Location = new System.Drawing.Point(335, 42);
            this.lilChongzhijine.Name = "lilChongzhijine";
            this.lilChongzhijine.Size = new System.Drawing.Size(29, 12);
            this.lilChongzhijine.TabIndex = 11;
            this.lilChongzhijine.TabStop = true;
            this.lilChongzhijine.Text = "明细";
            this.lilChongzhijine.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lilChongzhijine_LinkClicked);
            // 
            // lilZhongjiangjine
            // 
            this.lilZhongjiangjine.AutoSize = true;
            this.lilZhongjiangjine.Location = new System.Drawing.Point(335, 75);
            this.lilZhongjiangjine.Name = "lilZhongjiangjine";
            this.lilZhongjiangjine.Size = new System.Drawing.Size(29, 12);
            this.lilZhongjiangjine.TabIndex = 12;
            this.lilZhongjiangjine.TabStop = true;
            this.lilZhongjiangjine.Text = "明细";
            this.lilZhongjiangjine.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lilZhongjiangjine_LinkClicked);
            // 
            // lilGoujiangjine
            // 
            this.lilGoujiangjine.AutoSize = true;
            this.lilGoujiangjine.Location = new System.Drawing.Point(335, 102);
            this.lilGoujiangjine.Name = "lilGoujiangjine";
            this.lilGoujiangjine.Size = new System.Drawing.Size(29, 12);
            this.lilGoujiangjine.TabIndex = 13;
            this.lilGoujiangjine.TabStop = true;
            this.lilGoujiangjine.Text = "明细";
            this.lilGoujiangjine.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lilGoujiangjine_LinkClicked);
            // 
            // lilYue
            // 
            this.lilYue.AutoSize = true;
            this.lilYue.Location = new System.Drawing.Point(335, 137);
            this.lilYue.Name = "lilYue";
            this.lilYue.Size = new System.Drawing.Size(29, 12);
            this.lilYue.TabIndex = 14;
            this.lilYue.TabStop = true;
            this.lilYue.Text = "明细";
            this.lilYue.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lilYue_LinkClicked);
            // 
            // AccountStaticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 546);
            this.Controls.Add(this.lilYue);
            this.Controls.Add(this.lilGoujiangjine);
            this.Controls.Add(this.lilZhongjiangjine);
            this.Controls.Add(this.lilChongzhijine);
            this.Controls.Add(this.dgvIncome);
            this.Controls.Add(this.txtChongzhijine);
            this.Controls.Add(this.lblGoujiangjine);
            this.Controls.Add(this.lblZhongjiangjine);
            this.Controls.Add(this.lblZongjine);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblYue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChongzhi);
            this.Name = "AccountStaticsForm";
            this.Text = "账户总览";
            this.Load += new System.EventHandler(this.AccountStaticsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncome)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChongzhi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblYue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblZongjine;
        private System.Windows.Forms.Label lblZhongjiangjine;
        private System.Windows.Forms.Label lblGoujiangjine;
        private System.Windows.Forms.TextBox txtChongzhijine;
        private System.Windows.Forms.DataGridView dgvIncome;
        private System.Windows.Forms.LinkLabel lilChongzhijine;
        private System.Windows.Forms.LinkLabel lilZhongjiangjine;
        private System.Windows.Forms.LinkLabel lilGoujiangjine;
        private System.Windows.Forms.LinkLabel lilYue;
    }
}
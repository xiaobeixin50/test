namespace GoldenPigs
{
    partial class btnImportDataFromUrlForm
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
            this.btnImportTaocan = new System.Windows.Forms.Button();
            this.btnImportKaijiang = new System.Windows.Forms.Button();
            this.txtImportDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBatchImportTaocan = new System.Windows.Forms.Button();
            this.btnBatchImportKaijiang = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnImportBifa = new System.Windows.Forms.Button();
            this.btnImportPeilv = new System.Windows.Forms.Button();
            this.btnImportCurrentTaocan = new System.Windows.Forms.Button();
            this.btnImportCaike = new System.Windows.Forms.Button();
            this.btnAnalysisCaike = new System.Windows.Forms.Button();
            this.btnImportPeilvFromKaijiang = new System.Windows.Forms.Button();
            this.btnImportYDN = new System.Windows.Forms.Button();
            this.btnImportLanqiu = new System.Windows.Forms.Button();
            this.btnImportLanqiuYuce = new System.Windows.Forms.Button();
            this.btnUpdateLanqiuResult = new System.Windows.Forms.Button();
            this.btnImportYdnMul = new System.Windows.Forms.Button();
            this.btnImportKaijianMul = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtImportSingleUrl = new System.Windows.Forms.Button();
            this.btnLanqiuRqResult = new System.Windows.Forms.Button();
            this.btnImportDanchang = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImportTaocan
            // 
            this.btnImportTaocan.Location = new System.Drawing.Point(571, 40);
            this.btnImportTaocan.Name = "btnImportTaocan";
            this.btnImportTaocan.Size = new System.Drawing.Size(104, 23);
            this.btnImportTaocan.TabIndex = 0;
            this.btnImportTaocan.Text = "导入套餐";
            this.btnImportTaocan.UseVisualStyleBackColor = true;
            this.btnImportTaocan.Click += new System.EventHandler(this.btnImportTaocan_Click);
            // 
            // btnImportKaijiang
            // 
            this.btnImportKaijiang.Location = new System.Drawing.Point(711, 40);
            this.btnImportKaijiang.Name = "btnImportKaijiang";
            this.btnImportKaijiang.Size = new System.Drawing.Size(118, 23);
            this.btnImportKaijiang.TabIndex = 1;
            this.btnImportKaijiang.Text = "导入开奖结果";
            this.btnImportKaijiang.UseVisualStyleBackColor = true;
            this.btnImportKaijiang.Click += new System.EventHandler(this.btnImportKaijiang_Click);
            // 
            // txtImportDate
            // 
            this.txtImportDate.Location = new System.Drawing.Point(74, 37);
            this.txtImportDate.Name = "txtImportDate";
            this.txtImportDate.Size = new System.Drawing.Size(122, 21);
            this.txtImportDate.TabIndex = 2;
            this.txtImportDate.Text = "2014-8-20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "日期：";
            // 
            // btnBatchImportTaocan
            // 
            this.btnBatchImportTaocan.Location = new System.Drawing.Point(571, 11);
            this.btnBatchImportTaocan.Name = "btnBatchImportTaocan";
            this.btnBatchImportTaocan.Size = new System.Drawing.Size(104, 23);
            this.btnBatchImportTaocan.TabIndex = 4;
            this.btnBatchImportTaocan.Text = "批量导入套餐";
            this.btnBatchImportTaocan.UseVisualStyleBackColor = true;
            this.btnBatchImportTaocan.Click += new System.EventHandler(this.btnBatchImportTaocan_Click);
            // 
            // btnBatchImportKaijiang
            // 
            this.btnBatchImportKaijiang.Location = new System.Drawing.Point(711, 101);
            this.btnBatchImportKaijiang.Name = "btnBatchImportKaijiang";
            this.btnBatchImportKaijiang.Size = new System.Drawing.Size(118, 23);
            this.btnBatchImportKaijiang.TabIndex = 5;
            this.btnBatchImportKaijiang.Text = "批量导入开奖结果";
            this.btnBatchImportKaijiang.UseVisualStyleBackColor = true;
            this.btnBatchImportKaijiang.Click += new System.EventHandler(this.btnBatchImportKaijiang_Click);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(74, 90);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(140, 21);
            this.dtpStartDate.TabIndex = 6;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(308, 90);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(131, 21);
            this.dtpEndDate.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "开始日期";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(236, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "结束日期";
            // 
            // btnImportBifa
            // 
            this.btnImportBifa.Location = new System.Drawing.Point(571, 162);
            this.btnImportBifa.Name = "btnImportBifa";
            this.btnImportBifa.Size = new System.Drawing.Size(116, 23);
            this.btnImportBifa.TabIndex = 10;
            this.btnImportBifa.Text = "批量导入必发Top3";
            this.btnImportBifa.UseVisualStyleBackColor = true;
            this.btnImportBifa.Click += new System.EventHandler(this.btnImportBifa_Click);
            // 
            // btnImportPeilv
            // 
            this.btnImportPeilv.Location = new System.Drawing.Point(711, 162);
            this.btnImportPeilv.Name = "btnImportPeilv";
            this.btnImportPeilv.Size = new System.Drawing.Size(118, 23);
            this.btnImportPeilv.TabIndex = 11;
            this.btnImportPeilv.Text = "导入赔率";
            this.btnImportPeilv.UseVisualStyleBackColor = true;
            this.btnImportPeilv.Click += new System.EventHandler(this.btnImportPeilv_Click);
            // 
            // btnImportCurrentTaocan
            // 
            this.btnImportCurrentTaocan.Location = new System.Drawing.Point(571, 223);
            this.btnImportCurrentTaocan.Name = "btnImportCurrentTaocan";
            this.btnImportCurrentTaocan.Size = new System.Drawing.Size(116, 23);
            this.btnImportCurrentTaocan.TabIndex = 12;
            this.btnImportCurrentTaocan.Text = "导入当前套餐";
            this.btnImportCurrentTaocan.UseVisualStyleBackColor = true;
            this.btnImportCurrentTaocan.Click += new System.EventHandler(this.btnImportCurrentTaocan_Click);
            // 
            // btnImportCaike
            // 
            this.btnImportCaike.Location = new System.Drawing.Point(571, 285);
            this.btnImportCaike.Name = "btnImportCaike";
            this.btnImportCaike.Size = new System.Drawing.Size(116, 23);
            this.btnImportCaike.TabIndex = 13;
            this.btnImportCaike.Text = "导入彩客网预测";
            this.btnImportCaike.UseVisualStyleBackColor = true;
            this.btnImportCaike.Click += new System.EventHandler(this.btnImportCaike_Click);
            // 
            // btnAnalysisCaike
            // 
            this.btnAnalysisCaike.Location = new System.Drawing.Point(711, 285);
            this.btnAnalysisCaike.Name = "btnAnalysisCaike";
            this.btnAnalysisCaike.Size = new System.Drawing.Size(126, 23);
            this.btnAnalysisCaike.TabIndex = 14;
            this.btnAnalysisCaike.Text = "分析彩客网预测数据";
            this.btnAnalysisCaike.UseVisualStyleBackColor = true;
            this.btnAnalysisCaike.Click += new System.EventHandler(this.btnAnalysisCaike_Click);
            // 
            // btnImportPeilvFromKaijiang
            // 
            this.btnImportPeilvFromKaijiang.Location = new System.Drawing.Point(711, 223);
            this.btnImportPeilvFromKaijiang.Name = "btnImportPeilvFromKaijiang";
            this.btnImportPeilvFromKaijiang.Size = new System.Drawing.Size(126, 23);
            this.btnImportPeilvFromKaijiang.TabIndex = 15;
            this.btnImportPeilvFromKaijiang.Text = "从开奖结果导入赔率";
            this.btnImportPeilvFromKaijiang.UseVisualStyleBackColor = true;
            this.btnImportPeilvFromKaijiang.Click += new System.EventHandler(this.btnImportPeilvFromKaijiang_Click);
            // 
            // btnImportYDN
            // 
            this.btnImportYDN.Location = new System.Drawing.Point(185, 236);
            this.btnImportYDN.Name = "btnImportYDN";
            this.btnImportYDN.Size = new System.Drawing.Size(147, 23);
            this.btnImportYDN.TabIndex = 16;
            this.btnImportYDN.Text = "导入一定牛数据";
            this.btnImportYDN.UseVisualStyleBackColor = true;
            this.btnImportYDN.Click += new System.EventHandler(this.btnImportYDN_Click);
            // 
            // btnImportLanqiu
            // 
            this.btnImportLanqiu.Location = new System.Drawing.Point(313, 346);
            this.btnImportLanqiu.Name = "btnImportLanqiu";
            this.btnImportLanqiu.Size = new System.Drawing.Size(126, 23);
            this.btnImportLanqiu.TabIndex = 17;
            this.btnImportLanqiu.Text = "导入篮球结果数据";
            this.btnImportLanqiu.UseVisualStyleBackColor = true;
            this.btnImportLanqiu.Click += new System.EventHandler(this.btnImportLanqiu_Click);
            // 
            // btnImportLanqiuYuce
            // 
            this.btnImportLanqiuYuce.Location = new System.Drawing.Point(29, 346);
            this.btnImportLanqiuYuce.Name = "btnImportLanqiuYuce";
            this.btnImportLanqiuYuce.Size = new System.Drawing.Size(125, 23);
            this.btnImportLanqiuYuce.TabIndex = 18;
            this.btnImportLanqiuYuce.Text = "导入篮球预测数据";
            this.btnImportLanqiuYuce.UseVisualStyleBackColor = true;
            this.btnImportLanqiuYuce.Click += new System.EventHandler(this.btnImportLanqiuYuce_Click);
            // 
            // btnUpdateLanqiuResult
            // 
            this.btnUpdateLanqiuResult.Location = new System.Drawing.Point(185, 346);
            this.btnUpdateLanqiuResult.Name = "btnUpdateLanqiuResult";
            this.btnUpdateLanqiuResult.Size = new System.Drawing.Size(104, 23);
            this.btnUpdateLanqiuResult.TabIndex = 19;
            this.btnUpdateLanqiuResult.Text = "更新篮球结果";
            this.btnUpdateLanqiuResult.UseVisualStyleBackColor = true;
            this.btnUpdateLanqiuResult.Click += new System.EventHandler(this.btnUpdateLanqiuResult_Click);
            // 
            // btnImportYdnMul
            // 
            this.btnImportYdnMul.Location = new System.Drawing.Point(185, 285);
            this.btnImportYdnMul.Name = "btnImportYdnMul";
            this.btnImportYdnMul.Size = new System.Drawing.Size(147, 23);
            this.btnImportYdnMul.TabIndex = 20;
            this.btnImportYdnMul.Text = "多线程导入一定牛预测";
            this.btnImportYdnMul.UseVisualStyleBackColor = true;
            this.btnImportYdnMul.Click += new System.EventHandler(this.btnImportYdnMul_Click);
            // 
            // btnImportKaijianMul
            // 
            this.btnImportKaijianMul.Location = new System.Drawing.Point(540, 101);
            this.btnImportKaijianMul.Name = "btnImportKaijianMul";
            this.btnImportKaijianMul.Size = new System.Drawing.Size(147, 23);
            this.btnImportKaijianMul.TabIndex = 21;
            this.btnImportKaijianMul.Text = "多线程导入开奖结果";
            this.btnImportKaijianMul.UseVisualStyleBackColor = true;
            this.btnImportKaijianMul.Click += new System.EventHandler(this.btnImportKaijianMul_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "页码";
            // 
            // txtPage
            // 
            this.txtPage.Location = new System.Drawing.Point(74, 284);
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(100, 21);
            this.txtPage.TabIndex = 23;
            this.txtPage.Text = "1";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(74, 201);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(100, 21);
            this.txtUrl.TabIndex = 24;
            // 
            // txtImportSingleUrl
            // 
            this.txtImportSingleUrl.Location = new System.Drawing.Point(185, 199);
            this.txtImportSingleUrl.Name = "txtImportSingleUrl";
            this.txtImportSingleUrl.Size = new System.Drawing.Size(147, 23);
            this.txtImportSingleUrl.TabIndex = 25;
            this.txtImportSingleUrl.Text = "测试单个url导入";
            this.txtImportSingleUrl.UseVisualStyleBackColor = true;
            this.txtImportSingleUrl.Click += new System.EventHandler(this.txtImportSingleUrl_Click);
            // 
            // btnLanqiuRqResult
            // 
            this.btnLanqiuRqResult.Location = new System.Drawing.Point(24, 411);
            this.btnLanqiuRqResult.Name = "btnLanqiuRqResult";
            this.btnLanqiuRqResult.Size = new System.Drawing.Size(150, 23);
            this.btnLanqiuRqResult.TabIndex = 26;
            this.btnLanqiuRqResult.Text = "导入篮球让球预测结果";
            this.btnLanqiuRqResult.UseVisualStyleBackColor = true;
            this.btnLanqiuRqResult.Click += new System.EventHandler(this.btnLanqiuRqResult_Click);
            // 
            // btnImportDanchang
            // 
            this.btnImportDanchang.Location = new System.Drawing.Point(24, 471);
            this.btnImportDanchang.Name = "btnImportDanchang";
            this.btnImportDanchang.Size = new System.Drawing.Size(138, 23);
            this.btnImportDanchang.TabIndex = 27;
            this.btnImportDanchang.Text = "导入单场记录";
            this.btnImportDanchang.UseVisualStyleBackColor = true;
            this.btnImportDanchang.Click += new System.EventHandler(this.btnImportDanchang_Click);
            // 
            // btnImportDataFromUrlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 620);
            this.Controls.Add(this.btnImportDanchang);
            this.Controls.Add(this.btnLanqiuRqResult);
            this.Controls.Add(this.txtImportSingleUrl);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.txtPage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnImportKaijianMul);
            this.Controls.Add(this.btnImportYdnMul);
            this.Controls.Add(this.btnUpdateLanqiuResult);
            this.Controls.Add(this.btnImportLanqiuYuce);
            this.Controls.Add(this.btnImportLanqiu);
            this.Controls.Add(this.btnImportYDN);
            this.Controls.Add(this.btnImportPeilvFromKaijiang);
            this.Controls.Add(this.btnAnalysisCaike);
            this.Controls.Add(this.btnImportCaike);
            this.Controls.Add(this.btnImportCurrentTaocan);
            this.Controls.Add(this.btnImportPeilv);
            this.Controls.Add(this.btnImportBifa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.btnBatchImportKaijiang);
            this.Controls.Add(this.btnBatchImportTaocan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtImportDate);
            this.Controls.Add(this.btnImportKaijiang);
            this.Controls.Add(this.btnImportTaocan);
            this.Name = "btnImportDataFromUrlForm";
            this.Text = "批量导入";
            this.Load += new System.EventHandler(this.ImportDataFromUrl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImportTaocan;
        private System.Windows.Forms.Button btnImportKaijiang;
        private System.Windows.Forms.TextBox txtImportDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBatchImportTaocan;
        private System.Windows.Forms.Button btnBatchImportKaijiang;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnImportBifa;
        private System.Windows.Forms.Button btnImportPeilv;
        private System.Windows.Forms.Button btnImportCurrentTaocan;
        private System.Windows.Forms.Button btnImportCaike;
        private System.Windows.Forms.Button btnAnalysisCaike;
        private System.Windows.Forms.Button btnImportPeilvFromKaijiang;
        private System.Windows.Forms.Button btnImportYDN;
        private System.Windows.Forms.Button btnImportLanqiu;
        private System.Windows.Forms.Button btnImportLanqiuYuce;
        private System.Windows.Forms.Button btnUpdateLanqiuResult;
        private System.Windows.Forms.Button btnImportYdnMul;
        private System.Windows.Forms.Button btnImportKaijianMul;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button txtImportSingleUrl;
        private System.Windows.Forms.Button btnLanqiuRqResult;
        private System.Windows.Forms.Button btnImportDanchang;
    }
}
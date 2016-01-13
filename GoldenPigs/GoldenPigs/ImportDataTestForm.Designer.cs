namespace GoldenPigs
{
    partial class ImportDataTestForm
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
            this.txtHtml = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnImportDocument = new System.Windows.Forms.Button();
            this.cbImportDate = new System.Windows.Forms.CheckBox();
            this.txtImportDate = new System.Windows.Forms.TextBox();
            this.btnImportJifen = new System.Windows.Forms.Button();
            this.comLiansai = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImportResult = new System.Windows.Forms.Button();
            this.btnImportCaikeDetail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtHtml
            // 
            this.txtHtml.Location = new System.Drawing.Point(42, 121);
            this.txtHtml.MaxLength = 3276700;
            this.txtHtml.Multiline = true;
            this.txtHtml.Name = "txtHtml";
            this.txtHtml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHtml.Size = new System.Drawing.Size(611, 481);
            this.txtHtml.TabIndex = 0;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(711, 119);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(116, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "导入网络最新套餐";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(42, 75);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(611, 21);
            this.txtUrl.TabIndex = 2;
            this.txtUrl.Text = "http://www.aicai.com/pages/lotnew/zq/index_vote.shtml";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(752, 569);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "导入HTML";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnImportDocument
            // 
            this.btnImportDocument.Location = new System.Drawing.Point(711, 174);
            this.btnImportDocument.Name = "btnImportDocument";
            this.btnImportDocument.Size = new System.Drawing.Size(116, 23);
            this.btnImportDocument.TabIndex = 4;
            this.btnImportDocument.Text = "导入文档";
            this.btnImportDocument.UseVisualStyleBackColor = true;
            this.btnImportDocument.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbImportDate
            // 
            this.cbImportDate.AutoSize = true;
            this.cbImportDate.Location = new System.Drawing.Point(42, 38);
            this.cbImportDate.Name = "cbImportDate";
            this.cbImportDate.Size = new System.Drawing.Size(108, 16);
            this.cbImportDate.TabIndex = 5;
            this.cbImportDate.Text = "自定义导入时间";
            this.cbImportDate.UseVisualStyleBackColor = true;
            // 
            // txtImportDate
            // 
            this.txtImportDate.Location = new System.Drawing.Point(165, 33);
            this.txtImportDate.Name = "txtImportDate";
            this.txtImportDate.Size = new System.Drawing.Size(100, 21);
            this.txtImportDate.TabIndex = 6;
            // 
            // btnImportJifen
            // 
            this.btnImportJifen.Location = new System.Drawing.Point(711, 229);
            this.btnImportJifen.Name = "btnImportJifen";
            this.btnImportJifen.Size = new System.Drawing.Size(116, 23);
            this.btnImportJifen.TabIndex = 7;
            this.btnImportJifen.Text = "导入积分榜";
            this.btnImportJifen.UseVisualStyleBackColor = true;
            this.btnImportJifen.Click += new System.EventHandler(this.btnImportJifen_Click);
            // 
            // comLiansai
            // 
            this.comLiansai.FormattingEnabled = true;
            this.comLiansai.Items.AddRange(new object[] {
            "无",
            "日职乙",
            "日职联"});
            this.comLiansai.Location = new System.Drawing.Point(448, 33);
            this.comLiansai.Name = "comLiansai";
            this.comLiansai.Size = new System.Drawing.Size(121, 20);
            this.comLiansai.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(372, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "选择联赛";
            // 
            // btnImportResult
            // 
            this.btnImportResult.Location = new System.Drawing.Point(711, 279);
            this.btnImportResult.Name = "btnImportResult";
            this.btnImportResult.Size = new System.Drawing.Size(116, 23);
            this.btnImportResult.TabIndex = 10;
            this.btnImportResult.Text = "导入结果";
            this.btnImportResult.UseVisualStyleBackColor = true;
            this.btnImportResult.Click += new System.EventHandler(this.btnImportResult_Click);
            // 
            // btnImportCaikeDetail
            // 
            this.btnImportCaikeDetail.Location = new System.Drawing.Point(711, 75);
            this.btnImportCaikeDetail.Name = "btnImportCaikeDetail";
            this.btnImportCaikeDetail.Size = new System.Drawing.Size(116, 23);
            this.btnImportCaikeDetail.TabIndex = 11;
            this.btnImportCaikeDetail.Text = "导入彩客网明细";
            this.btnImportCaikeDetail.UseVisualStyleBackColor = true;
            this.btnImportCaikeDetail.Click += new System.EventHandler(this.btnImportCaikeDetail_Click);
            // 
            // ImportDataTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 637);
            this.Controls.Add(this.btnImportCaikeDetail);
            this.Controls.Add(this.btnImportResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comLiansai);
            this.Controls.Add(this.btnImportJifen);
            this.Controls.Add(this.txtImportDate);
            this.Controls.Add(this.cbImportDate);
            this.Controls.Add(this.btnImportDocument);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.txtHtml);
            this.Name = "ImportDataTestForm";
            this.Text = "ImportData";
            this.Load += new System.EventHandler(this.ImportData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHtml;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnImportDocument;
        private System.Windows.Forms.CheckBox cbImportDate;
        private System.Windows.Forms.TextBox txtImportDate;
        private System.Windows.Forms.Button btnImportJifen;
        private System.Windows.Forms.ComboBox comLiansai;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImportResult;
        private System.Windows.Forms.Button btnImportCaikeDetail;
    }
}
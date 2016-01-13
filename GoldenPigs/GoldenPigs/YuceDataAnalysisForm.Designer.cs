namespace GoldenPigs
{
    partial class YuceDataAnalysisForm
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
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpTouzhushijian = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdateLucky = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvResult
            // 
            this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Location = new System.Drawing.Point(36, 111);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.Size = new System.Drawing.Size(775, 468);
            this.dgvResult.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(736, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpTouzhushijian
            // 
            this.dtpTouzhushijian.Location = new System.Drawing.Point(105, 26);
            this.dtpTouzhushijian.Name = "dtpTouzhushijian";
            this.dtpTouzhushijian.Size = new System.Drawing.Size(154, 21);
            this.dtpTouzhushijian.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "投注时间：";
            // 
            // btnUpdateLucky
            // 
            this.btnUpdateLucky.Location = new System.Drawing.Point(628, 26);
            this.btnUpdateLucky.Name = "btnUpdateLucky";
            this.btnUpdateLucky.Size = new System.Drawing.Size(88, 23);
            this.btnUpdateLucky.TabIndex = 6;
            this.btnUpdateLucky.Text = "更新中奖信息";
            this.btnUpdateLucky.UseVisualStyleBackColor = true;
            this.btnUpdateLucky.Click += new System.EventHandler(this.btnUpdateLucky_Click);
            // 
            // YuceDataAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 591);
            this.Controls.Add(this.btnUpdateLucky);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpTouzhushijian);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvResult);
            this.Name = "YuceDataAnalysisForm";
            this.Text = "预测数据分析";
            this.Load += new System.EventHandler(this.YuceDataAnalysisForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpTouzhushijian;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdateLucky;
    }
}
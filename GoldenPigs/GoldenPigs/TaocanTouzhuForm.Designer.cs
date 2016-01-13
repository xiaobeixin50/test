namespace GoldenPigs
{
    partial class TaocanTouzhuForm
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
            this.btnTouzhu = new System.Windows.Forms.Button();
            this.dgvTaocan = new System.Windows.Forms.DataGridView();
            this.lbTaocanDetail = new System.Windows.Forms.ListBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpSearchDate = new System.Windows.Forms.DateTimePicker();
            this.txtBeishu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaocan)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTouzhu
            // 
            this.btnTouzhu.Location = new System.Drawing.Point(672, 82);
            this.btnTouzhu.Name = "btnTouzhu";
            this.btnTouzhu.Size = new System.Drawing.Size(75, 23);
            this.btnTouzhu.TabIndex = 0;
            this.btnTouzhu.Text = "投注";
            this.btnTouzhu.UseVisualStyleBackColor = true;
            this.btnTouzhu.Click += new System.EventHandler(this.btnTouzhu_Click);
            // 
            // dgvTaocan
            // 
            this.dgvTaocan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTaocan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaocan.Location = new System.Drawing.Point(65, 112);
            this.dgvTaocan.Name = "dgvTaocan";
            this.dgvTaocan.RowTemplate.Height = 23;
            this.dgvTaocan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaocan.Size = new System.Drawing.Size(682, 147);
            this.dgvTaocan.TabIndex = 1;
            this.dgvTaocan.SelectionChanged += new System.EventHandler(this.dgvTaocan_SelectionChanged);
            // 
            // lbTaocanDetail
            // 
            this.lbTaocanDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTaocanDetail.FormattingEnabled = true;
            this.lbTaocanDetail.ItemHeight = 12;
            this.lbTaocanDetail.Location = new System.Drawing.Point(65, 313);
            this.lbTaocanDetail.Name = "lbTaocanDetail";
            this.lbTaocanDetail.Size = new System.Drawing.Size(682, 136);
            this.lbTaocanDetail.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(672, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpSearchDate
            // 
            this.dtpSearchDate.Location = new System.Drawing.Point(65, 23);
            this.dtpSearchDate.Name = "dtpSearchDate";
            this.dtpSearchDate.Size = new System.Drawing.Size(200, 21);
            this.dtpSearchDate.TabIndex = 4;
            // 
            // txtBeishu
            // 
            this.txtBeishu.Location = new System.Drawing.Point(527, 82);
            this.txtBeishu.Name = "txtBeishu";
            this.txtBeishu.Size = new System.Drawing.Size(100, 21);
            this.txtBeishu.TabIndex = 5;
            this.txtBeishu.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(454, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "倍数：";
            // 
            // TaocanTouzhuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 501);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBeishu);
            this.Controls.Add(this.dtpSearchDate);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lbTaocanDetail);
            this.Controls.Add(this.dgvTaocan);
            this.Controls.Add(this.btnTouzhu);
            this.Name = "TaocanTouzhuForm";
            this.Text = "TaocanTouzhuForm";
            this.Load += new System.EventHandler(this.TaocanTouzhuForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaocan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTouzhu;
        private System.Windows.Forms.DataGridView dgvTaocan;
        private System.Windows.Forms.ListBox lbTaocanDetail;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpSearchDate;
        private System.Windows.Forms.TextBox txtBeishu;
        private System.Windows.Forms.Label label1;
    }
}
namespace GoldenPigs
{
    partial class BaketBallYuce
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
            this.dgvBasketYuce = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnUpdateResult = new System.Windows.Forms.Button();
            this.btnImportResult = new System.Windows.Forms.Button();
            this.btnImportYuce = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBasketYuce)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBasketYuce
            // 
            this.dgvBasketYuce.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBasketYuce.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBasketYuce.Location = new System.Drawing.Point(49, 150);
            this.dgvBasketYuce.Name = "dgvBasketYuce";
            this.dgvBasketYuce.RowTemplate.Height = 23;
            this.dgvBasketYuce.Size = new System.Drawing.Size(752, 301);
            this.dgvBasketYuce.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(552, 37);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询结果";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnUpdateResult
            // 
            this.btnUpdateResult.Location = new System.Drawing.Point(552, 86);
            this.btnUpdateResult.Name = "btnUpdateResult";
            this.btnUpdateResult.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateResult.TabIndex = 2;
            this.btnUpdateResult.Text = "更新结果";
            this.btnUpdateResult.UseVisualStyleBackColor = true;
            this.btnUpdateResult.Click += new System.EventHandler(this.btnUpdateResult_Click);
            // 
            // btnImportResult
            // 
            this.btnImportResult.Location = new System.Drawing.Point(664, 86);
            this.btnImportResult.Name = "btnImportResult";
            this.btnImportResult.Size = new System.Drawing.Size(75, 23);
            this.btnImportResult.TabIndex = 3;
            this.btnImportResult.Text = "导入结果";
            this.btnImportResult.UseVisualStyleBackColor = true;
            this.btnImportResult.Click += new System.EventHandler(this.btnImportResult_Click);
            // 
            // btnImportYuce
            // 
            this.btnImportYuce.Location = new System.Drawing.Point(664, 36);
            this.btnImportYuce.Name = "btnImportYuce";
            this.btnImportYuce.Size = new System.Drawing.Size(75, 23);
            this.btnImportYuce.TabIndex = 4;
            this.btnImportYuce.Text = "导入预测";
            this.btnImportYuce.UseVisualStyleBackColor = true;
            this.btnImportYuce.Click += new System.EventHandler(this.btnImportYuce_Click);
            // 
            // BaketBallYuce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 479);
            this.Controls.Add(this.btnImportYuce);
            this.Controls.Add(this.btnImportResult);
            this.Controls.Add(this.btnUpdateResult);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvBasketYuce);
            this.Name = "BaketBallYuce";
            this.Text = "篮球预测功能";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBasketYuce)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBasketYuce;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnUpdateResult;
        private System.Windows.Forms.Button btnImportResult;
        private System.Windows.Forms.Button btnImportYuce;
    }
}
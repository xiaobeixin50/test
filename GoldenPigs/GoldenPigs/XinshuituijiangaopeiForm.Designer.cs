namespace GoldenPigs
{
    partial class XinshuituijiangaopeiForm
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
            this.dgvXinshui = new System.Windows.Forms.DataGridView();
            this.btnImportGaopei = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXinshui)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvXinshui
            // 
            this.dgvXinshui.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvXinshui.Location = new System.Drawing.Point(36, 135);
            this.dgvXinshui.Name = "dgvXinshui";
            this.dgvXinshui.RowTemplate.Height = 23;
            this.dgvXinshui.Size = new System.Drawing.Size(935, 417);
            this.dgvXinshui.TabIndex = 0;
            // 
            // btnImportGaopei
            // 
            this.btnImportGaopei.Location = new System.Drawing.Point(878, 92);
            this.btnImportGaopei.Name = "btnImportGaopei";
            this.btnImportGaopei.Size = new System.Drawing.Size(93, 23);
            this.btnImportGaopei.TabIndex = 1;
            this.btnImportGaopei.Text = "导入高赔数据";
            this.btnImportGaopei.UseVisualStyleBackColor = true;
            this.btnImportGaopei.Click += new System.EventHandler(this.btnImportGaopei_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(878, 47);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(93, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // XinshuituijiangaopeiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 614);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnImportGaopei);
            this.Controls.Add(this.dgvXinshui);
            this.Name = "XinshuituijiangaopeiForm";
            this.Text = "心水推荐单选高赔";
            ((System.ComponentModel.ISupportInitialize)(this.dgvXinshui)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvXinshui;
        private System.Windows.Forms.Button btnImportGaopei;
        private System.Windows.Forms.Button btnSearch;
    }
}
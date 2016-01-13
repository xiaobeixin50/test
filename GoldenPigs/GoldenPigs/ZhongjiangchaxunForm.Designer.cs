namespace GoldenPigs
{
    partial class ZhongjiangchaxunForm
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
            this.dtpTouzhuDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.dgTouzhuSpf = new System.Windows.Forms.DataGridView();
            this.btnPaijiang = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgTouzhuSpf)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTouzhuDate
            // 
            this.dtpTouzhuDate.Location = new System.Drawing.Point(181, 72);
            this.dtpTouzhuDate.Name = "dtpTouzhuDate";
            this.dtpTouzhuDate.Size = new System.Drawing.Size(200, 21);
            this.dtpTouzhuDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "时间:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(834, 78);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(669, 78);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(96, 23);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "更新中奖结果";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // dgTouzhuSpf
            // 
            this.dgTouzhuSpf.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTouzhuSpf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTouzhuSpf.Location = new System.Drawing.Point(26, 147);
            this.dgTouzhuSpf.Name = "dgTouzhuSpf";
            this.dgTouzhuSpf.RowTemplate.Height = 23;
            this.dgTouzhuSpf.Size = new System.Drawing.Size(1031, 418);
            this.dgTouzhuSpf.TabIndex = 4;
            this.dgTouzhuSpf.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgTouzhuSpf_CellPainting);
            // 
            // btnPaijiang
            // 
            this.btnPaijiang.Location = new System.Drawing.Point(968, 78);
            this.btnPaijiang.Name = "btnPaijiang";
            this.btnPaijiang.Size = new System.Drawing.Size(75, 23);
            this.btnPaijiang.TabIndex = 5;
            this.btnPaijiang.Text = "派奖";
            this.btnPaijiang.UseVisualStyleBackColor = true;
            this.btnPaijiang.Click += new System.EventHandler(this.btnPaijiang_Click);
            // 
            // ZhongjiangchaxunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 594);
            this.Controls.Add(this.btnPaijiang);
            this.Controls.Add(this.dgTouzhuSpf);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpTouzhuDate);
            this.Name = "ZhongjiangchaxunForm";
            this.Text = "投注中奖查询";
            this.Load += new System.EventHandler(this.ZhongjiangchaxunForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTouzhuSpf)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpTouzhuDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView dgTouzhuSpf;
        private System.Windows.Forms.Button btnPaijiang;
    }
}
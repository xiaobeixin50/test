namespace GoldenPigs
{
    partial class DataAnalysisForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.btnUpdateTiaozheng = new System.Windows.Forms.Button();
            this.dgvTaocanDetail = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnUpdateBianhao = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaocanDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(157, 72);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(114, 21);
            this.dtpStartDate.TabIndex = 0;
            // 
            // btnUpdateTiaozheng
            // 
            this.btnUpdateTiaozheng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateTiaozheng.Location = new System.Drawing.Point(822, 73);
            this.btnUpdateTiaozheng.Name = "btnUpdateTiaozheng";
            this.btnUpdateTiaozheng.Size = new System.Drawing.Size(131, 23);
            this.btnUpdateTiaozheng.TabIndex = 1;
            this.btnUpdateTiaozheng.Text = "更新调整球数据";
            this.btnUpdateTiaozheng.UseVisualStyleBackColor = true;
            this.btnUpdateTiaozheng.Click += new System.EventHandler(this.btnUpdateTiaozheng_Click);
            // 
            // dgvTaocanDetail
            // 
            this.dgvTaocanDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaocanDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTaocanDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTaocanDetail.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTaocanDetail.Location = new System.Drawing.Point(88, 126);
            this.dgvTaocanDetail.Name = "dgvTaocanDetail";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaocanDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTaocanDetail.RowTemplate.Height = 23;
            this.dgvTaocanDetail.Size = new System.Drawing.Size(865, 342);
            this.dgvTaocanDetail.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "开始日期：";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(578, 73);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(99, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnUpdateBianhao
            // 
            this.btnUpdateBianhao.Location = new System.Drawing.Point(698, 73);
            this.btnUpdateBianhao.Name = "btnUpdateBianhao";
            this.btnUpdateBianhao.Size = new System.Drawing.Size(100, 23);
            this.btnUpdateBianhao.TabIndex = 5;
            this.btnUpdateBianhao.Text = "更新编号";
            this.btnUpdateBianhao.UseVisualStyleBackColor = true;
            this.btnUpdateBianhao.Click += new System.EventHandler(this.btnUpdateBianhao_Click);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(400, 72);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(117, 21);
            this.dtpEndDate.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(314, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "结束日期：";
            // 
            // DataAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 507);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.btnUpdateBianhao);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTaocanDetail);
            this.Controls.Add(this.btnUpdateTiaozheng);
            this.Controls.Add(this.dtpStartDate);
            this.Name = "DataAnalysisForm";
            this.Text = "套餐数据分析";
            this.Load += new System.EventHandler(this.DataAnalysisForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaocanDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Button btnUpdateTiaozheng;
        private System.Windows.Forms.DataGridView dgvTaocanDetail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnUpdateBianhao;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label2;
    }
}
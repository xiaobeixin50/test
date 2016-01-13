namespace GoldenPigs
{
    partial class YucePeilvAnalysisForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnRandomTouzhu = new System.Windows.Forms.Button();
            this.lblTouzhuMsg = new System.Windows.Forms.Label();
            this.btnTouzhu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "投注时间：";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(131, 51);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(133, 21);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(760, 52);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(51, 135);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(801, 398);
            this.dataGridView1.TabIndex = 3;
            // 
            // btnRandomTouzhu
            // 
            this.btnRandomTouzhu.Location = new System.Drawing.Point(538, 52);
            this.btnRandomTouzhu.Name = "btnRandomTouzhu";
            this.btnRandomTouzhu.Size = new System.Drawing.Size(75, 23);
            this.btnRandomTouzhu.TabIndex = 4;
            this.btnRandomTouzhu.Text = "随机选择";
            this.btnRandomTouzhu.UseVisualStyleBackColor = true;
            this.btnRandomTouzhu.Click += new System.EventHandler(this.btnRandomTouzhu_Click);
            // 
            // lblTouzhuMsg
            // 
            this.lblTouzhuMsg.AutoSize = true;
            this.lblTouzhuMsg.Location = new System.Drawing.Point(73, 95);
            this.lblTouzhuMsg.Name = "lblTouzhuMsg";
            this.lblTouzhuMsg.Size = new System.Drawing.Size(89, 12);
            this.lblTouzhuMsg.TabIndex = 5;
            this.lblTouzhuMsg.Text = "随机投注信息：";
            // 
            // btnTouzhu
            // 
            this.btnTouzhu.Location = new System.Drawing.Point(655, 52);
            this.btnTouzhu.Name = "btnTouzhu";
            this.btnTouzhu.Size = new System.Drawing.Size(75, 23);
            this.btnTouzhu.TabIndex = 6;
            this.btnTouzhu.Text = "投注";
            this.btnTouzhu.UseVisualStyleBackColor = true;
            this.btnTouzhu.Click += new System.EventHandler(this.btnTouzhu_Click);
            // 
            // YucePeilvAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 557);
            this.Controls.Add(this.btnTouzhu);
            this.Controls.Add(this.lblTouzhuMsg);
            this.Controls.Add(this.btnRandomTouzhu);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Name = "YucePeilvAnalysisForm";
            this.Text = "预测赔率分析";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRandomTouzhu;
        private System.Windows.Forms.Label lblTouzhuMsg;
        private System.Windows.Forms.Button btnTouzhu;
    }
}
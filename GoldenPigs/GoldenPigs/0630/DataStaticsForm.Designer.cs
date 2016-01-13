namespace GoldenPigs._0630
{
    partial class DataStaticsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCalcShouyiDaily = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnCalcShouyiMatch = new System.Windows.Forms.Button();
            this.btnCalcBifa = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCalcShouyiDaily
            // 
            this.btnCalcShouyiDaily.Location = new System.Drawing.Point(741, 26);
            this.btnCalcShouyiDaily.Name = "btnCalcShouyiDaily";
            this.btnCalcShouyiDaily.Size = new System.Drawing.Size(123, 23);
            this.btnCalcShouyiDaily.TabIndex = 0;
            this.btnCalcShouyiDaily.Text = "计算每天的收益";
            this.btnCalcShouyiDaily.UseVisualStyleBackColor = true;
            this.btnCalcShouyiDaily.Click += new System.EventHandler(this.btnCalcShouyiDaily_Click);
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(71, 125);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(793, 361);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnCalcShouyiMatch
            // 
            this.btnCalcShouyiMatch.Location = new System.Drawing.Point(741, 66);
            this.btnCalcShouyiMatch.Name = "btnCalcShouyiMatch";
            this.btnCalcShouyiMatch.Size = new System.Drawing.Size(123, 23);
            this.btnCalcShouyiMatch.TabIndex = 2;
            this.btnCalcShouyiMatch.Text = "计算每场比赛的收益";
            this.btnCalcShouyiMatch.UseVisualStyleBackColor = true;
            this.btnCalcShouyiMatch.Click += new System.EventHandler(this.btnCalcShouyiMatch_Click);
            // 
            // btnCalcBifa
            // 
            this.btnCalcBifa.Location = new System.Drawing.Point(592, 66);
            this.btnCalcBifa.Name = "btnCalcBifa";
            this.btnCalcBifa.Size = new System.Drawing.Size(106, 23);
            this.btnCalcBifa.TabIndex = 3;
            this.btnCalcBifa.Text = "计算必发的收益";
            this.btnCalcBifa.UseVisualStyleBackColor = true;
            this.btnCalcBifa.Click += new System.EventHandler(this.btnCalcBifa_Click);
            // 
            // DataStaticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 566);
            this.Controls.Add(this.btnCalcBifa);
            this.Controls.Add(this.btnCalcShouyiMatch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnCalcShouyiDaily);
            this.Name = "DataStaticsForm";
            this.Text = "数据统计";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCalcShouyiDaily;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCalcShouyiMatch;
        private System.Windows.Forms.Button btnCalcBifa;
    }
}
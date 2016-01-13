namespace GoldenPigs._0630
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
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.btnYuceSuccess = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnShougailv = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(72, 93);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(714, 199);
            this.zedGraphControl1.TabIndex = 0;
            // 
            // btnYuceSuccess
            // 
            this.btnYuceSuccess.Location = new System.Drawing.Point(72, 37);
            this.btnYuceSuccess.Name = "btnYuceSuccess";
            this.btnYuceSuccess.Size = new System.Drawing.Size(115, 23);
            this.btnYuceSuccess.TabIndex = 1;
            this.btnYuceSuccess.Text = "预测成功率统计";
            this.btnYuceSuccess.UseVisualStyleBackColor = true;
            this.btnYuceSuccess.Click += new System.EventHandler(this.btnYuceSuccess_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(72, 327);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(714, 193);
            this.dataGridView1.TabIndex = 2;
            // 
            // btnShougailv
            // 
            this.btnShougailv.Location = new System.Drawing.Point(233, 37);
            this.btnShougailv.Name = "btnShougailv";
            this.btnShougailv.Size = new System.Drawing.Size(106, 23);
            this.btnShougailv.TabIndex = 3;
            this.btnShougailv.Text = "首概率统计";
            this.btnShougailv.UseVisualStyleBackColor = true;
            this.btnShougailv.Click += new System.EventHandler(this.btnShougailv_Click);
            // 
            // DataAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 566);
            this.Controls.Add(this.btnShougailv);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnYuceSuccess);
            this.Controls.Add(this.zedGraphControl1);
            this.Name = "DataAnalysisForm";
            this.Text = "数据分析界面";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Button btnYuceSuccess;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnShougailv;
    }
}
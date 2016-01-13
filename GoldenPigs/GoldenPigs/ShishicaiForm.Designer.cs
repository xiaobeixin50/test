namespace GoldenPigs
{
    partial class ShishicaiForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnTouzhu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(46, 174);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(739, 343);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnTouzhu
            // 
            this.btnTouzhu.Location = new System.Drawing.Point(710, 34);
            this.btnTouzhu.Name = "btnTouzhu";
            this.btnTouzhu.Size = new System.Drawing.Size(75, 23);
            this.btnTouzhu.TabIndex = 1;
            this.btnTouzhu.Text = "投注";
            this.btnTouzhu.UseVisualStyleBackColor = true;
            this.btnTouzhu.Click += new System.EventHandler(this.btnTouzhu_Click);
            // 
            // ShishicaiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 581);
            this.Controls.Add(this.btnTouzhu);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ShishicaiForm";
            this.Text = "时时彩投注";
            this.Load += new System.EventHandler(this.ShishicaiForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnTouzhu;
    }
}
namespace GoldenPigs._0630
{
    partial class MatchSuggestForm
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
            this.btnSuperbifaSuggest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(52, 117);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(700, 352);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnSuperbifaSuggest
            // 
            this.btnSuperbifaSuggest.Location = new System.Drawing.Point(637, 22);
            this.btnSuperbifaSuggest.Name = "btnSuperbifaSuggest";
            this.btnSuperbifaSuggest.Size = new System.Drawing.Size(115, 23);
            this.btnSuperbifaSuggest.TabIndex = 1;
            this.btnSuperbifaSuggest.Text = "必发指数选择比赛";
            this.btnSuperbifaSuggest.UseVisualStyleBackColor = true;
            this.btnSuperbifaSuggest.Click += new System.EventHandler(this.btnSuperbifaSuggest_Click);
            // 
            // MatchSuggestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 527);
            this.Controls.Add(this.btnSuperbifaSuggest);
            this.Controls.Add(this.dataGridView1);
            this.Name = "MatchSuggestForm";
            this.Text = "比赛推荐界面";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSuperbifaSuggest;
    }
}
namespace GoldenPigs
{
    partial class DanchangCelveForm
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
            this.btnAddControls = new System.Windows.Forms.Button();
            this.btnCalcResult = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comCelve = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBenqian = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddControls
            // 
            this.btnAddControls.Location = new System.Drawing.Point(39, 46);
            this.btnAddControls.Name = "btnAddControls";
            this.btnAddControls.Size = new System.Drawing.Size(75, 23);
            this.btnAddControls.TabIndex = 0;
            this.btnAddControls.Text = "添加";
            this.btnAddControls.UseVisualStyleBackColor = true;
            this.btnAddControls.Click += new System.EventHandler(this.btnAddControls_Click);
            // 
            // btnCalcResult
            // 
            this.btnCalcResult.Location = new System.Drawing.Point(153, 47);
            this.btnCalcResult.Name = "btnCalcResult";
            this.btnCalcResult.Size = new System.Drawing.Size(75, 23);
            this.btnCalcResult.TabIndex = 1;
            this.btnCalcResult.Text = "计算";
            this.btnCalcResult.UseVisualStyleBackColor = true;
            this.btnCalcResult.Click += new System.EventHandler(this.btnCalcResult_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(844, 49);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(100, 21);
            this.txtResult.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(767, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "最终收益";
            // 
            // comCelve
            // 
            this.comCelve.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comCelve.FormattingEnabled = true;
            this.comCelve.Items.AddRange(new object[] {
            "不投首赔",
            "不投中赔",
            "不投末赔"});
            this.comCelve.Location = new System.Drawing.Point(612, 49);
            this.comCelve.Name = "comCelve";
            this.comCelve.Size = new System.Drawing.Size(121, 20);
            this.comCelve.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "本钱";
            // 
            // txtBenqian
            // 
            this.txtBenqian.Location = new System.Drawing.Point(437, 49);
            this.txtBenqian.Name = "txtBenqian";
            this.txtBenqian.Size = new System.Drawing.Size(100, 21);
            this.txtBenqian.TabIndex = 6;
            this.txtBenqian.Text = "200";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(565, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "策略";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(268, 47);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // DanchangCelveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 624);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBenqian);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comCelve);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnCalcResult);
            this.Controls.Add(this.btnAddControls);
            this.Name = "DanchangCelveForm";
            this.Text = "单场投注策略";
            this.Load += new System.EventHandler(this.DanchangCelveForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddControls;
        private System.Windows.Forms.Button btnCalcResult;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comCelve;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBenqian;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClear;
    }
}
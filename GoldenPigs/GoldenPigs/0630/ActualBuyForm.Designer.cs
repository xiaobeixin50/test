namespace GoldenPigs._0630
{
    partial class ActualBuyForm
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
            this.txtBasePeilv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtThresholdPeilv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtAddedPercent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAdditionInfo = new System.Windows.Forms.Label();
            this.btnStrategy12 = new System.Windows.Forms.Button();
            this.btnStrategy14 = new System.Windows.Forms.Button();
            this.btnStrategy16 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGudingPercent = new System.Windows.Forms.TextBox();
            this.btnStrategy17 = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBasePeilv
            // 
            this.txtBasePeilv.Location = new System.Drawing.Point(380, 32);
            this.txtBasePeilv.Name = "txtBasePeilv";
            this.txtBasePeilv.Size = new System.Drawing.Size(100, 21);
            this.txtBasePeilv.TabIndex = 1;
            this.txtBasePeilv.Text = "1.80";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "中心赔率";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(50, 183);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(813, 174);
            this.dataGridView1.TabIndex = 4;
            // 
            // txtThresholdPeilv
            // 
            this.txtThresholdPeilv.Location = new System.Drawing.Point(571, 32);
            this.txtThresholdPeilv.Name = "txtThresholdPeilv";
            this.txtThresholdPeilv.Size = new System.Drawing.Size(100, 21);
            this.txtThresholdPeilv.TabIndex = 5;
            this.txtThresholdPeilv.Text = "1.60";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(512, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "过滤赔率";
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl1.Location = new System.Drawing.Point(50, 383);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(813, 321);
            this.zedGraphControl1.TabIndex = 7;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(48, 114);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(71, 12);
            this.lblResult.TabIndex = 10;
            this.lblResult.Text = "最终收益：0";
            // 
            // txtAddedPercent
            // 
            this.txtAddedPercent.Location = new System.Drawing.Point(119, 80);
            this.txtAddedPercent.Name = "txtAddedPercent";
            this.txtAddedPercent.Size = new System.Drawing.Size(100, 21);
            this.txtAddedPercent.TabIndex = 11;
            this.txtAddedPercent.Text = "0.05";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "追加百分比";
            // 
            // lblAdditionInfo
            // 
            this.lblAdditionInfo.AutoSize = true;
            this.lblAdditionInfo.Location = new System.Drawing.Point(48, 146);
            this.lblAdditionInfo.Name = "lblAdditionInfo";
            this.lblAdditionInfo.Size = new System.Drawing.Size(65, 12);
            this.lblAdditionInfo.TabIndex = 20;
            this.lblAdditionInfo.Text = "附加信息：";
            // 
            // btnStrategy12
            // 
            this.btnStrategy12.Location = new System.Drawing.Point(856, 32);
            this.btnStrategy12.Name = "btnStrategy12";
            this.btnStrategy12.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy12.TabIndex = 21;
            this.btnStrategy12.Text = "回测策略12";
            this.btnStrategy12.UseVisualStyleBackColor = true;
            this.btnStrategy12.Click += new System.EventHandler(this.btnStrategy12_Click);
            // 
            // btnStrategy14
            // 
            this.btnStrategy14.Location = new System.Drawing.Point(744, 32);
            this.btnStrategy14.Name = "btnStrategy14";
            this.btnStrategy14.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy14.TabIndex = 23;
            this.btnStrategy14.Text = "回测策略14";
            this.btnStrategy14.UseVisualStyleBackColor = true;
            this.btnStrategy14.Click += new System.EventHandler(this.btnStrategy14_Click);
            // 
            // btnStrategy16
            // 
            this.btnStrategy16.Location = new System.Drawing.Point(744, 78);
            this.btnStrategy16.Name = "btnStrategy16";
            this.btnStrategy16.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy16.TabIndex = 25;
            this.btnStrategy16.Text = "回测策略16";
            this.btnStrategy16.UseVisualStyleBackColor = true;
            this.btnStrategy16.Click += new System.EventHandler(this.btnStrategy16_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(261, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "固定收益百分比";
            // 
            // txtGudingPercent
            // 
            this.txtGudingPercent.Location = new System.Drawing.Point(380, 80);
            this.txtGudingPercent.Name = "txtGudingPercent";
            this.txtGudingPercent.Size = new System.Drawing.Size(100, 21);
            this.txtGudingPercent.TabIndex = 29;
            this.txtGudingPercent.Text = "0.05";
            // 
            // btnStrategy17
            // 
            this.btnStrategy17.Location = new System.Drawing.Point(856, 80);
            this.btnStrategy17.Name = "btnStrategy17";
            this.btnStrategy17.Size = new System.Drawing.Size(133, 23);
            this.btnStrategy17.TabIndex = 27;
            this.btnStrategy17.Text = "回测策略17（百分比）";
            this.btnStrategy17.UseVisualStyleBackColor = true;
            this.btnStrategy17.Click += new System.EventHandler(this.btnStrategy17_Click);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(119, 26);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(125, 21);
            this.dtpStartDate.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 31;
            this.label5.Text = "开始时间";
            // 
            // ActualBuyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 731);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.txtGudingPercent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnStrategy17);
            this.Controls.Add(this.btnStrategy16);
            this.Controls.Add(this.btnStrategy14);
            this.Controls.Add(this.btnStrategy12);
            this.Controls.Add(this.lblAdditionInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAddedPercent);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtThresholdPeilv);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBasePeilv);
            this.Name = "ActualBuyForm";
            this.Text = "股票收益回测";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBasePeilv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtThresholdPeilv;
        private System.Windows.Forms.Label label2;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox txtAddedPercent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAdditionInfo;
        private System.Windows.Forms.Button btnStrategy12;
        private System.Windows.Forms.Button btnStrategy14;
        private System.Windows.Forms.Button btnStrategy16;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGudingPercent;
        private System.Windows.Forms.Button btnStrategy17;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label5;
    }
}
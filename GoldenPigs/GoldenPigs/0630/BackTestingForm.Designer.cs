namespace GoldenPigs._0630
{
    partial class BackTestingForm
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
            this.btnStrategy1 = new System.Windows.Forms.Button();
            this.txtBasePeilv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStrategy2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtThresholdPeilv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.btnStrategy3 = new System.Windows.Forms.Button();
            this.btnStrategy4 = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtAddedPercent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStrategy5 = new System.Windows.Forms.Button();
            this.btnStrategy6 = new System.Windows.Forms.Button();
            this.btnStrategy7 = new System.Windows.Forms.Button();
            this.btnStrategy8 = new System.Windows.Forms.Button();
            this.btnStrategy9 = new System.Windows.Forms.Button();
            this.btnStrategy10 = new System.Windows.Forms.Button();
            this.btnStrategy11 = new System.Windows.Forms.Button();
            this.lblAdditionInfo = new System.Windows.Forms.Label();
            this.btnStrategy12 = new System.Windows.Forms.Button();
            this.btnStrategy13 = new System.Windows.Forms.Button();
            this.btnStrategy14 = new System.Windows.Forms.Button();
            this.btnStrategy15 = new System.Windows.Forms.Button();
            this.btnStrategy16 = new System.Windows.Forms.Button();
            this.btnStrategy12Opt = new System.Windows.Forms.Button();
            this.btnStrategy17 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGudingPercent = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStrategy1
            // 
            this.btnStrategy1.Location = new System.Drawing.Point(907, 280);
            this.btnStrategy1.Name = "btnStrategy1";
            this.btnStrategy1.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy1.TabIndex = 0;
            this.btnStrategy1.Text = "回测策略1";
            this.btnStrategy1.UseVisualStyleBackColor = true;
            this.btnStrategy1.Click += new System.EventHandler(this.btnStrategy1_Click);
            // 
            // txtBasePeilv
            // 
            this.txtBasePeilv.Location = new System.Drawing.Point(129, 38);
            this.txtBasePeilv.Name = "txtBasePeilv";
            this.txtBasePeilv.Size = new System.Drawing.Size(100, 21);
            this.txtBasePeilv.TabIndex = 1;
            this.txtBasePeilv.Text = "1.80";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "中心赔率";
            // 
            // btnStrategy2
            // 
            this.btnStrategy2.Location = new System.Drawing.Point(907, 484);
            this.btnStrategy2.Name = "btnStrategy2";
            this.btnStrategy2.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy2.TabIndex = 3;
            this.btnStrategy2.Text = "废弃策略2";
            this.btnStrategy2.UseVisualStyleBackColor = true;
            this.btnStrategy2.Click += new System.EventHandler(this.btnStrategy2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(50, 140);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(813, 217);
            this.dataGridView1.TabIndex = 4;
            // 
            // txtThresholdPeilv
            // 
            this.txtThresholdPeilv.Location = new System.Drawing.Point(320, 38);
            this.txtThresholdPeilv.Name = "txtThresholdPeilv";
            this.txtThresholdPeilv.Size = new System.Drawing.Size(100, 21);
            this.txtThresholdPeilv.TabIndex = 5;
            this.txtThresholdPeilv.Text = "1.60";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(261, 41);
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
            // btnStrategy3
            // 
            this.btnStrategy3.Location = new System.Drawing.Point(907, 396);
            this.btnStrategy3.Name = "btnStrategy3";
            this.btnStrategy3.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy3.TabIndex = 8;
            this.btnStrategy3.Text = "回测策略3";
            this.btnStrategy3.UseVisualStyleBackColor = true;
            this.btnStrategy3.Click += new System.EventHandler(this.btnStrategy3_Click);
            // 
            // btnStrategy4
            // 
            this.btnStrategy4.Location = new System.Drawing.Point(907, 338);
            this.btnStrategy4.Name = "btnStrategy4";
            this.btnStrategy4.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy4.TabIndex = 9;
            this.btnStrategy4.Text = "回测策略4";
            this.btnStrategy4.UseVisualStyleBackColor = true;
            this.btnStrategy4.Click += new System.EventHandler(this.btnStrategy4_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(48, 78);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(71, 12);
            this.lblResult.TabIndex = 10;
            this.lblResult.Text = "最终收益：0";
            // 
            // txtAddedPercent
            // 
            this.txtAddedPercent.Location = new System.Drawing.Point(520, 38);
            this.txtAddedPercent.Name = "txtAddedPercent";
            this.txtAddedPercent.Size = new System.Drawing.Size(100, 21);
            this.txtAddedPercent.TabIndex = 11;
            this.txtAddedPercent.Text = "0.10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(449, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "追加百分比";
            // 
            // btnStrategy5
            // 
            this.btnStrategy5.Location = new System.Drawing.Point(907, 309);
            this.btnStrategy5.Name = "btnStrategy5";
            this.btnStrategy5.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy5.TabIndex = 13;
            this.btnStrategy5.Text = "回测策略5";
            this.btnStrategy5.UseVisualStyleBackColor = true;
            this.btnStrategy5.Click += new System.EventHandler(this.btnStrategy5_Click);
            // 
            // btnStrategy6
            // 
            this.btnStrategy6.Location = new System.Drawing.Point(907, 67);
            this.btnStrategy6.Name = "btnStrategy6";
            this.btnStrategy6.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy6.TabIndex = 14;
            this.btnStrategy6.Text = "回测策略6";
            this.btnStrategy6.UseVisualStyleBackColor = true;
            this.btnStrategy6.Click += new System.EventHandler(this.btnStrategy6_Click);
            // 
            // btnStrategy7
            // 
            this.btnStrategy7.Location = new System.Drawing.Point(907, 441);
            this.btnStrategy7.Name = "btnStrategy7";
            this.btnStrategy7.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy7.TabIndex = 15;
            this.btnStrategy7.Text = "回测策略7";
            this.btnStrategy7.UseVisualStyleBackColor = true;
            this.btnStrategy7.Click += new System.EventHandler(this.btnStrategy7_Click);
            // 
            // btnStrategy8
            // 
            this.btnStrategy8.Location = new System.Drawing.Point(907, 367);
            this.btnStrategy8.Name = "btnStrategy8";
            this.btnStrategy8.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy8.TabIndex = 16;
            this.btnStrategy8.Text = "回测策略8";
            this.btnStrategy8.UseVisualStyleBackColor = true;
            this.btnStrategy8.Click += new System.EventHandler(this.btnStrategy8_Click);
            // 
            // btnStrategy9
            // 
            this.btnStrategy9.Location = new System.Drawing.Point(907, 110);
            this.btnStrategy9.Name = "btnStrategy9";
            this.btnStrategy9.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy9.TabIndex = 17;
            this.btnStrategy9.Text = "回测策略9";
            this.btnStrategy9.UseVisualStyleBackColor = true;
            this.btnStrategy9.Click += new System.EventHandler(this.btnStrategy9_Click);
            // 
            // btnStrategy10
            // 
            this.btnStrategy10.Location = new System.Drawing.Point(907, 153);
            this.btnStrategy10.Name = "btnStrategy10";
            this.btnStrategy10.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy10.TabIndex = 18;
            this.btnStrategy10.Text = "回测策略10";
            this.btnStrategy10.UseVisualStyleBackColor = true;
            this.btnStrategy10.Click += new System.EventHandler(this.btnStrategy10_Click);
            // 
            // btnStrategy11
            // 
            this.btnStrategy11.Location = new System.Drawing.Point(907, 192);
            this.btnStrategy11.Name = "btnStrategy11";
            this.btnStrategy11.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy11.TabIndex = 19;
            this.btnStrategy11.Text = "回测策略11";
            this.btnStrategy11.UseVisualStyleBackColor = true;
            this.btnStrategy11.Click += new System.EventHandler(this.btnStrategy11_Click);
            // 
            // lblAdditionInfo
            // 
            this.lblAdditionInfo.AutoSize = true;
            this.lblAdditionInfo.Location = new System.Drawing.Point(48, 110);
            this.lblAdditionInfo.Name = "lblAdditionInfo";
            this.lblAdditionInfo.Size = new System.Drawing.Size(65, 12);
            this.lblAdditionInfo.TabIndex = 20;
            this.lblAdditionInfo.Text = "附加信息：";
            // 
            // btnStrategy12
            // 
            this.btnStrategy12.Location = new System.Drawing.Point(788, 30);
            this.btnStrategy12.Name = "btnStrategy12";
            this.btnStrategy12.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy12.TabIndex = 21;
            this.btnStrategy12.Text = "回测策略12";
            this.btnStrategy12.UseVisualStyleBackColor = true;
            this.btnStrategy12.Click += new System.EventHandler(this.btnStrategy12_Click);
            // 
            // btnStrategy13
            // 
            this.btnStrategy13.Location = new System.Drawing.Point(907, 238);
            this.btnStrategy13.Name = "btnStrategy13";
            this.btnStrategy13.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy13.TabIndex = 22;
            this.btnStrategy13.Text = "回测策略13";
            this.btnStrategy13.UseVisualStyleBackColor = true;
            this.btnStrategy13.Click += new System.EventHandler(this.btnStrategy13_Click);
            // 
            // btnStrategy14
            // 
            this.btnStrategy14.Location = new System.Drawing.Point(672, 30);
            this.btnStrategy14.Name = "btnStrategy14";
            this.btnStrategy14.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy14.TabIndex = 23;
            this.btnStrategy14.Text = "回测策略14";
            this.btnStrategy14.UseVisualStyleBackColor = true;
            this.btnStrategy14.Click += new System.EventHandler(this.btnStrategy14_Click);
            // 
            // btnStrategy15
            // 
            this.btnStrategy15.Location = new System.Drawing.Point(907, 30);
            this.btnStrategy15.Name = "btnStrategy15";
            this.btnStrategy15.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy15.TabIndex = 24;
            this.btnStrategy15.Text = "回测测率15";
            this.btnStrategy15.UseVisualStyleBackColor = true;
            this.btnStrategy15.Click += new System.EventHandler(this.btnStrategy15_Click);
            // 
            // btnStrategy16
            // 
            this.btnStrategy16.Location = new System.Drawing.Point(672, 67);
            this.btnStrategy16.Name = "btnStrategy16";
            this.btnStrategy16.Size = new System.Drawing.Size(75, 23);
            this.btnStrategy16.TabIndex = 25;
            this.btnStrategy16.Text = "回测策略16";
            this.btnStrategy16.UseVisualStyleBackColor = true;
            this.btnStrategy16.Click += new System.EventHandler(this.btnStrategy16_Click);
            // 
            // btnStrategy12Opt
            // 
            this.btnStrategy12Opt.Location = new System.Drawing.Point(644, 105);
            this.btnStrategy12Opt.Name = "btnStrategy12Opt";
            this.btnStrategy12Opt.Size = new System.Drawing.Size(103, 23);
            this.btnStrategy12Opt.TabIndex = 26;
            this.btnStrategy12Opt.Text = "策略12参数优化";
            this.btnStrategy12Opt.UseVisualStyleBackColor = true;
            // 
            // btnStrategy17
            // 
            this.btnStrategy17.Location = new System.Drawing.Point(768, 67);
            this.btnStrategy17.Name = "btnStrategy17";
            this.btnStrategy17.Size = new System.Drawing.Size(133, 23);
            this.btnStrategy17.TabIndex = 27;
            this.btnStrategy17.Text = "回测策略17（百分比）";
            this.btnStrategy17.UseVisualStyleBackColor = true;
            this.btnStrategy17.Click += new System.EventHandler(this.btnStrategy17_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(425, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "固定收益百分比";
            // 
            // txtGudingPercent
            // 
            this.txtGudingPercent.Location = new System.Drawing.Point(520, 6);
            this.txtGudingPercent.Name = "txtGudingPercent";
            this.txtGudingPercent.Size = new System.Drawing.Size(100, 21);
            this.txtGudingPercent.TabIndex = 29;
            this.txtGudingPercent.Text = "0.10";
            // 
            // BackTestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 731);
            this.Controls.Add(this.txtGudingPercent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnStrategy17);
            this.Controls.Add(this.btnStrategy12Opt);
            this.Controls.Add(this.btnStrategy16);
            this.Controls.Add(this.btnStrategy15);
            this.Controls.Add(this.btnStrategy14);
            this.Controls.Add(this.btnStrategy13);
            this.Controls.Add(this.btnStrategy12);
            this.Controls.Add(this.lblAdditionInfo);
            this.Controls.Add(this.btnStrategy11);
            this.Controls.Add(this.btnStrategy10);
            this.Controls.Add(this.btnStrategy9);
            this.Controls.Add(this.btnStrategy8);
            this.Controls.Add(this.btnStrategy7);
            this.Controls.Add(this.btnStrategy6);
            this.Controls.Add(this.btnStrategy5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAddedPercent);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnStrategy4);
            this.Controls.Add(this.btnStrategy3);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtThresholdPeilv);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnStrategy2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBasePeilv);
            this.Controls.Add(this.btnStrategy1);
            this.Name = "BackTestingForm";
            this.Text = "股票收益回测";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStrategy1;
        private System.Windows.Forms.TextBox txtBasePeilv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStrategy2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtThresholdPeilv;
        private System.Windows.Forms.Label label2;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Button btnStrategy3;
        private System.Windows.Forms.Button btnStrategy4;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TextBox txtAddedPercent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStrategy5;
        private System.Windows.Forms.Button btnStrategy6;
        private System.Windows.Forms.Button btnStrategy7;
        private System.Windows.Forms.Button btnStrategy8;
        private System.Windows.Forms.Button btnStrategy9;
        private System.Windows.Forms.Button btnStrategy10;
        private System.Windows.Forms.Button btnStrategy11;
        private System.Windows.Forms.Label lblAdditionInfo;
        private System.Windows.Forms.Button btnStrategy12;
        private System.Windows.Forms.Button btnStrategy13;
        private System.Windows.Forms.Button btnStrategy14;
        private System.Windows.Forms.Button btnStrategy15;
        private System.Windows.Forms.Button btnStrategy16;
        private System.Windows.Forms.Button btnStrategy12Opt;
        private System.Windows.Forms.Button btnStrategy17;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGudingPercent;
    }
}
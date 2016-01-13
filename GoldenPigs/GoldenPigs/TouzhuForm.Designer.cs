namespace GoldenPigs
{
    partial class TouzhuForm
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
            this.btnTouzhu = new System.Windows.Forms.Button();
            this.txtBeishu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clbChuanJiang = new System.Windows.Forms.CheckedListBox();
            this.dgPeilv = new System.Windows.Forms.DataGridView();
            this.dtpRiqi = new System.Windows.Forms.DateTimePicker();
            this.cbLiansai = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lbSelectedMatch = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTouru = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMinJiangjin = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblMaxJiangjin = new System.Windows.Forms.Label();
            this.btnOptimize = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgPeilv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTouzhu
            // 
            this.btnTouzhu.Location = new System.Drawing.Point(887, 93);
            this.btnTouzhu.Name = "btnTouzhu";
            this.btnTouzhu.Size = new System.Drawing.Size(100, 23);
            this.btnTouzhu.TabIndex = 1;
            this.btnTouzhu.Text = "投注";
            this.btnTouzhu.UseVisualStyleBackColor = true;
            this.btnTouzhu.Click += new System.EventHandler(this.btnTouzhu_Click);
            // 
            // txtBeishu
            // 
            this.txtBeishu.Location = new System.Drawing.Point(131, 95);
            this.txtBeishu.Name = "txtBeishu";
            this.txtBeishu.Size = new System.Drawing.Size(100, 21);
            this.txtBeishu.TabIndex = 2;
            this.txtBeishu.Text = "100";
            this.txtBeishu.TextChanged += new System.EventHandler(this.txtBeishu_TextChanged);
            this.txtBeishu.Enter += new System.EventHandler(this.txtBeishu_Enter);
            this.txtBeishu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBeishu_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "倍数:";
            // 
            // clbChuanJiang
            // 
            this.clbChuanJiang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbChuanJiang.CheckOnClick = true;
            this.clbChuanJiang.FormattingEnabled = true;
            this.clbChuanJiang.Location = new System.Drawing.Point(881, 428);
            this.clbChuanJiang.Name = "clbChuanJiang";
            this.clbChuanJiang.Size = new System.Drawing.Size(191, 148);
            this.clbChuanJiang.TabIndex = 4;
            this.clbChuanJiang.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbChuanJiang_ItemCheck);
            // 
            // dgPeilv
            // 
            this.dgPeilv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPeilv.Location = new System.Drawing.Point(12, 143);
            this.dgPeilv.Name = "dgPeilv";
            this.dgPeilv.ReadOnly = true;
            this.dgPeilv.RowTemplate.Height = 23;
            this.dgPeilv.Size = new System.Drawing.Size(851, 433);
            this.dgPeilv.TabIndex = 5;
            this.dgPeilv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPeilv_CellClick);
            // 
            // dtpRiqi
            // 
            this.dtpRiqi.Location = new System.Drawing.Point(133, 49);
            this.dtpRiqi.Name = "dtpRiqi";
            this.dtpRiqi.Size = new System.Drawing.Size(200, 21);
            this.dtpRiqi.TabIndex = 6;
            // 
            // cbLiansai
            // 
            this.cbLiansai.FormattingEnabled = true;
            this.cbLiansai.Location = new System.Drawing.Point(449, 49);
            this.cbLiansai.Name = "cbLiansai";
            this.cbLiansai.Size = new System.Drawing.Size(121, 20);
            this.cbLiansai.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(389, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "联赛：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "日期：";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(887, 50);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 23);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lbSelectedMatch
            // 
            this.lbSelectedMatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSelectedMatch.FormattingEnabled = true;
            this.lbSelectedMatch.ItemHeight = 12;
            this.lbSelectedMatch.Location = new System.Drawing.Point(881, 143);
            this.lbSelectedMatch.Name = "lbSelectedMatch";
            this.lbSelectedMatch.Size = new System.Drawing.Size(191, 256);
            this.lbSelectedMatch.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(311, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "金额:";
            // 
            // txtTouru
            // 
            this.txtTouru.Location = new System.Drawing.Point(364, 95);
            this.txtTouru.Name = "txtTouru";
            this.txtTouru.Size = new System.Drawing.Size(100, 21);
            this.txtTouru.TabIndex = 13;
            this.txtTouru.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(530, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "单注奖金：";
            // 
            // lblMinJiangjin
            // 
            this.lblMinJiangjin.AutoSize = true;
            this.lblMinJiangjin.Location = new System.Drawing.Point(602, 99);
            this.lblMinJiangjin.Name = "lblMinJiangjin";
            this.lblMinJiangjin.Size = new System.Drawing.Size(11, 12);
            this.lblMinJiangjin.TabIndex = 15;
            this.lblMinJiangjin.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(663, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "~";
            // 
            // lblMaxJiangjin
            // 
            this.lblMaxJiangjin.AutoSize = true;
            this.lblMaxJiangjin.Location = new System.Drawing.Point(722, 99);
            this.lblMaxJiangjin.Name = "lblMaxJiangjin";
            this.lblMaxJiangjin.Size = new System.Drawing.Size(11, 12);
            this.lblMaxJiangjin.TabIndex = 17;
            this.lblMaxJiangjin.Text = "0";
            // 
            // btnOptimize
            // 
            this.btnOptimize.Location = new System.Drawing.Point(1021, 50);
            this.btnOptimize.Name = "btnOptimize";
            this.btnOptimize.Size = new System.Drawing.Size(87, 23);
            this.btnOptimize.TabIndex = 18;
            this.btnOptimize.Text = "优化投注";
            this.btnOptimize.UseVisualStyleBackColor = true;
            this.btnOptimize.Click += new System.EventHandler(this.btnOptimize_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1093, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 32);
            this.button1.TabIndex = 19;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1182, 181);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 32);
            this.button2.TabIndex = 20;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1276, 181);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 32);
            this.button3.TabIndex = 21;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1372, 228);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(40, 70);
            this.button4.TabIndex = 22;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1372, 318);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(40, 76);
            this.button5.TabIndex = 23;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1372, 421);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(40, 77);
            this.button6.TabIndex = 24;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1093, 228);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 70);
            this.button7.TabIndex = 25;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(1182, 228);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(78, 70);
            this.button8.TabIndex = 26;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(1276, 228);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(77, 70);
            this.button9.TabIndex = 27;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(1093, 318);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 67);
            this.button10.TabIndex = 28;
            this.button10.Text = "button10";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(1182, 318);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(78, 67);
            this.button11.TabIndex = 29;
            this.button11.Text = "button11";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(1276, 318);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(77, 67);
            this.button12.TabIndex = 30;
            this.button12.Text = "button12";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(1093, 421);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 77);
            this.button13.TabIndex = 31;
            this.button13.Text = "button13";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(1182, 421);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(78, 77);
            this.button14.TabIndex = 32;
            this.button14.Text = "button14";
            this.button14.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(1279, 421);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(74, 77);
            this.button15.TabIndex = 33;
            this.button15.Text = "button15";
            this.button15.UseVisualStyleBackColor = true;
            // 
            // TouzhuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1419, 588);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOptimize);
            this.Controls.Add(this.lblMaxJiangjin);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblMinJiangjin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTouru);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbSelectedMatch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbLiansai);
            this.Controls.Add(this.dtpRiqi);
            this.Controls.Add(this.dgPeilv);
            this.Controls.Add(this.clbChuanJiang);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBeishu);
            this.Controls.Add(this.btnTouzhu);
            this.Name = "TouzhuForm";
            this.Text = "模拟投注界面";
            this.Load += new System.EventHandler(this.TouzhuForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgPeilv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTouzhu;
        private System.Windows.Forms.TextBox txtBeishu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clbChuanJiang;
        private System.Windows.Forms.DataGridView dgPeilv;
        private System.Windows.Forms.DateTimePicker dtpRiqi;
        private System.Windows.Forms.ComboBox cbLiansai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lbSelectedMatch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTouru;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMinJiangjin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblMaxJiangjin;
        private System.Windows.Forms.Button btnOptimize;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
    }
}
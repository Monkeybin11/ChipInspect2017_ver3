﻿namespace Image_Processing_Test
{
	partial class Resizer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnREmove = new System.Windows.Forms.Button();
            this.btnAllClear = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.nudratio = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudratio)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 14);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(641, 623);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(676, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnREmove
            // 
            this.btnREmove.Location = new System.Drawing.Point(676, 41);
            this.btnREmove.Name = "btnREmove";
            this.btnREmove.Size = new System.Drawing.Size(75, 23);
            this.btnREmove.TabIndex = 2;
            this.btnREmove.Text = "Remove";
            this.btnREmove.UseVisualStyleBackColor = true;
            this.btnREmove.Click += new System.EventHandler(this.btnREmove_Click);
            // 
            // btnAllClear
            // 
            this.btnAllClear.Location = new System.Drawing.Point(667, 158);
            this.btnAllClear.Name = "btnAllClear";
            this.btnAllClear.Size = new System.Drawing.Size(75, 23);
            this.btnAllClear.TabIndex = 3;
            this.btnAllClear.Text = "AllClear";
            this.btnAllClear.UseVisualStyleBackColor = true;
            this.btnAllClear.Click += new System.EventHandler(this.btnAllClear_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(676, 91);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(44, 187);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(100, 20);
            this.maskedTextBox1.TabIndex = 5;
            // 
            // nudratio
            // 
            this.nudratio.DecimalPlaces = 1;
            this.nudratio.Location = new System.Drawing.Point(676, 120);
            this.nudratio.Name = "nudratio";
            this.nudratio.Size = new System.Drawing.Size(66, 20);
            this.nudratio.TabIndex = 6;
            this.nudratio.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // Resizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 649);
            this.Controls.Add(this.nudratio);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnAllClear);
            this.Controls.Add(this.btnREmove);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Resizer";
            this.Text = "Resizer";
            ((System.ComponentModel.ISupportInitialize)(this.nudratio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Button btnREmove;
		private System.Windows.Forms.Button btnAllClear;
		private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.NumericUpDown nudratio;
    }
}
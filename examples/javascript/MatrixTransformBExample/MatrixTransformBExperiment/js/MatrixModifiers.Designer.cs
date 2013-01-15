namespace MatrixTransformBExample.js
{
	partial class MatrixModifiers
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.TranslateY = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.TranslateX = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.M22 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.M12 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.M21 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.M11 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.ButtonClear = new System.Windows.Forms.Button();
			this.Visual1 = new System.Windows.Forms.CheckBox();
			this.Visual2 = new System.Windows.Forms.CheckBox();
			this.Debug1 = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.TranslateY);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.TranslateX);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(18, 15);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(263, 76);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "MatrixOrigin";
			// 
			// TranslateY
			// 
			this.TranslateY.Location = new System.Drawing.Point(28, 43);
			this.TranslateY.Name = "TranslateY";
			this.TranslateY.Size = new System.Drawing.Size(100, 20);
			this.TranslateY.TabIndex = 3;
			this.TranslateY.Text = "0";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(15, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "y:";
			// 
			// TranslateX
			// 
			this.TranslateX.Location = new System.Drawing.Point(28, 17);
			this.TranslateX.Name = "TranslateX";
			this.TranslateX.Size = new System.Drawing.Size(100, 20);
			this.TranslateX.TabIndex = 1;
			this.TranslateX.Text = "0";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(15, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "x:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.M22);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.M12);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.M21);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.M11);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Location = new System.Drawing.Point(18, 121);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(406, 73);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Matrix";
			// 
			// M22
			// 
			this.M22.Location = new System.Drawing.Point(235, 43);
			this.M22.Name = "M22";
			this.M22.Size = new System.Drawing.Size(146, 20);
			this.M22.TabIndex = 7;
			this.M22.Text = "0";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(198, 46);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(31, 13);
			this.label5.TabIndex = 6;
			this.label5.Text = "M22:";
			// 
			// M12
			// 
			this.M12.Location = new System.Drawing.Point(235, 17);
			this.M12.Name = "M12";
			this.M12.Size = new System.Drawing.Size(146, 20);
			this.M12.TabIndex = 5;
			this.M12.Text = "0";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(198, 20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(31, 13);
			this.label6.TabIndex = 4;
			this.label6.Text = "M12:";
			// 
			// M21
			// 
			this.M21.Location = new System.Drawing.Point(44, 43);
			this.M21.Name = "M21";
			this.M21.Size = new System.Drawing.Size(146, 20);
			this.M21.TabIndex = 3;
			this.M21.Text = "0";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 46);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "M21:";
			// 
			// M11
			// 
			this.M11.Location = new System.Drawing.Point(44, 17);
			this.M11.Name = "M11";
			this.M11.Size = new System.Drawing.Size(146, 20);
			this.M11.TabIndex = 1;
			this.M11.Text = "0";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 20);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(31, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "M11:";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(360, 15);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Apply";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// ButtonClear
			// 
			this.ButtonClear.Location = new System.Drawing.Point(360, 44);
			this.ButtonClear.Name = "ButtonClear";
			this.ButtonClear.Size = new System.Drawing.Size(75, 23);
			this.ButtonClear.TabIndex = 3;
			this.ButtonClear.Text = "Clear";
			this.ButtonClear.UseVisualStyleBackColor = true;
			// 
			// Visual1
			// 
			this.Visual1.AutoSize = true;
			this.Visual1.Checked = true;
			this.Visual1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.Visual1.Location = new System.Drawing.Point(28, 201);
			this.Visual1.Name = "Visual1";
			this.Visual1.Size = new System.Drawing.Size(60, 17);
			this.Visual1.TabIndex = 4;
			this.Visual1.Text = "Visual1";
			this.Visual1.UseVisualStyleBackColor = true;
			// 
			// Visual2
			// 
			this.Visual2.AutoSize = true;
			this.Visual2.Checked = true;
			this.Visual2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.Visual2.Location = new System.Drawing.Point(28, 224);
			this.Visual2.Name = "Visual2";
			this.Visual2.Size = new System.Drawing.Size(60, 17);
			this.Visual2.TabIndex = 4;
			this.Visual2.Text = "Visual2";
			this.Visual2.UseVisualStyleBackColor = true;
			// 
			// Debug1
			// 
			this.Debug1.AutoSize = true;
			this.Debug1.Checked = true;
			this.Debug1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.Debug1.Location = new System.Drawing.Point(28, 246);
			this.Debug1.Name = "Debug1";
			this.Debug1.Size = new System.Drawing.Size(89, 17);
			this.Debug1.TabIndex = 5;
			this.Debug1.Text = "Backgrounds";
			this.Debug1.UseVisualStyleBackColor = true;
			// 
			// MatrixModifiers
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Debug1);
			this.Controls.Add(this.Visual2);
			this.Controls.Add(this.Visual1);
			this.Controls.Add(this.ButtonClear);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "MatrixModifiers";
			this.Size = new System.Drawing.Size(452, 336);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button1;
		public System.Windows.Forms.TextBox TranslateY;
		public System.Windows.Forms.TextBox TranslateX;
		public System.Windows.Forms.Button ButtonClear;
		public System.Windows.Forms.TextBox M22;
		public System.Windows.Forms.TextBox M12;
		public System.Windows.Forms.TextBox M21;
		public System.Windows.Forms.TextBox M11;
		public System.Windows.Forms.CheckBox Visual1;
		public System.Windows.Forms.CheckBox Visual2;
		public System.Windows.Forms.CheckBox Debug1;
	}
}

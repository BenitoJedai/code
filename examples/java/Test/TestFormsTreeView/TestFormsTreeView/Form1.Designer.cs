namespace TestFormsTreeView
{
	partial class Form1
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
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node6");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode8});
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node2");
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Node4");
			System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Node5");
			System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Node3", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12});
			System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode13});
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.localMyPanel1 = new TestFormsTreeView.MyContext.LocalMyPanel();
			this.button7 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(204, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "This form was designed with Visual Studio";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(223, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "This project does not know about jsc compiler";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(24, 215);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(97, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Message 1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(24, 186);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(97, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Message 2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 101);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(234, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "This project does know about jsc.meta compiler.";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(21, 125);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(281, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "In a pre build event jsc.meta is used to generate .net code";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(308, 12);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(260, 23);
			this.button3.TabIndex = 6;
			this.button3.Text = "Show default information";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(12, 253);
			this.treeView1.Name = "treeView1";
			treeNode8.Name = "Node6";
			treeNode8.Text = "Node6";
			treeNode9.Name = "Node0";
			treeNode9.Text = "Node0";
			treeNode10.Name = "Node2";
			treeNode10.Text = "Node2";
			treeNode11.Name = "Node4";
			treeNode11.Text = "Node4";
			treeNode12.Name = "Node5";
			treeNode12.Text = "Node5";
			treeNode13.Name = "Node3";
			treeNode13.Text = "Node3";
			treeNode14.Name = "Node1";
			treeNode14.Text = "Node1";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode14});
			this.treeView1.Size = new System.Drawing.Size(231, 151);
			this.treeView1.TabIndex = 8;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(288, 309);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 9;
			this.button4.Text = "Add Root";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Enabled = false;
			this.button5.Location = new System.Drawing.Point(369, 320);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(75, 23);
			this.button5.TabIndex = 10;
			this.button5.Text = "Add Child";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.Enabled = false;
			this.button6.Location = new System.Drawing.Point(450, 329);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(75, 23);
			this.button6.TabIndex = 10;
			this.button6.Text = "remove";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// localMyPanel1
			// 
			this.localMyPanel1.Comment = "I like this!";
			this.localMyPanel1.Location = new System.Drawing.Point(308, 41);
			this.localMyPanel1.Name = "localMyPanel1";
			this.localMyPanel1.Size = new System.Drawing.Size(391, 216);
			this.localMyPanel1.TabIndex = 7;
			this.localMyPanel1.MoreDetails += new System.Action(this.localMyPanel1_MoreDetails);
			this.localMyPanel1.OK += new System.Action(this.localMyPanel1_OK);
			// 
			// button7
			// 
			this.button7.Enabled = false;
			this.button7.Location = new System.Drawing.Point(369, 289);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(75, 23);
			this.button7.TabIndex = 11;
			this.button7.Text = "clear";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(702, 416);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.localMyPanel1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "TestFormsTreeView. Your C# will be converted javascript and java.";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button3;
		private TestFormsTreeView.MyContext.LocalMyPanel localMyPanel1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
	}
}


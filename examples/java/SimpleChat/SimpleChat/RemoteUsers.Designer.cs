namespace SimpleChat
{
	partial class RemoteUsers
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node0");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node1");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node3");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node4");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node7");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node8");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node9");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node10");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node5", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node6");
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Node2", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode9,
            treeNode10});
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(3, 22);
			this.treeView1.Name = "treeView1";
			treeNode1.Name = "Node0";
			treeNode1.Text = "Node0";
			treeNode2.Name = "Node1";
			treeNode2.Text = "Node1";
			treeNode3.Name = "Node3";
			treeNode3.Text = "Node3";
			treeNode4.Name = "Node4";
			treeNode4.Text = "Node4";
			treeNode5.Name = "Node7";
			treeNode5.Text = "Node7";
			treeNode6.Name = "Node8";
			treeNode6.Text = "Node8";
			treeNode7.Name = "Node9";
			treeNode7.Text = "Node9";
			treeNode8.Name = "Node10";
			treeNode8.Text = "Node10";
			treeNode9.Name = "Node5";
			treeNode9.Text = "Node5";
			treeNode10.Name = "Node6";
			treeNode10.Text = "Node6";
			treeNode11.Name = "Node2";
			treeNode11.Text = "Node2";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode11});
			this.treeView1.Size = new System.Drawing.Size(251, 204);
			this.treeView1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(3, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(251, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Currently online chat buddies:";
			// 
			// RemoteUsers
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.label1);
			this.Name = "RemoteUsers";
			this.Size = new System.Drawing.Size(257, 229);
			this.SizeChanged += new System.EventHandler(this.RemoteUsers_SizeChanged);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Label label1;
	}
}

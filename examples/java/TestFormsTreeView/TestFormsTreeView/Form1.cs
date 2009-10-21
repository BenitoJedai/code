using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestFormsTreeView
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("hello world");
		}

		private void button2_Click(object sender, EventArgs e)
		{
			MessageBox.Show("hi");

			// http://www.codeproject.com/KB/edit/InputBox.aspx
			// InputBox

		}

		private void button3_Click(object sender, EventArgs e)
		{
			this.localMyPanel1.Author.Text = this.localMyPanel1.localInformation1.Author;
			this.localMyPanel1.Blog.Text = this.localMyPanel1.localInformation1.Blog;
			this.localMyPanel1.LabelLocation.Text = this.localMyPanel1.localLocation1.Text;

		}

		private void localMyPanel1_OK()
		{
			MessageBox.Show("localMyPanel1_OK: OK");
		}

		private void localMyPanel1_MoreDetails()
		{
			MessageBox.Show("localMyPanel1_MoreDetails: OK");

		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.Text = "treeView1_AfterSelect";

			//this.Text = "node: " + e.Node.Text;
		}

		TreeNode n1;

		private void button4_Click(object sender, EventArgs e)
		{
			//this.Text = this.treeView1.SelectedNode.Text;

			n1 = this.treeView1.Nodes.Add(
				"hi"
			);

			button5.Enabled = true;
			button6.Enabled = false;
			button7.Enabled = true;
		}
		TreeNode n2;

		private void button5_Click(object sender, EventArgs e)
		{
			n2 = n1.Nodes.Add(
				"hi2"
			);

			button6.Enabled = true;

		}

		private void button6_Click(object sender, EventArgs e)
		{
			n2.Remove();
			button6.Enabled = false;
		}

		private void button7_Click(object sender, EventArgs e)
		{
			n1.Nodes.Clear();
		}

	}
}

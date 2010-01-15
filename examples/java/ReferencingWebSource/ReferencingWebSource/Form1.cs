using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReferencingWebSource
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var x = new MySnippetProject.Class1();
			
			MessageBox.Show(new SomeNamespaceForJAVA.Class1().About() + " - " + x.Invoke("x"));
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

	}
}

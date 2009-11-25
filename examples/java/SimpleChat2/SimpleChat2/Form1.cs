using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimpleChat2.ClientProvider;
using SimpleChat2.Buffer;

namespace SimpleChat2
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//Console.WriteLine("asknames:");

			new ChatRequest.Requests.asknames(
				"foo",
				"bar"
			).SendTo(
				new Uri("http://ahman.no-ip.info"),
				response =>
				{
					Console.WriteLine(response);
				}
			);
		}
	}
}

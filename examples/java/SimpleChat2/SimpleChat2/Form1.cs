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


	

		private void findname_handler1_Request_1(SimpleChat2.Buffer.Server.findname_handler.findname_response e)
		{
			e.Content = "hi!";
		}



		private void button1_Click_1(object sender, EventArgs e)
		{
			this.requestDispatcher1.DispatcherEnabled = true;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.requestDispatcher1.DispatcherEnabled = false;

		}

		int Counter = 0;
		private void requestDispatcher1_Tick()
		{
			Counter++;
			this.Text = "" + Counter;
		}

		private void button3_Click(object sender, EventArgs e)
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

		private void button4_Click(object sender, EventArgs e)
		{
			new ChatRequest.Requests.sendmessage(
				"foo",
				"bar",
				"", 
				"hello world",
				""
			).SendTo(
				new Uri("http://127.0.0.1:" + this.requestDispatcher1.Port),
				response =>
				{
					Console.WriteLine(response);
				}
			);
		}

		private void sendmessage_handler1_Request(SimpleChat2.Buffer.Server.sendmessage_handler.sendmessage_response e)
		{
			this.mySync1.Queue.Enqueue(
				delegate
				{
					this.textBox1.AppendText(DateTime.Now + " " + e.message + Environment.NewLine);
					// update gui
				}
			);
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextualUserControls
{
	public partial class Form3 : Form
	{
		public Form3()
		{
			InitializeComponent();
		}

		private void control11_OK()
		{
			MessageBox.Show("you clicked ok! \n\n" + control11.Textbox1);
		}
	}
}

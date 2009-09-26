using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextualUserControls
{
	public partial class Form4 : Form
	{
		public event Action OK;
		public Form4()
		{
			InitializeComponent();
		}

		private void userControl11_OK()
		{


			if (OK != null)
				OK();
		}
	}
}

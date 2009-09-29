using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SimpleForms1
{
	public partial class Component2 : Component
	{
		public Component2()
		{
			InitializeComponent();
		}

		public Component2(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		public string SomeText { get; set; }

	}
}

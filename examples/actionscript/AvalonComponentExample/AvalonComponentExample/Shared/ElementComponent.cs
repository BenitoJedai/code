using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AvalonComponentExample.Shared
{
	public partial class ElementComponent : Component
	{
	
		public ElementComponent()
		{
			InitializeComponent();
		}

		public ElementComponent(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		public double X
		{
			get
			;
			set;
		}

		public double Y
		{
			get;
			set;
		}

		public double Size
		{
			get;
			set;
		}

		public System.Windows.Media.Color Foreground
		{
			get;
			set;
		}
	}
}

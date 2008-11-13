using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AvalonComponentExample.Shared
{
	public partial class TestContainerComponent : ContainerComponent
	{
		public TestContainerComponent()
		{
			InitializeComponent();
		}

		public TestContainerComponent(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}
	}
}

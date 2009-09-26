﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Drawing;
using System.Windows.Forms;

namespace ScriptCoreLibJava.BCLImplementation.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.ContainerControl))]
	internal class __ContainerControl : __ScrollableControl
	{
		protected void Dispose(bool disposing)
		{

		}

		public SizeF AutoScaleDimensions { get; set; }
		public AutoScaleMode AutoScaleMode { get; set; }

	}
}

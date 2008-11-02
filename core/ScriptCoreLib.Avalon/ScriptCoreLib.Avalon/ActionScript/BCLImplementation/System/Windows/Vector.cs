using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows
{
	[Script(Implements = typeof(global::System.Windows.Vector))]
	internal class __Vector
	{
		public double X { get; set; }
		public double Y { get; set; }

		public __Vector()
		{
			this.X = 0;
			this.Y = 0;
		}
				
		public double Length
		{
			get
			{
				return global::System.Math.Sqrt(LengthSquared);
			}
		}


		public double LengthSquared
		{
			get
			{
				return ((this.X * this.X) + (this.Y * this.Y));
			}
		}
 

 


	}
}

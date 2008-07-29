using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.geom;

namespace ScriptCoreLib.ActionScript.RayCaster
{
	[Script]
	public class Vector : IVector
	{
		public Vector()
			: this(null)
		{
		}

		public Vector(IVector source)
		{
			if (source != null)
			{
				Position = source.Position;
				Direction = source.Direction;
			}
		}

		public Point Position { get; set; }

		public double Direction { get; set; }
	}

	[Script]
	public interface IVector
	{
		Point Position { get; set; }

		double Direction { get; set; }
	}
}

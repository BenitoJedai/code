using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractiveAffineCube.AffineEngine
{
	public class AffineZoom : AffinePoint
	{
		public AffineZoom() : base(1, 1, 1)
		{
			
		}

		public static implicit operator AffineZoom(double Zoom)
		{
			return new AffineZoom { X = Zoom, Y = Zoom, Z = Zoom };
		}
	}
}

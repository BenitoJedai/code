using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ScriptCoreLib.Avalon
{
	
	public class AffineTransform : AffineTransformBase
	{
		// http://en.wikipedia.org/wiki/Affine_transformation

		public static implicit operator MatrixTransform(AffineTransform e)
		{
			double[] m = e;
		


			return new MatrixTransform(
				m[(int)Indecies.M11],
				m[(int)Indecies.M12],
				m[(int)Indecies.M21],
				m[(int)Indecies.M22],
				m[(int)Indecies.OX],
				m[(int)Indecies.OY]
			);
		}
	}
}

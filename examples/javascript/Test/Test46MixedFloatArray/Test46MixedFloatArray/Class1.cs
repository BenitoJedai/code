using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace Test46MixedFloatArray
{
	public class Class1
	{
		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150311

		public static void createQuadVBO(object gl
			, float right = 1.0f
			)
		{
			//__0003[2] = c;
			//__0003[6] = c;
			//__0003[8] = c;
			//d = [-1, -1, 0, -1, -1, 1, 0, -1, 0, 1, -1, 1];

			//enter createQuadVBO { { i = 0, value = null } }
			//enter createQuadVBO { { i = 1, value = null } }
			//enter createQuadVBO { { i = 2, value = 1 } }
			//enter createQuadVBO { { i = 3, value = null } }
			//enter createQuadVBO { { i = 4, value = null } }
			//enter createQuadVBO { { i = 5, value = null } }
			//enter createQuadVBO { { i = 6, value = 1 } }
			//enter createQuadVBO { { i = 7, value = null } }
			//enter createQuadVBO { { i = 8, value = 1 } }
			//enter createQuadVBO { { i = 9, value = null } }
			//enter createQuadVBO { { i = 10, value = null } }
			//enter createQuadVBO { { i = 11, value = null } }

			var fvertices =
				new float[]
				{
					// left to
					-1.0f, -1.0f,

					// right top
					right, -1.0f,

					// left bottom
					-1.0f, 1.0f,

					// right top
					right, -1.0f,

					// right bottom
					right, 1.0f,

					// left bottom
					-1.0f, 1.0f
				};

		}
	}
}

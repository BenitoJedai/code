using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTypeGenerics
{
	interface I1
	{
		void A();
	}


	class Class3 : I1
	{
		#region I1 Members

		void I1.A()
		{
		}

		#endregion
	}
}

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
			((I1)this).A();
			var i = 0;
			switch (i)
			{
				case 5:
					((I1)this).A();
					break;
				case 6:
					((I1)this).A();
					break;

				default:
					break;
			}
		
		}

		#endregion
	}
}

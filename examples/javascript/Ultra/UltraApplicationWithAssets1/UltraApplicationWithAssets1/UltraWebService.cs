using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltraApplicationWithAssets1
{
	public delegate void StringAction(string e);

	public sealed class UltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			result(x + DateTime.Now);
		}
	}
}

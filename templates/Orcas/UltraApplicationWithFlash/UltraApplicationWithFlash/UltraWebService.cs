using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltraApplicationWithFlash
{



	public delegate void GetTimeResult(string e);

	public sealed partial class UltraWebService
	{
		public void GetTime(string prefix, GetTimeResult result)
		{
			result(prefix + ": " + DateTime.Now);
		}
	}

}

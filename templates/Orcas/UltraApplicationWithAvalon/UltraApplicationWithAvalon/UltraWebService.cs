using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Library.Delegates;

namespace UltraApplicationWithAvalon
{
	public sealed class UltraWebService : IUltraWebService
	{
		public void GetTime(string x, StringAction result)
		{
			result(x + DateTime.Now);
		}

		/*
	<Compile Include="IUltraWebService.cs" >
		  <DependentUpon>UltraWebService.cs</DependentUpon>
	</Compile>
		 */

	}
}

using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace ScriptCoreLib.Shared.Nonoba
{
    [Script]
    public interface IDispatchHelper
    {
        Converter<uint, int> GetInt32 { get; set; }
        Converter<uint, double> GetDouble { get; set; }
		Converter<uint, string> GetString { get; set; }

		Converter<uint, byte[]> GetMemoryStream { get; set; }

		// MemoryStream is not allowed: reverting to byte[]
		// C:\work\jsc.svn\javascript\Tools\ConvertASToCS\ConvertASToCS.Any\js\Any\ProxyConverter.cs:478
		// C:\work\jsc.svn\javascript\Tools\ConvertASToCS\ConvertASToCS.Any\js\Any\ProxyConverter.cs:1007
		// C:\work\jsc.svn\actionscript\Games\FlashSpaceInvaders\FlashSpaceInvaders.MultiPlayer\ActionScript\MultiPlayer\NonobaClient.cs:36
		// C:\work\jsc.svn\actionscript\Games\FlashSpaceInvaders\FlashSpaceInvaders.MultiPlayer\ActionScript\MultiPlayer\NonobaClient.cs:70

        Converter<object, int> GetLength { get; set; }

        Converter<uint, int[]> GetInt32Array { get; set; }
        Converter<uint, double[]> GetDoubleArray { get; set; }
        Converter<uint, string[]> GetStringArray { get; set; }
    }

	[Script]
	public class DefaultImplementationForIDispatchHelper
	{
		public DefaultImplementationForIDispatchHelper(IDispatchHelper e)
		{
			e.GetDoubleArray =
				offset =>
				{
					int offseti = (int)offset;
					int len = e.GetLength(null) - offseti;

					var a = new double[len];

					for (var i = 0; i < a.Length; i++)
					{
						uint ii = (uint)i;
						uint j = ii + offset;

						a[i] = e.GetDouble(j);
					}

					return a;
				};

			e.GetInt32Array =
				offset =>
				{
					int offseti = (int)offset;
					int len = e.GetLength(null) - offseti;
					var a = new int[len];

					for (var i = 0; i < a.Length; i++)
					{
						uint ii = (uint)i;
						uint j = ii + offset;

						a[i] = e.GetInt32(j);
					}

					return a;
				};

			e.GetStringArray =
				offset =>
				{
					int offseti = (int)offset;
					int len = e.GetLength(null) - offseti;
					var a = new string[len];

					for (var i = 0; i < a.Length; i++)
					{
						uint ii = (uint)i;
						uint j = ii + offset;

						a[i] = e.GetString(j);
					}

					return a;
				};
		}
	}
}

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
}

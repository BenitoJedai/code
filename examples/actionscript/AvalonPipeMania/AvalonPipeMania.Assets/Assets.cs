using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.ActionScript;

//[assembly: ScriptResources(AvalonPipeMania.Assets.Shared.KnownAssets.Path.Data)]
//[assembly: ScriptResources(AvalonPipeMania.Assets.Shared.KnownAssets.Path.Assets)]
//[assembly: ScriptResources(AvalonPipeMania.Assets.Shared.KnownAssets.Path.Sounds)]
//[assembly: ScriptResources(AvalonPipeMania.Assets.Shared.KnownAssets.Path.Pipe.LeftToRight)]
//[assembly: ScriptResources(AvalonPipeMania.Assets.Shared.KnownAssets.Path.Pipe.LeftToDrain)]
//[assembly: ScriptResources(AvalonPipeMania.Assets.Shared.KnownAssets.Path.Pipe.TopToBottom)]
//[assembly: ScriptResources(AvalonPipeMania.Assets.Shared.KnownAssets.Path.Pipe.RightToBottom)]

namespace AvalonPipeMania.Assets
{
	namespace Shared
	{

		[Script]
		public class KnownAssets : AssetsImplementationDetails
		{
			public static readonly KnownAssets Default = new KnownAssets();

			[Script, ScriptResources]
			public static class Path
			{
				public const string Assets = "assets/AvalonPipeMania.Assets";
				public const string Data = "assets/AvalonPipeMania.Data";
				public const string Sounds = "assets/AvalonPipeMania.Sounds";

				[Script, ScriptResources]
				public static class Pipe
				{
					public const string LeftToRight = "assets/AvalonPipeMania.Pipe.LeftToRight";
					public const string LeftToDrain = "assets/AvalonPipeMania.Pipe.LeftToDrain";
					public const string TopToBottom = "assets/AvalonPipeMania.Pipe.TopToBottom";
					public const string RightToBottom = "assets/AvalonPipeMania.Pipe.RightToBottom";
				}
			}

		}

		public class AssetsImplementationDetails
		{
			// This class has the native implementation
			// JavaScript and ActionScript have their own implementations!

			public string[] FileNames
			{
				get
				{
					return ScriptCoreLib.CSharp.Extensions.EmbeddedResourcesExtensions.GetEmbeddedResources(null, this.GetType().Assembly);
				}
			}

		}

	}

	namespace JavaScript
	{
		[Script(Implements = typeof(Shared.AssetsImplementationDetails))]
		internal class __AssetsImplementationDetails
		{
			public string[] FileNames
			{
				[EmbedGetFileNames]
				get
				{
					throw new NotImplementedException();
				}
			}
		}
	}

	namespace ActionScript
	{
		[Script(Implements = typeof(Shared.AssetsImplementationDetails))]
		internal class __AssetsImplementationDetails
		{
			public string[] FileNames
			{
				[EmbedGetFileNames]
				get
				{
					throw new NotImplementedException();
				}
			}

		}


	}
}

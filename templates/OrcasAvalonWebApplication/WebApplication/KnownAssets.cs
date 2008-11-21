using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared;
using ScriptCoreLib;


// jsc:php: does not yet support the newest asset inclusing tech
[assembly: ScriptResources(WebApplication.Shared.KnownAssets.Path.Assets)]


namespace WebApplication
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
				// constants defined here define also the assets embedded
				// to the assembly
				// example: web/assets/OrcasAvalonApplication/about.txt

				public const string Assets = "assets/WebApplication";
			}


		}

		#region AssetsImplementationDetails
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
		#endregion


	}

	#region AssetsImplementationDetails
	namespace Client.JavaScript
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

	namespace Client.ActionScript
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

	namespace Client.Java
	{
		[Script(Implements = typeof(Shared.AssetsImplementationDetails))]
		internal class __AssetsImplementationDetails
		{
			public string[] FileNames
			{
				[EmbedGetFileNames]
				get
				{
					// this scenario is not supported
					// throw new NotImplementedException();
					return null;
				}
			}

		}


	}


	namespace Server
	{
		// PHP

		[Script(Implements = typeof(Shared.AssetsImplementationDetails))]
		internal class __AssetsImplementationDetails
		{
			public string[] FileNames
			{
				[EmbedGetFileNames]
				get
				{
					// this scenario is not supported
					// throw new NotImplementedException();
					return null;
				}
			}

		}


	}

	#endregion
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared;

namespace ScriptCoreLib
{
	namespace Shared.Avalon.Cards
	{

		[Script]
		public class KnownAssets : AssetsImplementationDetails
		{
			public static readonly KnownAssets Default = new KnownAssets();

			[Script, ScriptResources]
			public static class Path
			{
				public const string Assets = "assets/ScriptCoreLib.Avalon.Cards";

				public const string DefaultCards = "assets/ScriptCoreLib.Avalon.Cards/DefaultCards";
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
	namespace JavaScript.Avalon.Cards
	{
		[Script(Implements = typeof(Shared.Avalon.Cards.AssetsImplementationDetails))]
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

	namespace ActionScript.Avalon.Cards
	{
		[Script(Implements = typeof(Shared.Avalon.Cards.AssetsImplementationDetails))]
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
	#endregion
}

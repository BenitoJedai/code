using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra
{
	/// <summary>
	/// Ultra Application enables easy tier splitting. Tier split can be requested either
	/// by calling a method defined by Tier or when a referenced object is marked with TierAttribute.
	/// 
	/// A method using Tier switch hints cannot return a value and it will return earlier than it has 
	/// actually completed its execution.
	/// 
	/// If a method contains a single Tier clause then the whole method will be converted to the requested tier.
	/// </summary>
	public static class Tier
	{
		/// <summary>
		/// Methods marked as Tier.Server will run on the server
		/// </summary>
		[Tier(TierEnum.Server, RequestTierSwitchAtCaller = true)]
		public static void Server()
		{
		}

		/// <summary>
		/// Methods marked as Tier.JavaScript will run inside Browser
		/// </summary>
		[Tier(TierEnum.JavaScript, RequestTierSwitchAtCaller = true)]
		public static void JavaScript()
		{
		}

		/// <summary>
		/// Methods marked as Tier.Applet will run inside Java Virtual Machine
		/// </summary>
		[Tier(TierEnum.Applet, RequestTierSwitchAtCaller = true)]
		public static void Applet()
		{
		}

		/// <summary>
		/// Methods marked as Tier.Flash will run inside Flash Virtual Machine
		/// </summary>
		[Tier(TierEnum.Flash, RequestTierSwitchAtCaller = true)]
		public static void Flash()
		{
		}

		/// <summary>
		/// Methods marked as Tier.Alchemy will run inside Flash Virtual Machine as Alchemy
		/// </summary>
		[Tier(TierEnum.Alchemy, RequestTierSwitchAtCaller = true)]
		public static void Alchemy()
		{
		}

		/// <summary>
		/// Methods marked as Tier.Silverlight will run inside Silverlight
		/// </summary>
		[Tier(TierEnum.Silverlight, RequestTierSwitchAtCaller = true)]
		public static void Silverlight()
		{
		}

		/// <summary>
		/// Passing a Tier.Shared delegate to other tiers will instruct the other tier to use a local version of it.
		/// </summary>
		[Tier(TierEnum.Shared, RequestTierSwitchAtCaller = true)]
		public static void Shared()
		{

		}
	}

	
}

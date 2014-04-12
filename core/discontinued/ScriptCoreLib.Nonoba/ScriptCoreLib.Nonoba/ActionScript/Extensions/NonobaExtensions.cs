using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.Extensions
{
	[Script]
	public static class NonobaExtensions
	{
		public static MemoryStream GetMemoryStream(this Message m, uint index)
		{
			return m.GetByteArray(index).ToMemoryStream();
		}

		/// <summary>
		/// Opens the purchase process for the item defined in the request.
		/// </summary>
		/// <param name="stage">You must pass your stage object to the API for it to work.</param>
		/// <param name="item">The item's string identifier, you configure this in the shop admin, under edit game.</param>
		/// <param name="callback">Authenticating is done asynchronously, so a callback method is used to get the result state.</param>
		public static void ShowShop(Stage stage, string item, Action<string, bool> callback)
		{
			NonobaAPI.ShowShop(stage, item, callback.ToFunction());
		}
	}
}

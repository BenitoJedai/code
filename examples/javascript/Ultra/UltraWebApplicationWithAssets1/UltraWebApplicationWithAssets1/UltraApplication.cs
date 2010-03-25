using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using java.applet;
using java.awt;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Ultra.Library.Delegates;
using UltraWebApplicationWithAssets1.HTML.Pages.FromAssets;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Ultra;

namespace UltraWebApplicationWithAssets1
{
	public sealed class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			var a = new HTMLPage1();

			a.Button2.innerText = "Enumerate";

			Action Enumerate =
				delegate
				{
					a.Content.removeChildren();

					new UltraWebService().Enumerate(
						(Key, Field1) =>
						{
							var Delete = new IHTMLButton("Delete");
							var Item =
								new IHTMLDiv
								{
									Delete,
									Key + " " + Field1
								};

							Item.AttachTo(a.Content);

							ImplementDelete(Key, Delete, Item);
						}
					);
				};

			a.Button1.innerText = "Add";


			a.Button1.onclick +=
				delegate
				{
					new UltraWebService().Add(
						"Click at client time: " + DateTime.Now,
						delegate
						{
							Enumerate();
						}
					);

				};

			a.Button2.onclick +=
				delegate
				{
					a.Button2.style.color = "";
					Enumerate();
				};

			a.Container.AttachToDocument();
		}

		private static void ImplementDelete(string Key, IHTMLButton Delete, IHTMLDiv Item)
		{
			Delete.onclick +=
				delegate
				{
					Delete.disabled = true;

					new UltraWebService().Delete(
						Key,
						delegate
						{
							Item.Orphanize();
						}
					);

				};
		}

	}

	class vNext1
	{
		private static void ImplementDelete(string Key, IHTMLButton Delete, IHTMLDiv Item)
		{
			Delete.onclick +=
				delegate
				{
					Delete.disabled = true;

					Async<string, string> ServerDelete =
						(Key__, Yield) =>
						{
							Tier.Server();

							using (var ctx = new DataClasses1DataContext())
							{

								ctx.Table1s.DeleteAllOnSubmit(
									ctx.Table1s.Where(k => k.Key == Convert.ToInt32(Key__))
								);

								ctx.SubmitChanges();
							}

							if (Yield != null)
								Yield("");
						};

					ServerDelete(Key,
						delegate
						{
						}
					);


				};
		}


	}

	class vNext2
	{
		private static void ImplementDelete(string Key, IHTMLButton Delete, IHTMLDiv Item)
		{
			Delete.onclick +=
				delegate
				{
					Delete.disabled = true;

					Async<string, string> ServerDelete =
						(Key__, Yield) =>
						{
							new CodeAtServer().Invoke();

							using (var ctx = new DataClasses1DataContext())
							{

								ctx.Table1s.DeleteAllOnSubmit(
									ctx.Table1s.Where(k => k.Key == Convert.ToInt32(Key__))
								);

								ctx.SubmitChanges();
							}

							if (Yield != null)
								Yield("");
						};

					ServerDelete(Key,
						delegate
						{
						}
					);


				};
		}


	}

	[Tier(TierEnum.Server)]
	class CodeAtServer
	{

		internal void Invoke()
		{
			throw new NotImplementedException();
		}
	}

	class vNext3
	{
		private static void ImplementDelete(string Key, IHTMLButton Delete, IHTMLDiv Item)
		{
			Delete.onclick +=
				delegate
				{
					Delete.disabled = true;

					Tier.Server();

					using (var ctx = new DataClasses1DataContext())
					{

						ctx.Table1s.DeleteAllOnSubmit(
							ctx.Table1s.Where(k => k.Key == Convert.ToInt32(Key))
						);

						ctx.SubmitChanges();
					}

					Tier.JavaScript();

					//...

					Tier.Flash();

					//...

				};
		}


	}
}
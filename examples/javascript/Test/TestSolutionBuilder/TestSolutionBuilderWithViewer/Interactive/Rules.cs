using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.Ultra.Studio;
using ScriptCoreLib.Ultra.Studio.Languages;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Studio.InteractiveExpressions;

namespace TestSolutionBuilderWithViewer.Interactive
{
	class Rules
	{
		public Rules(SolutionFileView v, SolutionBuilder sln, Action Update)
		{

			#region AtLink
			Action<Uri, Action<int>> AtLink =
				(Link, Handler) =>
				{
					var Counter = 0;

					v.LinkCommentClick +=
						uri =>
						{
							if (uri == Link)
							{
								Counter++;
								Handler(Counter);
							}
						};
				};
			#endregion


			AtLink(sln.Interactive.ToVisualCSharpLanguage,
				delegate
				{
					sln.Language = new VisualCSharpLanguage();
					sln.Name = "VisualCSharpProject1";
					Update();
				}
			);

			AtLink(sln.Interactive.ToVisualBasicLanguage,
				delegate
				{
					sln.Language = new VisualBasicLanguage();
					sln.Name = "VisualBasicProject1";
					Update();
				}
			);


			AtLink(sln.Interactive.ToVisualFSharpLanguage,
				delegate
				{
					sln.Language = new VisualFSharpLanguage();
					sln.Name = "VisualFSharpProject1";
					Update();
				}
			);



			AtLink(sln.Interactive.ApplicationToDocumentTitle.Comment,
				ApplicationToDocumentTitleVariation =>
				{
					var Now = DateTime.Now;


					if (ApplicationToDocumentTitleVariation % 2 == 0)
						sln.Interactive.ApplicationToDocumentTitle.Title.Value =
							"Time: " + Now.ToString();
					else
						sln.Interactive.ApplicationToDocumentTitle.Title.Value =
							sln.Name;

					Update();
				}
			);

			var WebMethod2_From = new[]
			{
				"IL",
				"C#",
				"Visual Basic",
				"F#",
			};

			var WebMethod2_To = new[]
			{
				"JavaScript",
				"ActionScript",
				"Java",
				"PHP",
			};

			AtLink(sln.Interactive.WebMethod2,
				Variation =>
				{

					sln.Interactive.WebMethod2.Title.Value =
						"jsc can convert " + WebMethod2_From.Random() + " to " + WebMethod2_To.Random();

					Update();
				}
			);

			Action<InteractiveComment> AtInteractiveComment =
				c =>
				{
					AtLink(c,
						vv =>
						{
							c.RaiseClick();
							Update();
						}
					);
				};


			sln.Interactive.Comments.WithEach(AtInteractiveComment);

			

		}
	}
}

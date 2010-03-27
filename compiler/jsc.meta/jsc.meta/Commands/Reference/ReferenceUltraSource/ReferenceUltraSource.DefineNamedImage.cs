using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using System.Xml.XPath;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using jsc.meta.Library.Templates;
using jsc.meta.Library.Templates.Avalon;
using jsc.meta.Library.Templates.JavaScript;
using jsc.Script;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Ultra.Library.Extensions;
using jsc.meta.Library.Templates.JavaScript.Named;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource
{
	partial class ReferenceUltraSource
	{


		public static Type DefineNamedImage(
			RewriteToAssembly.AssemblyRewriteArguments a,
			RewriteToAssembly r,
			string ImageFullName,
			string ImageSource,
			MethodInfo get_ImageSource)
		{
			var TemplateType = typeof(NamedImage);

			using (a.context.ToTransientTransaction())
			{
				r.AtILOverride +=
					(m, il_a) =>
					{
						if (m.DeclaringType != TemplateType)
							return;

						il_a[OpCodes.Ldstr] =
							(e) =>
							{
								if (e.i.TargetLiteral != NamedImage.DefaultSource)
								{
									e.Default();
									return;
								}

								if (ImageSource != null)
								{
									e.il.Emit(OpCodes.Ldstr, ImageSource);
									return;
								}

								e.il.Emit(OpCodes.Call, get_ImageSource);
							};
					};

				a.context.TypeRenameCache.Resolve +=
					SourceType =>
					{
						if (SourceType != TemplateType)
							return;

						a.context.TypeRenameCache[SourceType] =
							ImageFullName;
					};

				var MyType = a.context.TypeCache[TemplateType];

				TemplateType = null;

				return MyType;
			}
		}




	}
}

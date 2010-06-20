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
using jsc.meta.Library.Templates.JavaScript.Named;
using jsc.Script;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace jsc.meta.Commands.Reference.ReferenceUltraSource
{
	partial class ReferenceUltraSource
	{


        public static Type DefineAvalonNamedImage(
            RewriteToAssembly.AssemblyRewriteArguments a,
            RewriteToAssembly r,
            string ImageFullName,
            string ImageSource,
            MethodInfo get_ImageSource,
            Action<TypeBuilder> PostTypeRewrite = null,
            TypeBuilder DeclaringType = null
            )
        {
            var TemplateType = typeof(AvalonNamedImage);


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
                                if (e.i.TargetLiteral != AvalonNamedImage._src)
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

                r.RewriteArguments.context.OverrideDeclaringType[TemplateType] = DeclaringType;
                r.RewriteArguments.context.TypeRenameCache[TemplateType] = ImageFullName;


               

                if (PostTypeRewrite != null)
                    r.PostTypeRewrite +=
                        e =>
                        {
                            if (e.SourceType != TemplateType)
                                return;

                            PostTypeRewrite(e.Type);

                        };

                var MyType = a.context.TypeCache[TemplateType];

                TemplateType = null;

                return MyType;
            }
        }



	}
}

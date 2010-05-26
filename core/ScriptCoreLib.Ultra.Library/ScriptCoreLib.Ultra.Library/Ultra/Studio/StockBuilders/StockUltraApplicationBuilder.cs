using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.Studio.StockMethods;
using ScriptCoreLib.Ultra.Studio.StockAttributes;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Ultra.Studio.StockBuilders
{
    public class StockUltraApplicationBuilder
    {
        public StockUltraApplicationBuilder(
                Action<SolutionFile> AddFile,
                SolutionBuilder Context,
                XElement ItemGroupForCompile)
        {
            Func<string, string> ToProjectFile =
                f => Context.Name + "/" + Context.Name + "/" + f;

            Func<string, string, SolutionFile> AddProjectFile =
                (f, t) =>
                {
                    var r = new SolutionFile
                    {
                        Name = ToProjectFile(f),
                        Content = t,
                        Context = Context
                    };

                    AddFile(
                        r
                    );

                    return r;
                };

            var FileHeader = new SolutionFileComment
            {
                Comment = "For more information visit:",
                Link = new Uri("http://studio.jsc-solutions.net"),
                MarginBottom = 1
            };

            var FileComments =
                new[]
					{
						FileHeader
                        //,
                        //Context.Interactive.ToVisualBasicLanguage,
                        //Context.Interactive.ToVisualCSharpLanguage,
                        //Context.Interactive.ToVisualFSharpLanguage
					};

            #region DefaultPage
            var DefaultPageElement =
                new XElement("html",
                    new XElement("head",
                        new XElement("title", "DefaultPage")
                    ),
                    Context.ApplicationPage
                );

            var DefaultPage =
                new SolutionFile
                {
                    Name = ToProjectFile("Design/Default.htm"),
                };

            DefaultPage.WriteHTMLElement(DefaultPageElement);


            ItemGroupForCompile.Add(
                new XElement("Content",
                    new XAttribute("Include",
                        @"Design\Default.htm"
                    )
                )
            );

            AddFile(DefaultPage);

            #endregion


            #region RaiseGenerateHTMLFiles
            Context.Interactive.RaiseGenerateHTMLFiles(
                item =>
                {
                    var f =
                        new SolutionFile
                        {
                            Name = ToProjectFile(item.Name),
                            DependentUpon = DefaultPage
                        };

                    f.WriteHTMLElement(item.Content);

                    ItemGroupForCompile.Add(
                        new XElement("Content",
                            new XAttribute("Include",
                                item.Name.Replace("/", "\\")
                            ),
                            new XElement("DependentUpon", DefaultPage.Name.SkipUntilLastIfAny("/"))

                        )
                    );

                    AddFile(f);

                }
            );
            #endregion

            var AddTypeFiles = new Dictionary<SolutionProjectLanguageType, SolutionFile>();

            Action<SolutionProjectLanguageType, string> AddTypeWithoutMerge =
                (SourceType, IncludeName) =>
                {
                    var Folder = SourceType.Namespace.SkipUntilIfAny(Context.Name + ".");
                    var Include = "";

                    if (Folder != "")
                        if (Folder != Context.Name)
                            Include += Folder.Replace(".", "/") + "/";

                    Include += IncludeName + Context.Language.CodeFileExtension;

                    var SourceFile =
                        new SolutionFile
                        {
                            Name = ToProjectFile(Include),
                            ContextType = SourceType
                        };

                    AddTypeFiles[SourceType] = SourceFile;

                    Context.Language.WriteType(SourceFile, SourceType, Context);

                    var Compile =
                        new XElement("Compile",
                            new XAttribute("Include",
                                Include.Replace("/", "\\")
                            )
                        );

                    if (SourceType.DependentUpon != null)
                    {
                        SourceFile.DependentUpon = AddTypeFiles[SourceType.DependentUpon];

                        if (Context.Language.SupportsDependentUpon())
                        {
                            // F# does not?

                            Compile.Add(
                                new XElement("DependentUpon", SourceFile.DependentUpon.Name.SkipUntilLastIfAny("/"))
                            );
                        }
                    }

                    ItemGroupForCompile.Add(Compile);
                    AddFile(SourceFile);
                };

            Action<SolutionProjectLanguageType> AddType =
                SourceType =>
                {
                    // if partial is not supported then
                    // we need to merge the types
                    // later we may need to have an identity object? :)

                    AddTypeWithoutMerge(
                        SourceType,
                        SourceType.Name
                    );

                    SourceType.DependentPartialTypes.WithEach(
                        PartialType =>
                        {
                            PartialType.Type.DependentUpon = SourceType;

                            AddTypeWithoutMerge(
                                PartialType.Type,
                                PartialType.Name
                            );
                        }
                    );
                };

            Context.Interactive.RaiseGenerateTypes(AddType);


            // http://thevalerios.net/matt/2009/01/assembly-information-for-f-console-applications/

            #region ApplicationWebService

            var ApplicationWebServiceType = Context.Interactive.ApplicationWebServiceType;

            ApplicationWebServiceType.Namespace = Context.Name;
            ApplicationWebServiceType.Comments = FileComments;

            AddType(ApplicationWebServiceType);

            #endregion

            #region Application

            var ApplicationType = new SolutionProjectLanguageType
            {
                IsSealed = true,

                Namespace = Context.Name,
                Name = "Application",
                Summary = "This type can be used from javascript. The method calls will seamlessly be proxied to the server.",
                Comments = FileComments
            };

            ApplicationType.UsingNamespaces.Add("System");
            ApplicationType.UsingNamespaces.Add("System.Text");
            ApplicationType.UsingNamespaces.Add("System.Linq");
            ApplicationType.UsingNamespaces.Add("System.Xml.Linq");
            ApplicationType.UsingNamespaces.Add("ScriptCoreLib");
            ApplicationType.UsingNamespaces.Add("ScriptCoreLib.JavaScript");
            ApplicationType.UsingNamespaces.Add("ScriptCoreLib.JavaScript.DOM");
            ApplicationType.UsingNamespaces.Add("ScriptCoreLib.JavaScript.DOM.HTML");
            ApplicationType.UsingNamespaces.Add("ScriptCoreLib.JavaScript.Components");
            ApplicationType.UsingNamespaces.Add("ScriptCoreLib.JavaScript.Extensions");
            ApplicationType.UsingNamespaces.Add("ScriptCoreLib.Extensions");
            ApplicationType.UsingNamespaces.Add(Context.Name + ".HTML.Pages");

            //ApplicationType.Properties.Add(
            //    new SolutionProjectLanguageProperty
            //    {
            //        Name = "Property1",
            //        PropertyType = new SolutionProjectLanguageType
            //        {
            //            Name = "string"
            //        },
            //        GetMethod = new SolutionProjectLanguageMethod(),
            //        SetMethod = new SolutionProjectLanguageMethod()
            //    }
            //);

            var ApplicationConstructor = new StockMethodApplication(ApplicationType, Context.Interactive);

            ApplicationType.Methods.Add(ApplicationConstructor);


            AddType(ApplicationType);

            #endregion

            #region AssemblyInfo
            var AssemblyInfo =
                new SolutionFile
                {
                    Name = ToProjectFile("Properties/AssemblyInfo" + Context.Language.CodeFileExtension),
                };


            {
                AssemblyInfo.Write(Context.Language, Context, FileComments);

                AssemblyInfo.WriteLine();

                Context.Language.WriteAssemblyAttributeNamespace(AssemblyInfo, Context.Name,
                    delegate
                    {
                        Context.Language.WriteUsingNamespace(AssemblyInfo, "System.Reflection");

                        AssemblyInfo.WriteLine();

                        // language write assembly attribute

                        Action<string, string> WriteGeneralInformation =
                            (TypeName, Constant) =>
                            {
                                Context.Language.WriteAssemblyAttribute(
                                    AssemblyInfo,
                                    new StockAttributeGeneralInformation(
                                        new SolutionProjectLanguageType { Name = TypeName, Namespace = "System.Reflection" },
                                        Constant
                                    ),
                                    Context
                                );
                            };

                        Context.Language.WriteIndentedComment(AssemblyInfo,
@"General Information about an assembly is controlled through the following 
set of attributes. Change these attribute values to modify the information
associated with an assembly."
                        );

                        //[assembly: AssemblyTitle("Ultra Application")]
                        //[assembly: AssemblyDescription("Ultra Application. Write javascript, flash and java applets within a C# project. http://jsc-solutions.net")]
                        //[assembly: AssemblyConfiguration("")]
                        //[assembly: AssemblyCompany("jsc-solutions.net")]
                        //[assembly: AssemblyProduct("UltraApplication")]
                        //[assembly: AssemblyCopyright("Copyright © jsc-solutions.net 2010")]

                        WriteGeneralInformation("AssemblyTitle", Context.Name);
                        WriteGeneralInformation("AssemblyDescription", Context.Description);
                        WriteGeneralInformation("AssemblyCompany", Context.Company);


                        WriteGeneralInformation("AssemblyProduct", Context.Name.Replace(" ", ""));
                        WriteGeneralInformation("AssemblyCopyright", "Copyright © " + Context.Company + " " + DateTime.Now.Year);
                        WriteGeneralInformation("AssemblyVersion", "1.0.0.0");
                        WriteGeneralInformation("AssemblyFileVersion", "1.0.0.0");

                        Context.Language.WriteAssemblyAttribute(
                            AssemblyInfo,
                            new StockAttributeObfuscation(),
                            Context
                        );
                    }
                );

            }

            ItemGroupForCompile.Add(
                new XElement("Compile",
                    new XAttribute("Include",
                        @"Properties\AssemblyInfo" + Context.Language.CodeFileExtension
                    )
                )
            );


            AddFile(AssemblyInfo);
            #endregion


            #region Program

            var ProgramType = new SolutionProjectLanguageType
            {
                IsStatic = true,
                Namespace = Context.Name,
                Name = "Program",
                Summary = "You can debug your application by hitting F5.",
                Comments = FileComments,
                DependentUpon = ApplicationType
            };

            ProgramType.UsingNamespaces.Add("System");
            ProgramType.UsingNamespaces.Add("jsc.meta.Commands.Rewrite.RewriteToUltraApplication");
            ProgramType.Methods.Add(new StockMethodMain(ApplicationType));

            AddType(ProgramType);

            #endregion

        }

    }

}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.Studio.StockMethods;
using ScriptCoreLib.Ultra.Studio.StockAttributes;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Studio.PseudoExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;
using ScriptCoreLib.Ultra.Studio.Languages;

namespace ScriptCoreLib.Ultra.Studio.StockBuilders
{
    public class StockUltraApplicationBuilder
    {
        public StockUltraApplicationBuilder(
                Action<SolutionFile> AddFile,
                SolutionBuilder Context,
                XElement ItemGroupForCompile,
                Action<SolutionProjectLanguageType> NotifyStartupType)
        {
            Context.Interactive.Initialize();

            Func<string, string> ToProjectFile =
                f => Context.Name + "/" + Context.Name + "/" + f;

            #region AddProjectFile
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
            #endregion



            #region Design/App.css
            var DesignStyle =
               new SolutionFile
               {
                   Name = ToProjectFile("Design/App.css"),
                   Content = "h1 { color: blue; }"
               };

            ItemGroupForCompile.Add(
                new XElement("Content",
                    new XAttribute("Include",
                        @"Design\App.css"
                    )
                )
            );

            AddFile(DesignStyle);

            #endregion

            #region Design/App.htm
            var DefaultPageElement =
                new XElement("html",
                    new XElement("head",
                        new XElement("title", "App")
                    ),
                    Context.ApplicationPage
                );

            var DefaultPage =
                new SolutionFile
                {
                    Name = ToProjectFile("Design/App.htm"),
                };

            DefaultPage.WriteHTMLElement(DefaultPageElement);


            ItemGroupForCompile.Add(
                new XElement("Content",
                    new XAttribute("Include",
                        @"Design\App.htm"
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

            #region AddTypeWithoutMerge
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

                    if (SourceType.BaseType != null)
                    {
                        if (SourceType.BaseType is KnownStockTypes.System.Windows.Forms.UserControl)
                            Compile.Add(
                                new XElement("SubType", SourceType.BaseType.Name)
                            );

                        if (SourceType.BaseType is KnownStockTypes.System.ComponentModel.Component)
                            Compile.Add(
                                new XElement("SubType", SourceType.BaseType.Name)
                            );
                    }


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
            #endregion

            #region AddType
            Action<SolutionProjectLanguageType> AddType =
                SourceType =>
                {
                    // if partial is not supported then
                    // we need to merge the types
                    // later we may need to have an identity object? :)

                    if (!Context.Language.SupportsPartialTypes())
                    {
                        SourceType.DependentPartialTypes.WithEach(
                              PartialType =>
                              {
                                  SourceType.Fields.AddRange(PartialType.Type.Fields);
                                  SourceType.Methods.AddRange(PartialType.Type.Methods);
                              }
                        );

                        SourceType.DependentPartialTypes = null;
                    }

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
            #endregion

            #region AssemblyInfo

            var AssemblyInfoFolder = "Properties";

            if (Context.Language == KnownLanguages.VisualBasic)
                AssemblyInfoFolder = "My Project";

            var AssemblyInfo =
                new SolutionFile
                {
                    Name = ToProjectFile(AssemblyInfoFolder + "/AssemblyInfo" + Context.Language.CodeFileExtension),
                };


            {
                AssemblyInfo.Write(Context.Language, Context, new[] { Context.Interactive.FileHeader });

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
                        AssemblyInfoFolder + @"\AssemblyInfo" + Context.Language.CodeFileExtension
                    )
                )
            );


            AddFile(AssemblyInfo);
            #endregion



            Context.Interactive.RaiseGenerateTypes(AddType);


            // http://thevalerios.net/matt/2009/01/assembly-information-for-f-console-applications/

            #region ApplicationWebService

            var ApplicationWebServiceType = Context.Interactive.ApplicationWebServiceType;

            ApplicationWebServiceType.Namespace = Context.Name;

            AddType(ApplicationWebServiceType);

            #endregion

            #region Application

            var ApplicationType = Context.Interactive.ApplicationType;

            ApplicationType.Namespace = Context.Name;

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
            ApplicationType.UsingNamespaces.Add("ScriptCoreLib.Delegates");
            ApplicationType.UsingNamespaces.Add(Context.Name + ".HTML.Pages");
            ApplicationType.UsingNamespaces.Add(Context.Name + ".Design");

            // css

            //var DefaultStyle = new SolutionProjectLanguageType
            //{
            //    Name = "DefaultStyle"
            //};

            //var DefaultStyleField = DefaultStyle.ToInitializedField("style");

            //ApplicationType.Fields.Add(DefaultStyleField);



            var ApplicationConstructor = new StockMethodApplication(
                ApplicationType, Context.Interactive/*, DefaultStyleField*/);

            ApplicationType.Methods.Add(ApplicationConstructor);


            AddType(ApplicationType);

            #endregion


            #region Program

            var ProgramType = Context.Interactive.ProgramType;

            ProgramType.Namespace = Context.Name;


            ProgramType.UsingNamespaces.Add("System");
            ProgramType.UsingNamespaces.Add("jsc.meta.Commands.Rewrite.RewriteToUltraApplication");
            AddType(ProgramType);

            NotifyStartupType(ProgramType);

            #endregion

        }

    }

}

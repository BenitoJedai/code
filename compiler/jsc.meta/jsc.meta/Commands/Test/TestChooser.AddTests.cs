using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System.Drawing;
using System.Threading;
using jsc.meta.Commands.Reference;
using System.IO;
using System.Media;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Commands.Rewrite.RewriteToReplacedReferences;

namespace jsc.meta.Commands.Test
{
    partial class TestChooser
    {

        private static void AddTests(Action<string, Action> AddButton)
        {
            #region Add
            Action<string, Func<CommandBase>> Add =
                (text, e) =>
                {
                    var n = e();

                    AddButton(n.GetType().Name + " " + text,
                      delegate
                      {
                          n.Invoke();
                          n = e();
                      }
                    );
                };
            #endregion


            #region AddRewriteToAssembly
            Action<FileInfo> AddRewriteToAssembly = f =>
                Add(f.Name, () =>
                    new RewriteToAssembly
                    {
                        Output = f,

                        AssemblyMerge = new RewriteToAssembly.AssemblyMergeInstruction[]
                        {
                            f.FullName
                        }
                    }
                );
            #endregion

            Add("ScriptCoreLib", () =>
               new RewriteToReplacedReferences
               {
                   DefaultToOrcas = true,
                   Assembly = new FileInfo(@"c:\util\jsc\bin\ScriptCoreLib.dll"),
               }
           );

            Add("CLR40", () =>
                new RewriteToReplacedReferences
                {
                    DefaultToOrcas = true,
                    Assembly = new FileInfo(@"W:\jsc.svn\examples\rewrite\CLR25\CLR40\bin\Debug\CLR40.dll"),
                }
            );

            Add("AvalonTriangleTexture", () =>
                new ReferenceJavaScriptDocument
                {
                    ProjectFileName = new FileInfo(@"W:\jsc.svn\examples\javascript\AvalonTriangleTexture\AvalonTriangleTexture\AvalonTriangleTexture.csproj"),
                    Configuration = "Assets",
                    SelectAll = true
                }
            );


            Add("ScriptCoreLib.Redux", () =>
                new ReferenceJavaScriptDocument
                {
                    ProjectFileName = new FileInfo(@"W:\jsc.svn\core\ScriptCoreLib.Redux\ScriptCoreLib.Redux\ScriptCoreLib.Redux.csproj"),
                    Configuration = "Assets",
                    SelectAll = true
                }
            );

            //start /WAIT C:\util\jsc\bin\jsc.meta.exe ReferenceJavaScriptDocument /ProjectFileName:"$(ProjectPath)" /Configuration:"$(ConfigurationName)" /AttachDebugger:false /SelectAll:true


            new[]
            {
                @"W:\jsc.svn\examples\rewrite\CircularGenericInterfaces\CircularGenericInterfaces\bin\Debug\CircularGenericInterfaces.dll",
                @"c:\util\jsc\bin\ScriptCoreLib.dll",
                @"W:\jsc.svn\examples\rewrite\NestedTypesWithExtensions\NestedTypesWithExtensions\bin\Debug\NestedTypesWithExtensions.dll"
            }.WithEach(k => AddRewriteToAssembly(new FileInfo(k)));

            Add("ScriptCoreLib.exe IDocument", () =>
                 new RewriteToAssembly
                 {
                     assembly = new FileInfo(@"W:\jsc.svn\examples\rewrite\ScriptCoreLib.IDocument\ScriptCoreLib.IDocument\bin\Debug\ScriptCoreLib.IDocument.exe"),
                     type = "IDocument`1"
                 }
             );

            var ScriptCoreLib = @"c:\util\jsc\bin\ScriptCoreLib.dll";

            Add("ScriptCoreLib::IDocument`1", () =>
                new RewriteToAssembly
                {
                    Output = new FileInfo(ScriptCoreLib + ".IDocument.dll"),
                    assembly = new FileInfo(ScriptCoreLib),
                    type = "ScriptCoreLib.JavaScript.DOM.IDocument`1"
                }
            );

            Add("ScriptCoreLib::ICommentNode", () =>
              new RewriteToAssembly
              {
                  Output = new FileInfo(ScriptCoreLib + ".ICommentNode.dll"),
                  assembly = new FileInfo(ScriptCoreLib),
                  type = "ScriptCoreLib.JavaScript.DOM.ICommentNode"
              }
          );

            Add("ScriptCoreLib::IWindow", () =>
                new RewriteToAssembly
                {
                    Output = new FileInfo(ScriptCoreLib + ".IWindow.dll"),
                    assembly = new FileInfo(ScriptCoreLib),
                    type = "ScriptCoreLib.JavaScript.DOM.IWindow"
                }
            );


            Add("ScriptCoreLib::IHTMLElement", () =>
               new RewriteToAssembly
               {
                   Output = new FileInfo(ScriptCoreLib + ".IHTMLElement.dll"),
                   assembly = new FileInfo(ScriptCoreLib),
                   type = "ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement"
               }
           );

            Add("ScriptCoreLib::IElement", () =>
                 new RewriteToAssembly
                 {
                     Output = new FileInfo(ScriptCoreLib + ".IElement.dll"),
                     assembly = new FileInfo(ScriptCoreLib),
                     type = "ScriptCoreLib.JavaScript.DOM.IElement"
                 }
             );


            Add("ScriptCoreLib::INode", () =>
                 new RewriteToAssembly
                 {
                     Output = new FileInfo(ScriptCoreLib + ".INode.dll"),
                     assembly = new FileInfo(ScriptCoreLib),
                     type = "ScriptCoreLib.JavaScript.DOM.INode"
                 }
             );

            Add("ScriptCoreLib::IHTMLDocument", () =>
               new RewriteToAssembly
               {
                   Output = new FileInfo(ScriptCoreLib + ".IHTMLDocument.dll"),
                   assembly = new FileInfo(ScriptCoreLib),
                   type = "ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDocument"
               }
           );


            Add("ScriptCoreLib::CustomSwitch`1", () =>
               new RewriteToAssembly
               {
                   Output = new FileInfo(ScriptCoreLib + ".CustomSwitch.dll"),
                   assembly = new FileInfo(ScriptCoreLib),
                   type = "ScriptCoreLib.PHP.Runtime.CustomSwitch`1"
               }
           );

            Add("ScriptCoreLib::CustomSwitch`2", () =>
                  new RewriteToAssembly
                  {
                      Output = new FileInfo(ScriptCoreLib + ".CustomSwitch.dll"),
                      assembly = new FileInfo(ScriptCoreLib),
                      type = "ScriptCoreLib.PHP.Runtime.CustomSwitch`2"
                  }
              );

            Add("ScriptCoreLib::MySQL", () =>
                new RewriteToAssembly
                {
                    Output = new FileInfo(ScriptCoreLib + ".MySQL.dll"),
                    assembly = new FileInfo(ScriptCoreLib),
                    type = "ScriptCoreLib.PHP.Runtime.MySQL"
                }
            );

            Add("ScriptCoreLib::PHP.Native", () =>
             new RewriteToAssembly
             {
                 Output = new FileInfo(ScriptCoreLib + ".PHP.Native.dll"),
                 assembly = new FileInfo(ScriptCoreLib),
                 type = "ScriptCoreLib.PHP.Native"
             }
         );


            Add("TestUInt16ArrayInitializer", () =>
              new RewriteToAssembly
              {
                  Output = new FileInfo(@"W:\jsc.svn\examples\rewrite\TestUInt16ArrayInitializer\TestUInt16ArrayInitializer\bin\Debug\N.dll"),
                  assembly = new FileInfo(@"W:\jsc.svn\examples\rewrite\TestUInt16ArrayInitializer\TestUInt16ArrayInitializer\bin\Debug\TestUInt16ArrayInitializer.dll"),
                  type = "TestUInt16ArrayInitializer.MyClass"
              }
          );

            Add("TestStruct", () =>
               new RewriteToAssembly
               {
                   Output = new FileInfo(@"W:\jsc.svn\examples\rewrite\TestStruct\TestStruct\bin\Debug\N.dll"),
                   assembly = new FileInfo(@"W:\jsc.svn\examples\rewrite\TestStruct\TestStruct\bin\Debug\TestStruct.dll"),
               }
           );

            Add("PromotionWebApplication1", () =>
               new RewriteToJavaScriptDocument
               {
                   assembly = new FileInfo(@"W:\jsc.svn\examples\java\PromotionWebApplication\PromotionWebApplication1\bin\Release\PromotionWebApplication1.exe"),
               }
           );
           
        }
    }
}

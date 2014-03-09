﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.ActionScript.Components;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Studio.StockToolboxTabs;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.Ultra.Studio;
using ScriptCoreLib.Ultra.Studio.Languages;
using ScriptCoreLib.Ultra.Studio.StockExpressions;
using ScriptCoreLib.Ultra.Studio.StockPages;
using ScriptCoreLib.Ultra.Studio.StockTypes;
using TestSolutionBuilderV1.HTML.Pages;
using TestSolutionBuilderV1.HTML.Images.FromAssets;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript;

namespace TestSolutionBuilderV1.Views
{
    public class StudioView
    {
        public readonly IHTMLDiv Content = new IHTMLDiv();

        public StudioView(Action<IHTMLElement, Action<ISaveAction>> AddSaveButton = null)
        {
            Content.style.position = IStyle.PositionEnum.absolute;
            Content.style.left = "0px";
            Content.style.right = "0px";
            Content.style.top = "0px";
            Content.style.bottom = "0px";

            new TwentyTenWorkspace().ToBackground(Content.style, true);

            var WorkspaceHeaderTab0 = new IHTMLDiv().With(
                div =>
                {
                    div.style.position = IStyle.PositionEnum.absolute;
                    div.style.top = "0px";
                    div.style.left = "0px";
                    div.style.width = "14em";
                    div.style.height = "6em";

                    div.style.padding = "0.5em";

                    new Glow1().ToBackground(div.style, false);
                }
            ).AttachTo(Content);

            var WorkspaceHeaderTab1 = new IHTMLDiv().With(
                div =>
                {
                    div.style.position = IStyle.PositionEnum.absolute;
                    div.style.top = "0px";
                    div.style.left = "14em";
                    div.style.width = "20em";
                    div.style.height = "6em";

                    div.style.padding = "0.5em";

                    new Glow1().ToBackground(div.style, false);
                }
            ).AttachTo(Content);

            var WorkspaceHeaderTab2 = new IHTMLDiv().With(
                div =>
                {
                    div.style.position = IStyle.PositionEnum.absolute;
                    div.style.top = "0px";
                    div.style.left = "34em";
                    div.style.right = "6em";
                    div.style.height = "6em";

                    div.style.padding = "0.5em";

                    new Glow1().ToBackground(div.style, false);
                }
            ).AttachTo(Content);

            var WorkspaceHeaderTab7 = new IHTMLDiv().With(
                 div =>
                 {
                     div.style.position = IStyle.PositionEnum.absolute;
                     div.style.top = "0px";
                     div.style.width = "6em";
                     div.style.right = "0px";
                     div.style.height = "6em";

                     //div.style.padding = "0.5em";

                     new Glow1().ToBackground(div.style, false);
                 }
             ).AttachTo(Content);

            var WorkspaceHeaderTab0Text = default(IHTMLSpan);



            new DownloadSDK
            {

            }.AttachTo(
                 new IHTMLAnchor
                 {
                     title = "Download JSC SDK!",
                     href = "http://download.jsc-solutions.net"
                 }.AttachTo(WorkspaceHeaderTab7)
            );

            @"studio.jsc-solutions.net".ToDocumentTitle().With(
                title =>
                {
                    WorkspaceHeaderTab0Text = new IHTMLSpan { innerText = title };

                    WorkspaceHeaderTab0Text.AttachTo(WorkspaceHeaderTab0);
                    //WorkspaceHeaderTab0Text.style.SetLocation(16, 8);
                    WorkspaceHeaderTab0Text.style.fontFamily = IStyle.FontFamilyEnum.Tahoma;
                    WorkspaceHeaderTab0Text.style.color = Color.White;
                    WorkspaceHeaderTab0Text.style.display = IStyle.DisplayEnum.block;

                    // http://www.quirksmode.org/css/textshadow.html
                    WorkspaceHeaderTab0Text.style.textShadow = "#808080 4px 2px 2px";

                }
            );


            if (AddSaveButton != null)
                AddSaveButton(WorkspaceHeaderTab0Text, i => Save = i);

            // em + px :)
            var Workspace0 = new IHTMLDiv().With(
                div =>
                {
                    div.style.position = IStyle.PositionEnum.absolute;
                    div.style.left = "0px";
                    div.style.right = "0px";
                    div.style.bottom = "0px";
                    div.style.top = "6em";
                }
            ).AttachTo(Content);

            // workspace contains the split views
            var Workspace = new IHTMLDiv().With(
                div =>
                {
                    div.style.position = IStyle.PositionEnum.absolute;
                    div.style.left = "6px";
                    div.style.right = "6px";
                    div.style.bottom = "6px";
                    div.style.top = "6px";
                }
            ).AttachTo(Workspace0);

            // in this project we wont be having toolbox or toolbar yet

            Action<HorizontalSplit> ApplyStyle =
                t =>
                {
                    t.Split.Splitter.style.backgroundColor = Color.None;
                    t.SplitImageContainer.Orphanize();
                    t.SplitArea.Target.style.borderLeft = "0";
                    t.SplitArea.Target.style.borderRight = "0";
                    t.SplitArea.Target.style.width = "6px";
                    t.SplitArea.Target.style.Opacity = 0.7;

                    // should we obselete JSColor already?
                    t.SelectionColor = JSColor.Black;
                };

            var EditorTreeSplit = new HorizontalSplit
            {
                Minimum = 0,
                Maximum = 1,
                Value = 0.7,
            };

            EditorTreeSplit.With(ApplyStyle);

            EditorTreeSplit.Split.Splitter.style.backgroundColor = Color.None;


            EditorTreeSplit.Container.AttachTo(Workspace);



            var Split = new HorizontalSplit
            {
                Minimum = 0,
                Maximum = 1,
                Value = 0.3,
            };

            Split.With(ApplyStyle);

            Split.Split.Splitter.style.backgroundColor = Color.None;

            EditorTreeSplit.LeftContainer = Split.Container;



            var SolutionToolbox = new SolutionDockWindowPage();

            SolutionToolbox.HeaderText.innerText = "Toolbox";
            SolutionToolbox.Content.style.backgroundColor = Color.White;
            SolutionToolbox.Content.style.padding = "2px";
            SolutionToolbox.Content.style.overflow = IStyle.OverflowEnum.auto;
            SolutionToolbox.Content.Clear();


            var vv = new SolutionToolboxListView();

            vv.Container.style.color = Color.Black;
            //vv.Container.AttachTo(SolutionToolbox.Content);

            var items = new StockToolboxTabsForHTMLDocument();

            // jsc market components

            vv.Add(
             new SolutionToolboxListViewTab
             {
                 DataType = "DataTable",

                 Name = "DataTable",
                 Title = "DataTable",
                 Text = "DataTable",
                 Icon = new DataTableImage()
             }
          );

            vv.Add(
                new SolutionToolboxListViewTab
                {
                    DataType = "SpiralDataType",

                    Name = "Spiral1",
                    Title = "Spiral",
                    Text = "Spiral",
                    Icon = new Spiral()
                }
             );

            // can we drag this into 
            // code ?
            // msvs gets the image link
            //http://192.168.43.252:11924/assets/ScriptCoreLib.Ultra.Components/StockToolboxImageTransparent64.png?data-jsc-type=DAETruck
            vv.Add(
                  new SolutionToolboxListViewTab
                  {
                      DataType = "DAETruck",

                      Name = "DAETruck",
                      Title = "DAETruck",
                      Text = "DAETruck",
                      Icon = new DAETruck()
                  }
               );

            vv.Add(
                new SolutionToolboxListViewTab
                {
                    DataType = "WebGLEarthByBjorn",

                    Name = "WebGLEarthByBjorn",
                    Title = "WebGLEarthByBjorn",
                    Text = "WebGLEarthByBjorn",
                    Icon = new WebGLEarthByBjorn()
                }
                );


            items.WithEach(vv.Add);




            var Viewer = new SolutionDocumentViewer();
            SolutionDocumentViewerTab File7Tab = "Design/App.htm";
            Viewer.Add(File7Tab);

            #region OutputFile
            var OutputFile = new SolutionFile();
            var OutputFileViewer = new SolutionFileView();

            // fullscreen! :)
            OutputFileViewer.Container.style.height = "100%";

            OutputFile.IndentStack.Push(
                delegate
                {
                    OutputFile.Write(SolutionFileTextFragment.Comment, "" + DateTime.Now);
                    OutputFile.WriteSpace();
                }
            );

            Action<string> OutputWriteLine =
                n =>
                {
                    // Would we want to rewire System.Out? Console.WriteLine?
                    OutputFile.WriteIndent();
                    OutputFile.WriteLine(n);

                    // we could have a resume feature? now we just go and clear...
                    OutputFileViewer.File = OutputFile;
                };


            OutputWriteLine("studio.jsc-solutions.net ready!");
            #endregion

            SolutionDocumentViewerTab OutputTab = "Output";
            Viewer.Add(OutputTab);
            OutputTab.TabElement.style.Float = IStyle.FloatEnum.right;


            SolutionDocumentViewerTab AboutTab = "Project";
            Viewer.Add(AboutTab);
            AboutTab.TabElement.style.Float = IStyle.FloatEnum.right;


            var CurrentDesigner = new SolutionFileDesigner();




            var HTMLDesigner = new SolutionFileDesignerHTMLElementTabs();

            CurrentDesigner.Add(HTMLDesigner);


            // undoable?
            var sln = new SolutionBuilder();




            #region CodeSourceA
            var CodeSourceATab =
                new SolutionFileDesignerTab
                {
                    Image = new RTA_mode_html(),
                    Text = "Generated Code"
                };

            var CodeSourceAView = new SolutionFileView();

            CodeSourceAView.Container.style.position = IStyle.PositionEnum.absolute;
            CodeSourceAView.Container.style.left = "0px";
            CodeSourceAView.Container.style.right = "0px";
            CodeSourceAView.Container.style.top = "0px";
            CodeSourceAView.Container.style.bottom = "0px";

            CodeSourceAView.Container.style.display = IStyle.DisplayEnum.none;
            CodeSourceAView.Container.AttachTo(CurrentDesigner.Content);

            CodeSourceATab.Deactivated +=
                delegate
                {
                    CodeSourceAView.Container.style.display = IStyle.DisplayEnum.none;
                };

            CodeSourceATab.Activated +=
                delegate
                {
                    HTMLDesigner.HTMLDesignerContent.WhenContentReady(
                        body =>
                        {
                            var CodeSourceFile = new SolutionFile
                            {

                                Name = "Default.htm"
                            };

                            var Type = new SolutionProjectLanguageType
                            {
                                Comments = new SolutionFileComment[] { "This type was generated from the HTML file." },
                                Namespace = sln.Name + ".HTML.Pages",
                                Name = "IDefaultPage",
                                IsInterface = true,
                            };

                            (from n in body.AsXElement().DescendantsAndSelf()
                             let id = n.Attribute("id")
                             where id != null
                             select new { n, id }
                            ).WithEach(
                                k =>
                                {
                                    Type.Properties.Add(
                                        new KnownStockTypes.ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement().ToAutoProperty(k.id.Value)
                                    );
                                }
                            );

                            sln.Language.WriteType(CodeSourceFile, Type, null);

                            CodeSourceAView.File = CodeSourceFile;

                            CodeSourceAView.Container.style.display = IStyle.DisplayEnum.empty;
                        }
                    );
                };


            #endregion


            #region CodeSourceB
            var CodeSourceBTab =
                new SolutionFileDesignerTab
                {
                    Image = new RTA_mode_html(),
                    // all source code, not just html?
                    Text = "Source"
                };

            var CodeSourceBView = new SolutionFileView();

            CodeSourceBView.Container.style.position = IStyle.PositionEnum.absolute;
            CodeSourceBView.Container.style.left = "0px";
            CodeSourceBView.Container.style.right = "0px";
            CodeSourceBView.Container.style.top = "0px";
            CodeSourceBView.Container.style.bottom = "0px";

            CodeSourceBView.Container.style.display = IStyle.DisplayEnum.none;
            CodeSourceBView.Container.AttachTo(CurrentDesigner.Content);

            CodeSourceBTab.Deactivated +=
                delegate
                {
                    CodeSourceBView.Container.style.display = IStyle.DisplayEnum.none;
                };

            CodeSourceBTab.Activated +=
                delegate
                {

                    CodeSourceBView.Container.style.display = IStyle.DisplayEnum.empty;
                };


            #endregion

            #region CodeSourceFormsDesignerTab
            var CodeSourceFormsDesignerTab =
                new SolutionFileDesignerTab
                {
                    Image = new RTA_mode_design(),
                    // all source code, not just html?
                    Text = "Designer"
                };


            var CodeSourceFormsDesignerTabView = new SolutionFileView();

            CodeSourceFormsDesignerTabView.Container.style.With(
                style =>
                {
                    style.position = IStyle.PositionEnum.absolute;
                    style.left = "0px";
                    style.right = "0px";
                    style.top = "0px";
                    style.bottom = "0px";

                    style.display = IStyle.DisplayEnum.none;
                }
            );

            new IHTMLDiv().With(
                div =>
                {
                    div.style.position = IStyle.PositionEnum.absolute;
                    div.style.left = "16px";
                    div.style.top = "16px";
                    div.style.width = "400px";
                    div.style.height = "300px";
                    div.style.backgroundColor = Color.FromGray(0xe0);
                    div.style.border = "1px solid gray";
                    div.AttachTo(CodeSourceFormsDesignerTabView.Container);
                }
            );


            CodeSourceFormsDesignerTabView.Container.AttachTo(CurrentDesigner.Content);

            CodeSourceFormsDesignerTab.Deactivated +=
                delegate
                {
                    CodeSourceFormsDesignerTabView.Container.style.display = IStyle.DisplayEnum.none;
                };

            CodeSourceFormsDesignerTab.Activated +=
                delegate
                {

                    CodeSourceFormsDesignerTabView.Container.style.display = IStyle.DisplayEnum.empty;
                };


            #endregion

            CurrentDesigner.Add(CodeSourceFormsDesignerTab);
            CurrentDesigner.Add(CodeSourceBTab);
            CurrentDesigner.Add(CodeSourceATab);




            var wLeftScrollable = new System.Windows.Forms.Form
            {
                BackColor = global::System.Drawing.Color.White,
                Text = "Toolbox",
                ControlBox = false,
                ShowIcon = false,
                AutoScroll = true
            };

            vv.Container.AttachTo(
                wLeftScrollable.GetHTMLTargetContainer()
            );

            //wLeftScrollable.Show();

            Split.Split.LeftScrollable.style.zIndex = 0;
            wLeftScrollable.AttachFormTo(Split.Split.LeftScrollable);


            wLeftScrollable.PopupInsteadOfClosing();

            //Split.Split.LeftScrollable = (IHTMLDiv)(object)SolutionToolbox.body;
            Split.Split.RightScrollable = Viewer.Container;

            // ...





            #region dynamic content
            Func<IEnumerable<XElement>> GetPages = delegate
            {
                return from n in sln.ApplicationPage.DescendantsAndSelf()
                       let type = n.Attribute(SolutionBuilderInteractive.DataTypeAttribute)
                       where type != null
                       let id = n.Attribute("id")
                       where id != null
                       select n;
            };

            sln.Interactive.GenerateApplicationExpressions +=
                Add =>
                {


                    // page.PageContainer.ReplaceWith(
                    GetPages().WithEach(
                        k =>
                        {
                            var id = k.Attribute("id").Value;

                            if (id == "Page1")
                            {
                                Add(
                                    new StockReplaceWithNewPageExpression(id)
                                );
                            }

                            if (id == "UserControl1")
                            {
                                Add(
                                    new StockReplaceWithNewUserControlExpression(sln.Name + ".Components", id)
                                );
                            }

                            if (id == "Applet1")
                            {
                                Add(
                                    new StockReplaceWithNewAppletExpression(sln.Name + ".Components", id)
                                );
                            }

                            if (id == "Sprite1")
                            {
                                Add(
                                    new StockReplaceWithNewSpriteExpression(sln.Name + ".Components", id)
                                );
                            }

                            if (id == "AppletUserControl1")
                            {
                                Add(
                                    new StockReplaceWithNewAppletExpression(sln.Name + ".Components", id)
                                );
                            }
                        }
                    );
                };

            sln.Interactive.GenerateHTMLFiles +=
                Add =>
                {

                    GetPages().WithEach(
                        k =>
                        {
                            var id = k.Attribute("id").Value;

                            if (id == "Page1")
                            {
                                var __Content = new XElement(StockPageDefault.Page);


                                __Content.Element("head").Element("title").Value = id;

                                Add(
                                    new SolutionProjectHTMLFile
                                    {
                                        Name = "Design/" + id + ".htm",
                                        Content = __Content
                                    }
                                );
                            }
                        }
                     );
                };

            sln.Interactive.GenerateTypes +=
                Add =>
                {
                    GetPages().WithEach(
                        k =>
                        {
                            var id = k.Attribute("id").Value;

                            if (id == "UserControl1")
                            {
                                Add(
                                    new StockUserControlType(sln.Name + ".Components", id)
                                );
                            }

                            if (id == "Applet1")
                            {
                                Add(
                                    new StockAppletType(sln.Name + ".Components", id)
                                );
                            }

                            if (id == "Sprite1")
                            {
                                Add(
                                    new StockSpriteType(sln.Name + ".Components", id)
                                );
                            }

                            if (id == "AppletUserControl1")
                            {
                                var UserControl2 = new StockUserControlType(sln.Name + ".Components", "UserControl2");

                                Add(
                                    UserControl2
                                );

                                Add(
                                    new StockUserControlAppletType(sln.Name + ".Components", id, UserControl2)
                                );
                            }
                        }
                     );
                };
            #endregion


            var _Solution = new TreeNode(VistaTreeNodePage.Create);


            var _Project = _Solution.Add();

            var About = new About();

            #region UpdateFile1Text
            Action UpdateFile1Text =
                delegate
                {

                    if (CodeSourceBView.File != null)
                    {
                        File7Tab.Text = CodeSourceBView.File.Name.SkipUntilLastIfAny("/");
                    }
                    else
                    {
                        File7Tab.Text = sln.Name;
                    }


                };
            #endregion



            #region Update
            Action Update =
                delegate
                {
                    sln.Name = About.ProjectName.value;

                    UpdateFile1Text();

                    _Project.Clear();
                    UpdateTree(sln, CodeSourceBView, _Solution, _Project);
                };
            #endregion


            var PreviousVersion = default(string);

            #region HTMLDesigner.HTMLDesignerContent
            HTMLDesigner.HTMLDesignerContent.WhenContentReady(
                body =>
                {
                    if (PreviousVersion == null)
                    {
                        var x = new XElement(body.AsXElement());
                        var y = x.ToString();
                        PreviousVersion = y;
                    }

                    Action<bool> HTMLDesignerContentCheck =
                        DoUpdate =>
                        {
                            var x = new XElement(body.AsXElement());
                            var y = x.ToString();

                            if (PreviousVersion != y)
                            {
                                PreviousVersion = y;


                                sln.ApplicationPage = x;

                                // allow any blur causing action to complete first
                                // we get reselected for some odd reason, why?
                                new Timer(
                                    delegate
                                    {
                                        if (DoUpdate)
                                        {
                                            OutputWriteLine("Designer has caused an update.");
                                            Update();
                                        }
                                        else
                                        {
                                            OutputWriteLine("Designer will cause an update.");
                                        }

                                    }
                                ).StartTimeout(700);
                            }
                        };

                    var HTMLDesignerContentDirty = new Timer(
                        delegate
                        {
                            HTMLDesignerContentCheck(false);
                        }
                    );

                    HTMLDesigner.HTMLDesignerContent.contentWindow.onfocus +=
                        delegate
                        {
                            OutputWriteLine("Designer activated.");
                            //"focus".ToDocumentTitle();

                            //HTMLDesignerContentDirty.StartInterval(700);
                        };

                    HTMLDesigner.HTMLDesignerContent.contentWindow.onblur +=
                        delegate
                        {
                            //HTMLDesignerContentDirty.Stop();

                            OutputWriteLine("Designer deactivated.");
                            //"blur".ToDocumentTitle();
                            HTMLDesignerContentCheck(true);

                        };
                }
            );
            #endregion

            #region CodeSourceBView.FileChanged
            CodeSourceBView.FileChanged +=
                delegate
                {
                    UpdateFile1Text();


                    OutputWriteLine("Select: " + CodeSourceBView.File.Name);

                    CodeSourceFormsDesignerTab.TabElement.Hide();

                    // hack :)
                    if (CodeSourceBView.File.Name.EndsWith("/App.htm"))
                    {
                        // currently we only have one element :)

                        HTMLDesigner.HTMLDesignerTab.RaiseActivated();

                        HTMLDesigner.HTMLDesignerTab.TabElement.style.display = IStyle.DisplayEnum.inline_block;
                        HTMLDesigner.HTMLSourceTab.TabElement.style.display = IStyle.DisplayEnum.none;
                        CodeSourceATab.TabElement.style.display = IStyle.DisplayEnum.inline_block;
                        CodeSourceBTab.TabElement.style.display = IStyle.DisplayEnum.inline_block;

                        HTMLDesigner.HTMLDesignerContent.WhenContentReady(
                            body =>
                            {
                                HTMLDesigner.HTMLDesignerContent.contentWindow.focus();
                            }
                        );

                        // show the design/source buttons
                    }
                    else if (CodeSourceBView.File.Name.EndsWith(".sln"))
                    {
                        AboutTab.Activate();
                    }
                    else if (CodeSourceBView.File.Name.EndsWith(sln.Language.ProjectFileExtension))
                    {
                        AboutTab.Activate();
                    }
                    else if (CodeSourceBView.File.Name.EndsWith(sln.Language.CodeFileExtension))
                    {
                        // show type outline / member
                        CodeSourceBTab.RaiseActivated();

                        HTMLDesigner.HTMLDesignerTab.TabElement.style.display = IStyle.DisplayEnum.none;
                        HTMLDesigner.HTMLSourceTab.TabElement.style.display = IStyle.DisplayEnum.none;
                        CodeSourceATab.TabElement.style.display = IStyle.DisplayEnum.none;
                        CodeSourceBTab.TabElement.style.display = IStyle.DisplayEnum.inline_block;

                        CodeSourceBView.File.ContextType.BaseType.With(
                            BaseType =>
                            {
                                if (BaseType is KnownStockTypes.System.Windows.Forms.UserControl)
                                {
                                    CodeSourceFormsDesignerTab.TabElement.Show();
                                    CodeSourceFormsDesignerTab.RaiseActivated();

                                }

                                if (BaseType is KnownStockTypes.System.ComponentModel.Component)
                                {
                                    CodeSourceFormsDesignerTab.TabElement.Show();
                                    CodeSourceFormsDesignerTab.RaiseActivated();

                                }
                            }
                        );

                    }


                };
            #endregion


            //AddSaveButton(WorkspaceHeader, i => Save = i);

            About.ProjectName.value = sln.Name;
            About.ProjectName.onchange +=
                delegate
                {
                    OutputWriteLine("Project name has changed.");
                    Update();
                };



            Viewer.Content.Clear();
            Viewer.Content.Add(About.Container);
            Viewer.Content.Add(CurrentDesigner.Container);
            Viewer.Content.Add(OutputFileViewer.Container);

            AboutTab.WhenActivated(About.Container);
            File7Tab.WhenActivated(CurrentDesigner.Container);
            OutputTab.WhenActivated(OutputFileViewer.Container);



            Viewer.First().Activate();

            //var SolutionExplorer = new SolutionDockWindowPage();

            //SolutionExplorer.HeaderText.innerText = "Solution Explorer";
            //SolutionExplorer.Content.style.backgroundColor = Color.White;
            //SolutionExplorer.Content.style.padding = "2px";
            //SolutionExplorer.Content.ReplaceContentWith(_Solution.Container);


            var fSolutionExplorer = new System.Windows.Forms.Form
            {
                BackColor = global::System.Drawing.Color.White,
                Text = "Solution Explorer",
                ControlBox = false,
                ShowIcon = false

            };

            EditorTreeSplit.Split.RightScrollable.style.zIndex = 0;
            EditorTreeSplit.Split.RightScrollable.style.position = IStyle.PositionEnum.relative;

            fSolutionExplorer.AttachFormTo(EditorTreeSplit.Split.RightScrollable);

            _Solution.Container.AttachTo(fSolutionExplorer.GetHTMLTargetContainer());

            _Solution.Container.style.overflow = IStyle.OverflowEnum.auto;
            _Solution.Container.style.height = "100%";
            _Solution.Container.style.backgroundColor = Color.White;

            //EditorTreeSplit.Split.RightContainer = (IHTMLDiv)(object)SolutionExplorer.Container;

            EditorTreeSplit.Container.AttachTo(Workspace);

            //CurrentDesigner.First().RaiseActivated();

            Update();

            #region CreateLanguageButton
            Action<IHTMLImage, string, SolutionProjectLanguage, string> CreateLanguageButton =
                (Icon, Text, Language, Name) =>
                {
                    var span = new IHTMLSpan(Text);

                    span.style.marginLeft = "0.7em";
                    span.style.marginRight = "0.7em";

                    new IHTMLButton { Icon /*, span */ }.AttachTo(WorkspaceHeaderTab1).With(
                        btn =>
                        {
                            btn.onclick +=
                                delegate
                                {
                                    sln.Language = Language;
                                    sln.Name = Language.LanguageSpelledName.Replace(" ", "") + "Project1";
                                    Update();
                                };

                            //btn.style.display = IStyle.DisplayEnum.block;
                        }
                    );
                };
            #endregion


            CreateLanguageButton(new VisualCSharpProject(), "C#", KnownLanguages.VisualCSharp, "VisualCSharpProject1");
            CreateLanguageButton(new VisualFSharpProject(), "F#", KnownLanguages.VisualFSharp, "VisualFSharpProject1");
            CreateLanguageButton(new VisualBasicProject(), "Visual Basic", KnownLanguages.VisualBasic, "VisualBasicProject1");

            var ListOfCreateProjectTypeButton = new List<IHTMLButton>();

            #region CreateProjectTypeButton
            Action<string, Action> CreateProjectTypeButton =
              (Text, Handler) =>
              {
                  var span = new IHTMLSpan(Text);

                  span.style.marginLeft = "0.7em";
                  span.style.marginRight = "0.7em";

                  new IHTMLButton { span }.AttachTo(WorkspaceHeaderTab2).With(
                      btn =>
                      {
                          ListOfCreateProjectTypeButton.Add(btn);

                          btn.onclick +=
                              delegate
                              {
                                  ListOfCreateProjectTypeButton.WithEach(n => n.disabled = true);

                                  Handler();
                              };

                          //btn.style.display = IStyle.DisplayEnum.block;
                      }
                  );
              };
            #endregion

            #region ToSpecificProjectType
            Action<string, Action> ToSpecificProjectType =
                (Text, Handler) =>
                {
                    CreateProjectTypeButton(Text,
                        delegate
                        {
                            Handler();


                            HTMLDesigner.HTMLDesignerContent.WhenDocumentReady(
                                document =>
                                {
                                    document.WithContent(sln.ApplicationPage);
                                    // we should now also lock the designer!
                                    document.DesignMode = false;
                                }
                            );

                            Update();
                        }
                    );
                };
            #endregion

            #region Avalon, Forms
            ToSpecificProjectType("Avalon App",
                delegate
                {
                    sln.WithCanvas();
                }
            );


            ToSpecificProjectType("Avalon Flash App",
               delegate
               {
                   sln.WithCanvasAdobeFlash();

               }
            );

            ToSpecificProjectType("Forms App",
                delegate
                {
                    sln.WithForms();
                }
            );


            ToSpecificProjectType("Forms Applet App",
               delegate
               {
                   sln.WithFormsApplet();

               }
            );
            #endregion

            ToSpecificProjectType("Flash App",
              delegate
              {
                  sln.WithAdobeFlash();
              }
            );

            ToSpecificProjectType("Flash Camera App",
                delegate
                {
                    sln.WithAdobeFlashCamera();
                }
              );

            ToSpecificProjectType("Flash Flare3D App",
               delegate
               {
                   sln.WithAdobeFlashWithFlare3D();
               }
             );

            ToSpecificProjectType("Applet App",
              delegate
              {
                  sln.WithJavaApplet();
              }
            );
        }


        void UpdateTree(SolutionBuilder sln, SolutionFileView v, TreeNode _Solution, TreeNode _Project)
        {
            _Solution.Text = "Solution '" + sln.Name + "' (1 project)";
            _Solution.IsExpanded = true;

            _Solution.WithIcon(() => new SolutionTwentyTen());

            _Project.Text = sln.Name;
            _Project.IsExpanded = true;

            // Or my project?

            var PropertiesFolderName = "Properties";
            if (sln.Language == KnownLanguages.VisualBasic)
                PropertiesFolderName = "My Project";

            var _Properties = _Project.Add(PropertiesFolderName);
            _Properties.IsExpanded = true;
            _Properties.WithIcon(() => new SolutionProjectProperties());

            var _References = _Project.Add("References");
            _References.IsExpanded = false;
            _References.WithIcon(() => new References());


            RenderReferences(sln, _References);


            var FolderLookup = new Dictionary<string, TreeNode>();
            var FileLookup = new Dictionary<SolutionFile, TreeNode>();

            FolderLookup[_Properties.Text] = _Properties;




            var files = sln.ToFiles();

            files.WithEach(
                f =>
                {
                    var ProjectInclude = f.Name.SkipUntilIfAny("/").SkipUntilIfAny("/");

                    var Folder = ProjectInclude.TakeUntilLastOrEmpty("/");

                    if (!string.IsNullOrEmpty(Folder))
                    {
                        if (!FolderLookup.ContainsKey(Folder))
                        {
                            var _Folder = _Project.Add(Folder);
                            FolderLookup[Folder] = _Folder;
                            _Folder.IsExpanded = true;
                        }
                    }
                }
            );

            files.WithEach(
                (SolutionFile f) =>
                {
                    var n = default(TreeNode);

                    var Extension = "." + f.Name.SkipUntilLastIfAny(".");

                    if (Extension == ".sln")
                    {
                        n = _Solution;
                    }
                    else if (Extension == sln.Language.ProjectFileExtension)
                    {
                        n = _Project;

                        n.Element.TextArea.style.fontWeight = "bold";
                    }
                    else
                    {
                        var ProjectInclude = f.Name.SkipUntilIfAny("/").SkipUntilIfAny("/");

                        var Folder = ProjectInclude.TakeUntilLastOrEmpty("/");

                        var Parent = _Project;

                        if (!string.IsNullOrEmpty(Folder))
                        {
                            Parent = FolderLookup[Folder];
                        }

                        if (sln.Language.SupportsDependentUpon())
                            if (f.DependentUpon != null)
                            {
                                Parent = FileLookup[f.DependentUpon];
                                Parent.IsExpanded = false;
                            }

                        n = Parent.Add(ProjectInclude.SkipUntilLastIfAny("/"));

                        FileLookup[f] = n;
                    }

                    if (Extension == KnownLanguages.VisualCSharp.CodeFileExtension)
                        n.WithIcon(() => new VisualCSharpCode());
                    else if (Extension == KnownLanguages.VisualCSharp.ProjectFileExtension)
                        n.WithIcon(() => new VisualCSharpProject());
                    else if (Extension == KnownLanguages.VisualBasic.CodeFileExtension)
                        n.WithIcon(() => new VisualBasicCode());
                    else if (Extension == KnownLanguages.VisualBasic.ProjectFileExtension)
                        n.WithIcon(() => new VisualBasicProject());
                    else if (Extension == KnownLanguages.VisualFSharp.CodeFileExtension)
                        n.WithIcon(() => new VisualFSharpCode());
                    else if (Extension == KnownLanguages.VisualFSharp.ProjectFileExtension)
                        n.WithIcon(() => new VisualFSharpProject());
                    else if (Extension == ".htm")
                        n.WithIcon(() => new HTMLDocument());
                    else if (Extension == ".config")
                        n.WithIcon(() => new ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets.HTMLDocument());
                    else if (Extension == ".css")
                        n.WithIcon(() => new StyleSheetFile());

                    if (f.ContextType != null)
                    {
                        if (f.ContextType.BaseType != null)
                        {
                            if (f.ContextType.BaseType is KnownStockTypes.System.Windows.Forms.UserControl)
                                n.WithIcon(() => new SolutionProjectFormsControl());
                            if (f.ContextType.BaseType is KnownStockTypes.System.ComponentModel.Component)
                                n.WithIcon(() => new SolutionProjectComponentImage());
                        }
                    }

                    if (sln.Language.SupportsDependentUpon())
                        if (f.DependentUpon != null)
                        {
                            n.WithIcon(() => new SolutionProjectDependentUpon());
                        }

                    n.IsExpanded = true;


                    n.Click +=
                        delegate
                        {
                            v.File = f;
                        };

                    // somebody refreshed the solution.
                    if (v.File == null)
                    {
                        if (f.Name.SkipUntilLastIfAny("/").TakeUntilLastIfAny(".") == "Application")
                            v.File = f;
                    }
                    else
                    {
                        // we may not care about the file extensions, will we see a glitch? :)
                        if (v.File.Name.SkipUntilLastIfAny("/").TakeUntilLastIfAny(".") == f.Name.SkipUntilLastIfAny("/").TakeUntilLastIfAny("."))
                            v.File = f;
                    }
                }
            );

            if (this.Save != null)
            {
                this.Save.Clear();
                this.Save.FileName = sln.Name + ".sln.zip";
                files.WithEach(f => this.Save.Add(f.Name, f.Content));
            }
        }

        private static void RenderReferences(SolutionBuilder sln, TreeNode _References)
        {
            // why doesn't LINQ work anymore?
            var OrderedReferences = sln.References.Select(
                item =>
                {
                    var Include = item.Attribute("Include");
                    return Include.Value.TakeUntilIfAny(",");
                }
            )
            .OrderBy(k => k)
            .ToArray();



            foreach (var item in OrderedReferences)
            {
                var _Reference = _References.Add(item);
                _Reference.IsExpanded = true;

                if (sln.NuGetReferences.Any(k => k.id == item))
                {
                    // nuget pacages are special

                    // cant we have .important rule group?
                    _Reference.Element.TextArea.css.hover.style.setProperty(
                        "color", "blue", "important"
                    );

                    _Reference.Click +=
                        delegate
                        {
                            // go to jsc store
                            // or id we link up to that other service?
                            Native.window.open(
                                "http://my.jsc-solutions.net/#" + item
                            );

                        };

                    _Reference.WithIcon(() => new MergeAssemblyImage());

                }
                else
                {
                    //_Reference.Element.TextArea.style.color = "gray";

                    _Reference.WithIcon(() => new Assembly());

                }
            }
        }

        ISaveAction Save;


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.StockTypes
{
    public static class KnownStockTypes
    {
        // we are making hard assumptions on ScriptCoreLib types here.
        // would be nice if those types were generated.
        // ScriptCoreLib.Ultra.Documentation may be just for that?
        // or we should generate a SolutionBuilder from MSIL?
        // unless we want to use some ficttional type names?



        public static class java
        {
            public static class applet
            {
                public class Applet : SolutionProjectLanguageType
                {
                    public Applet()
                    {
                        Namespace = "java.applet";
                        Name = "Applet";
                    }
                }
            }
        }

        public static class System
        {
            public static class Windows
            {

                public static class Forms
                {
                    public class UserControl : SolutionProjectLanguageType
                    {
                        public UserControl()
                        {
                            Namespace = "System.Windows.Forms";
                            Name = "UserControl";
                        }
                    }
                }

                public static class Media
                {
                    public class Brushes : SolutionProjectLanguageType
                    {
                        public Brushes()
                        {
                            Namespace = "System.Windows.Media";
                            Name = "Brushes";
                        }

                        public class get_Red : SolutionProjectLanguageMethod
                        {
                            public get_Red()
                            {
                                DeclaringType = new KnownStockTypes.System.Windows.Media.Brushes();
                                IsStatic = true;
                                IsProperty = true;
                                Name = "get_Red";
                                ReturnType = new KnownStockTypes.System.Object();
                            }
                        }
                    }
                }

                public static class Shapes
                {
                    public class Rectangle : SolutionProjectLanguageType
                    {
                        public Rectangle()
                        {
                            Namespace = "System.Windows.Shapes";
                            Name = "Rectangle";
                        }

                        public class set_Fill : SolutionProjectLanguageMethod
                        {
                            public set_Fill()
                            {
                                DeclaringType = new Rectangle();
                                IsProperty = true;
                                Name = "set_Fill";
                            }
                        }

                    }
                }

                public static class Controls
                {
                    public class Canvas : SolutionProjectLanguageType
                    {
                        public Canvas()
                        {
                            BaseType = new FrameworkElement();

                            Namespace = "System.Windows.Controls";
                            Name = "Canvas";
                        }

                    }
                }

                public class FrameworkElement : SolutionProjectLanguageType
                {
                    public FrameworkElement()
                    {
                        Namespace = "System.Windows";
                        Name = "FrameworkElement";
                    }

                    public class add_SizeChanged : SolutionProjectLanguageMethod
                    {
                        public add_SizeChanged()
                        {
                            Name = "add_SizeChanged";
                            DeclaringType = new FrameworkElement();
                            IsEvent = true;
                        }
                    }

                    public class get_Width : SolutionProjectLanguageMethod
                    {
                        public get_Width()
                        {
                            IsProperty = true;
                            Name = "get_Width";
                            DeclaringType = new FrameworkElement();
                            ReturnType = new KnownStockTypes.System.Double();
                        }
                    }

                    public class get_Height : SolutionProjectLanguageMethod
                    {
                        public get_Height()
                        {
                            IsProperty = true;
                            Name = "get_Height";
                            DeclaringType = new FrameworkElement();
                            ReturnType = new KnownStockTypes.System.Double();
                        }
                    }
                }
            }

            public static class ComponentModel
            {
                public class IContainer : SolutionProjectLanguageType
                {
                    public IContainer()
                    {
                        Namespace = "System.ComponentModel";
                        Name = "IContainer";
                    }
                }
            }

            public static class Xml
            {
                public static class Linq
                {
                    public class XElement : SolutionProjectLanguageType
                    {
                        public XElement()
                        {
                            Namespace = "System.Xml.Linq";
                            Name = "XElement";
                        }

                    }
                }
            }


            public class String : SolutionProjectLanguageType
            {
                public String()
                {
                    Namespace = "System";
                    Name = "String";
                }

            }

            public class Double : SolutionProjectLanguageType
            {
                public Double()
                {
                    Namespace = "System";
                    Name = "Double";
                }

            }

            public class Boolean : SolutionProjectLanguageType
            {
                public Boolean()
                {
                    Namespace = "System";
                    Name = "Boolean";
                }

            }

            public class Int32 : SolutionProjectLanguageType
            {
                public Int32()
                {
                    Namespace = "Int32";
                    Name = "Int32";
                }

            }

            public class Object : SolutionProjectLanguageType
            {
                public Object()
                {
                    Namespace = "System";
                    Name = "Object";
                }

            }
        }

        public static class ScriptCoreLib
        {
            public static class Desktop
            {
                public static class Forms
                {
                    public static class Extensions
                    {
                        public class DesktopFormsExtensions : SolutionProjectLanguageType
                        {
                            public DesktopFormsExtensions()
                            {
                                Namespace = "ScriptCoreLib.Desktop.Forms.Extensions";
                                Name = "DesktopFormsExtensions";
                            }

                            public class Launch : SolutionProjectLanguageMethod
                            {
                                public Launch()
                                {
                                    Name = "Launch";
                                    IsStatic = true;
                                    DeclaringType = new DesktopFormsExtensions();
                                    ReturnType = new KnownStockTypes.System.Object();
                                }
                            }
                        }
                    }
                }

                public static class Extensions
                {
                    public class DesktopAvalonExtensions : SolutionProjectLanguageType
                    {
                        public DesktopAvalonExtensions()
                        {
                            Namespace = "ScriptCoreLib.Desktop.Extensions";
                            Name = "DesktopAvalonExtensions";
                        }

                        public class Launch : SolutionProjectLanguageMethod
                        {
                            public Launch()
                            {
                                Name = "Launch";
                                IsStatic = true;
                                DeclaringType = new DesktopAvalonExtensions();
                                ReturnType = new KnownStockTypes.System.Object();
                            }
                        }
                    }
                }
            }

            public static class Java
            {
                public static class Extensions
                {
                    public class WindowsFormsExtensions : SolutionProjectLanguageType
                    {
                        public WindowsFormsExtensions()
                        {
                            Namespace = "ScriptCoreLib.Java.Extensions";
                            Name = "WindowsFormsExtensions";
                        }

                        public class AutoSizeTo : SolutionProjectLanguageMethod
                        {
                            public AutoSizeTo()
                            {
                                Name = "AutoSizeTo";
                                IsExtensionMethod = true;
                                IsStatic = true;
                                DeclaringType = new WindowsFormsExtensions();
                            }
                        }

                        public class EnableVisualStyles : SolutionProjectLanguageMethod
                        {
                            public EnableVisualStyles()
                            {
                                Name = "EnableVisualStyles";
                                IsExtensionMethod = true;
                                IsStatic = true;
                                DeclaringType = new WindowsFormsExtensions();
                            }
                        }

                        public class AttachTo : SolutionProjectLanguageMethod
                        {
                            public AttachTo()
                            {
                                Name = "AttachTo";
                                IsExtensionMethod = true;
                                IsStatic = true;
                                DeclaringType = new WindowsFormsExtensions();
                            }
                        }
                    }
                }
            }

            public static class JavaScript
            {
                public static class Windows
                {
                    public static class Forms
                    {
                        public class WindowsFormsExtensions : SolutionProjectLanguageType
                        {
                            public WindowsFormsExtensions()
                            {
                                Namespace = "ScriptCoreLib.JavaScript.Windows.Forms";
                                Name = "WindowsFormsExtensions";
                            }
                            public class AutoSizeControlTo : SolutionProjectLanguageMethod
                            {
                                public AutoSizeControlTo()
                                {
                                    Name = "AutoSizeControlTo";
                                    IsExtensionMethod = true;
                                    IsStatic = true;
                                    DeclaringType = new WindowsFormsExtensions();
                                    ReturnType = new KnownStockTypes.System.Object();
                                }
                            }
                            public class AttachControlTo : SolutionProjectLanguageMethod
                            {
                                public AttachControlTo()
                                {
                                    Name = "AttachControlTo";
                                    IsExtensionMethod = true;
                                    IsStatic = true;
                                    DeclaringType = new WindowsFormsExtensions();
                                    ReturnType = new KnownStockTypes.System.Object();
                                }
                            }
                        }
                    }
                }

                public static class DOM
                {
                    public static class HTML
                    {
                        public class IHTMLElement : SolutionProjectLanguageType
                        {
                            public IHTMLElement()
                            {
                                Namespace = "ScriptCoreLib.JavaScript.DOM.HTML";
                                Name = "IHTMLElement";
                            }
                        }
                    }
                }

                public static class Extensions
                {
                    public class AppletExtensions : SolutionProjectLanguageType
                    {
                        public AppletExtensions()
                        {
                            Namespace = "ScriptCoreLib.JavaScript.Extensions";
                            Name = "AppletExtensions";
                        }

                        public class AttachAppletTo : SolutionProjectLanguageMethod
                        {
                            public AttachAppletTo()
                            {
                                Name = "AttachAppletTo";
                                IsExtensionMethod = true;
                                IsStatic = true;
                                DeclaringType = new AppletExtensions();
                                ReturnType = new KnownStockTypes.System.Object();
                            }
                        }

                        public class AutoSizeAppletTo : SolutionProjectLanguageMethod
                        {
                            public AutoSizeAppletTo()
                            {
                                Name = "AutoSizeAppletTo";
                                IsExtensionMethod = true;
                                IsStatic = true;
                                DeclaringType = new AppletExtensions();
                                ReturnType = new KnownStockTypes.System.Object();
                            }
                        }
                    }

                    public class SpriteExtensions : SolutionProjectLanguageType
                    {
                        public SpriteExtensions()
                        {
                            Namespace = "ScriptCoreLib.JavaScript.Extensions";
                            Name = "SpriteExtensions";
                        }

                        public class AutoSizeSpriteTo : SolutionProjectLanguageMethod
                        {
                            public AutoSizeSpriteTo()
                            {
                                Name = "AutoSizeSpriteTo";
                                IsExtensionMethod = true;
                                IsStatic = true;
                                DeclaringType = new AvalonUltraExtensions();
                                ReturnType = new KnownStockTypes.System.Object();
                            }
                        }

                        public class AttachSpriteTo : SolutionProjectLanguageMethod
                        {
                            public AttachSpriteTo()
                            {
                                Name = "AttachSpriteTo";
                                IsExtensionMethod = true;
                                IsStatic = true;
                                DeclaringType = new AvalonUltraExtensions();
                                ReturnType = new KnownStockTypes.System.Object();
                            }
                        }
                    }

                    public class AvalonUltraExtensions : SolutionProjectLanguageType
                    {
                        public AvalonUltraExtensions()
                        {
                            Namespace = "ScriptCoreLib.JavaScript.Extensions";
                            Name = "AvalonUltraExtensions";
                        }

                        public class AutoSizeTo : SolutionProjectLanguageMethod
                        {
                            public AutoSizeTo()
                            {
                                Name = "AutoSizeTo";
                                IsExtensionMethod = true;
                                IsStatic = true;
                                DeclaringType = new AvalonUltraExtensions();
                                ReturnType = new KnownStockTypes.System.Object();
                            }
                        }
                    }

                    public class AvalonExtensions : SolutionProjectLanguageType
                    {
                        public AvalonExtensions()
                        {
                            Namespace = "ScriptCoreLib.JavaScript.Extensions";
                            Name = "AvalonExtensions";
                        }

                        public class AttachToContainer : SolutionProjectLanguageMethod
                        {
                            public AttachToContainer()
                            {
                                Name = "AttachToContainer";
                                IsExtensionMethod = true;
                                IsStatic = true;
                                DeclaringType = new AvalonExtensions();
                                ReturnType = new KnownStockTypes.System.Object();
                            }
                        }
                    }

                    public class JavaScriptStringExtensions : SolutionProjectLanguageType
                    {
                        public JavaScriptStringExtensions()
                        {
                            Namespace = "ScriptCoreLib.JavaScript.Extensions";
                            Name = "JavaScriptStringExtensions";
                        }

                        public class ToDocumentTitle : SolutionProjectLanguageMethod
                        {
                            public ToDocumentTitle()
                            {
                                Name = "ToDocumentTitle";
                                IsExtensionMethod = true;
                                IsStatic = true;
                                DeclaringType = new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.JavaScriptStringExtensions();
                                ReturnType = new KnownStockTypes.System.String();
                            }
                        }
                    }
                }
            }

            public static class ActionScript
            {
                const string Namespace = "ScriptCoreLib.ActionScript";

                public static class Extensions
                {
                    const string Namespace = ActionScript.Namespace + ".Extensions";

                    public class ActionScriptAvalonExtensions : SolutionProjectLanguageType
                    {
                        public ActionScriptAvalonExtensions()
                        {
                            Namespace = Namespace;
                            Name = "ActionScriptAvalonExtensions";
                        }

                        public class AutoSizeTo : SolutionProjectLanguageMethod
                        {
                            public AutoSizeTo()
                            {
                                Name = "AutoSizeTo";
                                IsStatic = true;
                                IsExtensionMethod = true;
                                DeclaringType = new AvalonExtensions();
                                ReturnType = new KnownStockTypes.System.Object();
                            }
                        }
                    }
                    public class AvalonExtensions : SolutionProjectLanguageType
                    {
                        public AvalonExtensions()
                        {
                            Namespace = "ScriptCoreLib.ActionScript.Extensions";
                            Name = "AvalonExtensions";
                        }

                        public class AttachToContainer : SolutionProjectLanguageMethod
                        {
                            public AttachToContainer()
                            {
                                Name = "AttachToContainer";
                                IsStatic = true;
                                IsExtensionMethod = true;
                                DeclaringType = new AvalonExtensions();
                                ReturnType = new KnownStockTypes.System.Object();

                            }
                        }
                    }

                    public class CommonExtensions : SolutionProjectLanguageType
                    {
                        public CommonExtensions()
                        {
                            Namespace = "ScriptCoreLib.ActionScript.Extensions";
                            Name = "CommonExtensions";
                        }

                        public class InvokeWhenStageIsReady : SolutionProjectLanguageMethod
                        {
                            public InvokeWhenStageIsReady()
                            {
                                Name = "InvokeWhenStageIsReady";
                                IsStatic = true;
                                IsExtensionMethod = true;
                                DeclaringType = new CommonExtensions();
                            }
                        }
                    }
                }

                public static class flash
                {
                    public static class display
                    {
                        public class DisplayObject : SolutionProjectLanguageType
                        {
                            public DisplayObject()
                            {
                                Namespace = "ScriptCoreLib.ActionScript.flash.display";
                                Name = "DisplayObject";
                            }

                            public class get_stage : SolutionProjectLanguageMethod
                            {
                                public get_stage()
                                {
                                    Name = "get_stage";
                                    IsProperty = true;
                                    DeclaringType = new DisplayObject();
                                }
                            }
                        }

                        public class Sprite : SolutionProjectLanguageType
                        {
                            public Sprite()
                            {
                                Namespace = "ScriptCoreLib.ActionScript.flash.display";
                                Name = "Sprite";
                            }

                        }
                    }

                }

            }

            public static class Delegates
            {
                public class StringAction : SolutionProjectLanguageType
                {
                    public StringAction()
                    {
                        Namespace = "ScriptCoreLib.Delegates";
                        Name = "StringAction";
                    }
                }
            }

            public static class Shared
            {
                public static class Avalon
                {
                    public static class Extensions
                    {
                        public class SupportsContainerExtensions : SolutionProjectLanguageType
                        {
                            public SupportsContainerExtensions()
                            {
                                this.Namespace = "ScriptCoreLib.Shared.Avalon.Extensions";
                                this.Name = "SupportsContainerExtensions";
                            }

                            public class AttachTo : SolutionProjectLanguageMethod
                            {
                                public AttachTo()
                                {
                                    Name = "AttachTo";
                                    IsStatic = true;
                                    IsExtensionMethod = true;
                                    DeclaringType = new SupportsContainerExtensions();
                                    ReturnType = new KnownStockTypes.System.Object();
                                }
                            }

                            public class MoveTo : SolutionProjectLanguageMethod
                            {
                                public MoveTo()
                                {
                                    Name = "MoveTo";
                                    IsStatic = true;
                                    IsExtensionMethod = true;
                                    DeclaringType = new SupportsContainerExtensions();
                                    ReturnType = new KnownStockTypes.System.Object();
                                }
                            }

                            public class SizeTo : SolutionProjectLanguageMethod
                            {
                                public SizeTo()
                                {
                                    Name = "SizeTo";
                                    IsStatic = true;
                                    IsExtensionMethod = true;
                                    DeclaringType = new SupportsContainerExtensions();
                                    ReturnType = new KnownStockTypes.System.Object();
                                }
                            }
                        }
                    }
                }

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.Ultra.Studio.StockTypes
{
    [Description("Namespace and Extension management")]
    public static class KnownStockTypes
    {
        // we are making hard assumptions on ScriptCoreLib types here.
        // would be nice if those types were generated.
        // ScriptCoreLib.Ultra.Documentation may be just for that?
        // or we should generate a SolutionBuilder from MSIL?
        // unless we want to use some ficttional type names?


        #region java.applet.Applet
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
        #endregion


        #region ScriptCoreLib.DOM.HTML.IHTMLElement
        public static partial class ScriptCoreLib
        {
            public static partial class JavaScript
            {
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
            }
        }
        #endregion


        #region ScriptCoreLib.ActionScript.flash.display.DisplayObject
        public static partial class ScriptCoreLib
        {
            public static partial class ActionScript
            {
                public static partial class flash
                {
                    public static partial class display
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

                    }
                }
            }
        }
        #endregion


        #region ScriptCoreLib.ActionScript.flash.display.Sprite
        public static partial class ScriptCoreLib
        {
            public static partial class ActionScript
            {
                public static partial class flash
                {
                    public static partial class display
                    {
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
        }
        #endregion


        #region ScriptCoreLib.Extensions.LinqExtensions
        public static partial class ScriptCoreLib
        {
            public static partial class Extensions
            {
                public class LinqExtensions : SolutionProjectLanguageType
                {
                    public LinqExtensions()
                    {
                        Namespace = "ScriptCoreLib.Extensions";
                        Name = "LinqExtensions";
                    }

                    public class With : SolutionProjectLanguageMethod
                    {
                        public With()
                        {
                            Name = "With";
                            IsStatic = true;
                            IsExtensionMethod = true;
                            DeclaringType = new KnownStockTypes.ScriptCoreLib.Extensions.LinqExtensions();
                        }
                    }
                }
            }
        }
        #endregion

        #region ScriptCoreLib.ActionScript.flash.media.Camera
        public static partial class ScriptCoreLib
        {
            public static partial class ActionScript
            {
                public static partial class flash
                {
                    public static partial class media
                    {
                        public class Camera : SolutionProjectLanguageType
                        {
                            public Camera()
                            {
                                Namespace = "ScriptCoreLib.ActionScript.flash.media";
                                Name = "Camera";
                            }


                            public class setMode : SolutionProjectLanguageMethod
                            {
                                public setMode()
                                {
                                    Name = "setMode";
                                    DeclaringType = new KnownStockTypes.ScriptCoreLib.ActionScript.flash.media.Camera();
                                }
                            }

                            public class getCamera : SolutionProjectLanguageMethod
                            {
                                public getCamera()
                                {
                                    Name = "getCamera";
                                    IsStatic = true;
                                    DeclaringType = new KnownStockTypes.ScriptCoreLib.ActionScript.flash.media.Camera();
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region ScriptCoreLib.ActionScript.flash.media.Camera
        public static partial class ScriptCoreLib
        {
            public static partial class ActionScript
            {
                public static partial class flash
                {
                    public static partial class media
                    {
                        public class Video : SolutionProjectLanguageType
                        {
                            public Video()
                            {
                                Namespace = "ScriptCoreLib.ActionScript.flash.media";
                                Name = "Video";
                            }

                            public class attachCamera : SolutionProjectLanguageMethod
                            {
                                public attachCamera()
                                {
                                    Name = "attachCamera";
                                    DeclaringType = new KnownStockTypes.ScriptCoreLib.ActionScript.flash.media.Video();
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region ScriptCoreLib.JavaScript.Extensions
        public static partial class ScriptCoreLib
        {
            public static partial class JavaScript
            {
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
                                DeclaringType = new SpriteExtensions();
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



                    public class INodeExtensions : SolutionProjectLanguageType
                    {
                        public INodeExtensions()
                        {
                            Namespace = "ScriptCoreLib.JavaScript.Extensions";
                            Name = "INodeExtensions";
                        }

                        public class AttachToHead : SolutionProjectLanguageMethod
                        {
                            public AttachToHead()
                            {
                                Name = "AttachToHead";
                                IsExtensionMethod = true;
                                IsStatic = true;
                                DeclaringType = new KnownStockTypes.ScriptCoreLib.JavaScript.Extensions.INodeExtensions();
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
        }
        #endregion


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

                public class Component : SolutionProjectLanguageType
                {
                    public Component()
                    {
                        Namespace = "System.ComponentModel";
                        Name = "Component";
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

            public class ActionOfString : SolutionProjectLanguageType
            {
                public ActionOfString()
                {
                    Namespace = "System";
                    Name = "Action";

                    Arguments.Add(
                        new KnownStockTypes.System.String()
                    );
                }
            }
        }



        public static partial class ScriptCoreLib
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

            public static partial class JavaScript
            {
                public class FormExtensions : SolutionProjectLanguageType
                {
                    public FormExtensions()
                    {
                        Namespace = "ScriptCoreLib.JavaScript";
                        Name = "FormExtensions";
                    }
             
                    public class AttachControlToDocument : SolutionProjectLanguageMethod
                    {
                        public AttachControlToDocument()
                        {
                            Name = "AttachControlToDocument";
                            IsExtensionMethod = true;
                            IsStatic = true;
                            DeclaringType = new FormExtensions();
                            ReturnType = new KnownStockTypes.System.Object();
                        }
                    }
                }

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



            }

            public static partial class ActionScript
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



            }

            [Obsolete]
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

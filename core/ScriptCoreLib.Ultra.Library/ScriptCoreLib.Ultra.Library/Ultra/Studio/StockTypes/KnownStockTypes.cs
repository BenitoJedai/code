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

                public static class Controls
                {
                    public class Canvas : SolutionProjectLanguageType
                    {
                        public Canvas()
                        {
                            Namespace = "System.Windows.Controls";
                            Name = "Canvas";
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

            public class Boolean : SolutionProjectLanguageType
            {
                public Boolean()
                {
                    Namespace = "System";
                    Name = "Boolean";
                }

            }
        }

        public static class ScriptCoreLib
        {
            public static class JavaScript
            {
                public static class Extensions
                {
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
                public static class flash
                {
                    public static class display
                    {
                        public class Sprite : SolutionProjectLanguageType
                        {
                            public Sprite()
                            {
                                Namespace = "System.ActionScript.flash.display";
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
        }
    }
}

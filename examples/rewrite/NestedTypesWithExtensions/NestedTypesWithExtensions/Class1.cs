using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "merge")]

namespace NestedTypesWithExtensions
{
    public class SolutionProjectLanguageType
    {
        public string Name;
        public string Namespace = "";
    }

    public class SolutionProjectLanguageMethod
    {
        public string Name;
        public bool IsStatic;
        public bool IsExtensionMethod;
        public SolutionProjectLanguageType DeclaringType;
        public SolutionProjectLanguageType ReturnType;
    }

    public static class KnownStockTypes
    {
        public static class System
        {
            public class Object : SolutionProjectLanguageType
            {
                public Object()
                {
                    Namespace = "System";
                    Name = "Object";
                }

            }

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


                        public class AttachTo : SolutionProjectLanguageMethod
                        {
                            public AttachTo()
                            {
                                Name = "AttachTo";
                                IsStatic = true;
                                IsExtensionMethod = true;
                                DeclaringType = new UserControl();
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
                                DeclaringType = new UserControl();
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
                                DeclaringType = new UserControl();
                                ReturnType = new KnownStockTypes.System.Object();
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
                            //BaseType = new FrameworkElement();

                            Namespace = "System.Windows.Controls";
                            Name = "Canvas";
                        }

                    }
                }
            }
        }
    }

    public class Class1
    {
        public class Class2
        {
            Class1 Value;

            public void Invoke()
            {
                Value.Invoke();
                this.Invoke2();
            }
        }
    }

    public static class Extension
    {
        public static void Invoke<T>(this T e) where T : Class1
        {

        }

        public static void Invoke2<T>(this T e) where T : Class1.Class2
        {

        }
    }
}

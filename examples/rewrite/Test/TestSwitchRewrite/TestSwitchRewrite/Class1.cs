using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestSwitchRewrite
{


    public class Class1
    {
        Class1 checkBox1;
        public bool Checked { get; set; }


        public void MoveNext()
        {
            //Foo();

            //try
            //{
            //    ExceptionTryStart2();
            switch (i)
            {
                case 3:
                    try
                    {
                        //    ExceptionTryStart1();

                        //    var _this = this;
                        //    var _checkBox1 = _this.checkBox1;

                        //    var Checked = _checkBox1.Checked;

                        //    if (Checked)
                        throw null;

                        //    Foo();
                        //    ExceptionTryEnd1();
                    }
                    catch  //(Exception ex)
                    {
                        //ExceptionHandler1();

                    }
                //Foo();
                    break;

                //return;
                case 4:
                    //    {
                    //        Foo();
                    break;
                //    }
            }
            //    ExceptionTryEnd2();
            //}
            //catch // (Exception ex)
            //{
            //    ExceptionHandler2();

            //}

            //Foo();
        }


        static void ExceptionTryStart1()
        {
        }
        static void ExceptionTryEnd1()
        {
        }

        static void ExceptionTryStart2()
        {
        }
        static void ExceptionTryEnd2()
        {
        }

        static void ExceptionHandler1()
        {
        }

        static void ExceptionHandler2()
        {
        }

        static int Foo()
        {
            var i = 0;
            try
            {
                i = Bar();
            }
            catch
            {
                throw;
            }
            return i;
        }

        static int Bar()
        {
            return -1;
        }


        static int i;


    }
}

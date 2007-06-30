using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using System.Reflection;

namespace jsx.Tests
{


    public partial class ILTest
    {
        public static Action bu_Test = bu_Switch2;

        public static void bu_Switch2()
        {
            switch (GetInteger())
            {
                case 0:
                    Do0();
                    break;
                case 3:
                    Do3();
                    break;
                case 2:
                    Do2();
                    break;
                case 1:
                    Do1();
                    break;
                default:
                    Do();
                    break;

            }

            switch (GetInteger())
            {
                case 1:
                    Do1();
                    return;
                case 2:
                    Do2();
                    return;
                case 3:
                    Do3();
                    return;
                case 0:
                    Do0();
                    return;
                default:
                    Do();
                    return;

            }
        }

        private static void Do0()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public static void bu_Switch1()
        {
            DoIfElseInnerA();

            int i = 0;

            switch (GetInteger())
            {
                    /*
                default:
                    Do6();
                    //break;

                    goto case 0;*/

                default:
                case 0:
                    Do1();
                    break;
                case -1:
                    Do2();
                    return;
                case -2:
                    Do3();
                    throw null;
                case -3:

                    DoNothing();

                    goto skip1;
                next1:
                    DoSomething();

                    goto skip1;
                case -4:
                    DoNothing();
            skip1:
                    if (i++ > 4) goto next1;

                    Do5();
                    break;

            }

            DoIfElseInnerB();
        }

        private static void Do6()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private static void Do5()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private static void Do4()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private static void Do3()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public static void bu_PlainIf()
        {
            DoIfElseOuterA();

            if (GetBoolean())
            {
                DoIfElseInnerA();

                Debugger.Break();
            }

            DoIfElseOuterB();
        }

        public static void bu_PlainIfNested()
        {
            DoIfElseOuterA();

            if (GetBoolean())
            {
                DoIfElseInnerA();

                if (GetBoolean())
                {
                    DoIfElseInnerA();

                    Debugger.Break();
                }

                Debugger.Break();
            }

            DoIfElseOuterB();
        }


        public static void bu_PlainIfElse()
        {
            //cond: 0x0011
            // jump: 0x001b -> 0x0025
            DoIfElseOuterA();

            if (GetBoolean())
            {
                DoIfElseInnerA();
            }
            else
            {
                DoIfElseInnerB();
            }

            DoIfElseOuterB();

        }

        public static void bu_PlainIfElseNested()
        {
            //cond: 0x0011
            // jump: 0x001b -> 0x0025
            DoIfElseOuterA();

            if (GetBoolean())
            {
                DoIfElseInnerA();
            }

            if (GetBoolean())
            {
                DoIfElseInnerA();

                if (GetBoolean())
                {
                    DoIfElseInnerA();
                }
                else
                {
                    DoIfElseInnerB();
                }
            }
            else
            {
                DoIfElseInnerB();

                if (GetBoolean())
                {
                    DoIfElseInnerA();
                }
                else
                {
                    DoIfElseInnerB();
                }
            }

            DoIfElseOuterB();

        }

        public static void bu_For()
        {
            //cond: 0x0011
            // jump: 0x001b -> 0x0025

            for (int i = 0; i < GetInteger(); i++)
            {
                if (GetBoolean())
                    return;

                DoSomething();
            }

        }



        public static void bu_PlainIfElseOptimized()
        {
            if (GetBoolean()) goto @else;
            DoIfElseInnerA();
            return;

        @else: DoIfElseInnerB();
        }

        public static void bu_PlainIfNestedReturn()
        {
            //cond: 0x0011
            // join: 0x005f
            // throw: 0x0057
            // return: 0x0041
            //cond: 0x0024
            // join: 0x002e
            //cond: 0x0038
            // return: 0x0041
            //cond: 0x004d
            // throw: 0x0057
            DoIfElseOuterA();

            if (GetBoolean())
            {
                DoIfElseInnerA();


                if (GetBoolean())
                {
                    DoIfElseInnerA();
                }

                if (GetBoolean())
                {
                    DoIfElseInnerA();

                    return;
                }

                if (GetBoolean())
                {
                    DoIfElseInnerA();

                    throw null;
                }

                Debugger.Break();
            }

            DoIfElseOuterB();
        }
    }

}

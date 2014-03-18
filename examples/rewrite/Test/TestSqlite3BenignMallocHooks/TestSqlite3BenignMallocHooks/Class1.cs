using System;
using System.Diagnostics;

namespace Community.CsharpSqlite
{
    public partial class Sqlite3
    {
        public delegate void void_function();
        public struct BenignMallocHooks//
        {
            public void_function xBenignBegin;//void (*xBenignBegin)(void);
            public void_function xBenignEnd;    //void (*xBenignEnd)(void);
            public BenignMallocHooks(void_function xBenignBegin, void_function xBenignEnd)
            {
                this.xBenignBegin = xBenignBegin;
                this.xBenignEnd = xBenignEnd;
            }
        }
        static BenignMallocHooks sqlite3Hooks = new BenignMallocHooks(null, null);
        //static void sqlite3BenignMallocHooks(
        //    void_function xBenignBegin, //void (*xBenignBegin)(void),
        //    void_function xBenignEnd //void (*xBenignEnd)(void)
        //)
        //{

        //}


    }
}

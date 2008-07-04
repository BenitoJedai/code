using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ConvertASToCS.js.Any
{
    [Script]
    public class IndexInfo
    {
        public string Source;
        public string Subject;

        public int Index;

        public string BeforeSubject
        {
            get
            {
                return Source.Substring(0, Index);
            }
        }

        public string AfterSubject
        {
            get
            {
                return Source.Substring(Index + Subject.Length);
            }
        }
    }

    [Script]
    public class IndexInfoResult : IndexInfo
    {
        public string Text;
    }

    [Script]
    public class Tuple<T, F>
    {
        public T TValue;
        public F FValue;
    }
}

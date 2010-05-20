﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Studio.Languages
{
    public static class KnownLanguages
    {
        public static readonly VisualBasicLanguage VisualBasic = new VisualBasicLanguage();
        public static readonly VisualCSharpLanguage VisualCSharp = new VisualCSharpLanguage();
        public static readonly VisualFSharpLanguage VisualFSharp = new VisualFSharpLanguage();


        public static  IEnumerable<SolutionProjectLanguage> GetLanguages()
        {
            return new SolutionProjectLanguage[]
            {
                VisualCSharp,
                VisualBasic,
                VisualFSharp
            };
        }
    }
}

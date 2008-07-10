using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler
    {
        public enum Keywords
        {
            _public,
            _private,
            _override,
            _protected,
            _internal,
            _get,
            _set,
            _static,
            _function,



        }

        public void WriteKeyword(Keywords kw)
        {
            this.Write(kw.ToString().Substring(1));
        }

        public void WriteKeywordSpace(Keywords kw)
        {
            WriteKeyword(kw);
                WriteSpace();
        }
    }
}

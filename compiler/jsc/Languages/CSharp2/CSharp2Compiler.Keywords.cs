﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.CSharp2
{
    partial class CSharp2Compiler
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
            _namespace,
            _using,
            _class,



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

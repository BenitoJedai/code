using System;
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
            _lock,
            _as,
            _base,
            _event,
            _virtual,
            _null,
            _public,
            _private,
            _override,
            _protected,
            _internal,
            _interface,
            _enum,
            _delegate,
            _where,
            _get,
            _set,
            _static,
            _function,
            _namespace,
            _using,
            _class,
            _partial,
            _abstract,
            _sealed,
            _throw,
            _new,
            _var,
            _default,
			_readonly,
			_true,
			_false,
			_this,


        }

        public void WriteKeyword(Keywords kw)
        {
            this.Write(kw.ToString().Substring(1));
        }

        public IEnumerable<string> GetKeywords()
        {
            return Enum.GetNames(typeof(Keywords)).Select(i => i.Substring(1));
        }

        public void WriteKeywordSpace(Keywords kw)
        {
            WriteKeyword(kw);
            WriteSpace();
        }
    }
}

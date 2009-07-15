
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Threading;

using jsc.CodeModel;

using ScriptCoreLib;
using jsc.Script;

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {
        public enum Keywords
        {
			_false,
			_for,
            _this,
            _super,
			_as,
			_instanceof,
            _base,
			_event,
			_extends,
            _virtual,
            _null,
            _public,
            _private,
            _override,
            _protected,
            _internal,
			_interface,
			_implements,
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
			_return,
			_true,


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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Script.PHP
{
	partial class PHPCompiler
    {
        public enum Keywords
        {
			_arguments,
			_is,
			_as,
			_null,
			_return,
			_if,
			_this,
			_var,
			_new,
			_super,
            _public,
            _private,
            _override,
            _protected,
            _internal,
            _get,
            _set,
            _static,
            _function,
			_true,
			_extends,
			_implements,
			_interface,
			_instanceof,
			_abstract,
			_class,

			___construct

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

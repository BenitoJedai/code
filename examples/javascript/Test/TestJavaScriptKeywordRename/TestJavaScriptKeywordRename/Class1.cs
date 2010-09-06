using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
[assembly: Obfuscation(Feature = "script")]
namespace TestJavaScriptKeywordRename
{
    public class Class1
    {
        public string @this;

        public Class1(string @this)
        {
            this.@this = @this;
        }
    }
}

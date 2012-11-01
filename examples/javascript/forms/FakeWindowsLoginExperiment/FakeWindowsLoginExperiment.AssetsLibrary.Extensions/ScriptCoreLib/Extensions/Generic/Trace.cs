using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.Extensions
{
    public static class Trace
    {
        // extensions to use T or object
        // shall be in a special Generic namespace?

        // http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.callermembernameattribute.aspx

        public static T ToTrace<T>(this T message,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        {
            // move to ScriptCoreLib.Extensions
            // TpyeForwarding to be used to also support .NET 4 ?

            Console.WriteLine(
                "message: " + message
                + "\nmember name: " + memberName
                + "\nsource file path: " + sourceFilePath
                + "\nsource line number: " + sourceLineNumber);

            return message;
        }
    }
}

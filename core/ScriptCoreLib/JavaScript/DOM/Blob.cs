using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "Blob")]
    public class Blob
    {
        public readonly ulong size;

    }

    [Script]
    public static class BlobExtensions
    {
        public static string ToObjectURL(this Blob e)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\ScriptDynamicSourceBuilder\ScriptDynamicSourceBuilder\Application.cs

            Console.WriteLine("ToObjectURL");

            // is jsc trying to inline without the line above?
            return URL.createObjectURL(e);
        }
    }
}

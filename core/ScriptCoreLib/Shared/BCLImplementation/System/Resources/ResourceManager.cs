using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Resources
{
    [Script(Implements = typeof(global::System.Resources.ResourceManager))]
    public class __ResourceManager
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131217-picturebox
        // X:\jsc.svn\examples\javascript\forms\FormsPictureBox\FormsPictureBox\Properties\Resources.Designer.cs

        string __baseName;
        Assembly __assembly;

        public __ResourceManager(string baseName, Assembly assembly)
        {
            this.__baseName = baseName;
            this.__assembly = assembly;

            Console.WriteLine("__ResourceManager..ctor " + new { baseName, assembly });
            // global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FormsPictureBox.Properties.Resources", typeof(Resources).Assembly);

        }

        public static Func<string, Assembly, string, object> InternalGetObject;

        public virtual object GetObject(string name, CultureInfo culture)
        {
            Console.WriteLine("__ResourceManager.GetObject " + new { name, culture });

            var value = default(object);

            if (InternalGetObject != null)
                value = InternalGetObject(
                    this.__baseName,
                    this.__assembly,
                    name
                );


            //object obj = ResourceManager.GetObject("debitcard", resourceCulture);
            //return ((System.Drawing.Bitmap)(obj));

            //ScriptCoreLib.ActionScript.Extensions.KnownEmbeddedResources
            return value;
        }
    }
}

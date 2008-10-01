using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.VML
{
    [Script(InternalConstructor = true)]
    public class IVMLElementBase : IHTMLElement
    {
        [Script]
        public class Settings
        {
            public static bool IsSupported
            {
                get
                {
                    var namespaces = Native.Document.namespaces;

                    return namespaces != null;
                }
            }

            internal IStyleSheetRule Behaviour;
            internal IHTMLDocument.IMSNamespace Namespace;

            static Settings _Default;



            public Settings()
            {
                var namespaces = Native.Document.namespaces;

                if (namespaces == null)
                    throw new ArgumentNullException("namespaces");

                this.Namespace = namespaces.add("v", "urn:schemas-microsoft-com:vml");
                this.Behaviour = IStyleSheet.Default.AddRule("v\\:*", "behavior:url(#default#VML)", 0);
            }

            public static Settings GetDefault()
            {
                if (_Default == null)
                {
                    _Default = new Settings();
                }

                return _Default;
            }
        }


        protected IVMLElementBase()
        {

        }

        public IVMLElementBase(string tag)
        {
        }

        public static IVMLElementBase InternalConstructor(string tag)
        {
            Settings.GetDefault();

            return (IVMLElementBase)new IHTMLElement("v:" + tag);
        }

		// http://msdn.microsoft.com/en-us/library/bb263877(VS.85).aspx
		public int rotation;
    }
}

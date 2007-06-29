using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.LanguageProviders
{
    // description:
    // each language will have its provider
    // each provider can be extended or replaced

    public partial class CSharp3Provider : LanguageProvider { }
    public partial class PHP5Provider : LanguageProvider { }
    public partial class PHP4Provider : LanguageProvider { }
    public partial class ActionScript2Provider : LanguageProvider { }
    public partial class ActionScript3Provider : LanguageProvider { }
    public partial class JavaScriptProvider : LanguageProvider { }
    public partial class Java4Provider : LanguageProvider { }
    public partial class Java5Provider : LanguageProvider { }
    public partial class VisualBasic6Provider : LanguageProvider { }
}

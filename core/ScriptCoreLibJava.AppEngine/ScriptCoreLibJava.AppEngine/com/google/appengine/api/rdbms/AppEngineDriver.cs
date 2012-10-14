using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.google.appengine.api.rdbms
{
    // https://developers.google.com/appengine/docs/java/cloud-sql/developers-guide
    [Script(IsNative = true)]
    public class AppEngineDriver : java.sql.Driver
    {
    }
}

using System;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;



using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.JavaScript, "JavaScript")]

namespace JavaScript
{

    [Script]
    public sealed class U
    {
        public string A;
        public int B;
    }

    [Script]
    public sealed class UX
    {
        public string A;
        public U B;
    }
}

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public JavaScript.U HelloWorld()
    {
        JavaScript.U u = new JavaScript.U();

        u.A = "CLR:" + Environment.Version;
        u.B = 55;

        return u;
    }

    [WebMethod]
    public JavaScript.UX HelloWorld2()
    {
        JavaScript.UX u = new JavaScript.UX();

        u.A = "" + DateTime.Now.ToShortTimeString();

        u.B = HelloWorld();

        return u;
    }
    
}


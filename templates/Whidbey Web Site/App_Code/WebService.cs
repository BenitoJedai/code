using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.JavaScript, "JavaScript")]

namespace JavaScript
{
    [Script]
    internal class __Private
    {
        internal static ScriptCoreLib.Shared.Action ReferenceHack;
    }

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
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }



    [WebMethod]
    public JavaScript.U HelloWorld()
    {
        JavaScript.U u = new JavaScript.U();

        u.A = "Hello World";
        u.B = 55;

        return u;
    }

    [WebMethod]
    public JavaScript.UX HelloWorld2()
    {
        JavaScript.UX u = new JavaScript.UX();

        u.A = "Hello World2";
        u.B = HelloWorld();

        return u;
    }

}


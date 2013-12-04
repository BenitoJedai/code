using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FBLibrary;
using FBLibrary.Design;
using FBLibrary.HTML.Pages;

namespace FBLibrary
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/201312/20131204-appengine-db-test

            #region hosts file
            new IHTMLAnchor
            {
                href = "http://local.xfb.xavalon.com"
                    + ":" + Native.document.location.host.SkipUntilIfAny(":")

                ,
                innerText = "local.xfb.xavalon.com"
            }.AttachToDocument();
            #endregion

            new { appId = "766655520014699" }.With(
                async a =>
                {
                    await FB.init(appId: a.appId);

                    // is enum supported in async?
                    var status = await FB.getLoginStatus();

                    new IHTMLPre { innerText = new { status }.ToString() }.AttachToDocument();

                    Action AtConnected = async delegate
                    {


                        new IHTMLButton { "logout" }.AttachToDocument().WhenClicked(
                             async button =>
                             {
                                 await FB.logout();

                                 button.Orphanize();
                             }
                         );


                        var x = await FB.api();

                        // { id = 1527339800, third_party_id = RIU6rex1k09qGEvNbgBJKvl0VMc, 
                        // name = Xarvo Prism Xsulakatko, 
                        // accessToken = CAAK5ROXvgWsBALAloGSt1SpYidGiGyPqfZB1oYoPmWBuLRNG2dyAgkZBcEoG1VA9A5HJxgiZCLOLALIJ5SqStM8ZADbDkHCJswFDwTkiL4ScR7RJHYu1chkBx6UjgcuAl4BkI422jVwYmJ5godSndjonOZAq90eTqR8ZCZARswgd5OEhxE15nAi2Olvgu4VN94ZD }
                        new IHTMLPre
                        {
                            innerText = new
                            {
                                x.id,
                                x.third_party_id,
                                x.name,

                                //Error	4	'FB.UserProperties' does not contain a definition for 'age_range' and no extension method
                                // 'age_range' accepting a first argument of type 'FB.UserProperties' could be found (are you missing a using directive or an assembly reference?)
                                // X:\jsc.svn\market\synergy\javascript\FBLibrary\FBLibrary\Application.cs	82	35	FBLibrary
                                // Error	4	Cannot assign method group to anonymous type property	X:\jsc.svn\market\synergy\javascript\FBLibrary\FBLibrary\Application.cs	88	33	FBLibrary


                                //x.age_range,
                                x.gender,
                                x.verified
                                //x.accessToken 

                            }.ToString()
                        }.AttachToDocument();


                        // http://stackoverflow.com/questions/11442442/get-user-profile-picture-by-id

                        new IHTMLImage
                        {
                            src = "https://graph.facebook.com/" + x.id + "/picture"
                        }.AttachToDocument();

                        // http://stackoverflow.com/questions/7599638/how-to-get-large-photo-url-in-one-api-call
                        new IHTMLImage
                        {
                            src = "https://graph.facebook.com/" + x.id + "/picture?type=large"
                        }.AttachToDocument();

                    };

                    // { status = unknown }
                    //if (status == FB.LoginStatusEnum.connected)
                    if (status == "connected")
                    {
                        AtConnected();
                    }
                    else
                    {
                        new IHTMLButton { "login" }.AttachToDocument().WhenClicked(
                            async button =>
                            {

                                var loginstatus = await FB.login();
                                new IHTMLPre { innerText = new { loginstatus }.ToString() }.AttachToDocument();

                                //if (s == FB.LoginStatusEnum.connected)
                                if (loginstatus == "connected")
                                {
                                    button.Orphanize();

                                    AtConnected();
                                }
                            }
                        );
                    }
                }
            );


        }

    }
}

//static class X
//{
//    public static string age_range(this FB.UserProperties x)
//    {
//        return "";
//    }
//}
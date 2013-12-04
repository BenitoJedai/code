using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;
using ScriptCoreLib;
using FaceBook.API;

//namespace FBLibrary.Library
//{

namespace FaceBook.API
{
    [Script(IsStringEnum = true)]
    public enum FBLoginStatusEnum
    {
        unknown,

        connected,
        not_authorized,
        not_logged_in,
    }


    [Obsolete("if this class is used in java, it cannot be without namespace")]
    public sealed class FBUserProperties
    {
        /// <summary>
        /// The user's full name
        /// </summary>
        public string name;

        public string id;

        /// <summary>
        /// A string containing an anonymous, but unique identifier for the user. You can use this identifier with third-parties
        /// </summary>
        public string third_party_id;
        public string accessToken;

        //public string age_range;
        public string gender;

        /// <summary>
        /// Indicates whether the user account has been verified. 
        /// </summary>
        public string verified;
    }

}


[Obsolete("we are playing man in the middle for FB connect API. we do this manually for now")]
public class FB
{

    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/201312/20131204-appengine-db-test
    // https://developers.facebook.com/docs/reference/javascript/

    static IHTMLScript WhenReadyElement;

    static async Task WhenReady()
    {
        if (WhenReadyElement == null)
        {

            WhenReadyElement = new IHTMLScript
            {
                src = "//connect.facebook.net/en_US/all.js",

                //src = "http://connect.facebook.net/en_US/all/debug.js"
            };

            await WhenReadyElement;
        }

        // did we trust the caller to wait on the first result?
        return;
    }

    public static async Task init(
        string appId,
        bool status = true,
        bool cookie = true,
        bool oauth = true,
        bool xfml = true
        )
    {
        Console.WriteLine("enter FB.init " + new { appId, Native.document.location.href });

        await WhenReady();

        // http://stackoverflow.com/questions/13618098/fb-init-error-given-url-is-not-allowed-by-the-application-configuration

        //enter FB.init { appID = 625051627510580, href = http://192.168.1.86:13332/ }

        //The "fb-root" div has not been created, auto-creating debug.js:2763
        //Type Mismatch for appId: expected `string|number`, actual `undefined` ([object Window]) debug.js:310
        //Invalid App Id: Must be a number or numeric string representing the application id. 


        new IFunction("e", "return FB.init(e);").apply(null,
            new { appId, status, cookie, oauth, xfml }
        );

        return; // value? 
    }


    public static async Task<FBUserProperties> api()
    {
        Console.WriteLine("enter FB.api ");

        var x = new TaskCompletionSource<FBUserProperties>();

        Action<dynamic> AtAPI =
                response =>
                {
                    // https://developers.facebook.com/docs/reference/api/user/
                    string
                        name = response.name,
                        id = response.id,
                        third_party_id = response.third_party_id,
                        //age_range = response.age_range,
                        gender = response.gender,
                        verified = response.verified;

                    // http://stackoverflow.com/questions/4758770/how-to-get-access-token-from-fb-login-method-in-javascript-sdk
                    var accessToken = (string)new IFunction("return FB.getAuthResponse()['accessToken'];").apply(null);

                    x.SetResult(
                        new FBUserProperties
                        {
                            name = name,
                            third_party_id = third_party_id,
                            id = id,
                            accessToken = accessToken,
                            //age_range = age_range,
                            gender = gender,
                            verified = verified
                        }
                    );
                };



        // http://stackoverflow.com/questions/7365110/get-facebook-third-party-id-from-uid-in-javascript
        // ?fields=third_party_id
        // https://developers.facebook.com/docs/reference/login/public-profile-and-friend-list/
        new IFunction("e",
            "return FB.api('/me?fields=name,third_party_id,gender,verified', e);"

            ).apply(null, IFunction.OfDelegate(AtAPI));

        return await x.Task;
    }

    //response.status === 'connected' will be true whenever the User viewing the page is both logged into Facebook and has already previously authorized the current app.
    //response.status === 'not_authorized' is true whenever the User viewing the page is logged into Facebook, but has not yet authorized the current app. In this case, the FB.login() code shown in Step 4 can be used to prompt them to authenticate.
    //   // not_logged_in


    public static async Task<FBLoginStatusEnum> getLoginStatus()
    {
        Console.WriteLine("enter FB.getLoginStatus ");

        // was init done?

        //await WhenReady();


        var x = new TaskCompletionSource<FBLoginStatusEnum>();

        Action<dynamic> AtLoginStatus =
            response =>
            {
                string status = response.status;

                var xstatus = (FBLoginStatusEnum)(object)status;
                x.SetResult(xstatus);
            };

        Console.WriteLine("before getLoginStatus...");

        //enter FB.getLoginStatus 
        // view-source:29877
        //before getLoginStatus...
        // view-source:29877
        //Given URL is not allowed by the Application configuration.: One or more of the given URLs is not allowed by the App's settings.  It must match the Website URL or Canvas URL, or the domain must be a subdomain of one of the App's domains.

        // https://developers.facebook.com/docs/facebook-login/login-flow-for-web/

        new IFunction("e", "return FB.getLoginStatus(e);").apply(null, IFunction.OfDelegate(AtLoginStatus));


        return await x.Task;
    }


    public static async Task<FBLoginStatusEnum> login()
    {
        var x = new TaskCompletionSource<FBLoginStatusEnum>();

        Action<dynamic> AtLogin =
            response =>
            {
                //page.FacebookLogin.style.color = "";

                dynamic authResponse = response.authResponse;


                Console.WriteLine("AtLogin: " + new { authResponse });
                string status = response.status;
                var xstatus = (FBLoginStatusEnum)(object)status;

                Console.WriteLine("AtLogin: " + new { xstatus });



                x.SetResult(xstatus);

            };


        // !!
        // Under normal circumstances you should attach this FB.login() call to a Javascript onClick event 
        // as the call results in a popup window being opened, which will be blocked by most browsers.
        Console.WriteLine("FB.login...");
        new IFunction("e", "return FB.login(e);").apply(null, IFunction.OfDelegate(AtLogin));

        return await x.Task;
    }

    public static async Task logout()
    {
        var x = new TaskCompletionSource<object>();

        Action<dynamic> AtLogout =
         response =>
         {

             x.SetResult(new object());

         };


        // Now whenever the Log out button is clicked, the user will be logged out of your app, their session cleared and also logged out of Facebook. 
        // They will not, however, have authorization for your app revoked.
        Console.WriteLine("FB.logout...");
        new IFunction("e", "return FB.logout(e);").apply(null, IFunction.OfDelegate(AtLogout));

        await x.Task;

        return;
    }
}

//}

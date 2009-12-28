using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP
{
    public static partial class Native
    {
        /// <summary>
        /// http://ee.php.net/variables.predefined
        /// </summary>

        [Script]
        public static class SuperGlobals
        {

            #region PHP Superglobals

            #region $GLOBALS

            /// <summary>    
            /// Contains a reference to every variable which is currently available within the global scope of the script. The keys of this array are the names of the global variables. $GLOBALS has existed since PHP 3.    
            /// </summary>    
            static public IArray<string, IArray> Globals
            {
                [Script(OptimizedCode = @"
/* for some reason globals will be empty if we do not mention them on xampp */
$_SERVER;
$_GET;
$_POST;
$_COOKIE;
$_FILES;
$_ENV;
$_REQUEST;
$_SESSION;

return $GLOBALS;")]
                get
                {
                    return default(IArray<string, IArray>);
                }
            }

            #endregion

            #region $_SERVER

            [Script(IsStringEnum = true)]
            public enum ServerVariables
            {
                /// <summary>  
                ///     The filename of the currently executing script, relative to the document root. For instance, $_SERVER['PHP_SELF'] in a script at the address http://example.com/test.php/foo.bar would be /test.php/foo.bar. The __FILE__ constant contains the full path and filename of the current (i.e. included) file.  
                /// </summary>  
                PHP_SELF,
                /// <summary>  
                ///     Array of arguments passed to the script. When the script is run on the command line, this gives C-style access to the command line parameters. When called via the GET method, this will contain the query string.   
                /// </summary>  
                argv,
                /// <summary>  
                ///     Contains the number of command line parameters passed to the script (if run on the command line).   
                /// </summary>  
                argc,
                /// <summary>  
                ///     What revision of the CGI specification the server is using; i.e. 'CGI/1.1'.   
                /// </summary>  
                GATEWAY_INTERFACE,
                /// <summary>  
                ///     The name of the server host under which the current script is executing. If the script is running on a virtual host, this will be the value defined for that virtual host.   
                /// </summary>  
                SERVER_NAME,
                SERVER_ADDR,
                /// <summary>  
                ///     Server identification string, given in the headers when responding to requests.   
                /// </summary>  
                SERVER_SOFTWARE,
                /// <summary>  
                ///     Name and revision of the information protocol via which the page was requested; i.e. 'HTTP/1.0';   
                /// </summary>  
                SERVER_PROTOCOL,
                /// <summary>  
                ///     Which request method was used to access the page; i.e. 'GET', 'HEAD', 'POST', 'PUT'.  
                /// </summary>  
                REQUEST_METHOD,
                /// <summary>  
                ///     The timestamp of the start of the request. Available since PHP 5.1.0.   
                /// </summary>  
                REQUEST_TIME,
                /// <summary>  
                ///     The query string, if any, via which the page was accessed.   
                /// </summary>  
                QUERY_STRING,
                /// <summary>  
                ///     The document root directory under which the current script is executing, as defined in the server's configuration file.   
                /// </summary>  
                DOCUMENT_ROOT,
                /// <summary>  
                ///     Contents of the Accept: header from the current request, if there is one.   
                /// </summary>  
                HTTP_ACCEPT,
                /// <summary>  
                ///     Contents of the Accept-Charset: header from the current request, if there is one. Example: 'iso-8859-1,*,utf-8'.   
                /// </summary>  
                HTTP_ACCEPT_CHARSET,
                /// <summary>  
                ///     Contents of the Accept-Encoding: header from the current request, if there is one. Example: 'gzip'.   
                /// </summary>  
                HTTP_ACCEPT_ENCODING,
                /// <summary>  
                ///     Contents of the Accept-Language: header from the current request, if there is one. Example: 'en'.   
                /// </summary>  
                HTTP_ACCEPT_LANGUAGE,
                /// <summary>  
                ///     Contents of the Connection: header from the current request, if there is one. Example: 'Keep-Alive'.   
                /// </summary>  
                HTTP_CONNECTION,
                /// <summary>  
                ///     Contents of the Host: header from the current request, if there is one.   
                /// </summary>  
                HTTP_HOST,
                /// <summary>  
                ///     The address of the page (if any) which referred the user agent to the current page. This is set by the user agent. Not all user agents will set this, and some provide the ability to modify HTTP_REFERER as a feature. In short, it cannot really be trusted.   
                /// </summary>  
                HTTP_REFERER,
                /// <summary>  
                ///     Contents of the User-Agent: header from the current request, if there is one. This is a string denoting the user agent being which is accessing the page. A typical example is: Mozilla/4.5 [en] (X11; U; Linux 2.2.9 i586). Among other things, you can use this value with get_browser() to tailor your page's output to the capabilities of the user agent.   
                /// </summary>  
                HTTP_USER_AGENT,
                /// <summary>  
                ///     Set to a non-empty value if the script was queried through the HTTPS protocol.   
                /// </summary>  
                HTTPS,
                /// <summary>  
                ///     The IP address from which the user is viewing the current page.   
                /// </summary>  
                REMOTE_ADDR,
                /// <summary>  
                ///     The Host name from which the user is viewing the current page. The reverse dns lookup is based off the REMOTE_ADDR of the user.  
                /// </summary>  
                REMOTE_HOST,
                /// <summary>  
                ///     The port being used on the user's machine to communicate with the web server.   
                /// </summary>  
                REMOTE_PORT,
                /// <summary>  
                ///     The absolute pathname of the currently executing script.  
                /// </summary>  
                SCRIPT_FILENAME,
                /// <summary>  
                ///     The value given to the SERVER_ADMIN (for Apache) directive in the web server configuration file. If the script is running on a virtual host, this will be the value defined for that virtual host.   
                /// </summary>  
                SERVER_ADMIN,
                /// <summary>  
                ///     The port on the server machine being used by the web server for communication. For default setups, this will be '80'; using SSL, for instance, will change this to whatever your defined secure HTTP port is.   
                /// </summary>  
                SERVER_PORT,
                /// <summary>  
                ///     String containing the server version and virtual host name which are added to server-generated pages, if enabled.   
                /// </summary>  
                SERVER_SIGNATURE,
                /// <summary>  
                ///     Filesystem- (not document root-) based path to the current script, after the server has done any virtual-to-real mapping.  
                /// </summary>  
                PATH_TRANSLATED,
                /// <summary>  
                ///     Contains the current script's path. This is useful for pages which need to point to themselves. The __FILE__ constant contains the full path and filename of the current (i.e. included) file.   
                /// </summary>  
                SCRIPT_NAME,
                /// <summary>  
                ///     The URI which was given in order to access this page; for instance, '/index.html'.   
                /// </summary>  
                REQUEST_URI,
                /// <summary>  
                ///     When running under Apache as module doing Digest HTTP authentication this variable is set to the 'Authorization' header sent by the client (which you should then use to make the appropriate validation).   
                /// </summary>  
                PHP_AUTH_DIGEST,
                /// <summary>  
                ///     When running under Apache or IIS (ISAPI on PHP 5) as module doing HTTP authentication this variable is set to the username provided by the user.   
                /// </summary>  
                PHP_AUTH_USER,
                /// <summary>  
                ///     When running under Apache or IIS (ISAPI on PHP 5) as module doing HTTP authentication this variable is set to the password provided by the user.   
                /// </summary>  
                PHP_AUTH_PW,
                /// <summary>  
                ///     When running under Apache as module doing HTTP authenticated this variable is set to the authentication type.   
                /// </summary>  
                AUTH_TYPE
            }

            /// <summary>    
            /// Variables set by the web server or otherwise directly related to the execution environment of the current script. Analogous to the old $HTTP_SERVER_VARS array (which is still available, but deprecated).    
            /// </summary>    
            static readonly public IArray<ServerVariables, string> Server = (IArray<ServerVariables, string>)Globals["_SERVER"];

            #endregion

            #region $_GET

            /// <summary>    
            /// Variables provided to the script via URL query string. Analogous to the old $HTTP_GET_VARS array (which is still available, but deprecated).    
            /// </summary>    
            static readonly public IArray<string, string> Get = (IArray<string, string>)Globals["_GET"];


            #endregion
            #region $_POST

            /// <summary>    
            /// Variables provided to the script via HTTP POST. Analogous to the old $HTTP_POST_VARS array (which is still available, but deprecated).    
            /// </summary>    
            static readonly public IArray<string, string> Post = (IArray<string, string>)Globals["_POST"];


            #endregion
            #region $_COOKIE

            /// <summary>    
            /// Variables provided to the script via HTTP cookies. Analogous to the old $HTTP_COOKIE_VARS array (which is still available, but deprecated).    
            /// </summary>    
            static readonly public IArray<string, string> Cookie = (IArray<string, string>)Globals["_COOKIE"];


            #endregion
            #region $_FILES

            /// <summary>    
            /// Variables provided to the script via HTTP post file uploads. Analogous to the old $HTTP_POST_FILES array (which is still available, but deprecated). See POST method uploads for more information.    
            /// </summary>    
            static readonly public IArray Files = Globals["_FILES"];


            #endregion
            #region $_ENV

            /// <summary>    
            /// Variables provided to the script via the environment. Analogous to the old $HTTP_ENV_VARS array (which is still available, but deprecated).    
            /// </summary>    
            static readonly public IArray Env = Globals["_ENV"];


            #endregion
            #region $_REQUEST

            /// <summary>    
            /// Variables provided to the script via the GET, POST, and COOKIE input mechanisms, and which therefore cannot be trusted. The presence and order of variable inclusion in this array is defined according to the PHP variables_order configuration directive. This array has no direct analogue in versions of PHP prior to 4.1.0. See also import_request_variables().    
            /// </summary>    
            static readonly public IArray Request = Globals["_REQUEST"];

            #endregion
            #region $_SESSION

            /// <summary>    
            /// Variables which are currently registered to a script's session. Analogous to the old $HTTP_SESSION_VARS array (which is still available, but deprecated). See the Session handling functions section for more information.    
            /// </summary>    
            static public IArray<string> Session
            {
                get
                {
                    return (IArray<string>)Globals["_SESSION"];
                }
                set
                {
                    Globals["_SESSION"] = value;
                }
            }


            #endregion

            #endregion

        }
    }
}

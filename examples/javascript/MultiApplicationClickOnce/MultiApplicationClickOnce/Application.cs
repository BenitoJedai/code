using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using MultiApplicationClickOnce.Design;
using MultiApplicationClickOnce.HTML.Pages;

namespace MultiApplicationClickOnce
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            new IHTMLButton { innerText = "open Other page" }.AttachToDocument().onclick +=
                delegate
                {
                    Native.Window.open("/Other", "_self");

                };

            new IHTMLButton { innerText = "load Other page into this once" }.AttachToDocument().onclick +=
                delegate
                {
                    new IHTMLScript { src = "MultiApplicationClickOnce.OtherApplication.exe.js" }.AttachToDocument();

                };

            new IHTMLAnchor {
                href =  @"javascript:
((function(h,i)
{ 
    var a=-1, 
    b='onreadystatechange', 
    c=document.getElementsByTagName('HEAD')[0], 
    d, 
    e, 
    f, 
    g=c.childNodes, 
    d; 

    d = function () 
    {  
        next: while (1)  
        {   
            a++;     
            if (a ==h.length)   
            {   
                i();    
                return;   
            }     

            /*
            for (f=0;f<g.length;f++)   
            {   
                var v =g[f];    
                var w =h[a];       

                if (v.nodeName =='SCRIPT')    
                    if (v.src ==w || v.src.substr(v.src.length - w.length - 1,w.length + 1) =='/' + w)    
                        continue next;   
            } */      
            e=document.createElement('SCRIPT');   
            e.src='%%' + h[a];     
            e[b in e?b:'onload']=  function()    
            {       
                var f=e.readyState;    
                if(f==null||f=='loaded'||f=='complete')     
                    d();    
            };  
 
        c.appendChild(e);     
        return;  
    } 
}; 
d();

}

)(['ScriptCoreLib.dll.js','MultiApplicationClickOnce.OtherApplication.exe.js'],function(){}))".Replace("%%", Native.Document.location + ""),
                
                innerText = "Click to open"
            }.AttachToDocument();



            //Native.Document.getElementsByTagName


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }

    public sealed class OtherApplication
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public OtherApplication(IOtherPage page)
        {
            new IHTMLButton { innerText = "open Default page" }.AttachToDocument().onclick +=
               delegate
               {
                   Native.Window.open("/", "_self");

               };

            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"other",
                value => value.ToDocumentTitle()
            );
        }

    }
}

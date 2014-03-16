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
using DraggableExperiment.Design;
using DraggableExperiment.HTML.Pages;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.Controls;

namespace DraggableExperiment
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
            // http://dev.w3.org/html5/spec/dnd.html#dom-draggable

            //if (page == null)
            //{
            Native.window.alert("DraggableExperiment");
            //    return;
            //}

            var href = @"javascript:
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

    e=document.createElement('base');   
    e.href='%%';
    c.appendChild(e);     

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

)(['view-source'],function(){}))".Replace("%%", Native.Document.location + "");

            if (page.xdrag != null)
                page.xdrag.ondragstart +=
                    e =>
                    {
                        //e.dataTransfer.setData("text/uri-list", Native.Document.location.ToString());
                        e.dataTransfer.setData("text/uri-list", href);

                    };




            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}

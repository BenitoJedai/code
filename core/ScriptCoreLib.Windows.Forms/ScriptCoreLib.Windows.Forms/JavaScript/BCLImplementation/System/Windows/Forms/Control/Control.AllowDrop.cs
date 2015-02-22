using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.JavaScript.DOM;
namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{



    public partial class __Control : __Component
    {


        // x:\jsc.svn\examples\javascript\forms\formstreeviewdrag\formstreeviewdrag\applicationcontrol.cs
        //protected Action<object, DragDropEffects> InternalDoDragDrop;
        public static Action<Control, object, DragDropEffects> InternalUIDoDragDrop;

        public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
        {
            // https://github.com/adobe/chromium/blob/master/webkit/glue/webdropdata.cc
            // http://src.chromium.org/viewvc/chrome/trunk/src/webkit/glue/webdropdata.cc?pathrev=128579
            //e.dataTransfer.setData("text/uri-list", "http://my.jsc-solutions.net");

            //Console.WriteLine("enter DoDragDrop " + new { InternalUIDoDragDrop });


            // tested by?

            // .AllowDrop?
            // X:\jsc.svn\examples\javascript\forms\FormsTreeViewDrag\FormsTreeViewDrag\ApplicationControl.cs

            if (InternalUIDoDragDrop != null)
                InternalUIDoDragDrop(this, data, allowedEffects);


            //Console.WriteLine("exit DoDragDrop");
            return allowedEffects;
        }

        public event EventHandler DragLeave
        {
            add
            {
                this.HTMLTargetRef.ondragleave +=
                  e =>
                  {
                      e.stopPropagation();
                      e.preventDefault();

                      value(this, null);
                  };

            }

            remove { }
        }
        public event DragEventHandler DragDrop
        {
            add
            {

                this.HTMLTargetRef.ondrop +=
                   e =>
                   {
                       e.stopPropagation();
                       e.preventDefault();

                       var files = e.dataTransfer.files;


                       ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__File.InternalFiles =

                                Enumerable.Range(
                                  0,
                                  (int)files.length
                              ).Select(k => files[(uint)k]);

                       var a = new __DragEventArgs
                       {
                           Data = new __DataObject { InternalData = e }
                       };

                       value(this, (DragEventArgs)(object)a);



                   };

            }
            remove { }
        }


        // ?
        public event DragEventHandler DragEnter;

        public event DragEventHandler DragOver
        {
            add
            {
                // http://blogs.msdn.com/b/adamroot/archive/2008/02/19/shell-style-drag-and-drop-in-net-part-3.aspx

                this.HTMLTargetRef.ondragover +=
                    e =>
                    {
                        e.stopPropagation();
                        e.preventDefault();

                        var a = new __DragEventArgs
                        {
                            Data = new __DataObject { InternalData = e },
                            InternalSetEffect =
                                Effect =>
                                {

                                    // translate?
                                    e.dataTransfer.dropEffect = "copy";

                                }
                        };

                        value(this, (DragEventArgs)(object)a);



                    };
            }
            remove { }
        }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201305/2130522-forms-drag
        public virtual bool AllowDrop { get; set; }


      

    }
}

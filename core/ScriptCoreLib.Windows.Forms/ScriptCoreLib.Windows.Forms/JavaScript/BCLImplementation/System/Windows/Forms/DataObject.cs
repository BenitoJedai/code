using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using global::System.Windows.Forms;
using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(DataObject))]
    internal class __DataObject : __IDataObject
    {
        // X:\jsc.svn\examples\javascript\forms\FormsTreeViewDrag\FormsTreeViewDrag\ApplicationControl.cs

        public DragEvent InternalData;


        [Obsolete("called within ondragstart")]
        public Action<DataTransfer> AtDataTransfer = delegate { };

        #region get
        public virtual string GetText()
        {
            return GetData(typeof(string)) as string;
        }

        public object GetData(Type format)
        {
            // x:\jsc.svn\examples\javascript\forms\formstreeviewdrag\formstreeviewdrag\applicationcontrol.cs
            if (format == typeof(string))
                return GetData("Text");


            return GetData(format.Name);
        }

        public object GetData(string format)
        {
            //if (format == "Text")
            //    format = "text/plain";


            if (format == "FileDrop")
            {
                // tested by?

                var files = this.InternalData.dataTransfer.files;

                return Enumerable.Range(
                    0,
                    (int)files.length
                ).Select(k => files[(uint)k].name);

            }


            if (format == "text/plain")
                format = "Text";



            var value = this.InternalData.dataTransfer.getData(format);

            return value;
        }

        public object GetData(string format, bool autoConvert)
        {
            throw new NotImplementedException();
        }

        public bool GetDataPresent(Type format)
        {
            throw new NotImplementedException();
        }

        public bool GetDataPresent(string format)
        {
            throw new NotImplementedException();
        }

        public bool GetDataPresent(string format, bool autoConvert)
        {
            throw new NotImplementedException();
        }


        public string[] GetFormats()
        {
            return InternalData.dataTransfer.types;
        }

        public string[] GetFormats(bool autoConvert)
        {
            throw new NotImplementedException();
        }
        #endregion


        public __DataObject()
        {

        }

        public __DataObject(object data)
        {
            this.SetData(data);
        }

        public virtual void SetText(string textData)
        {
            this.SetData(typeof(string), textData);
        }

        #region set
        public void SetData(object data)
        {
            // X:\jsc.svn\examples\javascript\Test\TestDragStart\TestDragStart\Application.cs

            SetData(typeof(string), data);
        }

        public void SetData(Type format, object data)
        {
            // http://stackoverflow.com/questions/1772102/c-sharp-drag-and-drop-from-my-custom-app-to-notepad
            //DataFormats.
            if (format == typeof(string))
            {
                SetData("Text", data);
                return;
            }


            SetData(format.Name, data);
        }

        public void SetData(string format, object data)
        {
            // http://stackoverflow.com/questions/1772102/c-sharp-drag-and-drop-from-my-custom-app-to-notepad
            var text = "" + data;

            AtDataTransfer += (DataTransfer x) =>
            {
                Console.WriteLine("AtDataTransfer " + new { format, text });

                x.setData(format, text);
            };
        }

        public void SetData(string format, bool autoConvert, object data)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

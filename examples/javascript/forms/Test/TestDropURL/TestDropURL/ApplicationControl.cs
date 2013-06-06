using TestDropURL;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;

namespace TestDropURL
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_DragOver(object sender, DragEventArgs e)
        {
            //    -		e.Data.GetFormats()	{string[0x0000000a]}	string[]
            //[0x00000000]	"FileContents"	string
            //[0x00000001]	"FileGroupDescriptorW"	string
            //[0x00000002]	"FileGroupDescriptor"	string
            //[0x00000003]	"UniformResourceLocatorW"	string
            //[0x00000004]	"System.String"	string
            //[0x00000005]	"UnicodeText"	string
            //[0x00000006]	"Text"	string
            //[0x00000007]	"UniformResourceLocator"	string
            //[0x00000008]	"Shell IDList Array"	string
            //[0x00000009]	"IESiteModeToUrl"	string



            // Firefox
            //-		e.Data.GetFormats()	{string[0x0000000e]}	string[]
            //        [0x00000000]	"text/x-moz-url"	string
            //        [0x00000001]	"FileGroupDescriptor"	string
            //        [0x00000002]	"FileGroupDescriptorW"	string
            //        [0x00000003]	"FileContents"	string
            //        [0x00000004]	"UniformResourceLocator"	string
            //        [0x00000005]	"UniformResourceLocatorW"	string
            //        [0x00000006]	"text/uri-list"	string
            //        [0x00000007]	"System.String"	string
            //        [0x00000008]	"UnicodeText"	string
            //        [0x00000009]	"Text"	string
            //        [0x0000000a]	"text/html"	string
            //        [0x0000000b]	"HTML Format"	string
            //        [0x0000000c]	"DragImageBits"	string
            //        [0x0000000d]	"DragContext"	string


            // IE allows only link
            e.Effect = DragDropEffects.Link;

            // Chrome
            //    -		e.Data.GetFormats()	{string[0x0000000a]}	string[]
            //[0x00000000]	"DragContext"	string
            //[0x00000001]	"DragImageBits"	string
            //[0x00000002]	"text/x-moz-url"	string
            //[0x00000003]	"FileGroupDescriptorW"	string
            //[0x00000004]	"FileContents"	string
            //[0x00000005]	"UniformResourceLocatorW"	string
            //[0x00000006]	"UniformResourceLocator"	string
            //[0x00000007]	"System.String"	string
            //[0x00000008]	"UnicodeText"	string
            //[0x00000009]	"Text"	string


        }

        private void ApplicationControl_DragDrop(object sender, DragEventArgs e)
        {
            var formats = e.Data.GetFormats();

            label1.Text = (string)e.Data.GetData("Text");

            //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.WebBrowser.set_ScriptErrorsSuppressed(System.Boolean)]

            if (Uri.IsWellFormedUriString(label1.Text, UriKind.Absolute))
            {
                webBrowser1.Navigate(label1.Text);

            }

        }

        private void ApplicationControl_Load(object sender, EventArgs e)
        {

        }

    }
}

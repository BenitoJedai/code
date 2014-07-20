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
using TestLdind_i2;
using TestLdind_i2.Design;
using TestLdind_i2.HTML.Pages;

namespace TestLdind_i2
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

            //            02000003 TestLdind_i2.ApplicationWebService
            //02000002 TestLdind_i2.Application
            //script: error JSC1000: opcode unsupported - [0x0019] ldind.i2 + 1 - 1{[0x0018]
            //        ldloc.0    +1 -0}

            FlushBlock();


        }


        class Tree
        {
            public short[] freqs;
        }
        const int EOF_SYMBOL = 256;
        Tree literalTree;





        //// TestLdind_i2.Application.FlushBlock
        //type$OFyXgMp69zqflZP_aqjv7FQ.AQAABsp69zqflZP_aqjv7FQ = function()
        //{
        //    var a = [this], b, c;

        //    b = a[0].literalTree.freqs[256];
        //    c = b[0];
        //    b[0] = (~~(((c + 1))));
        //};

        public void FlushBlock()
        {
            // this is not correct
            literalTree.freqs[EOF_SYMBOL]++;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace FakeWindowsLoginExperiment.Library.Designers
{
    public class SampleRootDesigner :

        ComponentDesigner,
        //ControlDesigner,
        IRootDesigner
    {

        //jsc.meta.Library.ScriptResourceWriter ref1;
        //global::jsc.Library.VirtualDictionaryBase ref2;

        ScriptCoreLib.Ultra.Components.Volatile.Avalon.Images.Google_Chrome ref0 = new ScriptCoreLib.Ultra.Components.Volatile.Avalon.Images.Google_Chrome();


        // Member field of custom type RootDesignerView, a control that  
        // will be shown in the Forms designer view. This member is  
        // cached to reduce processing needed to recreate the  
        // view control on each call to GetView(). 
        private RootDesignerView m_view;

        // This method returns an instance of the view for this root 
        // designer. The "view" is the user interface that is presented
        // in a document window for the user to manipulate.  
        object IRootDesigner.GetView(ViewTechnology technology)
        {
            if (technology != ViewTechnology.Default)
            {
                throw new ArgumentException("Not a supported view technology", "technology");
            }
            if (m_view == null)
            {
                // Some type of displayable Form or control is required  
                // for a root designer that overrides GetView(). In this  
                // example, a Control of type RootDesignerView is used. 
                // Any class that inherits from Control will work.
                try
                {

                    m_view = new RootDesignerView(this);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                    Debugger.Break();

                }
            }
            return m_view;
        }


        //public override Control Control
        //{
        //    get
        //    {
        //        if (m_view == null)
        //        {
        //            // Some type of displayable Form or control is required  
        //            // for a root designer that overrides GetView(). In this  
        //            // example, a Control of type RootDesignerView is used. 
        //            // Any class that inherits from Control will work.
        //            m_view = new RootDesignerView(this);
        //        }
        //        return m_view;
        //    }
        //}

        // IRootDesigner.SupportedTechnologies is a required override for an 
        // IRootDesigner. Default is the view technology used by this designer.  
        ViewTechnology[] IRootDesigner.SupportedTechnologies
        {
            get
            {
                return new ViewTechnology[] { ViewTechnology.Default };
            }
        }


    }

}

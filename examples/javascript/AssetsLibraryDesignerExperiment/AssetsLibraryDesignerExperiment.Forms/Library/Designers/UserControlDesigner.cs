using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System.Collections.Specialized;
using System.Reflection;
using System.Windows.Forms.Design;
using ScriptCoreLib.Extensions;

namespace AssetsLibraryDesignerExperiment.Components
{

    interface IDropZone
    {
        Panel DropZone { get; }
    }
    public class UserControlDesigner : ParentControlDesigner
    {
        // http://msdn.microsoft.com/en-us/library/system.windows.forms.design.parentcontroldesigner.aspx

        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);


            this.EnableDesignMode(
                (this.Control as IDropZone).DropZone, "?DropZone");
        }

        // Use pull model to populate smart tag menu.
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                var actionLists = new DesignerActionListCollection();
                actionLists.Add(new Class1DesignerActionList(this.Component) { AutoShow = true });
                return actionLists;
            }
        }

        // This method is invoked when the associated component is double-clicked. 
        public override void DoDefaultAction()
        {
            //this.Component
            MessageBox.Show("The event handler for the Example Designer Verb Command was invoked.");
        }

        // This method provides designer verbs. 
        public override System.ComponentModel.Design.DesignerVerbCollection Verbs
        {
            get
            {
                return new DesignerVerbCollection(
                    new[] { 
                        new DesignerVerb(
                            "Example Designer Verb Command",
                            
                            
                            delegate
                            {
            MessageBox.Show("The event handler for the Example Designer Verb Command was invoked.");
                            
                            }
                    
                        )    
                    }
                    );
            }
        }

        // Event handling method for the example designer verb 
        private void onVerb(object sender, EventArgs e)
        {
        }

    }

    public class Class1Designer : System.ComponentModel.Design.ComponentDesigner
    {

        // Use pull model to populate smart tag menu.
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                var actionLists = new DesignerActionListCollection();
                actionLists.Add(new Class1DesignerActionList(this.Component) { AutoShow = true });
                return actionLists;
            }
        }

        // This method is invoked when the associated component is double-clicked. 
        public override void DoDefaultAction()
        {
            //this.Component
            MessageBox.Show("The event handler for the Example Designer Verb Command was invoked.");
        }

        // This method provides designer verbs. 
        public override System.ComponentModel.Design.DesignerVerbCollection Verbs
        {
            get
            {
                return new DesignerVerbCollection(new DesignerVerb[] { new DesignerVerb("Example Designer Verb Command", new EventHandler(this.onVerb)) });
            }
        }

        // Event handling method for the example designer verb 
        private void onVerb(object sender, EventArgs e)
        {
            MessageBox.Show("The event handler for the Example Designer Verb Command was invoked.");
        }

        // http://msdn.microsoft.com/en-us/magazine/cc163758.aspx
    }


    public class Class1DesignerActionList : System.ComponentModel.Design.DesignerActionList
    {
        //private Class1 colUserControl;

        private DesignerActionUIService designerActionUISvc = null;

        //The constructor associates the control with the smart tag list.
        public Class1DesignerActionList(IComponent component)
            : base(component)
        {
            //this.colUserControl = component as Class1;

            // Cache a reference to DesignerActionUIService, 
            // so the DesigneractionList can be refreshed.
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService))
                as DesignerActionUIService;
        }

        public string[] stringarray { get; set; }
        public string bar { get; set; }
        public bool checkbox { get; set; }
        public Color color { get; set; }
        public DateTime datetime { get; set; }
        public Form form { get; set; }
        public Size Size { get; set; }
        public Point Point { get; set; }

        public enum fooenumtype
        {
            hello,
            world
        }

        public fooenumtype fooenum { get; set; }

        public void foo()
        {
            MessageBox.Show("foo");
        }
        // Implementation of this abstract method creates smart tag  items, 
        // associates their targets, and collects into list.
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();


            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Information"));


            items.Add(
                new DesignerActionTextItem("hey!", "Information")
                {


                }
            );


            this.GetType().GetProperties(
                System.Reflection.BindingFlags.DeclaredOnly
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance
                ).WithEach(
                p =>
                {
                    var n = p.Name;

                    items.Add(
                        new DesignerActionPropertyItem(
                            p.Name,
                            p.Name + " : " + p.PropertyType.Name,
                            "Information")
                            {
                            });
                }
            );


            items.Add(
                new DesignerActionMethodItem(
                    this,
                    "foo",
                    "foo"
                )
                {
                }
            );



            return items;
        }
    }

}

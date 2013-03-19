using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace FakeWindowsLoginExperiment.Library
{
//    [ToolboxItem(true)]
//    [DesignTimeVisible(true)]
//#if DEBUG
//    [Designer(typeof(MyControlDesigner))]
//#endif
    [DefaultEvent("FormButtonClick")]
    [DefaultProperty("Foo")]
    public partial class Form1 : Form
    {
        // http://www.xtremedotnettalk.com/showthread.php?t=84522

        public Form1()
        {
            InitializeComponent();
        }

        public string Foo { get; set; }
        public event Action FormButtonClick;

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (FormButtonClick != null)
                FormButtonClick();
        }

        private void applicationExitFullscreen1_ExitFullscreen()
        {
            label1.Show();
        }

        private void applicationExitFullscreen1_EnterFullscreen()
        {
            label1.Hide();

        }
    }

    public class MyControlDesigner : ComponentDesigner
    {
        // http://forums.codeguru.com/showthread.php?353307-How-do-I-hide-inherited-events

        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            base.PreFilterProperties(properties);

            try
            {
                //                ---------------------------

                //---------------------------
                //error: System.InvalidOperationException: Collection was modified; enumeration operation may not execute.

                //   at System.Collections.ArrayList.ArrayListEnumeratorSimple.MoveNext()

                //   at System.Collections.Specialized.OrderedDictionary.OrderedDictionaryEnumerator.MoveNext()

                //   at FakeWindowsLoginExperiment.Library.MyControlDesigner.PreFilterEvents(IDictionary events) in x:\jsc.svn\examples\javascript\forms\FakeWindowsLoginExperiment\FakeWindowsLoginExperiment\Library\Form1.cs:line 48
                //---------------------------
                //OK   
                //---------------------------

                var keep = this.Component.GetType().GetProperties(
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly
                ).Select(k => k.Name).ToList();

                //keep.Add("ClientSize");
                //keep.Add("(Name)");
                keep.Add("Name");
                keep.Add("Text");
                //keep.Add("Visible");
                //keep.Add("Location");


                var a = new List<string>();
                foreach (string item in properties.Keys)
                {
                    // name is special
                    if (keep.Contains(item) ||
                        item.StartsWith("Name_")
                        )
                        continue;

                    a.Add(item);
                }

                //MessageBox.Show(
                //    string.Join(", ", a.ToArray())
                //);

                foreach (var item in a)
                {
                    properties.Remove(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex);
            }
        }

        protected override void PreFilterEvents(System.Collections.IDictionary events)
        {
            base.PreFilterEvents(events);

            try
            {
                //                ---------------------------

                //---------------------------
                //error: System.InvalidOperationException: Collection was modified; enumeration operation may not execute.

                //   at System.Collections.ArrayList.ArrayListEnumeratorSimple.MoveNext()

                //   at System.Collections.Specialized.OrderedDictionary.OrderedDictionaryEnumerator.MoveNext()

                //   at FakeWindowsLoginExperiment.Library.MyControlDesigner.PreFilterEvents(IDictionary events) in x:\jsc.svn\examples\javascript\forms\FakeWindowsLoginExperiment\FakeWindowsLoginExperiment\Library\Form1.cs:line 48
                //---------------------------
                //OK   
                //---------------------------

                var keep = this.Component.GetType().GetEvents(
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly
                ).Select(k => k.Name).ToList();

                keep.Add("FormClosed");

                var a = new List<string>();
                foreach (string item in events.Keys)
                {
                    if (keep.Contains(item))
                        continue;

                    a.Add(item);
                }

                foreach (var item in a)
                {
                    events.Remove(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex);
            }

        }

    }
}

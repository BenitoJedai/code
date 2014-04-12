using SharedBrowserSessionExperiment.DataLayer.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace SharedBrowserSessionExperiment
{
    public partial class TheBrowserTab : Form
    {
        // X:\jsc.svn\examples\javascript\forms\FormsDualDataSource\FormsDualDataSource\ApplicationControl.cs

        public TheBrowserTab()
        {
            NavigationOrdersNavigateBindingSource.CreateDataSource.With(
                CreateDataSource =>
                {
                    NavigationOrdersNavigateBindingSource.CreateDataSource =
                        delegate
                        {
                            Console.WriteLine("NavigationOrdersNavigateBindingSource.CreateDataSource");

                            return CreateDataSource();
                        };
                }
            );

            InitializeComponent();
        }

        private void navigationOrdersNavigateBindingSourceBindingNavigator_RefreshItems(object sender, EventArgs e)
        {
            Console.WriteLine("navigationOrdersNavigateBindingSourceBindingNavigator_RefreshItems");

            //this.Text = new
            //{
            //    navigationOrdersNavigateBindingSourceBindingNavigator.PositionItem.Text,
            //    navigationOrdersNavigateBindingSourceBindingNavigator.BindingSource.Position
            //}.ToString();
        }

        private void navigationOrdersNavigateBindingSourceBindingSource_PositionChanged(object sender, EventArgs e)
        {
            Console.WriteLine("navigationOrdersNavigateBindingSourceBindingSource_PositionChanged");
            this.Text = new
            {
                navigationOrdersNavigateBindingSourceBindingSource.Position
            }.ToString();
        }

        private void TheBrowserTab_Load(object sender, EventArgs e)
        {
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Resources\ResourceManager.cs
            //script: error JSC1000: No implementation found for this native method, please implement [System.Resources.ResourceManager.GetObject(System.String)]
        }

        private void TheBrowserTab_DragDrop(object sender, DragEventArgs e)
        {
            // X:\jsc.svn\examples\javascript\forms\Test\TestDropURL\TestDropURL\ApplicationControl.cs


            var r = new NavigationOrdersNavigateRow { urlString = (string)e.Data.GetData("Text") };


            //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.WebBrowser.set_ScriptErrorsSuppressed(System.Boolean)]

            var IsWellFormedUriString = Uri.IsWellFormedUriString(r.urlString, UriKind.Absolute);
            Console.WriteLine(new { IsWellFormedUriString, r.urlString });


            if (IsWellFormedUriString)
            {
                // 58:24479ms { IsWellFormedUriString = true, urlString = https://www.youtube.com/embed/lsbYqjMkK9Q }

                (this.navigationOrdersNavigateBindingSourceBindingSource.AddNew() as DataRowView).With(
                    rr =>
                    {
                        rr["urlString"] = r.urlString;
                    }
                );

            }
        }

        private void TheBrowserTab_DragOver(object sender, DragEventArgs e)
        {
            // IE allows only link
            e.Effect = DragDropEffects.Link;
        }
    }
}

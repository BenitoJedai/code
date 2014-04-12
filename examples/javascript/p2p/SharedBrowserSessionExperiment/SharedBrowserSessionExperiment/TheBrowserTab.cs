using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharedBrowserSessionExperiment
{
    public partial class TheBrowserTab : Form
    {
        public TheBrowserTab()
        {
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
    }
}

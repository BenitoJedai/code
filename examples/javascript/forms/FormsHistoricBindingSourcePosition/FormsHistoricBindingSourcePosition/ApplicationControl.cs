using FormsHistoricBindingSourcePosition;
using HashForBindingSource.DataSourcez.Dataz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace FormsHistoricBindingSourcePosition
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        public ZeDocumentTextzNavigateRow CurrentZeDocumentTextzNavigateRow
        {
            get
            {
                return (ZeDocumentTextzNavigateRow)CurrentDataRowView.Row;
            }
        }

        public DataRowView CurrentDataRowView
        {
            get
            {
                BindingSource x = this.navigationOrdersNavigateBindingSourceBindingSource;

                // is there an interface for .Current ?
                return (DataRowView)x.Current;
            }
        }

        private void navigationOrdersNavigateBindingSourceBindingSource_CurrentChanged(object sender, System.EventArgs e)
        {
            //            An exception of type 'System.NullReferenceException' occurred in FormsHistoricBindingSourcePosition.exe but was not handled in user code

            //Additional information: Object reference not set to an instance of an object.

            if (CurrentZeDocumentTextzNavigateRow == null)
            {
                Console.WriteLine("CurrentZeDocumentTextzNavigateRow null");
                return;
            }

            // ParentForm missing at setup?
            this.ParentForm.With(
                p => p.Text = CurrentZeDocumentTextzNavigateRow.hash
            );

            // webbrowser does not easly allow databind DocumentText yet without redefinidtion
            this.webBrowser1.DocumentText = CurrentZeDocumentTextzNavigateRow.DocumentText;



            CurrentDataRowView.PropertyChanged +=
                delegate
                {
                    Console.WriteLine("CurrentDataRowView.PropertyChanged");
                };


        }

        private void navigationOrdersNavigateBindingSourceBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            Console.WriteLine("navigationOrdersNavigateBindingSourceBindingSource_CurrentItemChanged");

        }

        private void navigationOrdersNavigateBindingSourceBindingSource_DataMemberChanged(object sender, EventArgs e)
        {

            Console.WriteLine("navigationOrdersNavigateBindingSourceBindingSource_DataMemberChanged");
        }

        private void navigationOrdersNavigateBindingSourceBindingSource_BindingComplete(object sender, BindingCompleteEventArgs e)
        {

        }

        private void navigationOrdersNavigateBindingSourceBindingSource_DataSourceChanged(object sender, EventArgs e)
        {
            Console.WriteLine("navigationOrdersNavigateBindingSourceBindingSource_DataSourceChanged");

        }

        private void ApplicationControl_Load(object sender, EventArgs e)
        {
            Console.WriteLine("ApplicationControl_Load - Why aint CLR calling this? missing EndInit?");
        }

        private void ApplicationControl_VisibleChanged(object sender, EventArgs e)
        {
            Console.WriteLine("ApplicationControl_VisibleChanged " + new { this.Visible });


        }

        private void ApplicationControl_ParentChanged(object sender, EventArgs e)
        {
            Console.WriteLine("ApplicationControl_ParentChanged ");

            CurrentDataRowView.Row.Table.ColumnChanged +=
              (object xsender, DataColumnChangeEventArgs xe) =>
              {

                  // this is the only way to be notified if a field was changed?
                  Console.WriteLine(
                      "ColumnChanged " + new
                      {
                          xe.Column.ColumnName,

                          RowIndex = CurrentDataRowView.Row.Table.Rows.IndexOf(xe.Row)
                      }
                      );

                  this.webBrowser1.DocumentText = CurrentZeDocumentTextzNavigateRow.DocumentText;
              };
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsAutoSumGridSelection.Data
{
    public class MyDataSource : BindingSource
    {


        // Additional information: DataMember property 'x' cannot be found on the DataSource.
        // http://www.unknownerror.org/Problem/index/1325682211/using-a-bindingsource-with-a-datatabledatacolumn-produces-strange-results/

        public MyDataSource()
        //: base(GetData(), "")
        {
            // how do we implement a static
            // data source?

            // https://www.google.ee/search?q=BindingSource+datatable&oq=BindingSource+datatab&aqs=chrome.0.69i59j69i57j69i60.2509j0j4&sourceid=chrome&es_sm=93&ie=UTF-8

            // http://social.msdn.microsoft.com/Forums/windows/en-US/30ab63cb-f436-4ff5-85c8-4d25c5c55700/datatable-property-from-within-bindingsource
            // http://www.codeproject.com/Articles/24656/A-Detailed-Data-Binding-Tutorial

            // http://stackoverflow.com/questions/10266611/what-are-the-benefits-of-using-a-bindingsource-with-bindinglistbusiness-obj-as
            // http://msdn.microsoft.com/en-us/library/aa480734.aspx
            // http://wiki.visualwebgui.com/pages/index.php/BindingSource_CodeSample_-_Databinding_to_Filterable_list_of_custom_objects
            // http://www.codeguru.com/csharp/.net/net_data/article.php/c19657/Extending-the-Win-Forms-Binding-Source-Component.htm
            // http://msdn.microsoft.com/en-us/library/0yy0c9z8(v=vs.110).aspx



            //new BindingSource(

            ////   Objects added to a BindingSource's list must all be of the same type.  
            //// Objects added to a BindingSource's list must all be of the same type. 
            //this.DataSource = typeof(DataTable);

            ////this.Add(x);

            this.DataSource = MyOtherDataSource.GetData();
        }



    }

 }

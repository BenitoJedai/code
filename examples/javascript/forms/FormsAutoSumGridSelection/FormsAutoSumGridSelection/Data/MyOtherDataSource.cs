using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FormsAutoSumGridSelection.Data
{
    public class MyOtherDataSource : Component, IListSource
    {
        // http://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview.datasource(v=vs.110).aspx

        DataTable x = new DataTable("x");
        public MyOtherDataSource()
        {

            x.Columns.Add("Column1");
            x.Columns.Add("Column2");
            x.Columns.Add("Column3");

            x.Rows.Add("00", "01", "011");
            x.Rows.Add("10", "11", "111");

            // end init just creates a new instance of this.

            //            ((System.ComponentModel.ISupportInitialize)(this.myOtherDataSourceBindingSource)).EndInit();
            // 

            //    FormsAutoSumGridSelection.exe!FormsAutoSumGridSelection.Data.MyOtherDataSource.MyOtherDataSource() Line 18	C#
            //    [Native to Managed Transition]	
            //    [Managed to Native Transition]	
            //    mscorlib.dll!System.RuntimeType.CreateInstanceSlow(bool publicOnly, bool skipCheckThis, bool fillCache, ref System.Threading.StackCrawlMark stackMark) + 0x72 bytes	
            //    mscorlib.dll!System.Activator.CreateInstance(System.Type type, bool nonPublic) + 0x54 bytes	
            //    mscorlib.dll!System.RuntimeType.CreateInstanceImpl(System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, object[] args, System.Globalization.CultureInfo culture, object[] activationAttributes, ref System.Threading.StackCrawlMark stackMark) + 0x405 bytes	
            //    mscorlib.dll!System.Activator.CreateInstance(System.Type type, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, object[] args, System.Globalization.CultureInfo culture, object[] activationAttributes) + 0x81 bytes	
            //    mscorlib.dll!System.Activator.CreateInstance(System.Type type, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, object[] args, System.Globalization.CultureInfo culture) + 0x13 bytes	
            //    System.Windows.Forms.dll!System.Windows.Forms.SecurityUtils.SecureCreateInstance(System.Type type, object[] args, bool allowNonPublic) + 0x69 bytes	
            //    System.Windows.Forms.dll!System.Windows.Forms.BindingSource.CreateInstanceOfType(System.Type type) + 0x25 bytes	
            //    System.Windows.Forms.dll!System.Windows.Forms.BindingSource.GetListFromType(System.Type type) + 0x74 bytes	
            //    System.Windows.Forms.dll!System.Windows.Forms.BindingSource.ResetList() + 0x47 bytes	
            //    System.Windows.Forms.dll!System.Windows.Forms.BindingSource.System.ComponentModel.ISupportInitialize.EndInit() + 0x6f bytes	
            //>	FormsAutoSumGridSelection.exe!FormsAutoSumGridSelection.ApplicationControl.InitializeComponent() Line 70 + 0xf bytes	C#


        }

        //public static IBindingListView GetData()
        //public static IBindingList GetData()
        public static IListSource GetData()
        {
            return new MyOtherDataSource();
        }

        bool IListSource.ContainsListCollection
        {
            get { return ((IListSource)this.x).ContainsListCollection; }
        }

        System.Collections.IList IListSource.GetList()
        {
            //-		z	{System.Data.DataView}	System.Collections.IList {System.Data.DataView}
            //-		[0x00000000]	{System.Data.DataRowView}	object {System.Data.DataRowView}


            IList z = ((IListSource)this.x).GetList();
            //var zz = (DataView)z;
            return new zz((DataView)z);
        }

        class zz : IBindingListView
        {
            readonly IBindingListView x;

            public zz(DataView x)
            {
                this.x = x;

            }

            void IBindingListView.ApplySort(ListSortDescriptionCollection sorts)
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            string IBindingListView.Filter
            {
                get
                {
                    Debugger.Break(); throw new NotImplementedException();
                }
                set
                {
                    Debugger.Break(); throw new NotImplementedException();
                }
            }

            void IBindingListView.RemoveFilter()
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            ListSortDescriptionCollection IBindingListView.SortDescriptions
            {
                get { Debugger.Break(); throw new NotImplementedException(); }
            }

            bool IBindingListView.SupportsAdvancedSorting
            {
                get { Debugger.Break(); throw new NotImplementedException(); }
            }

            bool IBindingListView.SupportsFiltering
            {
                get { Debugger.Break(); throw new NotImplementedException(); }
            }

            void IBindingList.AddIndex(PropertyDescriptor property)
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            object IBindingList.AddNew()
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            bool IBindingList.AllowEdit
            {
                // 8
                get
                {
                    return this.x.AllowEdit;
                }
            }

            bool IBindingList.AllowNew
            {
                // 5
                // x.AllowNew = true
                get { return x.AllowNew; }
            }

            bool IBindingList.AllowRemove
            {
                get { Debugger.Break(); throw new NotImplementedException(); }
            }

            void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            int IBindingList.Find(PropertyDescriptor property, object key)
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            bool IBindingList.IsSorted
            {
                // 7
                get
                {
                    return this.x.IsSorted;

                }
            }

            event ListChangedEventHandler IBindingList.ListChanged
            {
                // 2
                add
                {
                    Console.WriteLine("ListChanged add");

                    this.x.ListChanged +=
                        (sender, args) =>
                        {
                            value(sender, args);
                        };
                }
                remove
                {
                    Console.WriteLine("ListChanged remove");

                }
            }

            void IBindingList.RemoveIndex(PropertyDescriptor property)
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            void IBindingList.RemoveSort()
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            ListSortDirection IBindingList.SortDirection
            {
                get { Debugger.Break(); throw new NotImplementedException(); }
            }

            PropertyDescriptor IBindingList.SortProperty
            {
                get { Debugger.Break(); throw new NotImplementedException(); }
            }

            bool IBindingList.SupportsChangeNotification
            {
                // 1
                get
                {
                    //x.SupportsChangeNotification = true
                    return x.SupportsChangeNotification;
                }
            }

            bool IBindingList.SupportsSearching
            {
                get { Debugger.Break(); throw new NotImplementedException(); }
            }

            bool IBindingList.SupportsSorting
            {
                // 6
                // this.x.SupportsSorting = true
                get
                {
                    return this.x.SupportsSorting;

                    Debugger.Break(); throw new NotImplementedException();
                }
            }

            int IList.Add(object value)
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            void IList.Clear()
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            bool IList.Contains(object value)
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            int IList.IndexOf(object value)
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            void IList.Insert(int index, object value)
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            bool IList.IsFixedSize
            {
                get { Debugger.Break(); throw new NotImplementedException(); }
            }

            bool IList.IsReadOnly
            {
                get { Debugger.Break(); throw new NotImplementedException(); }
            }

            void IList.Remove(object value)
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            void IList.RemoveAt(int index)
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            object IList.this[int index]
            {
                // 4
                get
                {

                    var z = (System.Data.DataRowView)this.x[index];

                    // z = {System.Data.DataRowView}
                    // ICustomTypeDescriptor

                    var ic = (ICustomTypeDescriptor)z;

                    return z;
                }
                set
                {
                    Debugger.Break(); throw new NotImplementedException();
                }
            }

            void ICollection.CopyTo(Array array, int index)
            {
                Debugger.Break(); throw new NotImplementedException();
            }

            int ICollection.Count
            {
                // 3
                get
                {
                    //x.Count = 0x00000001
                    return x.Count;
                }
            }

            bool ICollection.IsSynchronized
            {
                get { Debugger.Break(); throw new NotImplementedException(); }
            }

            object ICollection.SyncRoot
            {
                get { Debugger.Break(); throw new NotImplementedException(); }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                Debugger.Break(); throw new NotImplementedException();
            }
        }
    }

}

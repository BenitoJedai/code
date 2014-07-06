using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections.ObjectModel;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
    // http://referencesource.microsoft.com/#System/compmod/system/componentmodel/BindingList.cs
    [Script(Implements = typeof(global::System.ComponentModel.BindingList<>))]
    internal class __BindingList<T> : __Collection<T>, __IBindingList, __IList
    {
        //        U:\staging\web\java\ScriptCoreLib\Shared\BCLImplementation\System\ComponentModel\__BindingList_1.java:15: ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel.__BindingList_1 is not abstract and does not override abstract method System_Collections_IList_RemoveAt(int) in ScriptCoreLib.Shared.BCLImplementation.System.Collections.__IList
        //public class __BindingList_1<T> extends ScriptCoreLib.Shared.BCLImplementation.System.Collections.ObjectModel.__Collection_1<T> implements __IBindingList, __IList, __ICollection
        //       ^

        public __BindingList()
        {
            RaiseListChangedEvents = true;
        }

        private void FireListChanged(ListChangedType type, int index)
        {
            if (this.RaiseListChangedEvents)
            {
                this.OnListChanged(new ListChangedEventArgs(type, index));
            }
        }




        public bool RaiseListChangedEvents { get; set; }




        protected virtual void OnListChanged(ListChangedEventArgs e)
        {
            if (this.ListChanged != null)
            {
                this.ListChanged(this, e);
            }
        }




        public event AddingNewEventHandler AddingNew;

        public event ListChangedEventHandler ListChanged;


        protected override void InsertItem(int index, T item)
        {
            //this.EndNew(this.addNewPos);
            base.InsertItemBody(index, item);
            //if (this.raiseItemChangedEvents)
            //{
            //    this.HookPropertyChanged(item);
            //}

            if (AddingNew != null)
                AddingNew(this, new AddingNewEventArgs(item));

            this.FireListChanged(ListChangedType.ItemAdded, index);
        }



        protected override void SetItem(int index, T item)
        {
            //if (this.raiseItemChangedEvents)
            //{
            //    this.UnhookPropertyChanged(base[index]);
            //}
            base.SetItemBody(index, item);
            //if (this.raiseItemChangedEvents)
            //{
            //    this.HookPropertyChanged(item);
            //}
            this.FireListChanged(ListChangedType.ItemChanged, index);
        }


        protected override void RemoveItem(int index)
        {
            //if (!this.allowRemove && ((this.addNewPos < 0) || (this.addNewPos != index)))
            //{
            //    throw new NotSupportedException();
            //}
            //this.EndNew(this.addNewPos);
            //if (this.raiseItemChangedEvents)
            //{
            //    this.UnhookPropertyChanged(base[index]);
            //}
            base.RemoveItemBody(index);
            this.FireListChanged(ListChangedType.ItemDeleted, index);
        }








        bool __IList.IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }

        bool __IList.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        object __IList.this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        int __IList.Add(object value)
        {
            throw new NotImplementedException();
        }

        void __IList.Clear()
        {
            throw new NotImplementedException();
        }

        bool __IList.Contains(object value)
        {
            throw new NotImplementedException();
        }

        int __IList.IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        void __IList.Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        void __IList.Remove(object value)
        {
            throw new NotImplementedException();
        }

        void __IList.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        int __ICollection.Count
        {
            get { throw new NotImplementedException(); }
        }

        //global::System.Collections.IEnumerator __IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}
    }
}

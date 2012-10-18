using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.BindingList<>))]
	internal class __BindingList<T> : Collections.ObjectModel.__Collection<T>, __IBindingList, __IList
    {
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

 

 


        public event ListChangedEventHandler ListChanged;


        protected override void InsertItem(int index, T item)
        {
            //this.EndNew(this.addNewPos);
            base.InsertItemBody(index, item);
            //if (this.raiseItemChangedEvents)
            //{
            //    this.HookPropertyChanged(item);
            //}
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







        public bool IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }

        public new object this[int index]
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

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public new global::System.Collections.IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

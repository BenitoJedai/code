using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.ObjectModel;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.BindingList<>))]
    internal class __BindingList<T> : __Collection<T>, __IBindingList
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







		#region __ICollection Members


		public bool IsSynchronized
		{
			get { throw new NotImplementedException(); }
		}

		public object SyncRoot
		{
			get { throw new NotImplementedException(); }
		}

		public void CopyTo(global::System.Array array, int index)
		{
			throw new NotImplementedException();
		}

		#endregion

	

	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.ComponentModel
{
    [Script(Implements = typeof(global::System.ComponentModel.ListChangedEventArgs))]
    internal class __ListChangedEventArgs : __EventArgs
    {
        // Fields
        private ListChangedType listChangedType;
        private int newIndex;
        private int oldIndex;

        // Properties
        public ListChangedType ListChangedType
        {
            get
            {
                return this.listChangedType;
            }
        }

        public int NewIndex
        {
            get
            {
                return this.newIndex;
            }
        }

        public int OldIndex
        {
            get
            {
                return this.oldIndex;
            }
        }


        public __ListChangedEventArgs(ListChangedType listChangedType, int newIndex) : this(listChangedType, newIndex, -1)
        {
        }

        public __ListChangedEventArgs(ListChangedType listChangedType, int newIndex, int oldIndex)
        {
            this.listChangedType = listChangedType;
            this.newIndex = newIndex;
            this.oldIndex = oldIndex;
        }


    }
}

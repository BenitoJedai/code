using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.BindingNavigator))]
    public class __BindingNavigator : __ToolStrip, __ISupportInitialize
    {
        // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\Application.cs


        public __BindingNavigator()
        {

        }

        public __BindingNavigator(IContainer container)
        {
        }


        #region AddNewItem
        public ToolStripItem InternalAddNewItem;
        public ToolStripItem AddNewItem
        {
            get { return InternalAddNewItem; }
            set
            {
                InternalAddNewItem = value;

                InternalAddNewItem.Click +=
                    delegate
                    {
                        // X:\jsc.svn\examples\javascript\forms\FormsNICWithDataSource\FormsNICWithDataSource\ApplicationControl.cs
                        if (!this.BindingSource.AllowNew)
                            return;

                        // X:\jsc.svn\examples\javascript\forms\FormsHistoricBindingSourcePosition\FormsHistoricBindingSourcePosition\ApplicationControl.cs

                        this.BindingSource.AddNew();
                        this.BindingSource.Position = this.BindingSource.Count - 1;
                    };
            }
        }
        #endregion


        BindingSource InternalBindingSource;
        public BindingSource BindingSource
        {
            get
            {

                return InternalBindingSource;
            }
            set
            {
                InternalBindingSource = value;


                InternalBindingSource.PositionChanged +=
                    delegate
                    {
                        this.PositionItem.Text = "" + InternalBindingSource.Position;
                    };
            }
        }

        public ToolStripItem CountItem { get; set; }
        public ToolStripItem DeleteItem { get; set; }
        public ToolStripItem MoveFirstItem { get; set; }
        public ToolStripItem MoveLastItem { get; set; }

        #region MoveNextItem
        public ToolStripItem InternalMoveNextItem;
        public ToolStripItem MoveNextItem
        {
            get { return InternalMoveNextItem; }
            set
            {
                InternalMoveNextItem = value;

                InternalMoveNextItem.Click +=
                    delegate
                    {
                        this.BindingSource.Position =
                (this.BindingSource.Position + 1) % this.BindingSource.Count
                ;

                    };
            }
        }
        #endregion

        #region MovePreviousItem
        public ToolStripItem InternalMovePreviousItem;
        public ToolStripItem MovePreviousItem
        {
            get { return InternalMovePreviousItem; }
            set
            {
                InternalMovePreviousItem = value;

                InternalMovePreviousItem.Click +=
                    delegate
                    {
                        this.BindingSource.Position =
                (this.BindingSource.Position + this.BindingSource.Count - 1) % this.BindingSource.Count
                ;

                    };
            }
        }
        #endregion

        public ToolStripItem PositionItem { get; set; }

        public event EventHandler RefreshItems;


        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }
}

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

        //no implementation for System.Windows.Forms.BindingNavigator 8d907746-455e-39a7-bd31-bc9f81468347
        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.BindingNavigator.set_AddNewItem(System.Windows.Forms.ToolStripItem)]
        //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?
        //script: error JSC1000: error at SharedBrowserSessionExperiment.TheBrowserTab.InitializeComponent,
        // assembly: U:\SharedBrowserSessionExperiment.Application.exe
        // type: SharedBrowserSessionExperiment.TheBrowserTab, SharedBrowserSessionExperiment.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // offset: 0x036f
        //  method:Void InitializeComponent()

        public __BindingNavigator()
        {

        }

        public __BindingNavigator(IContainer container)
        {
        }

        public ToolStripItem AddNewItem { get; set; }
        public BindingSource BindingSource { get; set; }
        public ToolStripItem CountItem { get; set; }
        public ToolStripItem DeleteItem { get; set; }
        public ToolStripItem MoveFirstItem { get; set; }
        public ToolStripItem MoveLastItem { get; set; }

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

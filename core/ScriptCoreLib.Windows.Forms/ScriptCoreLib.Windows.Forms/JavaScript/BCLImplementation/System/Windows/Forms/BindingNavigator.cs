using ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.Shared.BCLImplementation.System.Resources;
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

        [Obsolete("a workaround until jsc can actually use resx images by the designer.")]
        static __BindingNavigator()
        {
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Resources\ResourceManager.cs

            __ResourceManager.InternalGetObject +=
                (baseName, assembly, name, yield) =>
                {
                    // "bindingNavigatorDeleteItem.Image"

                    //Console.WriteLine(
                    //    "__BindingNavigator " + new { name }
                    //    );


                    if (name.Contains("MovePreviousItem"))
                        yield((__Bitmap)(IHTMLImage)"assets/ScriptCoreLib.Windows.Forms/BindingNavigatorMovePreviousItem.Image.png");

                    if (name.Contains("MoveNextItem"))
                        yield((__Bitmap)(IHTMLImage)"assets/ScriptCoreLib.Windows.Forms/BindingNavigatorMoveNextItem.Image.png");

                    if (name.Contains("MoveLastItem"))
                        yield((__Bitmap)(IHTMLImage)"assets/ScriptCoreLib.Windows.Forms/BindingNavigatorMoveLastItem.Image.png");

                    if (name.Contains("MoveFirstItem"))
                        yield((__Bitmap)(IHTMLImage)"assets/ScriptCoreLib.Windows.Forms/BindingNavigatorMoveFirstItem.Image.png");

                    if (name.Contains("AddNewItem"))
                        yield((__Bitmap)(IHTMLImage)"assets/ScriptCoreLib.Windows.Forms/BindingNavigatorAddNewItem.Image.png");

                    if (name.Contains("DeleteItem"))
                        yield((__Bitmap)(IHTMLImage)"assets/ScriptCoreLib.Windows.Forms/BindingNavigatorDeleteItem.Image.png");

                    if (name.Contains("SaveItem"))
                        yield((__Bitmap)(IHTMLImage)"assets/ScriptCoreLib.Windows.Forms/BindingNavigatorSaveItem.Image.png");
                };
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


        #region BindingSource
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

                        this.CountItem.Text = " of " + InternalBindingSource.Count;
                    };
            }
        }
        #endregion


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

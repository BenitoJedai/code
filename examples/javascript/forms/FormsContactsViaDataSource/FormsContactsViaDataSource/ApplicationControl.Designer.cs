using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FormsContactsViaDataSource
{
    public partial class ApplicationControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationControl));
            this.contactDataGetContactsBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.contactDataGetContactsBindingSourceBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.contactDataGetContactsBindingSourceBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.contactDataGetContactsBindingSourceDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.applicationWebService1 = new FormsContactsViaDataSource.ApplicationWebService();
            ((System.ComponentModel.ISupportInitialize)(this.contactDataGetContactsBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactDataGetContactsBindingSourceBindingNavigator)).BeginInit();
            this.contactDataGetContactsBindingSourceBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contactDataGetContactsBindingSourceDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // contactDataGetContactsBindingSourceBindingSource
            // 
            this.contactDataGetContactsBindingSourceBindingSource.DataSource = typeof(FormsContactsViaDataSource.Data.ContactDataGetContactsBindingSource);
            this.contactDataGetContactsBindingSourceBindingSource.Position = 0;
            // 
            // contactDataGetContactsBindingSourceBindingNavigator
            // 
            this.contactDataGetContactsBindingSourceBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.contactDataGetContactsBindingSourceBindingNavigator.BindingSource = this.contactDataGetContactsBindingSourceBindingSource;
            this.contactDataGetContactsBindingSourceBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.contactDataGetContactsBindingSourceBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.contactDataGetContactsBindingSourceBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.contactDataGetContactsBindingSourceBindingNavigatorSaveItem});
            this.contactDataGetContactsBindingSourceBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.contactDataGetContactsBindingSourceBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.contactDataGetContactsBindingSourceBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.contactDataGetContactsBindingSourceBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.contactDataGetContactsBindingSourceBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.contactDataGetContactsBindingSourceBindingNavigator.Name = "contactDataGetContactsBindingSourceBindingNavigator";
            this.contactDataGetContactsBindingSourceBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.contactDataGetContactsBindingSourceBindingNavigator.Size = new System.Drawing.Size(439, 25);
            this.contactDataGetContactsBindingSourceBindingNavigator.TabIndex = 0;
            this.contactDataGetContactsBindingSourceBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(28, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "1";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // contactDataGetContactsBindingSourceBindingNavigatorSaveItem
            // 
            this.contactDataGetContactsBindingSourceBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.contactDataGetContactsBindingSourceBindingNavigatorSaveItem.Enabled = false;
            this.contactDataGetContactsBindingSourceBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("contactDataGetContactsBindingSourceBindingNavigatorSaveItem.Image")));
            this.contactDataGetContactsBindingSourceBindingNavigatorSaveItem.Name = "contactDataGetContactsBindingSourceBindingNavigatorSaveItem";
            this.contactDataGetContactsBindingSourceBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.contactDataGetContactsBindingSourceBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // contactDataGetContactsBindingSourceDataGridView
            // 
            this.contactDataGetContactsBindingSourceDataGridView.AutoGenerateColumns = false;
            this.contactDataGetContactsBindingSourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.contactDataGetContactsBindingSourceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.contactDataGetContactsBindingSourceDataGridView.DataSource = this.contactDataGetContactsBindingSourceBindingSource;
            this.contactDataGetContactsBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactDataGetContactsBindingSourceDataGridView.Location = new System.Drawing.Point(0, 25);
            this.contactDataGetContactsBindingSourceDataGridView.Name = "contactDataGetContactsBindingSourceDataGridView";
            this.contactDataGetContactsBindingSourceDataGridView.Size = new System.Drawing.Size(439, 346);
            this.contactDataGetContactsBindingSourceDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn1.HeaderText = "name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "email";
            this.dataGridViewTextBoxColumn2.HeaderText = "email";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Key";
            this.dataGridViewTextBoxColumn3.HeaderText = "Key";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Tag";
            this.dataGridViewTextBoxColumn4.HeaderText = "Tag";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Timestamp";
            this.dataGridViewTextBoxColumn5.HeaderText = "Timestamp";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.contactDataGetContactsBindingSourceDataGridView);
            this.Controls.Add(this.contactDataGetContactsBindingSourceBindingNavigator);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(439, 371);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.contactDataGetContactsBindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactDataGetContactsBindingSourceBindingNavigator)).EndInit();
            this.contactDataGetContactsBindingSourceBindingNavigator.ResumeLayout(false);
            this.contactDataGetContactsBindingSourceBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contactDataGetContactsBindingSourceDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            // Note: This jsc project does not support unmanaged resources.
            base.Dispose(disposing);
        }

        private BindingSource contactDataGetContactsBindingSourceBindingSource;
        private BindingNavigator contactDataGetContactsBindingSourceBindingNavigator;
        private ToolStripButton bindingNavigatorAddNewItem;
        private ToolStripLabel bindingNavigatorCountItem;
        private ToolStripButton bindingNavigatorDeleteItem;
        private ToolStripButton bindingNavigatorMoveFirstItem;
        private ToolStripButton bindingNavigatorMovePreviousItem;
        private ToolStripSeparator bindingNavigatorSeparator;
        private ToolStripTextBox bindingNavigatorPositionItem;
        private ToolStripSeparator bindingNavigatorSeparator1;
        private ToolStripButton bindingNavigatorMoveNextItem;
        private ToolStripButton bindingNavigatorMoveLastItem;
        private ToolStripSeparator bindingNavigatorSeparator2;
        private ToolStripButton contactDataGetContactsBindingSourceBindingNavigatorSaveItem;
        private DataGridView contactDataGetContactsBindingSourceDataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private ApplicationWebService applicationWebService1;

    }
}

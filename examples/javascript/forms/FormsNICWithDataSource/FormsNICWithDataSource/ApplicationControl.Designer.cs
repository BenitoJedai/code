using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FormsNICWithDataSource
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
            this.nICDataGetInterfacesBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nICDataGetInterfacesBindingSourceBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.nICDataGetInterfacesBindingSourceBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.nICDataGetInterfacesBindingSourceDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nICDataGetInterfacesBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nICDataGetInterfacesBindingSourceBindingNavigator)).BeginInit();
            this.nICDataGetInterfacesBindingSourceBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nICDataGetInterfacesBindingSourceDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // nICDataGetInterfacesBindingSourceBindingSource
            // 
            this.nICDataGetInterfacesBindingSourceBindingSource.AllowNew = false;
            this.nICDataGetInterfacesBindingSourceBindingSource.DataSource = typeof(FormsNICWithDataSource.Data.NICDataGetInterfacesBindingSource);
            this.nICDataGetInterfacesBindingSourceBindingSource.Position = 0;
            // 
            // nICDataGetInterfacesBindingSourceBindingNavigator
            // 
            this.nICDataGetInterfacesBindingSourceBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.nICDataGetInterfacesBindingSourceBindingNavigator.BindingSource = this.nICDataGetInterfacesBindingSourceBindingSource;
            this.nICDataGetInterfacesBindingSourceBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.nICDataGetInterfacesBindingSourceBindingNavigator.DeleteItem = null;
            this.nICDataGetInterfacesBindingSourceBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.nICDataGetInterfacesBindingSourceBindingNavigatorSaveItem});
            this.nICDataGetInterfacesBindingSourceBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.nICDataGetInterfacesBindingSourceBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.nICDataGetInterfacesBindingSourceBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.nICDataGetInterfacesBindingSourceBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.nICDataGetInterfacesBindingSourceBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.nICDataGetInterfacesBindingSourceBindingNavigator.Name = "nICDataGetInterfacesBindingSourceBindingNavigator";
            this.nICDataGetInterfacesBindingSourceBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.nICDataGetInterfacesBindingSourceBindingNavigator.Size = new System.Drawing.Size(465, 25);
            this.nICDataGetInterfacesBindingSourceBindingNavigator.TabIndex = 0;
            this.nICDataGetInterfacesBindingSourceBindingNavigator.Text = "bindingNavigator1";
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
            // nICDataGetInterfacesBindingSourceBindingNavigatorSaveItem
            // 
            this.nICDataGetInterfacesBindingSourceBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nICDataGetInterfacesBindingSourceBindingNavigatorSaveItem.Enabled = false;
            this.nICDataGetInterfacesBindingSourceBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("nICDataGetInterfacesBindingSourceBindingNavigatorSaveItem.Image")));
            this.nICDataGetInterfacesBindingSourceBindingNavigatorSaveItem.Name = "nICDataGetInterfacesBindingSourceBindingNavigatorSaveItem";
            this.nICDataGetInterfacesBindingSourceBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.nICDataGetInterfacesBindingSourceBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // nICDataGetInterfacesBindingSourceDataGridView
            // 
            this.nICDataGetInterfacesBindingSourceDataGridView.AutoGenerateColumns = false;
            this.nICDataGetInterfacesBindingSourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.nICDataGetInterfacesBindingSourceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.nICDataGetInterfacesBindingSourceDataGridView.DataSource = this.nICDataGetInterfacesBindingSourceBindingSource;
            this.nICDataGetInterfacesBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nICDataGetInterfacesBindingSourceDataGridView.Location = new System.Drawing.Point(0, 25);
            this.nICDataGetInterfacesBindingSourceDataGridView.Name = "nICDataGetInterfacesBindingSourceDataGridView";
            this.nICDataGetInterfacesBindingSourceDataGridView.ReadOnly = true;
            this.nICDataGetInterfacesBindingSourceDataGridView.Size = new System.Drawing.Size(465, 358);
            this.nICDataGetInterfacesBindingSourceDataGridView.TabIndex = 1;
            this.nICDataGetInterfacesBindingSourceDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.nICDataGetInterfacesBindingSourceDataGridView_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "SupportsMulticast";
            this.dataGridViewTextBoxColumn2.HeaderText = "SupportsMulticast";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "GatewayAddresses";
            this.dataGridViewTextBoxColumn3.HeaderText = "GatewayAddresses";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Key";
            this.dataGridViewTextBoxColumn4.HeaderText = "Key";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Tag";
            this.dataGridViewTextBoxColumn5.HeaderText = "Tag";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Timestamp";
            this.dataGridViewTextBoxColumn6.HeaderText = "Timestamp";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.nICDataGetInterfacesBindingSourceDataGridView);
            this.Controls.Add(this.nICDataGetInterfacesBindingSourceBindingNavigator);
            this.Margin = new System.Windows.Forms.Padding(30);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(465, 383);
            ((System.ComponentModel.ISupportInitialize)(this.nICDataGetInterfacesBindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nICDataGetInterfacesBindingSourceBindingNavigator)).EndInit();
            this.nICDataGetInterfacesBindingSourceBindingNavigator.ResumeLayout(false);
            this.nICDataGetInterfacesBindingSourceBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nICDataGetInterfacesBindingSourceDataGridView)).EndInit();
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

        private BindingSource nICDataGetInterfacesBindingSourceBindingSource;
        private BindingNavigator nICDataGetInterfacesBindingSourceBindingNavigator;
        private ToolStripButton bindingNavigatorAddNewItem;
        private ToolStripLabel bindingNavigatorCountItem;
        private ToolStripButton bindingNavigatorMoveFirstItem;
        private ToolStripButton bindingNavigatorMovePreviousItem;
        private ToolStripSeparator bindingNavigatorSeparator;
        private ToolStripTextBox bindingNavigatorPositionItem;
        private ToolStripSeparator bindingNavigatorSeparator1;
        private ToolStripButton bindingNavigatorMoveNextItem;
        private ToolStripButton bindingNavigatorMoveLastItem;
        private ToolStripSeparator bindingNavigatorSeparator2;
        private ToolStripButton nICDataGetInterfacesBindingSourceBindingNavigatorSaveItem;
        private DataGridView nICDataGetInterfacesBindingSourceDataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

    }
}

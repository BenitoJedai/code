namespace SharedBrowserSessionExperiment
{
    partial class PositionsWatchdog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PositionsWatchdog));
            this.navigationOrdersPositionsBindingSourceBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.navigationOrdersPositionsBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
            this.navigationOrdersPositionsBindingSourceBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.navigationOrdersPositionsBindingSourceDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.applicationWebService1 = new SharedBrowserSessionExperiment.ApplicationWebService();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersPositionsBindingSourceBindingNavigator)).BeginInit();
            this.navigationOrdersPositionsBindingSourceBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersPositionsBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersPositionsBindingSourceDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // navigationOrdersPositionsBindingSourceBindingNavigator
            // 
            this.navigationOrdersPositionsBindingSourceBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.navigationOrdersPositionsBindingSourceBindingNavigator.BindingSource = this.navigationOrdersPositionsBindingSourceBindingSource;
            this.navigationOrdersPositionsBindingSourceBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.navigationOrdersPositionsBindingSourceBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.navigationOrdersPositionsBindingSourceBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.navigationOrdersPositionsBindingSourceBindingNavigatorSaveItem});
            this.navigationOrdersPositionsBindingSourceBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.navigationOrdersPositionsBindingSourceBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.navigationOrdersPositionsBindingSourceBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.navigationOrdersPositionsBindingSourceBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.navigationOrdersPositionsBindingSourceBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.navigationOrdersPositionsBindingSourceBindingNavigator.Name = "navigationOrdersPositionsBindingSourceBindingNavigator";
            this.navigationOrdersPositionsBindingSourceBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.navigationOrdersPositionsBindingSourceBindingNavigator.Size = new System.Drawing.Size(542, 27);
            this.navigationOrdersPositionsBindingSourceBindingNavigator.TabIndex = 0;
            this.navigationOrdersPositionsBindingSourceBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 24);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // navigationOrdersPositionsBindingSourceBindingSource
            // 
            this.navigationOrdersPositionsBindingSourceBindingSource.DataSource = typeof(SharedBrowserSessionExperiment.DataLayer.Data.NavigationOrdersPositionsBindingSource);
            this.navigationOrdersPositionsBindingSourceBindingSource.Position = 0;
            //this.navigationOrdersPositionsBindingSourceBindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.navigationOrdersPositionsBindingSourceBindingSource_AddingNew);
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(45, 24);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 24);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 24);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 24);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 27);
            this.bindingNavigatorPositionItem.Text = "1";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 24);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 24);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // navigationOrdersPositionsBindingSourceBindingNavigatorSaveItem
            // 
            this.navigationOrdersPositionsBindingSourceBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navigationOrdersPositionsBindingSourceBindingNavigatorSaveItem.Enabled = false;
            this.navigationOrdersPositionsBindingSourceBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("navigationOrdersPositionsBindingSourceBindingNavigatorSaveItem.Image")));
            this.navigationOrdersPositionsBindingSourceBindingNavigatorSaveItem.Name = "navigationOrdersPositionsBindingSourceBindingNavigatorSaveItem";
            this.navigationOrdersPositionsBindingSourceBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 24);
            this.navigationOrdersPositionsBindingSourceBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // navigationOrdersPositionsBindingSourceDataGridView
            // 
            this.navigationOrdersPositionsBindingSourceDataGridView.AutoGenerateColumns = false;
            this.navigationOrdersPositionsBindingSourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.navigationOrdersPositionsBindingSourceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.navigationOrdersPositionsBindingSourceDataGridView.DataSource = this.navigationOrdersPositionsBindingSourceBindingSource;
            this.navigationOrdersPositionsBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationOrdersPositionsBindingSourceDataGridView.Location = new System.Drawing.Point(0, 27);
            this.navigationOrdersPositionsBindingSourceDataGridView.Name = "navigationOrdersPositionsBindingSourceDataGridView";
            this.navigationOrdersPositionsBindingSourceDataGridView.ReadOnly = true;
            this.navigationOrdersPositionsBindingSourceDataGridView.RowTemplate.Height = 24;
            this.navigationOrdersPositionsBindingSourceDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.navigationOrdersPositionsBindingSourceDataGridView.Size = new System.Drawing.Size(542, 316);
            this.navigationOrdersPositionsBindingSourceDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "NavigateBindingSourcePosition";
            this.dataGridViewTextBoxColumn1.HeaderText = "NavigateBindingSourcePosition";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Left";
            this.dataGridViewTextBoxColumn2.HeaderText = "Left";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Top";
            this.dataGridViewTextBoxColumn3.HeaderText = "Top";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Width";
            this.dataGridViewTextBoxColumn4.HeaderText = "Width";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Height";
            this.dataGridViewTextBoxColumn5.HeaderText = "Height";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Key";
            this.dataGridViewTextBoxColumn6.HeaderText = "Key";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Tag";
            this.dataGridViewTextBoxColumn7.HeaderText = "Tag";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Timestamp";
            this.dataGridViewTextBoxColumn8.HeaderText = "Timestamp";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // PositionsWatchdog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 343);
            this.Controls.Add(this.navigationOrdersPositionsBindingSourceDataGridView);
            this.Controls.Add(this.navigationOrdersPositionsBindingSourceBindingNavigator);
            this.Name = "PositionsWatchdog";
            this.Text = "PositionsWatchdog";
            this.Load += new System.EventHandler(this.PositionsWatchdog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersPositionsBindingSourceBindingNavigator)).EndInit();
            this.navigationOrdersPositionsBindingSourceBindingNavigator.ResumeLayout(false);
            this.navigationOrdersPositionsBindingSourceBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersPositionsBindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersPositionsBindingSourceDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource navigationOrdersPositionsBindingSourceBindingSource;
        private System.Windows.Forms.BindingNavigator navigationOrdersPositionsBindingSourceBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton navigationOrdersPositionsBindingSourceBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView navigationOrdersPositionsBindingSourceDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private ApplicationWebService applicationWebService1;
    }
}
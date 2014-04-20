using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FormsHistoricBindingSourcePosition
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
            System.Windows.Forms.Label hashLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationControl));
            System.Windows.Forms.Label documentTextLabel;
            this.navigationOrdersNavigateBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.navigationOrdersNavigateBindingSourceBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.navigationOrdersNavigateBindingSourceDataGridView = new System.Windows.Forms.DataGridView();
            this.hashDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hashTextBox = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.documentTextTextBox = new System.Windows.Forms.TextBox();
            hashLabel = new System.Windows.Forms.Label();
            documentTextLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersNavigateBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersNavigateBindingSourceBindingNavigator)).BeginInit();
            this.navigationOrdersNavigateBindingSourceBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersNavigateBindingSourceDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // hashLabel
            // 
            hashLabel.AutoSize = true;
            hashLabel.Location = new System.Drawing.Point(292, 57);
            hashLabel.Name = "hashLabel";
            hashLabel.Size = new System.Drawing.Size(33, 13);
            hashLabel.TabIndex = 2;
            hashLabel.Text = "hash:";
            // 
            // navigationOrdersNavigateBindingSourceBindingSource
            // 
            this.navigationOrdersNavigateBindingSourceBindingSource.DataSource = typeof(HashForBindingSource.DataSourcez.Dataz.ZeDocumentTextzNavigateBindingSource);
            this.navigationOrdersNavigateBindingSourceBindingSource.Position = 0;
            this.navigationOrdersNavigateBindingSourceBindingSource.BindingComplete += new System.Windows.Forms.BindingCompleteEventHandler(this.navigationOrdersNavigateBindingSourceBindingSource_BindingComplete);
            this.navigationOrdersNavigateBindingSourceBindingSource.DataSourceChanged += new System.EventHandler(this.navigationOrdersNavigateBindingSourceBindingSource_DataSourceChanged);
            this.navigationOrdersNavigateBindingSourceBindingSource.DataMemberChanged += new System.EventHandler(this.navigationOrdersNavigateBindingSourceBindingSource_DataMemberChanged);
            this.navigationOrdersNavigateBindingSourceBindingSource.CurrentChanged += new System.EventHandler(this.navigationOrdersNavigateBindingSourceBindingSource_CurrentChanged);
            this.navigationOrdersNavigateBindingSourceBindingSource.CurrentItemChanged += new System.EventHandler(this.navigationOrdersNavigateBindingSourceBindingSource_CurrentItemChanged);
            // 
            // navigationOrdersNavigateBindingSourceBindingNavigator
            // 
            this.navigationOrdersNavigateBindingSourceBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.navigationOrdersNavigateBindingSourceBindingNavigator.BindingSource = this.navigationOrdersNavigateBindingSourceBindingSource;
            this.navigationOrdersNavigateBindingSourceBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.navigationOrdersNavigateBindingSourceBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.navigationOrdersNavigateBindingSourceBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem});
            this.navigationOrdersNavigateBindingSourceBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.navigationOrdersNavigateBindingSourceBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.navigationOrdersNavigateBindingSourceBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.navigationOrdersNavigateBindingSourceBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.navigationOrdersNavigateBindingSourceBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.navigationOrdersNavigateBindingSourceBindingNavigator.Name = "navigationOrdersNavigateBindingSourceBindingNavigator";
            this.navigationOrdersNavigateBindingSourceBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.navigationOrdersNavigateBindingSourceBindingNavigator.Size = new System.Drawing.Size(987, 25);
            this.navigationOrdersNavigateBindingSourceBindingNavigator.TabIndex = 0;
            this.navigationOrdersNavigateBindingSourceBindingNavigator.Text = "bindingNavigator1";
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
            // navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem
            // 
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.Enabled = false;
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.Image")));
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.Name = "navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem";
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // navigationOrdersNavigateBindingSourceDataGridView
            // 
            this.navigationOrdersNavigateBindingSourceDataGridView.AutoGenerateColumns = false;
            this.navigationOrdersNavigateBindingSourceDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.navigationOrdersNavigateBindingSourceDataGridView.BackgroundColor = System.Drawing.Color.Teal;
            this.navigationOrdersNavigateBindingSourceDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.navigationOrdersNavigateBindingSourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.navigationOrdersNavigateBindingSourceDataGridView.ColumnHeadersVisible = false;
            this.navigationOrdersNavigateBindingSourceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.hashDataGridViewTextBoxColumn});
            this.navigationOrdersNavigateBindingSourceDataGridView.DataSource = this.navigationOrdersNavigateBindingSourceBindingSource;
            this.navigationOrdersNavigateBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Left;
            this.navigationOrdersNavigateBindingSourceDataGridView.GridColor = System.Drawing.Color.Blue;
            this.navigationOrdersNavigateBindingSourceDataGridView.Location = new System.Drawing.Point(0, 25);
            this.navigationOrdersNavigateBindingSourceDataGridView.Name = "navigationOrdersNavigateBindingSourceDataGridView";
            this.navigationOrdersNavigateBindingSourceDataGridView.RowHeadersVisible = false;
            this.navigationOrdersNavigateBindingSourceDataGridView.Size = new System.Drawing.Size(214, 484);
            this.navigationOrdersNavigateBindingSourceDataGridView.TabIndex = 1;
            // 
            // hashDataGridViewTextBoxColumn
            // 
            this.hashDataGridViewTextBoxColumn.DataPropertyName = "hash";
            this.hashDataGridViewTextBoxColumn.HeaderText = "hash";
            this.hashDataGridViewTextBoxColumn.Name = "hashDataGridViewTextBoxColumn";
            // 
            // hashTextBox
            // 
            this.hashTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.navigationOrdersNavigateBindingSourceBindingSource, "hash", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.hashTextBox.Location = new System.Drawing.Point(331, 54);
            this.hashTextBox.Name = "hashTextBox";
            this.hashTextBox.Size = new System.Drawing.Size(544, 20);
            this.hashTextBox.TabIndex = 3;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(331, 80);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(544, 180);
            this.webBrowser1.TabIndex = 4;
            // 
            // documentTextLabel
            // 
            documentTextLabel.AutoSize = true;
            documentTextLabel.Location = new System.Drawing.Point(242, 269);
            documentTextLabel.Name = "documentTextLabel";
            documentTextLabel.Size = new System.Drawing.Size(83, 13);
            documentTextLabel.TabIndex = 5;
            documentTextLabel.Text = "Document Text:";
            // 
            // documentTextTextBox
            // 
            this.documentTextTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.navigationOrdersNavigateBindingSourceBindingSource, "DocumentText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.documentTextTextBox.Location = new System.Drawing.Point(331, 266);
            this.documentTextTextBox.Multiline = true;
            this.documentTextTextBox.Name = "documentTextTextBox";
            this.documentTextTextBox.Size = new System.Drawing.Size(544, 213);
            this.documentTextTextBox.TabIndex = 6;
            this.documentTextTextBox.WordWrap = false;
            // 
            // ApplicationControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Controls.Add(documentTextLabel);
            this.Controls.Add(this.documentTextTextBox);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(hashLabel);
            this.Controls.Add(this.hashTextBox);
            this.Controls.Add(this.navigationOrdersNavigateBindingSourceDataGridView);
            this.Controls.Add(this.navigationOrdersNavigateBindingSourceBindingNavigator);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(987, 509);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            this.VisibleChanged += new System.EventHandler(this.ApplicationControl_VisibleChanged);
            this.ParentChanged += new System.EventHandler(this.ApplicationControl_ParentChanged);
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersNavigateBindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersNavigateBindingSourceBindingNavigator)).EndInit();
            this.navigationOrdersNavigateBindingSourceBindingNavigator.ResumeLayout(false);
            this.navigationOrdersNavigateBindingSourceBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersNavigateBindingSourceDataGridView)).EndInit();
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
        private BindingNavigator navigationOrdersNavigateBindingSourceBindingNavigator;
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
        private ToolStripButton navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem;
        private DataGridView navigationOrdersNavigateBindingSourceDataGridView;
        private TextBox hashTextBox;
        private DataGridViewTextBoxColumn hashDataGridViewTextBoxColumn;
        public BindingSource navigationOrdersNavigateBindingSourceBindingSource;
        private WebBrowser webBrowser1;
        private TextBox documentTextTextBox;

    }
}

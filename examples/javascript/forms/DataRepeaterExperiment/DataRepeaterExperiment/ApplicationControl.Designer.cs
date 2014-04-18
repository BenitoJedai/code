using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DataRepeaterExperiment
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
            System.Windows.Forms.Label urlStringLabel;
            System.Windows.Forms.Label timestampLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationControl));
            System.Windows.Forms.Label keyLabel;
            this.dataRepeater1 = new Microsoft.VisualBasic.PowerPacks.DataRepeater();
            this.timestampDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.navigationOrdersNavigateBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.urlStringTextBox = new System.Windows.Forms.TextBox();
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
            this.keyLabel1 = new System.Windows.Forms.Label();
            urlStringLabel = new System.Windows.Forms.Label();
            timestampLabel = new System.Windows.Forms.Label();
            keyLabel = new System.Windows.Forms.Label();
            this.dataRepeater1.ItemTemplate.SuspendLayout();
            this.dataRepeater1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersNavigateBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersNavigateBindingSourceBindingNavigator)).BeginInit();
            this.navigationOrdersNavigateBindingSourceBindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // urlStringLabel
            // 
            urlStringLabel.AutoSize = true;
            urlStringLabel.Location = new System.Drawing.Point(157, 60);
            urlStringLabel.Name = "urlStringLabel";
            urlStringLabel.Size = new System.Drawing.Size(69, 17);
            urlStringLabel.TabIndex = 0;
            urlStringLabel.Text = "url String:";
            // 
            // timestampLabel
            // 
            timestampLabel.AutoSize = true;
            timestampLabel.Location = new System.Drawing.Point(145, 99);
            timestampLabel.Name = "timestampLabel";
            timestampLabel.Size = new System.Drawing.Size(81, 17);
            timestampLabel.TabIndex = 2;
            timestampLabel.Text = "Timestamp:";
            // 
            // dataRepeater1
            // 
            this.dataRepeater1.ItemHeaderVisible = false;
            // 
            // dataRepeater1.ItemTemplate
            // 
            this.dataRepeater1.ItemTemplate.Controls.Add(keyLabel);
            this.dataRepeater1.ItemTemplate.Controls.Add(this.keyLabel1);
            this.dataRepeater1.ItemTemplate.Controls.Add(timestampLabel);
            this.dataRepeater1.ItemTemplate.Controls.Add(this.timestampDateTimePicker);
            this.dataRepeater1.ItemTemplate.Controls.Add(urlStringLabel);
            this.dataRepeater1.ItemTemplate.Controls.Add(this.urlStringTextBox);
            this.dataRepeater1.ItemTemplate.Size = new System.Drawing.Size(564, 151);
            this.dataRepeater1.Location = new System.Drawing.Point(40, 77);
            this.dataRepeater1.Name = "dataRepeater1";
            this.dataRepeater1.Size = new System.Drawing.Size(572, 396);
            this.dataRepeater1.TabIndex = 0;
            this.dataRepeater1.Text = "dataRepeater1";
            // 
            // timestampDateTimePicker
            // 
            this.timestampDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.navigationOrdersNavigateBindingSourceBindingSource, "Timestamp", true));
            this.timestampDateTimePicker.Enabled = false;
            this.timestampDateTimePicker.Location = new System.Drawing.Point(232, 95);
            this.timestampDateTimePicker.Name = "timestampDateTimePicker";
            this.timestampDateTimePicker.Size = new System.Drawing.Size(294, 22);
            this.timestampDateTimePicker.TabIndex = 3;
            // 
            // navigationOrdersNavigateBindingSourceBindingSource
            // 
            this.navigationOrdersNavigateBindingSourceBindingSource.DataSource = typeof(SharedBrowserSessionExperiment.DataLayer.Data.NavigationOrdersNavigateBindingSource);
            this.navigationOrdersNavigateBindingSourceBindingSource.Position = 0;
            // 
            // urlStringTextBox
            // 
            this.urlStringTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.navigationOrdersNavigateBindingSourceBindingSource, "urlString", true));
            this.urlStringTextBox.Location = new System.Drawing.Point(232, 57);
            this.urlStringTextBox.Name = "urlStringTextBox";
            this.urlStringTextBox.Size = new System.Drawing.Size(294, 22);
            this.urlStringTextBox.TabIndex = 1;
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
            this.navigationOrdersNavigateBindingSourceBindingNavigator.Size = new System.Drawing.Size(646, 27);
            this.navigationOrdersNavigateBindingSourceBindingNavigator.TabIndex = 1;
            this.navigationOrdersNavigateBindingSourceBindingNavigator.Text = "bindingNavigator1";
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
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(126, 24);
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
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(98, 24);
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
            // navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem
            // 
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.Enabled = false;
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.Image")));
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.Name = "navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem";
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 24);
            this.navigationOrdersNavigateBindingSourceBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // keyLabel
            // 
            keyLabel.AutoSize = true;
            keyLabel.Location = new System.Drawing.Point(190, 24);
            keyLabel.Name = "keyLabel";
            keyLabel.Size = new System.Drawing.Size(36, 17);
            keyLabel.TabIndex = 4;
            keyLabel.Text = "Key:";
            // 
            // keyLabel1
            // 
            this.keyLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.keyLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.navigationOrdersNavigateBindingSourceBindingSource, "Key", true));
            this.keyLabel1.Location = new System.Drawing.Point(232, 24);
            this.keyLabel1.Name = "keyLabel1";
            this.keyLabel1.Size = new System.Drawing.Size(100, 23);
            this.keyLabel1.TabIndex = 5;
            this.keyLabel1.Text = "label1";
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.navigationOrdersNavigateBindingSourceBindingNavigator);
            this.Controls.Add(this.dataRepeater1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(646, 564);
            this.dataRepeater1.ItemTemplate.ResumeLayout(false);
            this.dataRepeater1.ItemTemplate.PerformLayout();
            this.dataRepeater1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersNavigateBindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersNavigateBindingSourceBindingNavigator)).EndInit();
            this.navigationOrdersNavigateBindingSourceBindingNavigator.ResumeLayout(false);
            this.navigationOrdersNavigateBindingSourceBindingNavigator.PerformLayout();
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

        private Microsoft.VisualBasic.PowerPacks.DataRepeater dataRepeater1;
        private DateTimePicker timestampDateTimePicker;
        private BindingSource navigationOrdersNavigateBindingSourceBindingSource;
        private TextBox urlStringTextBox;
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
        private Label keyLabel1;

    }
}

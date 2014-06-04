using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestBindingSource
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
            System.Windows.Forms.Label dateLabel;
            System.Windows.Forms.Label countLabel;
            System.Windows.Forms.Label keyLabel;
            System.Windows.Forms.Label tagLabel;
            System.Windows.Forms.Label timestampLabel;
            this.visualizationzDateToCountBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.visualizationzDateToCountBindingSourceBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.visualizationzDateToCountBindingSourceBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.dateLinkLabel = new System.Windows.Forms.LinkLabel();
            this.countTextBox = new System.Windows.Forms.TextBox();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.tagTextBox = new System.Windows.Forms.TextBox();
            this.timestampTextBox = new System.Windows.Forms.TextBox();
            dateLabel = new System.Windows.Forms.Label();
            countLabel = new System.Windows.Forms.Label();
            keyLabel = new System.Windows.Forms.Label();
            tagLabel = new System.Windows.Forms.Label();
            timestampLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.visualizationzDateToCountBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visualizationzDateToCountBindingSourceBindingNavigator)).BeginInit();
            this.visualizationzDateToCountBindingSourceBindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // visualizationzDateToCountBindingSourceBindingSource
            // 
            this.visualizationzDateToCountBindingSourceBindingSource.DataSource = typeof(TestBindingSource.Data.VisualizationzDateToCountBindingSource);
            this.visualizationzDateToCountBindingSourceBindingSource.Position = 0;
            // 
            // visualizationzDateToCountBindingSourceBindingNavigator
            // 
            this.visualizationzDateToCountBindingSourceBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.visualizationzDateToCountBindingSourceBindingNavigator.BindingSource = this.visualizationzDateToCountBindingSourceBindingSource;
            this.visualizationzDateToCountBindingSourceBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.visualizationzDateToCountBindingSourceBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.visualizationzDateToCountBindingSourceBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.visualizationzDateToCountBindingSourceBindingNavigatorSaveItem});
            this.visualizationzDateToCountBindingSourceBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.visualizationzDateToCountBindingSourceBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.visualizationzDateToCountBindingSourceBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.visualizationzDateToCountBindingSourceBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.visualizationzDateToCountBindingSourceBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.visualizationzDateToCountBindingSourceBindingNavigator.Name = "visualizationzDateToCountBindingSourceBindingNavigator";
            this.visualizationzDateToCountBindingSourceBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.visualizationzDateToCountBindingSourceBindingNavigator.Size = new System.Drawing.Size(318, 25);
            this.visualizationzDateToCountBindingSourceBindingNavigator.TabIndex = 0;
            this.visualizationzDateToCountBindingSourceBindingNavigator.Text = "bindingNavigator1";
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
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(28, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
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
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // visualizationzDateToCountBindingSourceBindingNavigatorSaveItem
            // 
            this.visualizationzDateToCountBindingSourceBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.visualizationzDateToCountBindingSourceBindingNavigatorSaveItem.Enabled = false;
            this.visualizationzDateToCountBindingSourceBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("visualizationzDateToCountBindingSourceBindingNavigatorSaveItem.Image")));
            this.visualizationzDateToCountBindingSourceBindingNavigatorSaveItem.Name = "visualizationzDateToCountBindingSourceBindingNavigatorSaveItem";
            this.visualizationzDateToCountBindingSourceBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.visualizationzDateToCountBindingSourceBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // dateLabel
            // 
            dateLabel.AutoSize = true;
            dateLabel.Location = new System.Drawing.Point(20, 47);
            dateLabel.Name = "dateLabel";
            dateLabel.Size = new System.Drawing.Size(33, 13);
            dateLabel.TabIndex = 1;
            dateLabel.Text = "Date:";
            // 
            // dateLinkLabel
            // 
            this.dateLinkLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.visualizationzDateToCountBindingSourceBindingSource, "Date", true));
            this.dateLinkLabel.Location = new System.Drawing.Point(87, 47);
            this.dateLinkLabel.Name = "dateLinkLabel";
            this.dateLinkLabel.Size = new System.Drawing.Size(100, 23);
            this.dateLinkLabel.TabIndex = 2;
            this.dateLinkLabel.TabStop = true;
            this.dateLinkLabel.Text = "linkLabel1";
            // 
            // countLabel
            // 
            countLabel.AutoSize = true;
            countLabel.Location = new System.Drawing.Point(20, 76);
            countLabel.Name = "countLabel";
            countLabel.Size = new System.Drawing.Size(38, 13);
            countLabel.TabIndex = 3;
            countLabel.Text = "Count:";
            // 
            // countTextBox
            // 
            this.countTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.visualizationzDateToCountBindingSourceBindingSource, "Count", true));
            this.countTextBox.Location = new System.Drawing.Point(87, 73);
            this.countTextBox.Name = "countTextBox";
            this.countTextBox.Size = new System.Drawing.Size(100, 20);
            this.countTextBox.TabIndex = 4;
            // 
            // keyLabel
            // 
            keyLabel.AutoSize = true;
            keyLabel.Location = new System.Drawing.Point(20, 102);
            keyLabel.Name = "keyLabel";
            keyLabel.Size = new System.Drawing.Size(28, 13);
            keyLabel.TabIndex = 5;
            keyLabel.Text = "Key:";
            // 
            // keyTextBox
            // 
            this.keyTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.visualizationzDateToCountBindingSourceBindingSource, "Key", true));
            this.keyTextBox.Location = new System.Drawing.Point(87, 99);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(100, 20);
            this.keyTextBox.TabIndex = 6;
            // 
            // tagLabel
            // 
            tagLabel.AutoSize = true;
            tagLabel.Location = new System.Drawing.Point(20, 128);
            tagLabel.Name = "tagLabel";
            tagLabel.Size = new System.Drawing.Size(29, 13);
            tagLabel.TabIndex = 7;
            tagLabel.Text = "Tag:";
            // 
            // tagTextBox
            // 
            this.tagTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.visualizationzDateToCountBindingSourceBindingSource, "Tag", true));
            this.tagTextBox.Location = new System.Drawing.Point(87, 125);
            this.tagTextBox.Name = "tagTextBox";
            this.tagTextBox.Size = new System.Drawing.Size(100, 20);
            this.tagTextBox.TabIndex = 8;
            // 
            // timestampLabel
            // 
            timestampLabel.AutoSize = true;
            timestampLabel.Location = new System.Drawing.Point(20, 154);
            timestampLabel.Name = "timestampLabel";
            timestampLabel.Size = new System.Drawing.Size(61, 13);
            timestampLabel.TabIndex = 9;
            timestampLabel.Text = "Timestamp:";
            // 
            // timestampTextBox
            // 
            this.timestampTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.visualizationzDateToCountBindingSourceBindingSource, "Timestamp", true));
            this.timestampTextBox.Location = new System.Drawing.Point(87, 151);
            this.timestampTextBox.Name = "timestampTextBox";
            this.timestampTextBox.Size = new System.Drawing.Size(100, 20);
            this.timestampTextBox.TabIndex = 10;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(dateLabel);
            this.Controls.Add(this.dateLinkLabel);
            this.Controls.Add(countLabel);
            this.Controls.Add(this.countTextBox);
            this.Controls.Add(keyLabel);
            this.Controls.Add(this.keyTextBox);
            this.Controls.Add(tagLabel);
            this.Controls.Add(this.tagTextBox);
            this.Controls.Add(timestampLabel);
            this.Controls.Add(this.timestampTextBox);
            this.Controls.Add(this.visualizationzDateToCountBindingSourceBindingNavigator);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(318, 207);
            ((System.ComponentModel.ISupportInitialize)(this.visualizationzDateToCountBindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visualizationzDateToCountBindingSourceBindingNavigator)).EndInit();
            this.visualizationzDateToCountBindingSourceBindingNavigator.ResumeLayout(false);
            this.visualizationzDateToCountBindingSourceBindingNavigator.PerformLayout();
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

        private BindingSource visualizationzDateToCountBindingSourceBindingSource;
        private BindingNavigator visualizationzDateToCountBindingSourceBindingNavigator;
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
        private ToolStripButton visualizationzDateToCountBindingSourceBindingNavigatorSaveItem;
        private LinkLabel dateLinkLabel;
        private TextBox countTextBox;
        private TextBox keyTextBox;
        private TextBox tagTextBox;
        private TextBox timestampTextBox;

    }
}

namespace DataRepeaterExperiment
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.Label navigateBindingSourcePositionLabel;
            System.Windows.Forms.Label timestampLabel;
            this.navigationOrdersPositionsBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.navigationOrdersPositionsBindingSourceBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.navigationOrdersPositionsBindingSourceBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.navigateBindingSourcePositionTextBox = new System.Windows.Forms.TextBox();
            this.timestampDateTimePicker = new System.Windows.Forms.DateTimePicker();
            navigateBindingSourcePositionLabel = new System.Windows.Forms.Label();
            timestampLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersPositionsBindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersPositionsBindingSourceBindingNavigator)).BeginInit();
            this.navigationOrdersPositionsBindingSourceBindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // navigationOrdersPositionsBindingSourceBindingSource
            // 
            this.navigationOrdersPositionsBindingSourceBindingSource.DataSource = typeof(SharedBrowserSessionExperiment.DataLayer.Data.NavigationOrdersPositionsBindingSource);
            this.navigationOrdersPositionsBindingSourceBindingSource.Position = 0;
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
            this.navigationOrdersPositionsBindingSourceBindingNavigator.Size = new System.Drawing.Size(566, 27);
            this.navigationOrdersPositionsBindingSourceBindingNavigator.TabIndex = 0;
            this.navigationOrdersPositionsBindingSourceBindingNavigator.Text = "bindingNavigator1";
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
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(45, 24);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
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
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
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
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 24);
            this.bindingNavigatorDeleteItem.Text = "Delete";
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
            // navigateBindingSourcePositionLabel
            // 
            navigateBindingSourcePositionLabel.AutoSize = true;
            navigateBindingSourcePositionLabel.Location = new System.Drawing.Point(110, 113);
            navigateBindingSourcePositionLabel.Name = "navigateBindingSourcePositionLabel";
            navigateBindingSourcePositionLabel.Size = new System.Drawing.Size(222, 17);
            navigateBindingSourcePositionLabel.TabIndex = 1;
            navigateBindingSourcePositionLabel.Text = "Navigate Binding Source Position:";
            // 
            // navigateBindingSourcePositionTextBox
            // 
            this.navigateBindingSourcePositionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.navigationOrdersPositionsBindingSourceBindingSource, "NavigateBindingSourcePosition", true));
            this.navigateBindingSourcePositionTextBox.Location = new System.Drawing.Point(338, 110);
            this.navigateBindingSourcePositionTextBox.Name = "navigateBindingSourcePositionTextBox";
            this.navigateBindingSourcePositionTextBox.Size = new System.Drawing.Size(100, 22);
            this.navigateBindingSourcePositionTextBox.TabIndex = 2;
            // 
            // timestampLabel
            // 
            timestampLabel.AutoSize = true;
            timestampLabel.Location = new System.Drawing.Point(151, 156);
            timestampLabel.Name = "timestampLabel";
            timestampLabel.Size = new System.Drawing.Size(81, 17);
            timestampLabel.TabIndex = 3;
            timestampLabel.Text = "Timestamp:";
            // 
            // timestampDateTimePicker
            // 
            this.timestampDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.navigationOrdersPositionsBindingSourceBindingSource, "Timestamp", true));
            this.timestampDateTimePicker.Location = new System.Drawing.Point(238, 152);
            this.timestampDateTimePicker.Name = "timestampDateTimePicker";
            this.timestampDateTimePicker.Size = new System.Drawing.Size(200, 22);
            this.timestampDateTimePicker.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 248);
            this.Controls.Add(timestampLabel);
            this.Controls.Add(this.timestampDateTimePicker);
            this.Controls.Add(navigateBindingSourcePositionLabel);
            this.Controls.Add(this.navigateBindingSourcePositionTextBox);
            this.Controls.Add(this.navigationOrdersPositionsBindingSourceBindingNavigator);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersPositionsBindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationOrdersPositionsBindingSourceBindingNavigator)).EndInit();
            this.navigationOrdersPositionsBindingSourceBindingNavigator.ResumeLayout(false);
            this.navigationOrdersPositionsBindingSourceBindingNavigator.PerformLayout();
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
        private System.Windows.Forms.TextBox navigateBindingSourcePositionTextBox;
        private System.Windows.Forms.DateTimePicker timestampDateTimePicker;
    }
}
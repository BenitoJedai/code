namespace SQLiteWithDataGridViewX.Library
{
    partial class GridFormX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridFormX));
            System.Windows.Forms.Label parentContentKeyLabel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.schemaTheGridTableViewBindingSourceDataGridView = new System.Windows.Forms.DataGridView();
            this.schemaTheGridTableViewBindingSourceBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.schemaTheGridTableViewBindingSourceBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.parentContentKeyTextBox = new System.Windows.Forms.TextBox();
            this.contentKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentCommentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentChildrenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.parentContentKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updateCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schemaTheGridTableViewBindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.applicationWebService1 = new SQLiteWithDataGridViewX.ApplicationWebService();
            parentContentKeyLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schemaTheGridTableViewBindingSourceDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schemaTheGridTableViewBindingSourceBindingNavigator)).BeginInit();
            this.schemaTheGridTableViewBindingSourceBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schemaTheGridTableViewBindingSourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(parentContentKeyLabel);
            this.panel1.Controls.Add(this.parentContentKeyTextBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 143);
            this.panel1.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "?";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(409, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(153, 17);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "Synchronise with database";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Parent:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Transaction:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.schemaTheGridTableViewBindingSourceDataGridView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 165);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(16);
            this.panel2.Size = new System.Drawing.Size(775, 358);
            this.panel2.TabIndex = 17;
            // 
            // schemaTheGridTableViewBindingSourceDataGridView
            // 
            this.schemaTheGridTableViewBindingSourceDataGridView.AutoGenerateColumns = false;
            this.schemaTheGridTableViewBindingSourceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.schemaTheGridTableViewBindingSourceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.contentKeyDataGridViewTextBoxColumn,
            this.contentValueDataGridViewTextBoxColumn,
            this.contentCommentDataGridViewTextBoxColumn,
            this.contentChildrenDataGridViewTextBoxColumn,
            this.parentContentKeyDataGridViewTextBoxColumn,
            this.updateCountDataGridViewTextBoxColumn});
            this.schemaTheGridTableViewBindingSourceDataGridView.DataSource = this.schemaTheGridTableViewBindingSourceBindingSource;
            this.schemaTheGridTableViewBindingSourceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schemaTheGridTableViewBindingSourceDataGridView.Location = new System.Drawing.Point(16, 16);
            this.schemaTheGridTableViewBindingSourceDataGridView.Name = "schemaTheGridTableViewBindingSourceDataGridView";
            this.schemaTheGridTableViewBindingSourceDataGridView.Size = new System.Drawing.Size(743, 326);
            this.schemaTheGridTableViewBindingSourceDataGridView.TabIndex = 0;
            this.schemaTheGridTableViewBindingSourceDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.schemaTheGridTableViewBindingSourceDataGridView_CellContentClick);
            // 
            // schemaTheGridTableViewBindingSourceBindingNavigator
            // 
            this.schemaTheGridTableViewBindingSourceBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.schemaTheGridTableViewBindingSourceBindingNavigator.BindingSource = this.schemaTheGridTableViewBindingSourceBindingSource;
            this.schemaTheGridTableViewBindingSourceBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.schemaTheGridTableViewBindingSourceBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.schemaTheGridTableViewBindingSourceBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.schemaTheGridTableViewBindingSourceBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.schemaTheGridTableViewBindingSourceBindingNavigatorSaveItem});
            this.schemaTheGridTableViewBindingSourceBindingNavigator.Location = new System.Drawing.Point(0, 523);
            this.schemaTheGridTableViewBindingSourceBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.schemaTheGridTableViewBindingSourceBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.schemaTheGridTableViewBindingSourceBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.schemaTheGridTableViewBindingSourceBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.schemaTheGridTableViewBindingSourceBindingNavigator.Name = "schemaTheGridTableViewBindingSourceBindingNavigator";
            this.schemaTheGridTableViewBindingSourceBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.schemaTheGridTableViewBindingSourceBindingNavigator.Size = new System.Drawing.Size(775, 25);
            this.schemaTheGridTableViewBindingSourceBindingNavigator.TabIndex = 18;
            this.schemaTheGridTableViewBindingSourceBindingNavigator.Text = "bindingNavigator1";
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
            // schemaTheGridTableViewBindingSourceBindingNavigatorSaveItem
            // 
            this.schemaTheGridTableViewBindingSourceBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.schemaTheGridTableViewBindingSourceBindingNavigatorSaveItem.Enabled = false;
            this.schemaTheGridTableViewBindingSourceBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("schemaTheGridTableViewBindingSourceBindingNavigatorSaveItem.Image")));
            this.schemaTheGridTableViewBindingSourceBindingNavigatorSaveItem.Name = "schemaTheGridTableViewBindingSourceBindingNavigatorSaveItem";
            this.schemaTheGridTableViewBindingSourceBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.schemaTheGridTableViewBindingSourceBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // parentContentKeyLabel
            // 
            parentContentKeyLabel.AutoSize = true;
            parentContentKeyLabel.Location = new System.Drawing.Point(13, 65);
            parentContentKeyLabel.Name = "parentContentKeyLabel";
            parentContentKeyLabel.Size = new System.Drawing.Size(102, 13);
            parentContentKeyLabel.TabIndex = 20;
            parentContentKeyLabel.Text = "Parent Content Key:";
            // 
            // parentContentKeyTextBox
            // 
            this.parentContentKeyTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.schemaTheGridTableViewBindingSourceBindingSource, "ParentContentKey", true));
            this.parentContentKeyTextBox.Location = new System.Drawing.Point(121, 62);
            this.parentContentKeyTextBox.Name = "parentContentKeyTextBox";
            this.parentContentKeyTextBox.Size = new System.Drawing.Size(100, 20);
            this.parentContentKeyTextBox.TabIndex = 21;
            // 
            // contentKeyDataGridViewTextBoxColumn
            // 
            this.contentKeyDataGridViewTextBoxColumn.DataPropertyName = "ContentKey";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Aqua;
            this.contentKeyDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.contentKeyDataGridViewTextBoxColumn.HeaderText = "ContentKey";
            this.contentKeyDataGridViewTextBoxColumn.Name = "contentKeyDataGridViewTextBoxColumn";
            // 
            // contentValueDataGridViewTextBoxColumn
            // 
            this.contentValueDataGridViewTextBoxColumn.DataPropertyName = "ContentValue";
            this.contentValueDataGridViewTextBoxColumn.HeaderText = "ContentValue";
            this.contentValueDataGridViewTextBoxColumn.Name = "contentValueDataGridViewTextBoxColumn";
            // 
            // contentCommentDataGridViewTextBoxColumn
            // 
            this.contentCommentDataGridViewTextBoxColumn.DataPropertyName = "ContentComment";
            this.contentCommentDataGridViewTextBoxColumn.HeaderText = "ContentComment";
            this.contentCommentDataGridViewTextBoxColumn.Name = "contentCommentDataGridViewTextBoxColumn";
            // 
            // contentChildrenDataGridViewTextBoxColumn
            // 
            this.contentChildrenDataGridViewTextBoxColumn.DataPropertyName = "ContentChildren";
            this.contentChildrenDataGridViewTextBoxColumn.HeaderText = "ContentChildren";
            this.contentChildrenDataGridViewTextBoxColumn.Name = "contentChildrenDataGridViewTextBoxColumn";
            this.contentChildrenDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.contentChildrenDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // parentContentKeyDataGridViewTextBoxColumn
            // 
            this.parentContentKeyDataGridViewTextBoxColumn.DataPropertyName = "ParentContentKey";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.parentContentKeyDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.parentContentKeyDataGridViewTextBoxColumn.HeaderText = "ParentContentKey";
            this.parentContentKeyDataGridViewTextBoxColumn.Name = "parentContentKeyDataGridViewTextBoxColumn";
            // 
            // updateCountDataGridViewTextBoxColumn
            // 
            this.updateCountDataGridViewTextBoxColumn.DataPropertyName = "UpdateCount";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.updateCountDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.updateCountDataGridViewTextBoxColumn.HeaderText = "UpdateCount";
            this.updateCountDataGridViewTextBoxColumn.Name = "updateCountDataGridViewTextBoxColumn";
            // 
            // schemaTheGridTableViewBindingSourceBindingSource
            // 
            this.schemaTheGridTableViewBindingSourceBindingSource.DataSource = typeof(SQLiteWithDataGridViewX.Data.SchemaTheGridTableViewBindingSource);
            this.schemaTheGridTableViewBindingSourceBindingSource.Position = 0;
            // 
            // GridFormX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 548);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.schemaTheGridTableViewBindingSourceBindingNavigator);
            this.Name = "GridFormX";
            this.Padding = new System.Windows.Forms.Padding(0, 22, 0, 0);
            this.Text = "GridFormX";
            this.Load += new System.EventHandler(this.GridFormX_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.schemaTheGridTableViewBindingSourceDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schemaTheGridTableViewBindingSourceBindingNavigator)).EndInit();
            this.schemaTheGridTableViewBindingSourceBindingNavigator.ResumeLayout(false);
            this.schemaTheGridTableViewBindingSourceBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.schemaTheGridTableViewBindingSourceBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView schemaTheGridTableViewBindingSourceDataGridView;
        private System.Windows.Forms.BindingSource schemaTheGridTableViewBindingSourceBindingSource;
        private System.Windows.Forms.BindingNavigator schemaTheGridTableViewBindingSourceBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton schemaTheGridTableViewBindingSourceBindingNavigatorSaveItem;
        private ApplicationWebService applicationWebService1;
        private System.Windows.Forms.TextBox parentContentKeyTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentKeyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentCommentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn contentChildrenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn parentContentKeyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updateCountDataGridViewTextBoxColumn;
    }
}
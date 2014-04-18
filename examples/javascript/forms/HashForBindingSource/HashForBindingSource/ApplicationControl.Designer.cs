using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HashForBindingSource
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.zeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.zeBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.zeBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.hashTextBox = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.xWebBrowser1 = new HashForBindingSource.XWebBrowser();
            hashLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.zeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zeBindingNavigator)).BeginInit();
            this.zeBindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // hashLabel
            // 
            hashLabel.AutoSize = true;
            hashLabel.Location = new System.Drawing.Point(329, 81);
            hashLabel.Name = "hashLabel";
            hashLabel.Size = new System.Drawing.Size(33, 13);
            hashLabel.TabIndex = 5;
            hashLabel.Text = "hash:";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(41, 113);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(250, 250);
            this.webBrowser1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(40, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(121, 75);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "why wont .Load work on CLR. whats missing?";
            // 
            // zeBindingSource
            // 
            this.zeBindingSource.DataSource = typeof(HashForBindingSource.DataSourcez.Dataz.ZeDocumentTextzNavigateBindingSource);
            this.zeBindingSource.Position = 0;
            this.zeBindingSource.CurrentChanged += new System.EventHandler(this.zeBindingSource_CurrentChanged);
            // 
            // zeBindingNavigator
            // 
            this.zeBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.zeBindingNavigator.AutoSize = false;
            this.zeBindingNavigator.BindingSource = this.zeBindingSource;
            this.zeBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.zeBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.zeBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.zeBindingNavigatorSaveItem});
            this.zeBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.zeBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.zeBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.zeBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.zeBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.zeBindingNavigator.Name = "zeBindingNavigator";
            this.zeBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.zeBindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.zeBindingNavigator.Size = new System.Drawing.Size(652, 34);
            this.zeBindingNavigator.TabIndex = 4;
            this.zeBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 31);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(28, 31);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 31);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 31);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(90, 31);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 34);
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
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(72, 31);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 31);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // zeBindingNavigatorSaveItem
            // 
            this.zeBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zeBindingNavigatorSaveItem.Enabled = false;
            this.zeBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("zeBindingNavigatorSaveItem.Image")));
            this.zeBindingNavigatorSaveItem.Name = "zeBindingNavigatorSaveItem";
            this.zeBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 31);
            this.zeBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // hashTextBox
            // 
            this.hashTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.zeBindingSource, "hash", true));
            this.hashTextBox.Location = new System.Drawing.Point(368, 78);
            this.hashTextBox.Name = "hashTextBox";
            this.hashTextBox.Size = new System.Drawing.Size(100, 20);
            this.hashTextBox.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(499, 66);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "MoveNext";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(324, 410);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "#blue";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 415);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "label2";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(499, 369);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "dock it";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // xWebBrowser1
            // 
            this.xWebBrowser1.DataBindings.Add(new System.Windows.Forms.Binding("DocumentText", this.zeBindingSource, "DocumentText", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.xWebBrowser1.Location = new System.Drawing.Point(324, 113);
            this.xWebBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.xWebBrowser1.Name = "xWebBrowser1";
            this.xWebBrowser1.Size = new System.Drawing.Size(250, 250);
            this.xWebBrowser1.TabIndex = 8;
            // 
            // ApplicationControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.xWebBrowser1);
            this.Controls.Add(this.button3);
            this.Controls.Add(hashLabel);
            this.Controls.Add(this.hashTextBox);
            this.Controls.Add(this.zeBindingNavigator);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.webBrowser1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(652, 474);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.zeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zeBindingNavigator)).EndInit();
            this.zeBindingNavigator.ResumeLayout(false);
            this.zeBindingNavigator.PerformLayout();
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

        private WebBrowser webBrowser1;
        private Button button1;
        private Button button2;
        private Label label1;
        private BindingNavigator zeBindingNavigator;
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
        private ToolStripButton zeBindingNavigatorSaveItem;
        private TextBox hashTextBox;
        private Button button3;
        private XWebBrowser xWebBrowser1;
        public BindingSource zeBindingSource;
        private Button button4;
        private Label label2;
        private Button button5;

    }
}

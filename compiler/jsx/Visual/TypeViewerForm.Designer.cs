namespace jsx.Visual
{
    partial class TypeViewerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TypeViewerForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node2", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.tree = new System.Windows.Forms.TreeView();
            this.TreeContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddToRoot = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveFromRoot = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.back = new System.Windows.Forms.ToolStripSplitButton();
            this.forward = new System.Windows.Forms.ToolStripSplitButton();
            this.options = new System.Windows.Forms.ToolStripButton();
            this.OptionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.TreeContext.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "assembly.PNG");
            this.imageList1.Images.SetKeyName(1, "class.PNG");
            this.imageList1.Images.SetKeyName(2, "ctor.PNG");
            this.imageList1.Images.SetKeyName(3, "delegate.PNG");
            this.imageList1.Images.SetKeyName(4, "enum.PNG");
            this.imageList1.Images.SetKeyName(5, "event.PNG");
            this.imageList1.Images.SetKeyName(6, "field.PNG");
            this.imageList1.Images.SetKeyName(7, "interface.PNG");
            this.imageList1.Images.SetKeyName(8, "internal.class.PNG");
            this.imageList1.Images.SetKeyName(9, "module.PNG");
            this.imageList1.Images.SetKeyName(10, "namespace.PNG");
            this.imageList1.Images.SetKeyName(11, "private.class.PNG");
            this.imageList1.Images.SetKeyName(12, "private.static.method.PNG");
            this.imageList1.Images.SetKeyName(13, "property.PNG");
            this.imageList1.Images.SetKeyName(14, "protected.method.PNG");
            this.imageList1.Images.SetKeyName(15, "static.method.PNG");
            this.imageList1.Images.SetKeyName(16, "struct.PNG");
            this.imageList1.Images.SetKeyName(17, "virtual.method.PNG");
            this.imageList1.Images.SetKeyName(18, "redo.PNG");
            this.imageList1.Images.SetKeyName(19, "undo.PNG");
            this.imageList1.Images.SetKeyName(20, "references.PNG");
            this.imageList1.Images.SetKeyName(21, "reference.PNG");
            this.imageList1.Images.SetKeyName(22, "method.PNG");
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tree);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(460, 244);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(460, 269);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // tree
            // 
            this.tree.ContextMenuStrip = this.TreeContext;
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.HideSelection = false;
            this.tree.ImageIndex = 0;
            this.tree.ImageList = this.imageList1;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Name = "tree";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Node0";
            treeNode2.ImageIndex = 4;
            treeNode2.Name = "Node3";
            treeNode2.Text = "Node3";
            treeNode3.ImageIndex = 3;
            treeNode3.Name = "Node2";
            treeNode3.Text = "Node2";
            treeNode4.Name = "Node1";
            treeNode4.Text = "Node1";
            this.tree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode4});
            this.tree.SelectedImageIndex = 0;
            this.tree.Size = new System.Drawing.Size(460, 244);
            this.tree.TabIndex = 3;
            // 
            // TreeContext
            // 
            this.TreeContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddToRoot,
            this.RemoveFromRoot});
            this.TreeContext.Name = "contextMenuStrip1";
            this.TreeContext.Size = new System.Drawing.Size(168, 48);
            // 
            // AddToRoot
            // 
            this.AddToRoot.Image = global::jsx.Properties.Resources.icon_add;
            this.AddToRoot.Name = "AddToRoot";
            this.AddToRoot.Size = new System.Drawing.Size(167, 22);
            this.AddToRoot.Text = "Add to root";
            // 
            // RemoveFromRoot
            // 
            this.RemoveFromRoot.Image = global::jsx.Properties.Resources.icon_delsm;
            this.RemoveFromRoot.Name = "RemoveFromRoot";
            this.RemoveFromRoot.Size = new System.Drawing.Size(167, 22);
            this.RemoveFromRoot.Text = "Remove from root";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.back,
            this.forward,
            this.options});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(184, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // back
            // 
            this.back.Image = global::jsx.Properties.Resources.previous;
            this.back.ImageTransparentColor = System.Drawing.Color.White;
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(61, 22);
            this.back.Text = "Back";
            // 
            // forward
            // 
            this.forward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.forward.Image = global::jsx.Properties.Resources.next;
            this.forward.ImageTransparentColor = System.Drawing.Color.White;
            this.forward.Name = "forward";
            this.forward.Size = new System.Drawing.Size(32, 22);
            this.forward.Text = "Next";
            // 
            // options
            // 
            this.options.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.options.Image = ((System.Drawing.Image)(resources.GetObject("options.Image")));
            this.options.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(48, 22);
            this.options.Text = "Options";
            this.options.Click += new System.EventHandler(this.options_Click);
            // 
            // OptionsMenu
            // 
            this.OptionsMenu.Name = "contextMenuStrip1";
            this.OptionsMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // TypeViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 269);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TypeViewerForm";
            this.Text = "Object Browser";
            this.Load += new System.EventHandler(this.TypeViewerForm_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.TreeContext.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.TreeView tree;
        public System.Windows.Forms.ToolStripSplitButton back;
        public System.Windows.Forms.ToolStripSplitButton forward;
        private System.Windows.Forms.ToolStripButton options;
        public System.Windows.Forms.ContextMenuStrip OptionsMenu;
        public System.Windows.Forms.ContextMenuStrip TreeContext;
        public System.Windows.Forms.ToolStripMenuItem AddToRoot;
        public System.Windows.Forms.ToolStripMenuItem RemoveFromRoot;
    }
}
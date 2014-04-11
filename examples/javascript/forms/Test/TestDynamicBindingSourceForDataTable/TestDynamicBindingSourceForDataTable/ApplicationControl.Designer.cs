using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestDynamicBindingSourceForDataTable
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.sheet1BindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fooColumnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gooColumnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheet1BindingSourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fooColumnDataGridViewTextBoxColumn,
            this.gooColumnDataGridViewTextBoxColumn,
            this.tagDataGridViewTextBoxColumn,
            this.timestampDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.sheet1BindingSourceBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(400, 300);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(15, 144);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(382, 153);
            this.dataGridView2.TabIndex = 1;
            // 
            // sheet1BindingSourceBindingSource
            // 
            this.sheet1BindingSourceBindingSource.DataSource = typeof(FormsAutoSumGridSelection.Data.XZooBook.Sheet1BindingSource);
            this.sheet1BindingSourceBindingSource.Position = 0;
            // 
            // fooColumnDataGridViewTextBoxColumn
            // 
            this.fooColumnDataGridViewTextBoxColumn.DataPropertyName = "FooColumn";
            this.fooColumnDataGridViewTextBoxColumn.HeaderText = "FooColumn";
            this.fooColumnDataGridViewTextBoxColumn.Name = "fooColumnDataGridViewTextBoxColumn";
            // 
            // gooColumnDataGridViewTextBoxColumn
            // 
            this.gooColumnDataGridViewTextBoxColumn.DataPropertyName = "GooColumn";
            this.gooColumnDataGridViewTextBoxColumn.HeaderText = "GooColumn";
            this.gooColumnDataGridViewTextBoxColumn.Name = "gooColumnDataGridViewTextBoxColumn";
            // 
            // tagDataGridViewTextBoxColumn
            // 
            this.tagDataGridViewTextBoxColumn.DataPropertyName = "Tag";
            this.tagDataGridViewTextBoxColumn.HeaderText = "Tag";
            this.tagDataGridViewTextBoxColumn.Name = "tagDataGridViewTextBoxColumn";
            // 
            // timestampDataGridViewTextBoxColumn
            // 
            this.timestampDataGridViewTextBoxColumn.DataPropertyName = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.HeaderText = "Timestamp";
            this.timestampDataGridViewTextBoxColumn.Name = "timestampDataGridViewTextBoxColumn";
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheet1BindingSourceBindingSource)).EndInit();
            this.ResumeLayout(false);

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

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn column3DataGridViewTextBoxColumn;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn column1DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn column2DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn fooColumnDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn gooColumnDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn tagDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
        private BindingSource sheet1BindingSourceBindingSource;

    }
}

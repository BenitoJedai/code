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
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.myDataSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.sheet1BindingSourceBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.sheet1BindingSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sheet1BindingSourceBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.column1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheet1BindingSourceBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheet1BindingSourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheet1BindingSourceBindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridView1.DataSource = this.myDataSourceBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(400, 300);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Column1";
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Column2";
            this.dataGridViewTextBoxColumn2.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Column3";
            this.dataGridViewTextBoxColumn3.HeaderText = "Column3";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // myDataSourceBindingSource
            // 
            this.myDataSourceBindingSource.DataSource = typeof(FormsAutoSumGridSelection.Data.MyDataSource);
            this.myDataSourceBindingSource.Position = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column1DataGridViewTextBoxColumn,
            this.column2DataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.sheet1BindingSourceBindingSource2;
            this.dataGridView2.Location = new System.Drawing.Point(15, 144);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(382, 153);
            this.dataGridView2.TabIndex = 1;
            // 
            // sheet1BindingSourceBindingSource1
            // 
            this.sheet1BindingSourceBindingSource1.DataSource = typeof(FormsAutoSumGridSelection.Data.XZooBook.Sheet1BindingSource);
            this.sheet1BindingSourceBindingSource1.Position = 0;
            // 
            // sheet1BindingSourceBindingSource
            // 
            this.sheet1BindingSourceBindingSource.DataSource = typeof(FormsAutoSumGridSelection.Data.XZooBook.Sheet1BindingSource);
            this.sheet1BindingSourceBindingSource.Position = 0;
            // 
            // sheet1BindingSourceBindingSource2
            // 
            this.sheet1BindingSourceBindingSource2.DataSource = typeof(FormsAutoSumGridSelection.Data.ZooBook.Sheet1BindingSource);
            this.sheet1BindingSourceBindingSource2.Position = 0;
            // 
            // column1DataGridViewTextBoxColumn
            // 
            this.column1DataGridViewTextBoxColumn.DataPropertyName = "Column1";
            this.column1DataGridViewTextBoxColumn.HeaderText = "Column1";
            this.column1DataGridViewTextBoxColumn.Name = "column1DataGridViewTextBoxColumn";
            // 
            // column2DataGridViewTextBoxColumn
            // 
            this.column2DataGridViewTextBoxColumn.DataPropertyName = "Column2";
            this.column2DataGridViewTextBoxColumn.HeaderText = "Column2";
            this.column2DataGridViewTextBoxColumn.Name = "column2DataGridViewTextBoxColumn";
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheet1BindingSourceBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheet1BindingSourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheet1BindingSourceBindingSource2)).EndInit();
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
        private BindingSource myDataSourceBindingSource;
        private BindingSource sheet1BindingSourceBindingSource;
        private DataGridViewTextBoxColumn fooColumnDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn gooColumnDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn tagDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
        private BindingSource sheet1BindingSourceBindingSource1;
        private DataGridViewTextBoxColumn column1DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn column2DataGridViewTextBoxColumn;
        private BindingSource sheet1BindingSourceBindingSource2;

    }
}

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FormsAutoSumGridSelection
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
            this.myDataSource1 = new FormsAutoSumGridSelection.Data.MyDataSource();
            this.column1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.myOtherDataSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myOtherDataSourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column1DataGridViewTextBoxColumn,
            this.column2DataGridViewTextBoxColumn,
            this.column3DataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.myOtherDataSourceBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(532, 298);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // myDataSource1
            // 
            this.myDataSource1.Position = 0;
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
            // column3DataGridViewTextBoxColumn
            // 
            this.column3DataGridViewTextBoxColumn.DataPropertyName = "Column3";
            this.column3DataGridViewTextBoxColumn.HeaderText = "Column3";
            this.column3DataGridViewTextBoxColumn.Name = "column3DataGridViewTextBoxColumn";
            // 
            // myOtherDataSourceBindingSource
            // 
            this.myOtherDataSourceBindingSource.DataSource = typeof(FormsAutoSumGridSelection.Data.MyOtherDataSource);
            this.myOtherDataSourceBindingSource.Position = 0;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.dataGridView1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(532, 298);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myOtherDataSourceBindingSource)).EndInit();
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
        private DataGridViewTextBoxColumn rowErrorDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn rowStateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn tableDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn hasErrorsDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn column1DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn column2DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn column3DataGridViewTextBoxColumn;
        private Data.MyDataSource myDataSource1;
        private BindingSource myOtherDataSourceBindingSource;

    }
}

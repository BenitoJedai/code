using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestDataSource
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
            this.dataSet11 = new TestDataSource.DataSet1();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataColumn1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataColumn2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.printForm1 = new Microsoft.VisualBasic.PowerPacks.Printing.PrintForm(this.components);
            this.fooDataSet = new TestDataSource.fooDataSet();
            this.tableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableTableAdapter = new TestDataSource.fooDataSetTableAdapters.TableTableAdapter();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fooDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataSet11
            // 
            this.dataSet11.DataSetName = "DataSet1";
            this.dataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.dataSet11.Initialized += new System.EventHandler(this.dataSet11_Initialized);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataColumn1DataGridViewTextBoxColumn,
            this.dataColumn2DataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.dataTable1BindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(16, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(396, 100);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataTable1BindingSource
            // 
            this.dataTable1BindingSource.DataMember = "DataTable1";
            this.dataTable1BindingSource.DataSource = this.dataSet11;
            // 
            // dataColumn1DataGridViewTextBoxColumn
            // 
            this.dataColumn1DataGridViewTextBoxColumn.DataPropertyName = "DataColumn1";
            this.dataColumn1DataGridViewTextBoxColumn.HeaderText = "DataColumn1";
            this.dataColumn1DataGridViewTextBoxColumn.Name = "dataColumn1DataGridViewTextBoxColumn";
            // 
            // dataColumn2DataGridViewTextBoxColumn
            // 
            this.dataColumn2DataGridViewTextBoxColumn.DataPropertyName = "DataColumn2";
            this.dataColumn2DataGridViewTextBoxColumn.HeaderText = "DataColumn2";
            this.dataColumn2DataGridViewTextBoxColumn.Name = "dataColumn2DataGridViewTextBoxColumn";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.contentKeyDataGridViewTextBoxColumn,
            this.contentValueDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.tableBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(16, 147);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(396, 87);
            this.dataGridView2.TabIndex = 1;
            // 
            // printForm1
            // 
            this.printForm1.DocumentName = "document";
            this.printForm1.Form = null;
            this.printForm1.PrintAction = System.Drawing.Printing.PrintAction.PrintToPreview;
            this.printForm1.PrinterSettings = ((System.Drawing.Printing.PrinterSettings)(resources.GetObject("printForm1.PrinterSettings")));
            this.printForm1.PrintFileName = null;
            // 
            // fooDataSet
            // 
            this.fooDataSet.DataSetName = "fooDataSet";
            this.fooDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableBindingSource
            // 
            this.tableBindingSource.DataMember = "Table";
            this.tableBindingSource.DataSource = this.fooDataSet;
            // 
            // tableTableAdapter
            // 
            this.tableTableAdapter.ClearBeforeFill = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // contentKeyDataGridViewTextBoxColumn
            // 
            this.contentKeyDataGridViewTextBoxColumn.DataPropertyName = "ContentKey";
            this.contentKeyDataGridViewTextBoxColumn.HeaderText = "ContentKey";
            this.contentKeyDataGridViewTextBoxColumn.Name = "contentKeyDataGridViewTextBoxColumn";
            // 
            // contentValueDataGridViewTextBoxColumn
            // 
            this.contentValueDataGridViewTextBoxColumn.DataPropertyName = "ContentValue";
            this.contentValueDataGridViewTextBoxColumn.HeaderText = "ContentValue";
            this.contentValueDataGridViewTextBoxColumn.Name = "contentValueDataGridViewTextBoxColumn";
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(515, 300);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fooDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableBindingSource)).EndInit();
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

        private DataSet1 dataSet11;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn dataColumn1DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataColumn2DataGridViewTextBoxColumn;
        private BindingSource dataTable1BindingSource;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn contentKeyDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn contentValueDataGridViewTextBoxColumn;
        private BindingSource tableBindingSource;
        private fooDataSet fooDataSet;
        private Microsoft.VisualBasic.PowerPacks.Printing.PrintForm printForm1;
        private fooDataSetTableAdapters.TableTableAdapter tableTableAdapter;

    }
}

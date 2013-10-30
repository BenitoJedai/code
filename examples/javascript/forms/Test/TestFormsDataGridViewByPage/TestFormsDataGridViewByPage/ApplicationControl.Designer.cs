using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestFormsDataGridViewByPage
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PrevPageLink = new System.Windows.Forms.LinkLabel();
            this.NextPageLink = new System.Windows.Forms.LinkLabel();
            this.PageNumberLabel = new System.Windows.Forms.Label();
            this.PageCountLabel = new System.Windows.Forms.Label();
            this.applicationWebService1 = new TestFormsDataGridViewByPage.ApplicationWebService();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(662, 408);
            this.dataGridView1.TabIndex = 0;
            // 
            // PrevPageLink
            // 
            this.PrevPageLink.AutoSize = true;
            this.PrevPageLink.Location = new System.Drawing.Point(38, 434);
            this.PrevPageLink.Name = "PrevPageLink";
            this.PrevPageLink.Size = new System.Drawing.Size(90, 13);
            this.PrevPageLink.TabIndex = 1;
            this.PrevPageLink.TabStop = true;
            this.PrevPageLink.Text = "<< Previous page";
            this.PrevPageLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PrevPageLink_LinkClicked);
            // 
            // NextPageLink
            // 
            this.NextPageLink.AutoSize = true;
            this.NextPageLink.Location = new System.Drawing.Point(583, 434);
            this.NextPageLink.Name = "NextPageLink";
            this.NextPageLink.Size = new System.Drawing.Size(71, 13);
            this.NextPageLink.TabIndex = 2;
            this.NextPageLink.TabStop = true;
            this.NextPageLink.Text = "Next page >>";
            this.NextPageLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.NextPageLink_LinkClicked);
            // 
            // PageNumberLabel
            // 
            this.PageNumberLabel.AutoSize = true;
            this.PageNumberLabel.Location = new System.Drawing.Point(329, 434);
            this.PageNumberLabel.Name = "PageNumberLabel";
            this.PageNumberLabel.Size = new System.Drawing.Size(0, 13);
            this.PageNumberLabel.TabIndex = 3;
            // 
            // PageCountLabel
            // 
            this.PageCountLabel.AutoSize = true;
            this.PageCountLabel.Location = new System.Drawing.Point(696, 89);
            this.PageCountLabel.Name = "PageCountLabel";
            this.PageCountLabel.Size = new System.Drawing.Size(0, 13);
            this.PageCountLabel.TabIndex = 4;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.PageCountLabel);
            this.Controls.Add(this.PageNumberLabel);
            this.Controls.Add(this.NextPageLink);
            this.Controls.Add(this.PrevPageLink);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(729, 505);
            this.Load += new System.EventHandler(this.ApplicationControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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

        private DataGridView dataGridView1;
        private LinkLabel PrevPageLink;
        private LinkLabel NextPageLink;
        private Label PageNumberLabel;
        private ApplicationWebService applicationWebService1;
        private Label PageCountLabel;

    }
}

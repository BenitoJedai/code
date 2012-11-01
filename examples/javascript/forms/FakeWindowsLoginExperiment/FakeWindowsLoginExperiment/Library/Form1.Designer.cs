namespace FakeWindowsLoginExperiment.Library
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.RequestNoContent = new System.Windows.Forms.CheckBox();
            this.AskBeforeDisconnectionSession = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.applicationControl1 = new FakeWindowsLoginExperiment.ApplicationControl();
            this.applicationExitFullscreen1 = new FakeWindowsLoginExperiment.Library.ApplicationExitFullscreen();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.RequestNoContent);
            this.panel1.Controls.Add(this.AskBeforeDisconnectionSession);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 101);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "! not fullsreen";
            this.label1.Visible = false;
            // 
            // RequestNoContent
            // 
            this.RequestNoContent.AutoSize = true;
            this.RequestNoContent.ForeColor = System.Drawing.Color.Red;
            this.RequestNoContent.Location = new System.Drawing.Point(117, 39);
            this.RequestNoContent.Name = "RequestNoContent";
            this.RequestNoContent.Size = new System.Drawing.Size(210, 17);
            this.RequestNoContent.TabIndex = 4;
            this.RequestNoContent.Text = "Tell server to send no content on close";
            this.RequestNoContent.UseVisualStyleBackColor = true;
            // 
            // AskBeforeDisconnectionSession
            // 
            this.AskBeforeDisconnectionSession.AutoSize = true;
            this.AskBeforeDisconnectionSession.Checked = true;
            this.AskBeforeDisconnectionSession.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AskBeforeDisconnectionSession.ForeColor = System.Drawing.Color.Red;
            this.AskBeforeDisconnectionSession.Location = new System.Drawing.Point(117, 16);
            this.AskBeforeDisconnectionSession.Name = "AskBeforeDisconnectionSession";
            this.AskBeforeDisconnectionSession.Size = new System.Drawing.Size(184, 17);
            this.AskBeforeDisconnectionSession.TabIndex = 3;
            this.AskBeforeDisconnectionSession.Text = "Ask before disconnecting session";
            this.AskBeforeDisconnectionSession.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(12, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // applicationControl1
            // 
            this.applicationControl1.Location = new System.Drawing.Point(3, 110);
            this.applicationControl1.Name = "applicationControl1";
            this.applicationControl1.Size = new System.Drawing.Size(400, 300);
            this.applicationControl1.TabIndex = 0;
            // 
            // applicationExitFullscreen1
            // 
            this.applicationExitFullscreen1.ExitFullscreen += new System.Action(this.applicationExitFullscreen1_ExitFullscreen);
            this.applicationExitFullscreen1.EnterFullscreen += new System.Action(this.applicationExitFullscreen1_EnterFullscreen);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 422);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.applicationControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ApplicationControl applicationControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.CheckBox AskBeforeDisconnectionSession;
        public System.Windows.Forms.CheckBox RequestNoContent;
        private System.Windows.Forms.Label label1;
        private ApplicationExitFullscreen applicationExitFullscreen1;
    }
}
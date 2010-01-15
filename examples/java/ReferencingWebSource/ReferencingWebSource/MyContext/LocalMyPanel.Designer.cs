namespace ReferencingWebSource.MyContext
{
	partial class LocalMyPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.localInformation1 = new ReferencingWebSource.MyContext.LocalInformation();
			this.localLocation1 = new ReferencingWebSource.MyContext.LocalLocation();
			this.SuspendLayout();
			// 
			// TextBoxComment
			// 
			this.TextBoxComment.Text = "I like this!";
			// 
			// LocalMyPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Comment = "I like this!";
			this.Name = "LocalMyPanel";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public LocalInformation localInformation1;
		public LocalLocation localLocation1;

	}
}

namespace SimpleChat2
{
	partial class DiscoveryService
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
			this.components = new System.ComponentModel.Container();
			this._DiscoveryTimer = new System.Windows.Forms.Timer(this.components);
			this.ChangeDetector = new System.Windows.Forms.Timer(this.components);
			// 
			// _DiscoveryTimer
			// 
			this._DiscoveryTimer.Interval = 1000;
			this._DiscoveryTimer.Tick += new System.EventHandler(this.DiscoveryTimer_Tick);
			// 
			// ChangeDetector
			// 
			this.ChangeDetector.Interval = 10000;
			this.ChangeDetector.Tick += new System.EventHandler(this.ChangeDetector_Tick);

		}

		#endregion

		protected System.Windows.Forms.Timer _DiscoveryTimer;
		private System.Windows.Forms.Timer ChangeDetector;


	}
}

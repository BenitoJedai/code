using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeWindowsLoginExperiment.Design
{
    partial class FakeMultimonitorDesktop : Component
    {
        public FakeMultimonitorDesktop()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ShadowOverlay = new FakeWindowsLoginExperiment.Library.ApplicationOverlay();
            this.windows_Hardware_Insert1 = new FakeWindowsLoginExperiment.Audio.Windows_Hardware_Insert();
            this.windows_Hardware_Remove1 = new FakeWindowsLoginExperiment.Audio.Windows_Hardware_Remove();
            // 
            // ShadowOverlay
            // 
            this.ShadowOverlay.HideAudio = this.windows_Hardware_Insert1;
            this.ShadowOverlay.OverlayColor = System.Drawing.Color.Black;
            this.ShadowOverlay.ShowAudio = this.windows_Hardware_Remove1;
            // 
            // windows_Hardware_Insert1
            // 
            this.windows_Hardware_Insert1.AutoBuffer = true;
            // 
            // windows_Hardware_Remove1
            // 
            this.windows_Hardware_Remove1.AutoBuffer = true;

        }

        private Library.ApplicationOverlay ShadowOverlay;
        private Audio.Windows_Hardware_Insert windows_Hardware_Insert1;
        private Audio.Windows_Hardware_Remove windows_Hardware_Remove1;
    }
}

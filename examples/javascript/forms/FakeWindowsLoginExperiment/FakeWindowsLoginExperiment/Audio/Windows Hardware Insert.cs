using FakeWindowsLoginExperiment.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeWindowsLoginExperiment.Audio
{
    public class Windows_Hardware_Insert : ApplicationAudio
    {
        public Windows_Hardware_Insert()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Audio = () => new HTML.Audio.FromAssets.Windows_Hardware_Insert();

        }
    }
}

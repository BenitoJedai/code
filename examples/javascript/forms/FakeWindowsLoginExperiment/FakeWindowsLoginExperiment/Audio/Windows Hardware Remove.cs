using FakeWindowsLoginExperiment.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeWindowsLoginExperiment.Audio
{
    public class Windows_Hardware_Remove : ApplicationAudio
    {
        public Windows_Hardware_Remove()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Audio = () => new HTML.Audio.FromAssets.Windows_Hardware_Remove();

        }
    }
}

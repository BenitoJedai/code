using FakeWindowsLoginExperiment.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeWindowsLoginExperiment.Audio
{
    public class Windows_Logoff_Sound : ApplicationAudio
    {
        public Windows_Logoff_Sound()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Audio = () => new HTML.Audio.FromAssets.Windows_Logoff_Sound();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeekerWithStarlingB2.Library
{
    public partial class GameUnitDiagnostics : UserControl
    {
        public GameUnitDiagnostics()
        {
            InitializeComponent();
        }

        IGameUnit current;

        public void switchto(IGameUnit x)
        {
            this.current = x;

            // this is a continuation from
            // X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeekerWithStarlingB2\ApplicationSprite.cs
            // 3300

            this.label2.Text = new { x.identity }.ToString();
        }

        private void GameUnitDiagnostics_Load(object sender, EventArgs e)
        {

        }

        private void hueControl1_AdjustHue(int delta)
        {
            Console.WriteLine(new { delta });

            if (current != null)
                current.AdjustHue("" + delta);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using jsc.meta.Commands.Rewrite.RewriteToInstaller.Templates;

namespace jsc.configuration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.AssemblyResolve +=
                (sender, args) =>
                {
                    if (args.Name.Contains("_jsc.installer,"))
                    {
                        var s = typeof(MainWindow).Assembly.GetManifestResourceStream("jsc.configuration.Content.latest_jsc.installer.exe");

                        if (s != null)
                        {
                            var buffer = new byte[s.Length];

                            s.Read(buffer, 0, buffer.Length);

                            return Assembly.Load(buffer);
                        }
                    }

                    return null;
                };

            EULA();
            //Splash();
        }

        private void EULA()
        {
            this.richTextBox1.Selection.Load(
                Installer.Archive.Entries.Single(k => k.FileName.EndsWith("EULA.rtf")).Data
                ,
                DataFormats.Rtf
            );
        }

        private void Splash()
        {
            typeof(Installer).Assembly.EntryPoint.Invoke(null, new object[] { default(string[])});
        }



        private void richTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            this.button1.IsEnabled = (bool)this.checkBox1.IsChecked;

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.button1.IsEnabled = false;
            this.checkBox1.IsEnabled = false;

            var i = new Installer.FileMonkey();

            Installer.Continue(i.files, false);

            MessageBox.Show("Installation complete!");
        }
    }
}

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
using System.Diagnostics;


namespace jsc.configuration.Controls
{
    /// <summary>
    /// Interaction logic for FolderEntry.xaml
    /// </summary>
    public partial class FolderEntry : UserControl
    {
        // http://stackoverflow.com/questions/1922204/wpf-open-directory-dialog

        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(FolderEntry), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(FolderEntry), new PropertyMetadata(null));

        public string Text { get { return GetValue(TextProperty) as string; } set { SetValue(TextProperty, value); } }

        public string Description { get { return GetValue(DescriptionProperty) as string; } set { SetValue(DescriptionProperty, value); } }

        public FolderEntry() { InitializeComponent(); }

        public Uri DownloadLocation { get; set; }

        private void Download(object sender, RoutedEventArgs e)
        {
            Process.Start(DownloadLocation.ToString());
        }

        private void BrowseFolder(object sender, RoutedEventArgs e)
        {
            using (var dlg = new System.Windows.Forms.FolderBrowserDialog())
            {
                dlg.Description = Description;
                dlg.SelectedPath = Text;
                
                dlg.ShowNewFolderButton = false;
                
                var result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Text = dlg.SelectedPath;
                    BindingExpression be = GetBindingExpression(TextProperty);
                    if (be != null)
                        be.UpdateSource();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace TestObfuscationWithXAML
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var u = typeof(App).Assembly.GetName().Name + ";component/" + this.StartupUri.ToString();

            this.StartupUri = new Uri(u, System.UriKind.Relative);

            base.OnStartup(e);
        }
    }
}

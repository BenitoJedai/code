﻿using System;
using System.Linq;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;
using System.Reflection;
using System.Windows.Media;
using ScriptCoreLib.Avalon;
using System.Windows.Media.Effects;
using jsc.meta.Commands.Rewrite.RewriteToInstaller.Templates;
using System.Windows;
using System.Threading;
using jsc.meta.Configuration;
using System.Windows.Threading;

namespace jsc.configuration
{
    public static class Program
    {
        [STAThread]
        static public void Main(string[] args)
        {
            InitializeDynamic();
            InitializeWindow();

        }

        private static void InitializeWindow()
        {
            var c = new Canvas
            {
                Width = 600,
                Height = 400
            };



            var a = new Agreement
            {
                BitmapEffect = new OuterGlowBitmapEffect { GlowColor = Colors.White }
            }.AttachTo(c);

            


            InitializeWindowContent(a);


            var cc = new JSCSolutionsNETCarouselCanvas();

            cc.CloseOnClick = false;

            // http://stackoverflow.com/questions/741956/wpf-pan-zoom-image
            cc.Container.RenderTransform = new ScaleTransform(0.5, 0.5);
            cc.MoveContainerTo(416, -60);
            cc.AttachContainerTo(c);


            var w = c.ToWindow();

            w.Activated +=
                delegate
                {
                    if (a == null)
                        return;

                    InitializeWindowContent(a);
                    a = null;
                };
            w.Title = "jsc";
            w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            w.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            w.ResizeMode = ResizeMode.CanMinimize;
            w.Icon = a.image1.Source;

            //w.ToTransparentWindow();

            w.WithGlass();
            w.ShowDialog();
        }

        private static void InitializeWindowContent(Agreement a)
        {
            a.richTextBox1.Selection.Load(
               Installer.Archive.Entries.Single(k => k.FileName.EndsWith("EULA.rtf")).Data
               ,
               DataFormats.Rtf
           );

            a.checkBox1.Checked +=
                delegate
                {
                    a.button1.IsEnabled = (bool)a.checkBox1.IsChecked;
                };


            a.button1.Click +=
                delegate
                {

                    a.button1.IsEnabled = false;
                    a.checkBox1.IsEnabled = false;

                    new Thread(
                        delegate()
                        {
                            var i = new Installer.FileMonkey();

                            Installer.Continue(i.files, false);

                            a.Dispatcher.Invoke(
                                new Action(
                                    delegate
                                    {
                                        var conf = SDKConfiguration.Default;

                                        SDKConfiguration.Default = conf;

                                        a.folderEntry1.Text = conf.JavaSDK.FullName;
                                        a.folderEntry2.Text = conf.FlexSDK.FullName;
                                        a.folderEntry3.Text = conf.GoogleAppEngineJavaSDK.FullName;
                                        a.folderEntry4.Text = conf.ApacheAntSDK.FullName;

                                        a.SaveButton.Click +=
                                            delegate
                                            {
                                                // binding?
                                                // validation?

                                                conf.JavaSDK = new System.IO.DirectoryInfo(a.folderEntry1.Text);
                                                conf.FlexSDK = new System.IO.DirectoryInfo(a.folderEntry2.Text);
                                                conf.GoogleAppEngineJavaSDK = new System.IO.DirectoryInfo(a.folderEntry3.Text);
                                                conf.ApacheAntSDK = new System.IO.DirectoryInfo(a.folderEntry4.Text);

                                                SDKConfiguration.Default = conf;
                                            };

                                        a.tabItem2.Show();
                                        a.tabItem2.IsSelected = true;

                                        a.checkBox1.Hide();
                                        a.button1.Hide();

                                        //MessageBox.Show(
                                        //    "Automatic configuration will continue in the background. \n\nYou may now change manual configuration or exit this installer.", "Installation complete!", MessageBoxButton.OK, MessageBoxImage.Information
                                        //);
                                    }
                                )
                            );
                        }
                    ).Start();
                };
        }

        private static void InitializeDynamic()
        {
            AppDomain.CurrentDomain.AssemblyResolve +=
                 (sender, args) =>
                 {
                     if (args.Name.Contains("_jsc.installer,"))
                     {
                         var s = typeof(Program).Assembly.GetManifestResourceStream("jsc.configuration.Content.latest_jsc.installer.exe");

                         if (s != null)
                         {
                             var buffer = new byte[s.Length];

                             s.Read(buffer, 0, buffer.Length);

                             return Assembly.Load(buffer);
                         }
                     }

                     return null;
                 };

        }
    }
}

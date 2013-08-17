using System;
using System.Windows.Forms;

namespace com.abstractatech.dcimgalleryappclr
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // 1>Error: The publish components are not available. You need to reinstall Visual Studio to publish your application.
            // Error	2	Unable to copy file "X:\jsc.internal.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryappclr\bin\staging.AssetsLibrary\com.abstractatech.dcimgalleryappclr.AssetsLibrary.dll" to "bin\Debug\app.publish\Application Files\com.abstractatech.dcimgalleryappclr_1_0_0_0\com.abstractatech.dcimgalleryappclr.AssetsLibrary.dll.deploy". The specified path, file name, or both are too long. The fully qualified file name must be less than 260 characters, and the directory name must be less than 248 characters.	com.abstractatech.dcimgalleryappclr


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Application.Run(new WithClickOnceLANLauncherClient.FindServiceProviderOverMulticastForm());

        }

    }
}

using java.io;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace FileExplorer
{


    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {

        public void getExternalStoragePublicDirectory(string e, Action<string> y)
        {
            var root = android.os.Environment.getExternalStoragePublicDirectory(
                                               android.os.Environment.DIRECTORY_PICTURES
                                               ).getPath();

            y(root);
        }

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void listFiles(string dirPath, Action<string> y)
        {
            File f = new File(dirPath);
            File[] files = f.listFiles();


            if (files != null)
                for (int i = 0; i < files.Length; i++)
                {
                    File file = files[i];

                    if (!file.isHidden() && file.canRead())
                    {
                        //path.add(file.getPath());
                        if (file.isDirectory())
                        {
                            y(file.getName() + "/");
                        }
                        else
                        {
                            y(file.getName());
                        }
                    }
                }


         
        }

    }
}

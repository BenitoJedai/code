using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using android.app;
using android.hardware;
using android.os;
using android.provider;
using android.webkit;
using android.widget;
using AndroidFileExplorerActivity.Library;
using java.io;
using java.util;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidFileExplorerActivity.Activities
{
    [Description("inspired by http://android-er.blogspot.com/2012/07/example-of-file-explorer-in-android.html")]
    public class AndroidFileExplorerActivity : ListActivity
    {

        // running it on device:
        // attach device to usb

        private ArrayList item = null;
        private ArrayList path = null;
        private string root;
        private TextView myPath;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            setContentView(R.layout.main);

            myPath = (TextView)findViewById(R.id.path);

            root = Environment.getRootDirectory().getPath();
            //root = Environment.getExternalStorageDirectory().getPath();

            getDir(root);


            this.ShowToast("http://jsc-solutions.net");
        }

        private void getDir(string dirPath)
        {
            myPath.setText("Location: " + dirPath);
            item = new ArrayList();
            path = new ArrayList();
            File f = new File(dirPath);
            File[] files = f.listFiles();

            if (!(root.StringEquals(dirPath)))
            //if (root != dirPath)
            {
                item.add(root);
                path.add(root);
                item.add("../");
                path.add(f.getParent());
            }


            if (files != null)
                for (int i = 0; i < files.Length; i++)
                {
                    File file = files[i];

                    if (!file.isHidden() && file.canRead())
                    {
                        path.add(file.getPath());
                        if (file.isDirectory())
                        {
                            item.add(file.getName() + "/");
                        }
                        else
                        {
                            item.add(file.getName());
                        }
                    }
                }

            var fileList =
              new ArrayAdapter(this, R.layout.row, item);
            //new ArrayAdapter<String>(this, R.layout.row, item);
            setListAdapter(fileList);
        }

        protected override void onListItemClick(ListView l, android.view.View v, int position, long id)
        {
            File file = new File((string)path.get(position));

            if (file.isDirectory())
            {

                if (file.canRead())
                {
                    getDir((string)path.get(position));
                }
                else
                {
                    new AlertDialog.Builder(this)
                        //.setIcon(R.drawable.ic_launcher)
                        .setTitle(file.getName() + "] folder can't be read!")
                        //.setPositiveButton("OK", null)
                        .show();
                }
            }
            else
            {
                new AlertDialog.Builder(this)
                    //.setIcon(R.drawable.ic_launcher)
                    .setTitle(file.getName() + "]")
                    //.setPositiveButton("OK", null)
                    .show();

            }

        }
    }


}

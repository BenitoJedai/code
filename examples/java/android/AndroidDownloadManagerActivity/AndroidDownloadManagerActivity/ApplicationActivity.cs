using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using android.content;
using android.preference;
using android.database;

namespace AndroidDownloadManagerActivity.Activities
{

    public class ApplicationActivity : Activity
    {

        // inspired by http://android-er.blogspot.com/2011/07/sample-code-using-androidappdownloadman.html

        const string strPref_Download_ID = "PREF_DOWNLOAD_ID";

        protected override void onCreate(Bundle savedInstanceState)
        {
            var file1 = "http://a.tumblr.com/tumblr_m8ueqqpyyy1rs64dko1.mp3";
            var name1 = "E43 Understanding the Dangers of Ego-Depletion by Tim";

            base.onCreate(savedInstanceState);

            var downloadManager = (DownloadManager)getSystemService(DOWNLOAD_SERVICE);
            var preferenceManager = PreferenceManager.getDefaultSharedPreferences(this);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new android.widget.Button(this).AttachTo(ll);

            downloadReceiver = new MyDownloadReceiver
            {

            };

            //            O:\src\AndroidDownloadManagerActivity\Activities\ApplicationActivity___c__DisplayClass2___c__DisplayClass4.java:60: cannot find symbol
            //symbol  : class Button
            //location: class AndroidDownloadManagerActivity.Activities.ApplicationActivity___c__DisplayClass2___c__DisplayClass4
            //                    ViewExtensions.<Button>WithText(this.CS___8__locals3.b, "Download " + this.CS___8__locals3.name1);
            //                                    ^

            b.WithText("Download " + name1);
            b.AtClick(
                v =>
                {

                    b.setText("Downloading...");
                    b.setEnabled(false);

                    var downloadUri = android.net.Uri.parse(file1);

                    // http://developer.android.com/reference/android/app/DownloadManager.Request.html
                    var request = new DownloadManager.Request(downloadUri);

                    request.setTitle("idea-remixer");
                    request.setDescription(name1);

                    // W/DownloadManager(15222): Aborting request for download 166: while trying to execute request: 
                    // java.net.UnknownHostException: Unable to resolve host "a.tumblr.com": 
                    // No address associated with hostname
                    var id = downloadManager.enqueue(request);

                    Toast.makeText(this, new { id, downloadUri }.ToString(), Toast.LENGTH_LONG).show();

                    //Save the request id   

                    //var PrefEdit = preferenceManager.edit();
                    //PrefEdit.putLong(strPref_Download_ID, id);
                    //PrefEdit.commit();

                    downloadReceiver.AtReceive = delegate
                    {
                        DownloadManager.Query query = new DownloadManager.Query();
                        //query.setFilterById(preferenceManager.getLong(strPref_Download_ID, 0));
                        // http://developer.android.com/reference/android/app/DownloadManager.Query.html#setFilterById(long...)
                        query.setFilterById(new[] { id });
                        Cursor cursor = downloadManager.query(query);

                        if (cursor.moveToFirst())
                        {
                            int columnIndex = cursor.getColumnIndex(DownloadManager.COLUMN_STATUS);
                            int status = cursor.getInt(columnIndex);

                            if (status == DownloadManager.STATUS_FAILED)
                            {
                                Toast.makeText(this, new { id, status }.ToString(), Toast.LENGTH_LONG).show();

                                b.WithText("(failed) Download " + name1);
                                b.setEnabled(true);
                            }
                            else if (status == DownloadManager.STATUS_SUCCESSFUL)
                            {
                                //Retrieve the saved request id     
                                //long downloadID = preferenceManager.getLong(strPref_Download_ID, 0);
                                var uri = downloadManager.getUriForDownloadedFile(id);


                                Toast.makeText(this, new { id, uri }.ToString(), Toast.LENGTH_LONG).show();

                                // jsc ignores this type in import?
                                Button __ref0;

                                b.WithText("Download " + name1);
                                b.setEnabled(true);

                                //ParcelFileDescriptor file;
                                //try
                                //{
                                //    file = downloadManager.openDownloadedFile(downloadID);
                                //    //FileInputStream fileInputStream       = new ParcelFileDescriptor.AutoCloseInputStream(file); 
                                //    //Bitmap bm = BitmapFactory.decodeStream(fileInputStream);
                                //    //image.setImageBitmap(bm);   
                                //}
                                //catch // (FileNotFoundException e) 
                                //{      // TODO Auto-generated catch block   
                                //    //e.printStackTrace();     
                                //    throw;
                                //}
                            }
                        }
                    };

                }
            );


            this.setContentView(sv);
        }

        protected override void onPause()
        {
            base.onPause();
            unregisterReceiver(downloadReceiver);
        }

        protected override void onResume()
        {
            base.onResume();
            var intentFilter = new IntentFilter(DownloadManager.ACTION_DOWNLOAD_COMPLETE);

            registerReceiver(downloadReceiver, intentFilter);
        }

        public MyDownloadReceiver downloadReceiver;

        public class MyDownloadReceiver : BroadcastReceiver
        {
            public Action AtReceive;

            public override void onReceive(Context arg0, Intent arg1)
            {
                // TODO Auto-generated method stub   

                if (AtReceive != null)
                    AtReceive();
            }
        }
    }


}

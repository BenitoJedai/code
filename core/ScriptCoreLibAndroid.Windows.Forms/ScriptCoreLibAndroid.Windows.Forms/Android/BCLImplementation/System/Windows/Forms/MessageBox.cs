﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.MessageBox))]
	internal class __MessageBox
	{
        public static global::System.Windows.Forms.DialogResult Show(string text)
        {
            //AlertDialog alertDialog = new AlertDialog.Builder(this).create();

            //alertDialog.setTitle("Reset...");
            //alertDialog.setMessage("Are you sure?");
            //alertDialog.setButton("OK", new DialogInterface.OnClickListener() {
            //   public void onClick(DialogInterface dialog, int which) {
            //      // here you can add functions
            //   }
            //});
            //alertDialog.setIcon(R.drawable.icon);
            //alertDialog.show();

            return global::System.Windows.Forms.DialogResult.OK;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
//using ScriptCoreLib.Extensions;
using ScriptCoreLib.Android.Extensions;
using android.text;
using android.content;
using System;

namespace AndroidEmailActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        // inspired by http://android-er.blogspot.com/2012/08/send-email-with-image-by-starting.html

        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;
        EditText __generic_parameter_ref;

        const int RQS_LOADIMAGE = 0;
        const int RQS_SENDEMAIL = 1;

        public android.net.Uri imageUri;

        TextView imagepath;
        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            // Error	1	The call is ambiguous between the following methods or properties: 
            // 'ScriptCoreLib.Extensions.GenericExtensions.With<android.widget.Button>(android.widget.Button, System.Action<android.widget.Button>)' and 
            // 'ScriptCoreLib.Extensions.LinqExtensions.With<android.widget.Button>(android.widget.Button, System.Action<android.widget.Button>)'	
            // y:\jsc.svn\examples\java\android\AndroidEmailActivity\AndroidEmailActivity\ApplicationActivity.cs	32	13	AndroidEmailActivity

            // can wee have inline c# extension methods already?

            #region AddProperty
            Func<string, int, EditText> AddProperty =
                (label, inputType) =>
                {
                    new TextView(this).With(
                      b =>
                      {
                          b.setText((java.lang.CharSequence)(object)label);


                          ll.addView(b);
                      }
                  );

                    return new EditText(this).With(
                       b =>
                       {
                           // http://developer.android.com/reference/android/text/InputType.html
                           // TYPE_TEXT_VARIATION_EMAIL_ADDRESS
                           b.setInputType(inputType);


                           ll.addView(b);
                       }
                   );
                };
            #endregion


            var email = AddProperty("Enter from email address:", 0x00000020);
            var subject = AddProperty("Enter email Subject:", 0x00000030);
            var text = AddProperty("Enter Text:", 1);

            this.imagepath = new TextView(this).With(
              b =>
              {


                  ll.addView(b);
              }
          );

            #region buttonSelectImage
            new Button(this).With(
                buttonSelectImage =>
                {
                    buttonSelectImage.setText((java.lang.CharSequence)(object)"Select image");

                    buttonSelectImage.AtClick(
                        delegate
                        {
                            Intent intent = new Intent(Intent.ACTION_PICK,
                                //android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI

                                // jsc incorrectly imports non root declaring type
                                EXTERNAL_CONTENT_URI
                            );
                            startActivityForResult(intent, RQS_LOADIMAGE);
                        }
                    );

                    ll.addView(buttonSelectImage);
                }
            );
            #endregion

            #region buttonSendEmail_intent
            new Button(this).With(
                buttonSendEmail_intent =>
                {
                    buttonSendEmail_intent.setText((java.lang.CharSequence)(object)"Send email using Intent.ACTION_SEND");

                    buttonSendEmail_intent.AtClick(
                        delegate
                        {
                            Intent intent = new Intent(Intent.ACTION_SEND);

                            intent.putExtra(Intent.EXTRA_EMAIL, 
                                new [] {
                                email.getText().ToString()
                                }
                                
                                );
                            intent.putExtra(Intent.EXTRA_SUBJECT, subject.getText().ToString());
                            intent.putExtra(Intent.EXTRA_TEXT, text.getText().ToString());

                            if (imageUri != null)
                            {
                                intent.putExtra(Intent.EXTRA_STREAM, imageUri);
                                intent.setType("image/png");
                            }
                            else
                            {
                                intent.setType("plain/text");
                            }

                            startActivity(Intent.createChooser(intent, (java.lang.CharSequence)(object)"Choice App to send email:"));
                        }
                    );

                    ll.addView(buttonSendEmail_intent);
                }
            );
            #endregion




            this.setContentView(sv);
        }

        public static android.net.Uri EXTERNAL_CONTENT_URI
        {
            [Script(OptimizedCode = "return android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI;")]
            get
            {
                android.provider.MediaStore hack;

                return null;
            }
        }

        protected override void onActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.onActivityResult(requestCode, resultCode, data);

            if (resultCode == RESULT_OK)
                if (requestCode == RQS_LOADIMAGE)
                {
                    imageUri = data.getData();
                    this.imagepath.setText((java.lang.CharSequence)(object)imageUri.ToString());
                }
        }
    }

    public static class GenericExtensions
    {
        // moving code from "Y:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs"

        [System.Diagnostics.DebuggerStepThrough]
        public static T With<T>(this T e, Action<T> h) where T : class
        {
            // jsc cast error~!
            //if (e != null)
            //if (h != null)
            h(e);

            return e;
        }
    }
}


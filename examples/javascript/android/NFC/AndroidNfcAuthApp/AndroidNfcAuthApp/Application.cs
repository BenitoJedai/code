using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AndroidNfcAuthApp;
using AndroidNfcAuthApp.Design;
using AndroidNfcAuthApp.HTML.Pages;
using System.Windows.Forms;


namespace AndroidNfcAuthApp
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public Application(IApp page)
        {

            page.VerifyPIN1.onclick += delegate
            {
          
                
                //var popupView = new android.widget.LinearLayout(this);
                //var dispWidth = getWindowManager().getDefaultDisplay().getWidth() - 60;

                //var popupText = new TextView(this);
                //popupText.setText("This is Popup Window!");
                //popupText.setPadding(0, 0, 0, 20);
                //popupText.setTextColor(-16711936);

                //var popupFormsTextBox = new TextBox();
                //popupFormsTextBox.PasswordChar = '*';
                //((__TextBox)(object)popupFormsTextBox).InternalBeforeSetContext(this);
                //var t = ((__TextBox)(object)popupFormsTextBox).InternalGetElement();
                //((EditText)t).setWidth(dispWidth);


                //var submitButt = new System.Windows.Forms.Button();
                //submitButt.Text = "Submit";
                //((__Button)(object)submitButt).InternalBeforeSetContext(this);
                //var b = ((__Button)(object)submitButt).InternalGetElement();

                //var cancelButt = new System.Windows.Forms.Button();
                //cancelButt.Text = "Cancel";
                //((__Button)(object)cancelButt).InternalBeforeSetContext(this);
                //var cb = ((__Button)(object)cancelButt).InternalGetElement();

                //popupView.addView(popupText);
                //popupView.addView(t);
                //popupView.addView(b);
                //popupView.addView(cb);

                //popupView.setOrientation(1);
                //popupView.setBackgroundColor(-3355444);

                //var popup = new android.widget.PopupWindow(popupView, dispWidth, 250);
                //popup.setContentView(popupView);
                //popup.setFocusable(true);
                //popup.setOutsideTouchable(true);


                //popup.showAsDropDown(((__Button)(object)u.button1).InternalGetElement(), android.view.Gravity.CENTER, 40, 0);

                //submitButt.Click += delegate
                //{
                //    submitButt.Text = ((EditText)t).getText().ToString();
                //    //TODO: communicate with NFC
                //};

                //cancelButt.Click += delegate
                //{
                //    popup.dismiss();
                //};

            };

        }

    }
}

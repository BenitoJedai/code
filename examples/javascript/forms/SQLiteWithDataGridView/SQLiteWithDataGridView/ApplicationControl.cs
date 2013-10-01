using Abstractatech.ConsoleFormPackage.Library;
using SQLiteWithDataGridView;
using SQLiteWithDataGridView.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLiteWithDataGridView
{
    public partial class ApplicationControl : UserControl
    {
//2658:02:01 00ad:01c7 SQLiteWithDataGridView.Application define Abstractatech.JavaScript.FormAsPopup::ScriptCoreLib.JavaScript.Extensions.X+<>c__DisplayClass1
//{ trace = X:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs, TargetMethod = ScriptCoreLib.JavaScript.DOM.IWindow postMessage(ScriptCoreLib.JavaScript.DOM.IWindow, System.Xml.Linq.XElement, System.Action`1[System.Xml.Linq.XElement]), DeclaringType = ScriptCoreLib.JavaScript.Extensions.X, Location =
// assembly: Y:\SQLiteWithDataGridView.Application\Abstractatech.JavaScript.FormAsPopup.dll
// type: ScriptCoreLib.JavaScript.Extensions.FormAsPopupExtensionsForConsoleFormPackageMediator, Abstractatech.JavaScript.FormAsPopup, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x00f4
//  method:Void .cctor(), ex = System.InvalidOperationException: Renew any references. TargetField can not be null. { Location =
// assembly: Y:\SQLiteWithDataGridView.Application\Abstractatech.JavaScript.FormAsPopup.dll
// type: ScriptCoreLib.JavaScript.Extensions.X, Abstractatech.JavaScript.FormAsPopup, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x004d
//  method:ScriptCoreLib.JavaScript.DOM.IWindow postMessage(ScriptCoreLib.JavaScript.DOM.IWindow, System.Xml.Linq.XElement, System.Action`1[System.Xml.Linq.XElement]) }
//   at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.<.ctor>b__47(ILInstruction )

        public ApplicationControl()
        {
            this.InitializeComponent();
        }


        public ConsoleForm con = new ConsoleForm();

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

            con.InitializeConsoleFormWriter();
            con.Show();

            Console.WriteLine("Console has been redirected!");
        }


        private void Table001_Click(object sender, System.EventArgs e)
        {
            var f = new GridForm { service = this.applicationWebService1 };


            f.Show();


            if (NewForm != null)
                NewForm(f);
        }

        public event Action<GridForm> NewForm;

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

    }
}

using ButterFlyWithInteractiveInt32Offset.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButterFlyWithInteractiveInt32Offset.Library
{
    public partial class InteractiveInt32Form : Form
    {
        public InteractiveInt32Form()
        {
            InitializeComponent();
        }
    }
}

namespace ScriptCoreLib.Extensions
{

    public static class XInteractiveInt32Form
    {
        //public static readonly ApplicationWebService service = new ApplicationWebService();

        public class Service
        {
            public Func<string, int, Task<string>> File_ReadLine;
            public Action<string, int, string> File_WriteLine;
        }

        public static Service service = new Service();




        static List<InteractiveInt32Form> lookup = new List<InteractiveInt32Form>();

        public static int ToInteractiveInt32Form(this int e,

            [CallerFilePathAttribute] string CallerFilePath = null,
            [CallerLineNumberAttribute] int CallerLineNumber = 0,
            [CallerMemberNameAttribute] string CallerMemberName = null

            )
        {
            var key = CallerFilePath + ":" + CallerLineNumber;

            var f = lookup.FirstOrDefault(
                k =>
                {
                    return k.label1.Text == key;
                }
            );

            if (f == null)
            {
                f = new InteractiveInt32Form { };
                f.label1.Text = key;



                f.label3.Text = CallerMemberName;

                f.textBox1.Text = "" + e;
                f.Show();

                lookup.Add(f);

                service.File_ReadLine.With(
                    File_ReadLine =>
                    {
                        File_ReadLine(CallerFilePath, CallerLineNumber).ContinueWithResult(
                            x =>
                            {

                                f.label2.Text = x;

                                f.textBox1.TextChanged +=
                                    delegate
                                    {
                                        f.label2.Text = x.Replace("" + e, f.textBox1.Text).Replace("default(int)", f.textBox1.Text);

                                        f.button1.Enabled = true;
                                    };

                                f.button1.Click +=
                                    delegate
                                    {
                                        f.button1.Enabled = false;

                                        if (service.File_WriteLine != null)
                                            service.File_WriteLine(
                                                CallerFilePath, CallerLineNumber,
                                                f.label2.Text
                                            );

                                    };
                            }
                        );

                    }
                 );
            }

            e = int.Parse(f.textBox1.Text);

            return e;
        }



    }
}

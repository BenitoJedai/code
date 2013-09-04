using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AppletAsyncWhenReady
{
    public partial class ApplicationControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();

//            W:\AppletAsyncWhenReady.ApplicationApplet\web\java\AppletAsyncWhenReady\ApplicationControl.java:46: cannot find symbol
//symbol  : method set_UseVisualStyleBackColor_06000043(boolean)
//location: class ScriptCoreLibJava.BCLImplementation.System.Windows.Forms.__Button
//        this.button1.set_UseVisualStyleBackColor_06000043(true);

            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(144, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(144, 160);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ApplicationControl
            // 
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "ApplicationControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.ResumeLayout(false);

        }

        public Button button2;
        public Button button1;

        ///// <summary>
        ///// Clean up any resources being used.
        ///// </summary>
        ///// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //"C:\Program Files (x86)\Java\jdk1.6.0_35\bin\javac.exe" -classpath "W:\AppletAsyncWhenReady.ApplicationApplet\web\java";release -d release java\AppletAsyncWhenReady\ApplicationApplet.java
            //W:\AppletAsyncWhenReady.ApplicationApplet\web\java\AppletAsyncWhenReady\ApplicationControl.java:36: cannot find symbol
            //symbol  : method Dispose_06000aa1(boolean)
            //location: class ScriptCoreLibJava.BCLImplementation.System.Windows.Forms.__UserControl
            //        super.Dispose_06000aa1(disposing);
            //             ^
            //W:\AppletAsyncWhenReady.ApplicationApplet\web\java\ScriptCoreLibJava\BCLImplementation\System\Windows\Forms\__Form.java:134: Dispose(boolean) in ScriptCoreLibJava.BCLImplementation.System.Windows.Forms.__Form cannot override Dispose(boolean) in ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel.__Component; attempting to assign weaker access privileges; was public
            //    protected  void Dispose(boolean e)
            //                    ^

            // Note: This jsc project does not support unmanaged resources.
            base.Dispose(disposing);
        }

    }
}

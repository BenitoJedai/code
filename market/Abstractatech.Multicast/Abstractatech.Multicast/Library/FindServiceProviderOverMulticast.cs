using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Extensions.Avalon;
using ScriptCoreLib.Shared.Avalon;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Runtime.InteropServices;
using System.Net;

namespace WithClickOnceLANLauncherShared
{
    [DefaultEvent("MissionAcomplished")]
    public class FindServiceProviderOverMulticast : Component
    {
        private System.Windows.Forms.Timer timer1;
        private MulticastListenerComponent multicastListener1;
        private MulticastSendComponent multicastSend1;
        private System.Windows.Forms.Timer LookAtOptions;
        private IContainer components;

        public FindServiceProviderOverMulticast()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.LookAtOptions = new System.Windows.Forms.Timer(this.components);
            this.multicastListener1 = new WithClickOnceLANLauncherShared.MulticastListenerComponent();
            this.multicastSend1 = new WithClickOnceLANLauncherShared.MulticastSendComponent();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LookAtOptions
            // 
            this.LookAtOptions.Enabled = true;
            this.LookAtOptions.Interval = 2000;
            this.LookAtOptions.Tick += new System.EventHandler(this.LookAtOptions_Tick);
            // 
            // multicastListener1
            // 
            this.multicastListener1.AtData += new System.Action<string>(this.multicastListener1_AtData);

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            // design time! timer enabled within design time..
            if (new StackTrace().GetFrames().Last().GetMethod().Name == "DebuggableCallback")
                return;

            //if (this.DesignMode)
            //    return;

            // http://imar.spaanjaars.com/278/how-do-i-detect-design-time-vs-run-time-in-a-net-control
            // http://stackoverflow.com/questions/34664/designmode-with-controls

            var IDesignerHost = GetService(typeof(IDesignerHost));
            //if (IDesignerHost != null)
            //    return;

            //, ProcessName = WDExpress, IDesignerHost =  }</string> }

            //           { listener = <string c="34">Where are you?? { Id = 4736, ProcessName = WDExpress, IDesignerHost = , stack =    at WithClickOnceLANLauncherShared.FindServiceProviderOverMulticast.timer1_Tick(Object sender, EventArgs e)
            //  at System.Windows.Forms.Timer.OnTick(EventArgs e)
            //  at System.Windows.Forms.Timer.TimerNativeWindow.WndProc(Message&amp; m)
            //  at System.Windows.Forms.NativeWindow.DebuggableCallback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)
            //}</string> }


            //           { listener = <string c="6">Where are you??? { DesignMode = False, ManagedThreadId = 1, Name = , Id = 7832, ProcessName = WDExpress, IDesignerHost = , stack =    at WithClickOnceLANLauncherShared.FindServiceProviderOverMulticast.timer1_Tick(Object sender, EventArgs e)
            //  at System.Windows.Forms.Timer.OnTick(EventArgs e)
            //  at System.Windows.Forms.Timer.TimerNativeWindow.WndProc(Message&amp; m)
            //  at System.Windows.Forms.NativeWindow.DebuggableCallback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)
            //}</string> }

            // with debugger
            //{ listener = <string c="3">Where are you??? { DesignMode = False, ManagedThreadId = 7, Name = , Id = 8972, ProcessName = WithClickOnceLANLauncherClient.vshost, IDesignerHost = , stack =    at WithClickOnceLANLauncherShared.FindServiceProviderOverMulticast.timer1_Tick(Object sender, EventArgs e)
            //   at System.Windows.Forms.Timer.TimerNativeWindow.WndProc(Message&amp; m)
            //   at System.Windows.Forms.NativeWindow.DebuggableCallback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)
            //   at System.Windows.Forms.UnsafeNativeMethods.DispatchMessageW(MSG&amp; msg)
            //   at System.Windows.Forms.UnsafeNativeMethods.DispatchMessageW(MSG&amp; msg)
            //   at System.Windows.Forms.Application.ComponentManager.System.Windows.Forms.UnsafeNativeMethods.IMsoComponentManager.FPushMessageLoop(IntPtr dwComponentID, Int32 reason, Int32 pvLoopData)
            //   at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(Int32 reason, ApplicationContext context)
            //   at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(Int32 reason, ApplicationContext context)
            //   at WithClickOnceLANLauncherClient.Program.Main()
            //   at System.AppDomain._nExecuteAssembly(RuntimeAssembly assembly, String[] args)
            //   at System.Runtime.Hosting.ApplicationActivator.CreateInstance(ActivationContext activationContext, String[] activationCustomData)
            //   at Microsoft.VisualStudio.HostingProcess.HostProc.RunUsersAssemblyDebugInZone()
            //   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
            //   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
            //   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
            //   at System.Threading.ThreadHelper.ThreadStart()

            // without debugger
            //         { listener = <string c="3">Where are you??? { DesignMode = False, ManagedThreadId = 1, Name = , Id = 7772, ProcessName = WithClickOnceLANLauncherClient, IDesignerHost = , stack =    at WithClickOnceLANLauncherShared.FindServiceProviderOverMulticast.timer1_Tick(Object sender, EventArgs e)
            //at System.Windows.Forms.Timer.TimerNativeWindow.WndProc(Message&amp; m)
            //at System.Windows.Forms.NativeWindow.Callback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)
            //at System.Windows.Forms.UnsafeNativeMethods.DispatchMessageW(MSG&amp; msg)
            //at System.Windows.Forms.UnsafeNativeMethods.DispatchMessageW(MSG&amp; msg)
            //at System.Windows.Forms.Application.ComponentManager.System.Windows.Forms.UnsafeNativeMethods.IMsoComponentManager.FPushMessageLoop(IntPtr dwComponentID, Int32 reason, Int32 pvLoopData)
            //at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(Int32 reason, ApplicationContext context)
            //at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(Int32 reason, ApplicationContext context)
            //at WithClickOnceLANLauncherClient.Program.Main()


            // what is this?
            // { listener = <string c="11">Where are you??? { DesignMode = False, ManagedThreadId = 1, Name = , Id = 3808, ProcessName = WDExpress, IDesignerHost = , stack =    at WithClickOnceLANLauncherShared.FindServiceProviderOverMulticast.timer1_Tick(Object sender, EventArgs e)
            //   at System.Windows.Forms.Timer.OnTick(EventArgs e)
            //   at System.Windows.Forms.Timer.TimerNativeWindow.WndProc(Message&amp; m)
            //   at System.Windows.Forms.NativeWindow.DebuggableCallback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)
            // }</string> }
            //time to send data over multicast




            Console.WriteLine(
                "time to send data over multicast"
            );

            this.multicastSend1.Send("Where are you??? "
                //+ new
                //{
                //    DesignMode = this.Site == null ? false : this.Site.DesignMode,
                //    Thread.CurrentThread.ManagedThreadId,
                //    Thread.CurrentThread.Name,
                //    System.Diagnostics.Process.GetCurrentProcess().Id,
                //    System.Diagnostics.Process.GetCurrentProcess().ProcessName,
                //    IDesignerHost,

                //    //   at System.Windows.Forms.NativeWindow.DebuggableCallback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)
                //    stack = new StackTrace().GetFrames().Last().ToString()
                //}
                );
        }


        public List<string> Options = new List<string>();

        private void multicastListener1_AtData(string listen)
        {
            Console.WriteLine(
              new { listen }
            );

            try
            {
                var xml = XElement.Parse(listen);

                if (xml.Value.StartsWith("Visit me at "))
                {
                    //StringExtensions
                    //StringExtensions

                    var uri = xml.Value.SkipUntilOrEmpty("Visit me at ");

                    Options.AddDistinct(uri);

                    //Console.WriteLine(new { uri });




                }
            }
            catch
            {

            }
        }


        public event Action<string> MissionAcomplished;

        // http://stackoverflow.com/questions/187894/how-do-i-obtain-the-physical-mac-address-of-an-ip-address-using-c

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int destIp, int srcIP, byte[] macAddr, ref uint physicalAddrLen);

        private void LookAtOptions_Tick(object sender, EventArgs e)
        {
            // design time! timer enabled within design time..
            if (new StackTrace().GetFrames().Last().GetMethod().Name == "DebuggableCallback")
                return;

            if (Options.Count == 0)
                return;

            this.timer1.Stop();
            LookAtOptions.Stop();



            // soon
            this.multicastListener1.InternalListener.Dispose();

            foreach (var uri in Options)
            {
                Console.WriteLine(new { uri });
            }

            if (Options.Count == 1)
            {
                if (MissionAcomplished != null)
                    MissionAcomplished(Options.Single());
            }
            else
            {
                var f = new OptionsDialog();

                foreach (var uri in Options)
                {

                    IPAddress dst = IPAddress.Parse(uri.TakeUntilIfAny(":")); // the destination IP address

                    byte[] macAddr = new byte[6];
                    uint macAddrLen = (uint)macAddr.Length;
                    string[] str = new string[(int)macAddrLen];

                    if (SendARP(BitConverter.ToInt32(dst.GetAddressBytes(), 0), 0, macAddr, ref macAddrLen) != 0)
                    {
                        // 
                        //Additional information: SendARP failed.
                        //throw new InvalidOperationException("SendARP failed.");
                    }
                    else
                    {


                        for (int i = 0; i < macAddrLen; i++)
                            str[i] = macAddr[i].ToString("x2");

                        Console.WriteLine();
                    }

                    f.comboBox1.Items.Add(uri + " " + string.Join(":", str));


                }
                f.comboBox1.SelectedIndex = 0;

                f.button1.Click +=
                    delegate
                    {
                        if (MissionAcomplished != null)
                            MissionAcomplished(f.comboBox1.Text.TakeUntilIfAny(" "));
                        f.Close();
                    };

                f.Show();

            }
        }
    }
}

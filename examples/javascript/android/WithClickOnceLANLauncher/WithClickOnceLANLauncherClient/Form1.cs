using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace WithClickOnceLANLauncherClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public event Action<Uri> MissionAcomplished;

        private void findServiceProviderOverMulticast1_MissionAcomplished(string uri)
        {
            //>	WithClickOnceLANLauncherClient.exe!WithClickOnceLANLauncherClient.Form1.findServiceProviderOverMulticast1_MissionAcomplished() Line 27	C#
            //    WithClickOnceLANLauncherShared.dll!WithClickOnceLANLauncherShared.FindServiceProviderOverMulticast.multicastListener1_AtData(string listen) Line 168 + 0x27 bytes	C#
            //    WithClickOnceLANLauncherShared.dll!WithClickOnceLANLauncherShared.MulticastListenerComponent.InternalInitialize.AnonymousMethod__0(byte[] bytes) Line 61 + 0x24 bytes	C#
            //    WithClickOnceLANLauncherShared.dll!WithClickOnceLANLauncherShared.MulticastListener.ReceiveCallback(System.IAsyncResult ar) Line 189 + 0x22 bytes	C#
            //    System.dll!System.Net.LazyAsyncResult.Complete(System.IntPtr userToken) + 0xc5 bytes	
            //    mscorlib.dll!System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state, bool preserveSyncCtx) + 0x285 bytes	
            //    mscorlib.dll!System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state, bool preserveSyncCtx) + 0x9 bytes	
            //    mscorlib.dll!System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state) + 0x57 bytes	
            //    System.dll!System.Net.ContextAwareResult.Complete(System.IntPtr userToken) + 0xb5 bytes	
            //    System.dll!System.Net.Sockets.BaseOverlappedAsyncResult.CompletionPortCallback(uint errorCode, uint numBytes, System.Threading.NativeOverlapped* nativeOverlapped) + 0x125 bytes	
            //    mscorlib.dll!System.Threading._IOCompletionCallback.PerformIOCompletionCallback(uint errorCode, uint numBytes, System.Threading.NativeOverlapped* pOVERLAP) + 0x96 bytes	
            //    [Native to Managed Transition]	

            this.Invoke(
                new Action(
                    delegate()
                    {
                        //                    >	WithClickOnceLANLauncherClient.exe!WithClickOnceLANLauncherClient.Form1.findServiceProviderOverMulticast1_MissionAcomplished.AnonymousMethod__0() Line 44	C#
                        //[Native to Managed Transition]	
                        //mscorlib.dll!System.Delegate.DynamicInvokeImpl(object[] args) + 0x6a bytes	
                        //System.Windows.Forms.dll!System.Windows.Forms.Control.InvokeMarshaledCallbackDo(System.Windows.Forms.Control.ThreadMethodEntry tme) + 0x97 bytes	
                        //System.Windows.Forms.dll!System.Windows.Forms.Control.InvokeMarshaledCallbackHelper(object obj) + 0xef bytes	
                        //mscorlib.dll!System.Threading.ExecutionContext.RunInternal(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state, bool preserveSyncCtx) + 0x285 bytes	
                        //mscorlib.dll!System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state, bool preserveSyncCtx) + 0x9 bytes	
                        //mscorlib.dll!System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext executionContext, System.Threading.ContextCallback callback, object state) + 0x57 bytes	
                        //System.Windows.Forms.dll!System.Windows.Forms.Control.InvokeMarshaledCallback(System.Windows.Forms.Control.ThreadMethodEntry tme) + 0x113 bytes	
                        //System.Windows.Forms.dll!System.Windows.Forms.Control.InvokeMarshaledCallbacks() + 0x11d bytes	
                        //System.Windows.Forms.dll!System.Windows.Forms.Control.WndProc(ref System.Windows.Forms.Message m) + 0x2aa bytes	
                        //System.Windows.Forms.dll!System.Windows.Forms.Form.WndProc(ref System.Windows.Forms.Message m) + 0x1ee bytes	
                        //System.Windows.Forms.dll!System.Windows.Forms.NativeWindow.DebuggableCallback(System.IntPtr hWnd, int msg, System.IntPtr wparam, System.IntPtr lparam) + 0x14c bytes	
                        //[Native to Managed Transition]	
                        //[Managed to Native Transition]	
                        //System.Windows.Forms.dll!System.Windows.Forms.Application.ComponentManager.System.Windows.Forms.UnsafeNativeMethods.IMsoComponentManager.FPushMessageLoop(System.IntPtr dwComponentID, int reason, int pvLoopData) + 0x681 bytes	
                        //System.Windows.Forms.dll!System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(int reason, System.Windows.Forms.ApplicationContext context) + 0x57c bytes	
                        //System.Windows.Forms.dll!System.Windows.Forms.Application.ThreadContext.RunMessageLoop(int reason, System.Windows.Forms.ApplicationContext context) + 0x6f bytes	
                        //WithClickOnceLANLauncherClient.exe!WithClickOnceLANLauncherClient.Program.Main() Line 19 + 0x2f bytes	C#

                        // or we can reconnect the webservice
                        // what if we want a stylized viewer?

                        var u = new Uri("http://" + uri);

                        if (MissionAcomplished != null)
                            MissionAcomplished(u);
                        else
                        {
                            u.NavigateTo();
                            this.Close();
                        }

                    }
                )
            );
        }
    }
}

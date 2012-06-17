using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestObfuscationWithXAMLDebugger
{
    class Program
    {
        static void Main(string[] args)
        {
            TestObfuscationWithXAML.App.Main();
            /*
             * 
             * http://msdn.microsoft.com/en-us/library/aa970069.aspx
             
C:\Users\Arvo\AppData\Local\Temp\SymbolCache\src\source\.NET\4\DEVDIV_TFS\Dev10\Releases\RTMRel\ndp\clr\src\BCL\System\Reflection\Assembly.cs\1305376\Assembly.cs
GetManifestResourceStream
             * 
C:\Users\Arvo\AppData\Local\Temp\SymbolCache\src\source\.NET\4\DEVDIV_TFS\Dev10\Releases\RTMRel\ndp\clr\src\BCL\System\AppDomain.cs\1305376\AppDomain.cs
OnResourceResolveEvent
             * 
             * * *
C:\Users\Arvo\AppData\Local\Temp\SymbolCache\src\source\.NET\4\DEVDIV_TFS\Dev10\Releases\RTMRel\ndp\clr\src\BCL\System\Resources\ManifestBasedResourceGroveler.cs\1305376\ManifestBasedResourceGroveler.cs
             * 
             * * 		BaseNameField	"TestObfuscationWithXAMLDebugger.g"	string

             * 
             *             Stream stream = part.GetStream();
             +		part	{MS.Internal.AppModel.ResourcePart}	System.IO.Packaging.PackagePart {MS.Internal.AppModel.ResourcePart}

             * 
-		ResourceManagerWrapper	MS.Internal.Resources.ResourceManagerWrapper	MS.Internal.Resources.ResourceManagerWrapper
		base	object	object
		LocalizableResourceNameSuffix	".g"	string
		UnLocalizableResourceNameSuffix	".unlocalizable.g"	string
              
             
C:\Users\Arvo\AppData\Local\Temp\SymbolCache\src\source\.NET\4\DEVDIV_TFS\Dev10\Releases\RTMRel\wpf\src\Framework\System\Windows\Application.cs\1305600\Application.cs

        /// <summary>
        /// This property gets and sets the assembly pack://application:,,,/ refers to.
        /// </summary> 
        public static Assembly ResourceAssembly
        { 
            get 
            {
 
                if (_resourceAssembly == null)
                {
                    lock (_globalLock)
                    { 
                        _resourceAssembly = Assembly.GetEntryAssembly();
                    } 
                } 

                return _resourceAssembly; 
            }
             
             * * 
             * 		m_String	"pack://application:,,,/MainWindow.xaml"	string

System.IO.IOException was unhandled
  Message=Cannot locate resource 'mainwindow.xaml'.
  Source=PresentationFramework
  StackTrace:
       at MS.Internal.AppModel.ResourcePart.GetStreamCore(FileMode mode, FileAccess access)
       at System.IO.Packaging.PackagePart.GetStream(FileMode mode, FileAccess access)
       at System.IO.Packaging.PackagePart.GetStream()
       at System.Windows.Application.LoadComponent(Uri resourceLocator, Boolean bSkipJournaledProperties)
       at System.Windows.Application.DoStartup()
       at System.Windows.Application.<.ctor>b__1(Object unused)
       at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)
       at MS.Internal.Threading.ExceptionFilterHelper.TryCatchWhen(Object source, Delegate method, Object args, Int32 numArgs, Delegate catchHandler)
       at System.Windows.Threading.DispatcherOperation.InvokeImpl()
       at System.Windows.Threading.DispatcherOperation.InvokeInSecurityContext(Object state)
       at System.Threading.ExecutionContext.runTryCode(Object userData)
       at System.Runtime.CompilerServices.RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(TryCode code, CleanupCode backoutCode, Object userData)
       at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
       at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean ignoreSyncCtx)
       at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
       at System.Windows.Threading.DispatcherOperation.Invoke()
       at System.Windows.Threading.Dispatcher.ProcessQueue()
       at System.Windows.Threading.Dispatcher.WndProcHook(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
       at MS.Win32.HwndWrapper.WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, Boolean& handled)
       at MS.Win32.HwndSubclass.DispatcherCallbackOperation(Object o)
       at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)
       at MS.Internal.Threading.ExceptionFilterHelper.TryCatchWhen(Object source, Delegate method, Object args, Int32 numArgs, Delegate catchHandler)
       at System.Windows.Threading.Dispatcher.InvokeImpl(DispatcherPriority priority, TimeSpan timeout, Delegate method, Object args, Int32 numArgs)
       at MS.Win32.HwndSubclass.SubclassWndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam)
       at MS.Win32.UnsafeNativeMethods.DispatchMessage(MSG& msg)
       at System.Windows.Threading.Dispatcher.PushFrameImpl(DispatcherFrame frame)
       at System.Windows.Threading.Dispatcher.PushFrame(DispatcherFrame frame)
       at System.Windows.Threading.Dispatcher.Run()
       at System.Windows.Application.RunDispatcher(Object ignore)
       at System.Windows.Application.RunInternal(Window window)
       at System.Windows.Application.Run(Window window)
       at System.Windows.Application.Run()
       at TestObfuscationWithXAML.App.Main()
       at TestObfuscationWithXAMLDebugger.Program.Main(String[] args) in w:\jsc.svn\examples\rewrite\TestObfuscationWithXAML\TestObfuscationWithXAMLDebugger\Program.cs:line 12
       at System.AppDomain._nExecuteAssembly(RuntimeAssembly assembly, String[] args)
       at System.AppDomain.ExecuteAssembly(String assemblyFile, Evidence assemblySecurity, String[] args)
       at Microsoft.VisualStudio.HostingProcess.HostProc.RunUsersAssembly()
       at System.Threading.ThreadHelper.ThreadStart_Context(Object state)
       at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean ignoreSyncCtx)
       at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
       at System.Threading.ThreadHelper.ThreadStart()
  InnerException: 
             
             
             */
        }
    }
}

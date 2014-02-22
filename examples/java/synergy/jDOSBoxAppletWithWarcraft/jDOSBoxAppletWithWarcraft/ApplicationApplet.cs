using java.applet;
using java.awt.@event;
using java.net;
using System;

namespace jDOSBoxAppletWithWarcraft
{
    public sealed class ApplicationApplet : jdos.gui.MainApplet
    {
        public const int DefaultWidth = 800;
        public const int DefaultHeight = 600;


        int x;
        int y;


        public static void Main(string[] args)
        {
            //enter Main
            //Applet is not signed, mouse capture will not work
            //Exception in thread "Thread-11" Exception in thread "Thread-12" java.lang.ExceptionInInitializerError
            //    at jdos.cpu.core_dynamic.Compiler.<clinit>(Compiler.java:7385)
            //    at jdos.Dosbox.Init(Dosbox.java:451)
            //    at jdos.gui.MainBase.main(MainBase.java:447)
            //    at jdos.gui.MainFrame$4.run(MainFrame.java:247)
            //    at java.lang.Thread.run(Unknown Source)
            //Caused by: java.security.AccessControlException: access denied ("java.lang.RuntimePermission" "accessDeclaredMembers")
            //    at java.security.AccessControlContext.checkPermission(Unknown Source)
            //    at java.security.AccessController.checkPermission(Unknown Source)
            //    at java.lang.SecurityManager.checkPermission(Unknown Source)
            //    at java.lang.Class.checkMemberAccess(Unknown Source)
            //    at java.lang.Class.getDeclaredMethod(Unknown Source)
            //    at javassist.ClassPool$1.run(ClassPool.java:78)
            //    at java.security.AccessController.doPrivileged(Native Method)
            //    at javassist.ClassPool.<clinit>(ClassPool.java:75)
            //    ... 5 more
            //java.lang.NoClassDefFoundError: Could not initialize class jdos.cpu.core_dynamic.Compiler
            //    at jdos.cpu.core_dynamic.Compiler$1.run(Compiler.java:46)
            //    at java.lang.Thread.run(Unknown Source)


            Console.WriteLine("enter Main");

            jdos.gui.MainFrame.main(args);
        }

        public ApplicationApplet()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140222

            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "W:\jDOSBoxAppletWithWarcraft.ApplicationApplet\web\java";release -d release java\jDOSBoxAppletWithWarcraft\ApplicationApplet.java
            //java\jDOSBoxAppletWithWarcraft\ApplicationApplet.java:40: error: cannot assign a value to final variable C_IPX
            //        Config.C_IPX = true;
            //              ^

            // http://jdosbox.sourceforge.net/change.html
            //jdos.misc.setup.Config.C_IPX = true;
            Console.WriteLine(
                new { jdos.misc.setup.Config.C_IPX }
                );

            // http://www.coderanch.com/t/486585/Applets/java/applet-socketpermissions
            // fk, java applets, you are useless!??
            // https://www.java.net/node/666745

            // http://www.experts-exchange.com/Programming/Languages/Java/Q_27992073.html
            // http://www.experts-exchange.com/Programming/Languages/Java/Q_27992073.html

            // https://bugs.openjdk.java.net/browse/JDK-4093502
            var s = new SocketPermission(
                 "localhost:213", "listen,resolve"
                );

            Console.WriteLine("SocketPermission getActions " + s.getActions());

            //java.lang.System.getSecurityManager().
            //s.
            //s.
            //java.security.AccessControlException: access denied ("java.net.SocketPermission" "127.0.0.1:4000" "connect,resolve")
            //    at java.security.AccessControlContext.checkPermission(Unknown Source)
            //    at java.security.AccessController.checkPermission(Unknown Source)
            //    at java.lang.SecurityManager.checkPermission(Unknown Source)
            //    at java.lang.SecurityManager.checkConnect(Unknown Source)
            //    at sun.plugin2.applet.SecurityManagerHelper.checkConnectHelper(Unknown Source)
            //    at sun.plugin2.applet.AWTAppletSecurityManager.checkConnect(Unknown Source)
            //    at java.net.DatagramSocket.send(Unknown Source)
            //    at jdos.hardware.IPX.ConnectToServer(IPX.java:956)
            //    at jdos.hardware.IPX.access$1700(IPX.java:25)
            //    at jdos.hardware.IPX$IPXNET.Run(IPX.java:1119)
            //    at jdos.misc.Program$1.call(Program.java:76)
            //    at jdos.Dosbox$1.call(Dosbox.java:75)
            //    at jdos.Dosbox.DOSBOX_RunMachine(Dosbox.java:205)
            //    at jdos.cpu.Callback.CALLBACK_RunRealInt(Callback.java:158)
            //    at jdos.shell.Dos_shell.Execute(Dos_shell.java:755)
            //    at jdos.shell.Dos_shell.DoCommand(Dos_shell.java:633)
            //    at jdos.shell.Dos_shell.ParseLine(Dos_shell.java:173)
            //    at jdos.shell.Dos_shell.Run(Dos_shell.java:98)
            //    at jdos.shell.Shell$5.call(Shell.java:398)
            //    at jdos.misc.setup.Config.StartUp(Config.java:97)
            //    at jdos.gui.MainBase.main(MainBase.java:523)
            //    at jdos.gui.MainApplet.run(MainApplet.java:269)
            //    at java.lang.Thread.run(Unknown Source)
            //IPX: Unable to connect to server
            //IPXSERVER: Connect from 192.168.43.252

            //IPXSERVER: enter IPX_StartServer
            //IPXSERVER: enter IPX_StartServer before getByName
            //IPXSERVER: enter IPX_StartServer before DatagramSocket
            //java.security.AccessControlException: access denied ("java.net.SocketPermission" "localhost:213" "listen,resolve")


            //this.i
            // http://www.vogons.org/viewtopic.php?p=227202

            //You must sign the applet so it can connect to a host other than the one it was loaded from, and either you must use a non-self-signed-certificate or the user must accept the certificate when prompted.
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140222
            // http://stackoverflow.com/questions/15074834/java-applet-socket-permission
            // http://stackoverflow.com/questions/10083332/java-applet-accesscontrolexception-access-denied-socketpermission-where-do
            // http://www.khattam.info/solved-java-security-accesscontrolexception-access-denied-java-net-socketpermission-host-connectresolve-2010-03-24.html

            // https://bbs.archlinux.org/viewtopic.php?id=142474

        }

        public void __MainApplet_mousePressed()
        {
            //Console.WriteLine("__MainApplet_mousemove " + new { x, y });

            var e = new MouseEvent(
                this,
                MouseEvent.MOUSE_PRESSED,
                0,
                0,
                x,
                y,
                0,
                false
            );

            mousePressed(e);
        }

        public void __MainApplet_mouseReleased()
        {
            //Console.WriteLine("__MainApplet_mousemove " + new { x, y });

            var e = new MouseEvent(
                this,
                MouseEvent.MOUSE_RELEASED,
                0,
                0,
                x,
                y,
                0,
                false
            );

            mouseReleased(e);
        }

        public void __MainApplet_mousemove(string dx, string dy)
        {
            x += int.Parse(dx);
            y += int.Parse(dy);

            x = x.Min(640).Max(0);
            y = y.Min(480).Max(0);

            //Console.WriteLine("__MainApplet_mousemove " + new { x, y });

            var e = new MouseEvent(
                this,
                MouseEvent.MOUSE_MOVED,
                0,
                0,
                x,
                y,
                0,
                false
            );

            mouseMoved(e);
        }

        public void __MainApplet_keyPressed(string _key, string _char, Action<string> yield)
        {
            //            KeyEvent(Component source, int id, long when, int modifiers, int keyCode, char keyChar) 
            //source - the Component that originated the event
            //id - an integer identifying the type of event
            //when - a long integer that specifies the time the event occurred
            //modifiers - the modifier keys down during event (shift, ctrl, alt, meta) Either extended _DOWN_MASK or old _MASK modifiers should be used, but both models should not be mixed in one event. Use of the extended modifiers is preferred.
            //keyCode - the integer code for an actual key, or VK_UNDEFINED (for a key-typed event)
            //keyChar - the Unicode character generated by this event, or CHAR_UNDEFINED (for key-pressed and key-released events which do not map to a valid Unicode character)
            try
            {
                var __keyCode = int.Parse(_key);
                var __keyChar = int.Parse(_char);

                //if (__keyChar == 0)
                //    __keyChar = KeyEvent.CHAR_UNDEFINED;


                //Console.WriteLine("__MainApplet_keyPressed " + new { __keyCode, __keyChar });

                base.keyPressed(
                    new java.awt.@event.KeyEvent(
                        this,
                        KeyEvent.KEY_PRESSED,
                        0,
                        __keyCode,
                        __keyChar
                    )
                );
            }
            catch (Exception ex)
            {
                yield(new { ex.Message }.ToString());
            }
        }

        public void __MainApplet_keyReleased(string _key, string _char, Action<string> yield)
        {
            try
            {
                var __keyCode = int.Parse(_key);
                var __keyChar = int.Parse(_char);

                //if (__keyChar == 0)
                //    __keyChar = KeyEvent.CHAR_UNDEFINED;


                //Console.WriteLine("__MainApplet_keyReleased " + new { __keyCode, __keyChar });

                base.keyReleased(
                    new java.awt.@event.KeyEvent(
                        this,
                        KeyEvent.KEY_RELEASED,
                        0,
                       __keyCode,
                       __keyChar
                    )
                );
            }
            catch (Exception ex)
            {
                yield(new { ex.Message }.ToString());
            }

        }
    }
}

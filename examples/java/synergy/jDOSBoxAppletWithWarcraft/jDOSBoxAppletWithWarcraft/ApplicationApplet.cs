using java.applet;
using java.awt.@event;
using System;

namespace jDOSBoxAppletWithWarcraft
{
    public sealed class ApplicationApplet : jdos.gui.MainApplet
    {
        public const int DefaultWidth = 800;
        public const int DefaultHeight = 600;


        int x;
        int y;

        public ApplicationApplet()
        {
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


            //this.i
            // http://www.vogons.org/viewtopic.php?p=227202

            //You must sign the applet so it can connect to a host other than the one it was loaded from, and either you must use a non-self-signed-certificate or the user must accept the certificate when prompted.
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140222

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

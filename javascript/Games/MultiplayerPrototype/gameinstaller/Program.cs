using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace gameinstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo Target = new DirectoryInfo("game");

            Target.Create();

            Console.WriteLine("installing gameserver...");

            Assembly a = Assembly.GetExecutingAssembly();

            string[] n = a.GetManifestResourceNames();

            Action<string> Extract =
                p =>
                {
                    foreach (string x in n)
                    {
                        if (x.StartsWith(p))
                        {
                            FileInfo f = new FileInfo(Target.FullName + "/" + x.Substring(p.Length + 1));

                            using (Stream s = f.OpenWrite()) CopyStream(a.GetManifestResourceStream(x), s);

                        }
                    }
                };

            Extract("gameinstaller.Package.client");
            Extract("gameinstaller.Package.server");
            Extract("gameinstaller.Package.jsc");

            // exec jsc

            // exce server

            {
                ProcessStartInfo pi = new ProcessStartInfo(Target.FullName + "/jsc.exe", "gameclient.dll -js");


                pi.WorkingDirectory = Target.FullName;
                pi.CreateNoWindow = false;
                pi.UseShellExecute = false;

                pi.RedirectStandardOutput = true;

                Process ps = Process.Start(pi);

                ps.EnableRaisingEvents = true;

                Console.WriteLine("compiling to javascript...");

                int i = Console.CursorTop;

                ps.BeginOutputReadLine();

                ps.OutputDataReceived +=
                    delegate(object sender, DataReceivedEventArgs e)
                    {
                        if (e.Data == null)
                            return;

                        string z = e.Data;

                        if (z.Length > Console.BufferWidth - 4)
                            z = z.Substring(0, Console.BufferWidth - 4) + "...";

                        Console.WriteLine(z.PadRight(Console.BufferWidth - 1));
                        Console.SetCursorPosition(0, i);
                    };

                ps.WaitForExit();


            }

            new FileInfo(Target.FullName + "/jsc.exe").Delete();
            new FileInfo(Target.FullName + "/Interop.Dia2Lib.dll").Delete();

            //Console.WriteLine("".PadLeft(Console.BufferWidth -1));
            //Console.WriteLine(@"type 'cd game\'");
            //Console.WriteLine(@"     'gameserver.exe' to start...");
            //Console.WriteLine(@"and to join navigate to 'http://localhost'");

            var ps2 = new ProcessStartInfo(Target.FullName + "/gameserver.exe");

            Process.Start(ps2);

        }

        public static void CopyStream(Stream FromStream, Stream ToStream)
        {

            try
            {
                //Creat a file to save to


                //use the binary reader & writer because
                //they can work with all formats
                //i.e images, text files ,avi,mp3..



                BinaryReader br = new BinaryReader(FromStream);


                BinaryWriter bw = new BinaryWriter(ToStream);


                //copy data from the FromStream to the outStream
                //convert from long to int 
                bw.Write(br.ReadBytes((int)FromStream.Length));
                //save
                bw.Flush();
                //clean up 
                bw.Close();
                br.Close();
            }

             //use Exception e as it can handle any exception 
            catch (Exception e)
            {
                //code if u like 
            }
        }

    }
}

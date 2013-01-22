using MyGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VanillaExperiment.GameCodesLauncher
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            //Trying last loaded dll
            // - error: Could not find any Player.IO room types [RoomType(...)] in the dll.

            //Trying last loaded dll
            // - error: Reference found to Abstractatech.PlayerIO. You can only reference PlayerIO.GameLibrary.dll

            //Checking if Abstractatech.PlayerIO.dll is a game dll (from application directory)
            // - error: Unable to load one or more of the requested types. Retrieve the LoaderExceptions property for more information.

            //Checking if VanillaExperiment.GameCodes.dll is a game dll (from application directory)
            // - error: Reference found to Abstractatech.PlayerIO. You can only reference PlayerIO.GameLibrary.dll

            //Checking if VanillaExperiment.GameCodes.dll is a game dll (from application directory)
            // - error: Could not find any Player.IO room types [RoomType(...)] in the dll.


            Console.WriteLine("VanillaExperiment.GameCodesLauncher");

            //ActiveX control '8856f961-340a-11d0-a96b-00c04fd705a2' cannot be instantiated because the current thread is not in a single-threaded apartment.

            // Could not load file or assembly 'ScriptCoreLibA, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.
            Application.SetCompatibleTextRenderingDefault(true);

            PlayerIO.DevelopmentServer.Server.StartWithDebugging();
        }
    }

}

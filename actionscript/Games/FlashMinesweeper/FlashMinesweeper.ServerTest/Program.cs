using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashMinesweeper.ServerTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Nonoba.DevelopmentServer.Server.StartWithDebugging(null);
			/*

Minor Update: v1.5.3
============
- Fixed small bug with case sensitivity of ranking lists


How to Update
=============
Simply download the new API package by clicking
the download now button, and extract it somewhere.

Then you need to do the following for each of
your games:
 
	a)	Overwrite [Your Game]\Nonoba .NET Libraries\Development Server.exe 
		and Nonoba.GameLibrary.dll 
		with the same files from a game in the newly downloaded zip file.
		This will update your development server
		
	b)	Overwrite [Your Game]\Flash\Nonoba 
		with the same files from a game in the newly downloaded zip file.
		This will update the flash api.
		
	c)	Delete the following folders 
			[Your Game]\Nonoba .NET Libraries\Test Server\bin	
		and 
			[Your Game]\Nonoba .NET Libraries\Test Server\obj 		
You're done :-)



==== Older Updates ===================
Minor Update: v1.5.2
============
- Exceptions and errors printed are now printed in red in the game console
- Bug: Exceptions during GameStarted() are now also caught and printed in the 
  game console like all other exceptions.
  
Minor Update: v1.5.1
============
- Switched to .NET 3.5 & Allowed extension methods, System.Core
- Fixed crash if codescan finds errors (nullref at startup)
- Fixed bug in Chat-integration in development server

Update: v1.5.0
============
New features
- Chat Integration: Control and interact with the chat
  from AS3
- GameSetup: Allow players to configure their game rooms
Read the online documentation to learn more.

Other stuff
- Private games: Games that aren't shown in the lobby
- Regular expressions and stopwatch added to whitelist
  of allowed classes

Minor update: v1.0.6
============
Fixed bug causing parsing of messages
to stop after the first double (floating)
value. 

Minor update: v1.0.5
============
Allows long running debugging without being 
caught by the CPU watch.

Minor update: v1.0.4
============
This is a pure feature release, adding
Nonoba Payment to the AS3 API.			 */
		}
    }
}

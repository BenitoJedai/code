Thank you for choosing the jsc compiler.

There are a few things you still have to do manually.

1. Add the references, which can be found in the same directory as the jsc.exe
	- ScriptCoreLibA.dll
	- ScriptCoreLib.dll
2. Open project properties and update the post build action with the correct location of jsc.exe.


Hit build and the Script Application will be compiled for you.

The web/version directory can be deleted to force a full recompilation.

more information: http://jsc.sourceforge.net

04.03.2007
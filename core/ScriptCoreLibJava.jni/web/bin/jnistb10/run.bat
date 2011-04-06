@echo off
set _path=bin
set _entry=jni.Main

set PATH=%PATH%;%_path%

rem cd
rem echo ~%_path%
echo + jar : %_path%\package.jar %_entry% 

call "C:\Program Files\Java\jdk1.6.0_24\bin\java.exe" -cp "%_path%\package.jar" %_entry% %*
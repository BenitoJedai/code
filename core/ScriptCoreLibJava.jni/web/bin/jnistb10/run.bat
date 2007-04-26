set _path=bin
set _entry=jni.Main

set PATH=%PATH%;%_path%

rem cd
rem echo ~%_path%
echo + jar : %_path%\package.jar %_entry% 

xjava -cp "%_path%\package.jar" %_entry% %*
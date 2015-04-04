
:: 20141204
:: after ten years, we still need a bat file for a quick and dirty iteration
:: how do we write one?
:: 20150404
:: https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402

@echo off

set TargetFileName=%2
set ConfigurationName=%3



pushd ..\bin\%ConfigurationName%

call c:\util\jsc\bin\jsc.exe %TargetFileName% -c

::copy "web\%TargetFileName%.*" ".\staging\jni\"
copy "web\*.mk" ".\staging\jni\"
copy "web\*.cpp" ".\staging\jni\"
copy "web\*.c" ".\staging\jni\"
copy "web\*.h" ".\staging\jni\"


pushd staging

echo update project
:: "X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\bin\Debug\staging\project.properties"
:: Error: Target id 'android-10' is not valid. Use 'android.bat list targets' to get the target ids.

call "x:\util\android-sdk-windows\tools\android.bat" list targets
call "x:\util\android-sdk-windows\tools\android.bat" update project -p . -s --target "android-21"

::pause

echo ndk-build
call X:\opensource\android-ndk-r10c\ndk-build.cmd
pause

popd

call c:\util\jsc\bin\jsc.exe %TargetFileName% -java
::pause
XCOPY web\java\* staging\src /s /i /Y  

pushd staging

::pause

echo sdk-build

set JAVA_HOME=C:\Program Files (x86)\Java\jdk1.7.0_45
call "x:\util\apache-ant-1.9.2\bin\ant.bat" debug

pushd bin

start /WAIT cmd /C android-install.bat
 
popd
popd


popd


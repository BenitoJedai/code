@echo off
setlocal

call find.java javac.exe
set TargetPath=%ReturnValue%

if '%TargetPath%' == '' (
    echo java not found
    goto :eof
)


pushd ..\bin\%ConfigurationName%\web

:: import settings
call setup.settings.cmd

echo + compile java [%ProjectName%:%CompilandNamespace0%\%CompilandType%]

:: add additional libraries here
set TargetSourceFiles=java

::%TargetPath% -source 1.4 -target 1.4 -Xlint:all,-serial,-unchecked -cp %TargetSourceFiles% -d release  java\%CompilandNamespace0%\%CompilandType%.java
::%TargetPath% -source 1.4 -target 1.4 -classpath %TargetSourceFiles% -d release java\%CompilandNamespace0%\%CompilandType%.java
%TargetPath% -classpath "%TargetSourceFiles%;C:\util\robocode\libs\robocode.jar" -d release java\%CompilandNamespace0%\%CompilandType%.java java\%CompilandNamespace0%\Kenny1.java java\%CompilandNamespace0%\Kenny3.java

endlocal
popd

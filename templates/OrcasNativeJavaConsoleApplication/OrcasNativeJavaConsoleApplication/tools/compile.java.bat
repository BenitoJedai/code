@echo off
setlocal

call find.java javac.exe
set TargetPath=%ReturnValue%

if '%TargetPath%' == '' (
    echo java not found
    goto :eof
)


pushd ..\bin\debug\web

:: import settings
call setup.settings.cmd

echo + compile java [%ProjectName%:%CompilandNamespace0%\%CompilandType%]

:: add additional libraries here
set TargetSourceFiles=java

::%TargetPath% -source 1.4 -target 1.4 -Xlint:all,-serial,-unchecked -cp %TargetSourceFiles% -d release  java\%CompilandNamespace0%\%CompilandType%.java
%TargetPath% -source 1.4 -target 1.4 -classpath %TargetSourceFiles% -d release java\%CompilandNamespace0%\%CompilandType%.java

endlocal
popd

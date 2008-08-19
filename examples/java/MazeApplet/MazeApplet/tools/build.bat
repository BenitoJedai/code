@echo off

pushd ..\bin\%3

call C:\util\jsc\bin\jsc.exe %2 -java


popd

call compile.java
call create.jar

@echo off
pushd ..
C:\Windows\Microsoft.NET\Framework\v3.5\msbuild /t:_CopyWebApplication /property:OutDir=%CD%\bin\publish\ /property:WebProjectOutputDir=%CD%\bin\publish\ WebApplication1.csproj

C:\Windows\Microsoft.NET\Framework\v2.0.50727\aspnet_compiler.exe -v / -p "%CD%\bin\publish" -f  "%CD%\bin\staging"
popd
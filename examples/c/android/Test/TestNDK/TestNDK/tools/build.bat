:: 20141204
:: after ten years, we still need a bat file for a quick and dirty iteration
:: how do we write one?

pushd ..\bin\%ConfigurationName%

call c:\util\jsc\bin\jsc.exe %TargetFileName% -c


popd
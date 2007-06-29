@echo off
setlocal

pushd "../bin/debug/web"

echo + publishing...

set Xscp=C:\Program Files\WinSCP3\winscp3.com
set Xhost=sftp://zproxy@jsc.sourceforge.net/
set Xpath=jsc_web/examples/web/DockMaster/

::call "%Xscp%" %Xhost% /command "cd %XPath%" "lls" "ls" "option exclude ""*.dll""" "option include ""*.packed.js""" "synchronize remote" "exit"

call "%Xscp%" %Xhost% /command "cd %XPath%" "option exclude ""*.dll""" "option include ""*.packed.js; *.gif; *.jpg; *.png""" "synchronize remote" "exit"

echo.
echo done

popd
endlocal
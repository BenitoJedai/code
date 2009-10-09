@echo off

if %3==Debug (
  echo Debug mode will not perform post build!
  goto :eof
)


@call compile.native %2 %3

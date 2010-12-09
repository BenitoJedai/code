﻿echo error 0
call command0.bat

echo error 1
call command1.bat

echo jsc.svn update
call TortoiseProc.exe /command:update /path:"w:\jsc.svn\" /closeonend:1

echo rebuild
cd w:\jsc.svn\tools\
call rebuild.bat

echo jsc.internal.svn update
call TortoiseProc.exe /command:update /path:"w:\jsc.internal.svn\" /closeonend:1

echo rebuild jsc.internal
cd w:\jsc.internal.svn\tools\
call rebuild.bat

echo rebuild cache
cd w:\jsc.svn\tools\
call rebuild.cache.bat

echo rebuild installer
cd w:\jsc.svn\tools\
call rebuild.installer.bat

echo rebuild website
cd w:\jsc.svn\tools\
call rebuild.release.bat

echo verify website
cd W:\jsc.svn\examples\java\PromotionWebApplication\PromotionWebApplication1\bin\Release\staging\PromotionWebApplication1.UltraWebService\staging.java\web
call start .

echo upload website
cd W:\jsc.svn\examples\java\PromotionWebApplication\PromotionWebApplication1\bin\Release\staging\PromotionWebApplication1.UltraWebService\staging.java\web
call upload.bat

echo shutdown in 3 min
call shutdown -s -t 180 -c "jsc.builder completed"

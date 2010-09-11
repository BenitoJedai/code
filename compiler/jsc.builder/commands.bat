echo launch builder (or log into aws and launch your instance manually)
rem aws

echo svn update
call TortoiseProc.exe /command:update /path:"w:\jsc.svn\" /closeonend:0

echo rebuild
cd w:\jsc.svn\tools\
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

echo upload
cd W:\jsc.svn\examples\java\PromotionWebApplication\PromotionWebApplication1\bin\Release\staging\PromotionWebApplication1.UltraWebService\staging.java\web
call upload.bat

echo exit windows
rundll32 user.exe,exitwindows 
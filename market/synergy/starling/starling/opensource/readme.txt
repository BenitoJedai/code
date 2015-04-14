link source from https://github.com/PrimaryFeather/Starling-Framework.git
https://github.com/Gamua/Starling-Framework.git

  <ItemGroup>
    <Content Include="X:\opensource\github\Starling-Framework\starling\src\**\*.*">
      <Link>opensource\github.com\Starling-Framework\src\%(RecursiveDir)%(FileName)%(Extension)</Link>
    </Content>
  </ItemGroup>

  if $(ConfigurationName)==Debug (
start /WAIT cmd /C c:\util\jsc\bin\jsc.meta.exe ConfigurationCreateNuGetPackage /AssemblyMerge:$(TargetPath) /AssemblyMerge:$(TargetName).AssetsLibrary.dll /OutputDirectory:C:\util\jsc\nuget
)
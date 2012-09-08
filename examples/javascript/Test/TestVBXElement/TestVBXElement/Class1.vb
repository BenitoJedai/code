Imports System.Reflection
Imports ScriptCoreLib

<Assembly: Script> 
<Assembly: ScriptTypeFilter(ScriptCoreLib.ScriptType.Java)> 

<Script>
Public Module VisualStudioTemplates

    ' http://www.simple-talk.com/dotnet/.net-tools/extending-msbuild/

    Public VisualCSharpProjectReferences As XElement = _
    <ItemGroup>
        <Reference Include="jsc.meta">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>C:\util\jsc\bin\jsc.meta.exe</HintPath>
        </Reference>
    </ItemGroup>

    Dim x As ScriptCoreLibJava.IAssemblyReferenceToken
    Dim y As ScriptCoreLib.Shared.IAssemblyReferenceToken

End Module

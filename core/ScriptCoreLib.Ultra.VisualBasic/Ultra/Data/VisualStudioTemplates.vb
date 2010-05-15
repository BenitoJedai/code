Public Module VisualStudioTemplates

    ' http://www.simple-talk.com/dotnet/.net-tools/extending-msbuild/

    Public VisualCSharpProjectReferences As XElement = _
    <ItemGroup>
        <Reference Include="jsc.meta, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>C:\util\jsc\bin\jsc.meta.exe</HintPath>
        </Reference>
        <Reference Include="PresentationCore">
            <RequiredTargetFramework>3.0</RequiredTargetFramework>
        </Reference>
        <Reference Include="PresentationFramework">
            <RequiredTargetFramework>3.0</RequiredTargetFramework>
        </Reference>
        <Reference Include="ScriptCoreLib, Version=3.0.2665.39893, Culture=neutral, processorArchitecture=MSIL">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>C:\util\jsc\bin\ScriptCoreLib.dll</HintPath>
        </Reference>
        <Reference Include="ScriptCoreLib.Avalon, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>C:\util\jsc\bin\ScriptCoreLib.Avalon.dll</HintPath>
        </Reference>
        <Reference Include="ScriptCoreLib.Query, Version=1.0.2666.38864, Culture=neutral, processorArchitecture=MSIL">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>C:\util\jsc\bin\ScriptCoreLib.Query.dll</HintPath>
        </Reference>
        <Reference Include="ScriptCoreLib.Windows.Forms, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>c:\util\jsc\bin\ScriptCoreLib.Windows.Forms.dll</HintPath>
        </Reference>
        <Reference Include="ScriptCoreLib.XLinq, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>C:\util\jsc\bin\ScriptCoreLib.XLinq.dll</HintPath>
        </Reference>
        <Reference Include="ScriptCoreLib.Ultra, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>c:\util\jsc\bin\ScriptCoreLib.Ultra.dll</HintPath>
        </Reference>
        <Reference Include="ScriptCoreLib.Ultra.Components, Version=0.0.0.0, Culture=neutral, processorArchitecture=MSIL">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>C:\util\jsc\bin\ScriptCoreLib.Ultra.Components.dll</HintPath>
        </Reference>
        <Reference Include="ScriptCoreLib.Ultra.Library, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>C:\util\jsc\bin\ScriptCoreLib.Ultra.Library.dll</HintPath>
        </Reference>
        <Reference Include="ScriptCoreLib.Ultra.VisualBasic, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>C:\util\jsc\bin\ScriptCoreLib.Ultra.VisualBasic.dll</HintPath>
        </Reference>
        <Reference Include="ScriptCoreLibA, Version=3.0.0.0, Culture=neutral, processorArchitecture=MSIL">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>C:\util\jsc\bin\ScriptCoreLibA.dll</HintPath>
        </Reference>
        <Reference Include="ScriptCoreLibJava, Version=1.2006.222.0, Culture=neutral, processorArchitecture=MSIL">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>c:\util\jsc\bin\ScriptCoreLibJava.dll</HintPath>
        </Reference>
        <Reference Include="System"/>
        <Reference Include="System.Core">
            <RequiredTargetFramework>3.5</RequiredTargetFramework>
        </Reference>
        <Reference Include="System.Data"/>
        <Reference Include="System.Web"/>
        <Reference Include="System.Xml"/>
        <Reference Include="System.Xml.Linq">
            <RequiredTargetFramework>3.5</RequiredTargetFramework>
        </Reference>
        <Reference Include="WindowsBase">
            <RequiredTargetFramework>3.0</RequiredTargetFramework>
        </Reference>
        <Reference Include="System.Drawing"/>
        <Reference Include="System.Windows.Forms"/>
    </ItemGroup>

    Public VisualCSharpProject As XElement = _
    <Project ToolsVersion="3.5" DefaultTargets="Build">
        <PropertyGroup>
            <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
            <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
            <ProductVersion>9.0.30729</ProductVersion>
            <SchemaVersion>2.0</SchemaVersion>
            <ProjectGuid>{0478BF11-A133-4914-BC29-B7F72E3B9535}</ProjectGuid>
            <OutputType>Exe</OutputType>
            <AppDesignerFolder>Properties</AppDesignerFolder>
            <RootNamespace>XElementEverywhere</RootNamespace>
            <AssemblyName>XElementEverywhere</AssemblyName>
            <TargetFrameworkVersion>v3.5</TargetFrameworkVersion>
            <FileAlignment>512</FileAlignment>
            <StartupObject>
            </StartupObject>
        </PropertyGroup>
        <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
            <DebugSymbols>true</DebugSymbols>
            <DebugType>full</DebugType>
            <Optimize>false</Optimize>
            <OutputPath>bin\Debug\</OutputPath>
            <DefineConstants>DEBUG;TRACE</DefineConstants>
            <ErrorReport>prompt</ErrorReport>
            <WarningLevel>4</WarningLevel>
        </PropertyGroup>
        <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
            <DebugType>pdbonly</DebugType>
            <Optimize>false</Optimize>
            <OutputPath>bin\Release\</OutputPath>
            <DefineConstants>TRACE</DefineConstants>
            <ErrorReport>prompt</ErrorReport>
            <WarningLevel>4</WarningLevel>
        </PropertyGroup>
        <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Assets|AnyCPU' ">
            <DebugSymbols>true</DebugSymbols>
            <OutputPath>bin\Assets\</OutputPath>
            <DefineConstants>DEBUG;TRACE</DefineConstants>
            <DebugType>full</DebugType>
            <PlatformTarget>AnyCPU</PlatformTarget>
            <ErrorReport>prompt</ErrorReport>
        </PropertyGroup>
        <ItemGroup>
            <Reference Include="System"/>
        </ItemGroup>
        <ItemGroup>
            <Compile Include="Program.cs"/>
        </ItemGroup>

        <Import Project="$(MSBuildBinPath)\Microsoft.CSharp.targets"/>

        <PropertyGroup>
            <PostBuildEvent>if $(ConfigurationName)==Debug (
rem c:\util\jsc\bin\jsc.meta.exe RewriteToMVSProjectTemplate /ProjectFileName:"$(ProjectPath)" /Assembly:"$(TargetPath)" /AttachDebugger:false /DefaultToOrcas:true
) 
if $(ConfigurationName)==Release (
c:\util\jsc\bin\jsc.meta.exe RewriteToJavaScriptDocument /assembly:"$(TargetFileName)"  /flashplayer:"C:\util\flex\runtimes\player\win\FlashPlayer.exe" /mxmlc:"C:\util\flex\bin\mxmlc.exe" /javapath:"c:\Program Files\Java\jdk1.6.0_14\bin" /AttachDebugger:false /DisableWebServiceJava:true
)</PostBuildEvent>
            <PreBuildEvent>C:\util\jsc\bin\jsc.meta.exe ReferenceJavaScriptDocument /ProjectFileName:"$(ProjectPath)" /Configuration:"$(ConfigurationName)" /AttachDebugger:false /SelectAll:true</PreBuildEvent>
        </PropertyGroup>
    </Project>
End Module

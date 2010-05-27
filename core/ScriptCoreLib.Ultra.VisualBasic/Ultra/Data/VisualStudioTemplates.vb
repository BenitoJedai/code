Public Module VisualStudioTemplates

    ' http://www.simple-talk.com/dotnet/.net-tools/extending-msbuild/

    Public VisualCSharpProjectReferences As XElement = _
    <ItemGroup>
        <Reference Include="jsc.meta, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL">
            <SpecificVersion>False</SpecificVersion>
            <HintPath>C:\util\jsc\bin\jsc.meta.exe</HintPath>
        </Reference>
        <Reference Include="FSharp.Core">
            <RequiredTargetFramework>4.0</RequiredTargetFramework>
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
            <PostBuildEvent>
if $(ConfigurationName)==Release (
c:\util\jsc\bin\jsc.meta.exe RewriteToJavaScriptDocument /assembly:"$(TargetFileName)" /AttachDebugger:false /DisableWebServiceJava:true
)</PostBuildEvent>
            <PreBuildEvent>C:\util\jsc\bin\jsc.meta.exe ReferenceJavaScriptDocument /ProjectFileName:"$(ProjectPath)" /Configuration:"$(ConfigurationName)" /AttachDebugger:false /SelectAll:true</PreBuildEvent>
        </PropertyGroup>
    </Project>

    Public VisualBasicProject As XElement = _
<Project ToolsVersion="3.5" DefaultTargets="Build">
    <PropertyGroup>
        <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
        <Platform Condition=" '$(Platform)' == '' ">x86</Platform>
        <ProductVersion>
        </ProductVersion>
        <SchemaVersion>
        </SchemaVersion>
        <ProjectGuid>{14311F94-F366-406F-91A0-F76F9CE6B7D6}</ProjectGuid>
        <OutputType>Exe</OutputType>
        <StartupObject>ConsoleApplication1.Module1</StartupObject>
        <RootNamespace>ConsoleApplication1</RootNamespace>
        <AssemblyName>ConsoleApplication1</AssemblyName>
        <FileAlignment>512</FileAlignment>
        <MyType>Console</MyType>
        <TargetFrameworkVersion>v3.5</TargetFrameworkVersion>
    </PropertyGroup>
    <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|x86' ">
        <PlatformTarget>x86</PlatformTarget>
        <DebugSymbols>true</DebugSymbols>
        <DebugType>full</DebugType>
        <DefineDebug>true</DefineDebug>
        <DefineTrace>true</DefineTrace>
        <OutputPath>bin\Debug\</OutputPath>
        <NoWarn>42016,41999,42017,42018,42019,42032,42036,42020,42021,42022</NoWarn>
    </PropertyGroup>
    <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|x86' ">
        <PlatformTarget>x86</PlatformTarget>
        <DebugType>pdbonly</DebugType>
        <DefineDebug>false</DefineDebug>
        <DefineTrace>true</DefineTrace>
        <Optimize>true</Optimize>
        <OutputPath>bin\Release\</OutputPath>
        <NoWarn>42016,41999,42017,42018,42019,42032,42036,42020,42021,42022</NoWarn>
    </PropertyGroup>
    <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Assets|x86' ">
        <PlatformTarget>x86</PlatformTarget>
        <DebugSymbols>true</DebugSymbols>
        <DebugType>full</DebugType>
        <DefineDebug>true</DefineDebug>
        <DefineTrace>true</DefineTrace>
        <OutputPath>bin\Debug\</OutputPath>
        <NoWarn>42016,41999,42017,42018,42019,42032,42036,42020,42021,42022</NoWarn>
    </PropertyGroup>
    <PropertyGroup>
        <OptionExplicit>On</OptionExplicit>
    </PropertyGroup>
    <PropertyGroup>
        <OptionCompare>Binary</OptionCompare>
    </PropertyGroup>
    <PropertyGroup>
        <OptionStrict>Off</OptionStrict>
    </PropertyGroup>
    <PropertyGroup>
        <OptionInfer>On</OptionInfer>
    </PropertyGroup>
    <ItemGroup>
        <Reference Include="System"/>
    </ItemGroup>
    <ItemGroup>
        <Compile Include="Module1.vb"/>
    </ItemGroup>

    <Import Project="$(MSBuildToolsPath)\Microsoft.VisualBasic.targets"/>

    <PropertyGroup>
        <PostBuildEvent>
if $(ConfigurationName)==Release (
c:\util\jsc\bin\jsc.meta.exe RewriteToJavaScriptDocument /assembly:"$(TargetFileName)" /AttachDebugger:false /DisableWebServiceJava:true
)</PostBuildEvent>
        <PreBuildEvent>C:\util\jsc\bin\jsc.meta.exe ReferenceJavaScriptDocument /ProjectFileName:"$(ProjectPath)" /Configuration:"$(ConfigurationName)" /AttachDebugger:false /SelectAll:true</PreBuildEvent>
    </PropertyGroup>
</Project>


    Public VisualFSharpProject As XElement = _
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
              <TargetFrameworkVersion>v4.0</TargetFrameworkVersion>
              <FileAlignment>512</FileAlignment>
              <StartupObject>
              </StartupObject>
          </PropertyGroup>
          <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
              <DebugSymbols>true</DebugSymbols>
              <DebugType>full</DebugType>
              <Optimize>false</Optimize>
              <Tailcalls>false</Tailcalls>
              <OutputPath>bin\Debug\</OutputPath>
              <DefineConstants>DEBUG;TRACE</DefineConstants>
              <ErrorReport>prompt</ErrorReport>
              <WarningLevel>4</WarningLevel>
          </PropertyGroup>
          <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
              <DebugType>pdbonly</DebugType>
              <Optimize>false</Optimize>
              <Tailcalls>false</Tailcalls>
              <OutputPath>bin\Release\</OutputPath>
              <DefineConstants>TRACE</DefineConstants>
              <ErrorReport>prompt</ErrorReport>
              <WarningLevel>4</WarningLevel>
          </PropertyGroup>
          <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Assets|AnyCPU' ">
              <DebugSymbols>true</DebugSymbols>
              <Optimize>false</Optimize>
              <Tailcalls>false</Tailcalls>
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

          <Import Project="$(MSBuildExtensionsPath32)\FSharp\1.0\Microsoft.FSharp.Targets" Condition="!Exists('$(MSBuildBinPath)\Microsoft.Build.Tasks.v4.0.dll')"/>
          <Import Project="$(MSBuildExtensionsPath32)\..\Microsoft F#\v4.0\Microsoft.FSharp.Targets" Condition=" Exists('$(MSBuildBinPath)\Microsoft.Build.Tasks.v4.0.dll')"/>

          <PropertyGroup>
              <PostBuildEvent>
if $(ConfigurationName)==Release (
c:\util\jsc\bin\jsc.meta.exe RewriteToJavaScriptDocument /assembly:"$(TargetFileName)" /AttachDebugger:false /DisableWebServiceJava:true
)</PostBuildEvent>
              <PreBuildEvent>C:\util\jsc\bin\jsc.meta.exe ReferenceJavaScriptDocument /ProjectFileName:"$(ProjectPath)" /Configuration:"$(ConfigurationName)" /AttachDebugger:false /SelectAll:true</PreBuildEvent>
          </PropertyGroup>
      </Project>
End Module

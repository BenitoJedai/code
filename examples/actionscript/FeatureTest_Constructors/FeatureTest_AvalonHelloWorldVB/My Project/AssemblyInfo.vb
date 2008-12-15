Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("FeatureTest_AvalonHelloWorldVB")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("xp")> 
<Assembly: AssemblyProduct("FeatureTest_AvalonHelloWorldVB")> 
<Assembly: AssemblyCopyright("Copyright © xp 2008")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("5b011f96-84d3-449b-a308-bf5f4b30cd42")> 

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' <Assembly: AssemblyVersion("1.0.*")> 

<Assembly: AssemblyVersion("1.0.0.0")> 
<Assembly: AssemblyFileVersion("1.0.0.0")> 

<Assembly: Script()> 
<Assembly: ScriptTypeFilter(ScriptType.ActionScript, GetType(ActionScript.ApplicationFlash))> 
<Assembly: ScriptTypeFilter(ScriptType.ActionScript, GetType([Shared].ApplicationCanvas))> 

<Assembly: ScriptTypeFilter(ScriptType.JavaScript, GetType(JavaScript.ApplicationDocument))> 
<Assembly: ScriptTypeFilter(ScriptType.JavaScript, GetType([Shared].ApplicationCanvas))> 

// For more information please visit us at:
// http://www.jsc-solutions.net/


using System.Reflection;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle(@"Heat Zeeker Flash")]
//[assembly: AssemblyDescription(@"Write JavaScript, Adobe Flash and Oracle Java Applets within a single .NET project. http://jsc-solutions.net")]
//[assembly: AssemblyCompany(@"jsc-solutions.net")]
//[assembly: AssemblyProduct(@"FlashApplication")]
//[assembly: AssemblyCopyright(@"Copyright © jsc-solutions.net 2013")]
[assembly: Obfuscation(Feature = @"merge")]

//0b08:02:01 RewriteToAssembly error: System.ArgumentException: The unmanaged Version information is too large to persist.
//   at System.Reflection.Emit.AssemblyBuilder.CreateVersionInfoResource(String filename, String title, String iconFilename, String description, String copyright, String trademark, String company, String product, String productVersion, String fileVersion, Int32 lcid, Boolean isDll, StringHandleOnStack retFileName)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestILSpyRewrite
{
    class Program
    {
        static void Main(string[] args)
        {
            //            ---------------------------
            //Sorry, we crashed
            //---------------------------
            //System.Windows.Markup.XamlParseException: Could not load file or assembly 'ILSpy, PublicKeyToken=d4bfe873e7598c49' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040) ---> System.IO.FileLoadException: Could not load file or assembly 'ILSpy, PublicKeyToken=d4bfe873e7598c49' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)

            //   at System.Reflection.RuntimeAssembly._nLoad(AssemblyName fileName, String codeBase, Evidence assemblySecurity, RuntimeAssembly locationHint, StackCrawlMark& stackMark, IntPtr pPrivHostBinder, Boolean throwOnFileNotFound, Boolean forIntrospection, Boolean suppressSecurityChecks)

            //   at System.Reflection.RuntimeAssembly.nLoad(AssemblyName fileName, String codeBase, Evidence assemblySecurity, RuntimeAssembly locationHint, StackCrawlMark& stackMark, IntPtr pPrivHostBinder, Boolean throwOnFileNotFound, Boolean forIntrospection, Boolean suppressSecurityChecks)

            //   at System.Reflection.RuntimeAssembly.InternalLoadAssemblyName(AssemblyName assemblyRef, Evidence assemblySecurity, RuntimeAssembly reqAssembly, StackCrawlMark& stackMark, IntPtr pPrivHostBinder, Boolean throwOnFileNotFound, Boolean forIntrospection, Boolean suppressSecurityChecks)

            //   at System.Reflection.Assembly.Load(AssemblyName assemblyRef)

            //   at System.Windows.Baml2006.Baml2006SchemaContext.ResolveAssembly(BamlAssembly bamlAssembly)

            //   at System.Windows.Baml2006.Baml2006SchemaContext.ResolveBamlTypeToType(BamlType bamlType)

            //   at System.Windows.Baml2006.Baml2006SchemaContext.ResolveBamlType(BamlType bamlType, Int16 typeId)

            //   at System.Windows.Baml2006.Baml2006SchemaContext.GetXamlType(Int16 typeId)

            //   at System.Windows.Baml2006.Baml2006Reader.Process_ElementStart()

            //   at System.Windows.Baml2006.Baml2006Reader.Process_OneBamlRecord()

            //   at System.Windows.Baml2006.Baml2006Reader.Process_BamlRecords()

            //   at System.Windows.Baml2006.Baml2006Reader.Read()

            //   at System.Windows.Markup.WpfXamlLoader.TransformNodes(XamlReader xamlReader, XamlObjectWriter xamlWriter, Boolean onlyLoadOneNode, Boolean skipJournaledProperties, Boolean shouldPassLineNumberInfo, IXamlLineInfo xamlLineInfo, IXamlLineInfoConsumer xamlLineInfoConsumer, XamlContextStack`1 stack, IStyleConnector styleConnector)

            //   at System.Windows.Markup.WpfXamlLoader.Load(XamlReader xamlReader, IXamlObjectWriterFactory writerFactory, Boolean skipJournaledProperties, Object rootObject, XamlObjectWriterSettings settings, Uri baseUri)

            //   --- End of inner exception stack trace ---

            //   at System.Windows.Markup.WpfXamlLoader.Load(XamlReader xamlReader, IXamlObjectWriterFactory writerFactory, Boolean skipJournaledProperties, Object rootObject, XamlObjectWriterSettings settings, Uri baseUri)

            //   at System.Windows.Markup.WpfXamlLoader.LoadBaml(XamlReader xamlReader, Boolean skipJournaledProperties, Object rootObject, XamlAccessLevel accessLevel, Uri baseUri)

            //   at System.Windows.Markup.XamlReader.LoadBaml(Stream stream, ParserContext parserContext, Object parent, Boolean closeStream)

            //   at System.Windows.Application.LoadBamlStreamWithSyncInfo(Stream stream, ParserContext pc)

            //   at System.Windows.Application.LoadComponent(Uri resourceLocator, Boolean bSkipJournaledProperties)

            //   at System.Windows.Application.DoStartup()

            //   at System.Windows.Application.<.ctor>b__1(Object unused)

            //   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)

            //   at MS.Internal.Threading.ExceptionFilterHelper.TryCatchWhen(Object source, Delegate method, Object args, Int32 numArgs, Delegate catchHandler)
            //---------------------------
            //OK   
            //---------------------------

            //            Error	2	The type 'System.Windows.Markup.IQueryAmbient' is defined in an assembly that is not referenced. You must add a reference to assembly 'System.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.	X:\jsc.svn\examples\rewrite\TestILSpyRewrite\TestILSpyRewrite\Program.cs	70	13	TestILSpyRewrite
            //Error	3	The type 'System.Windows.Application' is defined in an assembly that is not referenced. You must add a reference to assembly 'PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'.	X:\jsc.svn\examples\rewrite\TestILSpyRewrite\TestILSpyRewrite\Program.cs	70	13	TestILSpyRewrite


            //Additional information: Could not load file or assembly 'ILSpy, PublicKeyToken=d4bfe873e7598c49' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            // Additional information: Could not load file or assembly 'ILSpy, PublicKeyToken=d4bfe873e7598c49' 
            // or one of its dependencies. A strongly-named assembly is required. (Exception from HRESULT: 0x80131044)

            // http://blogs.msdn.com/b/securitytools/archive/2009/12/30/how-to-turn-off-strong-name-validation.aspx
            // http://blogs.msdn.com/b/mapo/archive/2007/05/24/skipping-strong-name-validation.aspx
            // http://blogs.msdn.com/b/shawnfa/archive/2008/05/14/strong-name-bypass.aspx

            //            C:\Windows\system32>sn -Vr X:\opensource\github\AssemblyVisualizer\build\x\ILSpy.exe
            // sn -Vr "X:\jsc.svn\examples\rewrite\TestILSpyRewrite\TestILSpyRewrite\bin\Debug\ILSpy.exe"
            //Microsoft (R) .NET Framework Strong Name Utility  Version 4.0.30319.1
            //Copyright (c) Microsoft Corporation.  All rights reserved.

            //Verification entry added for assembly 'ILSpy,766087C6A5DA89F0'

//            { Name = ILSpy, Version=2.1.0.1603, Culture=neutral, PublicKeyToken=d4bfe873e7598c49 }
//{ILSpy, Version=5.0.0.6, Culture=neutral, PublicKeyToken=766087c6a5da89f0}

            //System.Windows.Baml2006.Baml2006SchemaContext
            ICSharpCode.ILSpy.App.Main();
        }

        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Console.WriteLine(new { args.Name });

            if (args.Name.StartsWith("ILSpy"))
                return typeof(ICSharpCode.ILSpy.App).Assembly;

            return null;
        }
    }
}

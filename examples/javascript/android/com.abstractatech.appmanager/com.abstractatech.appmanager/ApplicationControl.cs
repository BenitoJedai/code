using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.abstractatech.appmanager
{
    public partial class ApplicationControl : UserControl
    {
        // http://stackoverflow.com/questions/16477960/collection-was-modified-enumeration-operation-may-not-execute-in-vs-winforms-de
        // http://connect.microsoft.com/VisualStudio/feedback/details/781193/form-designer-error-collection-was-modified-enumeration-operation-may-not-execute

        //        ---------------------------
        //Microsoft Visual Studio Express 2012 for Windows Desktop
        //---------------------------
        //Failed to create component 'ApplicationWebService'.  The error message follows:

        // 'Collection was modified; enumeration operation may not execute.'
        //---------------------------
        //OK   
        //---------------------------

        //at System.ThrowHelper.ThrowInvalidOperationException(ExceptionResource resource)
        //at System.Collections.Generic.List`1.Enumerator.MoveNextRare()
        //at System.Collections.Generic.List`1.Enumerator.MoveNext()
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.AssemblySpecFound(List`1 assemblies, String assemblyFullName)
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.AddDependencies(Assembly a, String fileName)
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.AssemblyEntry.get_Assembly()
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.SearchByShortName(String partialName, String fullName, AssemblyEntry[] entries, Assembly& assembly)
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.SearchNormalEntries(AssemblyName assemblyName, String typeName, Boolean ignoreTypeCase, Assembly& assembly, Boolean fastSearch)
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.SearchEntries(AssemblyName assemblyName, String typeName, Boolean ignoreCase, Assembly& assembly, ReferenceType refType)
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.SearchEntries(AssemblyName assemblyName, String typeName, Boolean ignoreTypeCase, Assembly& assembly)
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.System.ComponentModel.Design.ITypeResolutionService.GetAssembly(AssemblyName name, Boolean throwOnError)
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.System.ComponentModel.Design.ITypeResolutionService.GetAssembly(AssemblyName name)
        //at Microsoft.VisualStudio.Design.VSDynamicTypeService.OnAssemblyResolve(Object sender, ResolveEventArgs e)
        //at System.AppDomain.OnAssemblyResolveEvent(RuntimeAssembly assembly, String assemblyFullName)
        //at System.Reflection.RuntimeAssembly.GetType(RuntimeAssembly assembly, String name, Boolean throwOnError, Boolean ignoreCase, ObjectHandleOnStack type)
        //at System.Reflection.RuntimeAssembly.GetType(String name, Boolean throwOnError, Boolean ignoreCase)
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.AssemblyEntry.Search(String fullName, String typeName, Boolean ignoreTypeCase, Boolean allowPrivate, Assembly& assembly, String description)
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.AssemblyEntry.Search(String fullName, String typeName, Boolean ignoreTypeCase, Assembly& assembly, String description)
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.SearchProjectEntries(AssemblyName assemblyName, String typeName, Boolean ignoreTypeCase, Assembly& assembly)
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.SearchEntries(AssemblyName assemblyName, String typeName, Boolean ignoreCase, Assembly& assembly, ReferenceType refType)
        //at Microsoft.VisualStudio.Design.VSTypeResolutionService.GetType(String typeName, Boolean throwOnError, Boolean ignoreCase, ReferenceType refType)
        //at Microsoft.VisualStudio.Design.Serialization.CodeDom.AggregateTypeResolutionService.GetType(String name, Boolean throwOnError, Boolean ignoreCase)
        //at Microsoft.VisualStudio.Design.Serialization.CodeDom.AggregateTypeResolutionService.GetType(String name)
        //at System.ComponentModel.Design.Serialization.DesignerSerializationManager.GetRuntimeType(String typeName)
        //at System.ComponentModel.Design.Serialization.DesignerSerializationManager.GetType(String typeName)
        //at System.ComponentModel.Design.Serialization.DesignerSerializationManager.System.ComponentModel.Design.Serialization.IDesignerSerializationManager.GetType(String typeName)
        //at System.ComponentModel.Design.Serialization.CodeDomSerializerBase.GetType(IDesignerSerializationManager manager, String name, Dictionary`2 names)
        //at System.ComponentModel.Design.Serialization.CodeDomSerializerBase.FillStatementTable(IDesignerSerializationManager manager, IDictionary table, Dictionary`2 names, CodeStatementCollection statements, String className)
        //at System.ComponentModel.Design.Serialization.TypeCodeDomSerializer.Deserialize(IDesignerSerializationManager manager, CodeTypeDeclaration declaration)
        //at System.ComponentModel.Design.Serialization.CodeDomDesignerLoader.PerformLoad(IDesignerSerializationManager manager)
        //at Microsoft.VisualStudio.Design.Serialization.CodeDom.VSCodeDomDesignerLoader.PerformLoad(IDesignerSerializationManager serializationManager)
        //at Microsoft.VisualStudio.Design.Serialization.CodeDom.VSCodeDomDesignerLoader.DeferredLoadHandler.Microsoft.VisualStudio.TextManager.Interop.IVsTextBufferDataEvents.OnLoadCompleted(Int32 fReload) 


        public ApplicationControl()
        {
            InitializeComponent();
        }
    }
}

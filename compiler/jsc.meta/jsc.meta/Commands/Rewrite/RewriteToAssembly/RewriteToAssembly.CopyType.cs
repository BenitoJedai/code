using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using System.Reflection;
using System.Reflection.Emit;
using jsc.Languages.IL;
using jsc.Library;
using jsc;
using System.Diagnostics;

namespace jsc.meta.Commands.Rewrite
{
    public partial class RewriteToAssembly
    {
        internal static void CopyType(
                Type SourceType,
                AssemblyBuilder a,
                ModuleBuilder m,

                TypeBuilder OverrideDeclaringType,
                VirtualDictionary<Type, string> TypeRenameCache,
                VirtualDictionary<string, string> NameObfuscation,
                Func<Type, bool> ShouldCopyType,
                Func<string, string> FullNameFixup,
                Action<TypeBuilder> PostTypeRewrite,
                Action<TypeBuilder> PreTypeRewrite,

                Action<TypeBuilder> TypeCreated,

                RewriteToAssembly r,

                ILTranslationContext context,

                Action AtCodeTraceCreateType
            )
        {


            Action<string> Diagnostics =
                e =>
                {
                    Debug.WriteLine(e);

                    Console.WriteLine(e);
                };


            // sanity check
            if (context.TypeCache.BaseDictionary.ContainsKey(SourceType))
                return;

            #region invalidmerge
            if (SourceType.GetCustomAttributes<ObfuscationAttribute>().Any(k => k.Feature == "invalidmerge"))
                throw new InvalidOperationException(SourceType.FullName);
            #endregion


            var t = (TypeBuilder)context.TypeDefinitionCache[SourceType];
            context.TypeCache[SourceType] = t;

            foreach (var item in
                from kk in SourceType.GetCustomAttributes(false)
                let aa = kk.ToCustomAttributeBuilder()(context)
                where aa != null
                select aa
                )
            {
                t.SetCustomAttribute(item);
            }


            // at this point we should signal back? that a nested declaration can continue?
            // does everything still work after this change? :D


            //var BaseType__ = context.TypeCache[SourceType.BaseType];


            foreach (var k in SourceType.GetNestedTypes(
                BindingFlags.Public | BindingFlags.NonPublic
                ))
            {
                // just like try/catch initialized data fields are special....

                if (k.IsInitializedDataFieldType())
                    continue;

                // can we get away with defs only?
                var km = context.TypeDefinitionCache[k];
            }


            #region define fields now! as they are actually what the type is all about!
            foreach (var f in SourceType.GetFields(
                        BindingFlags.DeclaredOnly |
                        BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.Instance | BindingFlags.Static))
            {
                //Diagnostics("Field: " + SourceType.Name + "." + f.Name);

                var ff = context.FieldCache[f];
            }
            #endregion



            if (PreTypeRewrite != null)
                PreTypeRewrite(t);

            CopyTypeMembers(SourceType, NameObfuscation, t, context);



            if (PostTypeRewrite != null)
                PostTypeRewrite(t);


            // including other nested types?
            // if we dont need these types we will waste them
            // if we need them later we are doomed! :)


            // http://msdn.microsoft.com/en-us/library/system.reflection.emit.typebuilder.createtype.aspx


            // maybe we should call create type once we are sure there
            // are no more nested types?
            // actually if the members refer to the nested type
            // they have been declared by now...
            // more testing is needed to clarify this!

            // fixme:D
            // if we rewrite nested interfaces we cannot 
            // implement them?


            var AtTypeCreatedFilter_Trace = new Type[0];

            Action AtTypeCreated =
                delegate
                {
                    var _AtTypeCreatedFilter_Trace = AtTypeCreatedFilter_Trace;

                    #region ensure constraints exist
                    if (SourceType.IsGenericTypeDefinition)
                    {
                        var ga = SourceType.GetGenericArguments();


                        for (int i = 0; i < ga.Length; i++)
                        {
                            // http://msdn.microsoft.com/en-us/library/system.reflection.emit.generictypeparameterbuilder(v=VS.95).aspx


                            foreach (var item in ga[i].GetGenericParameterConstraints())
                            {
                                var Constraint = context.TypeCache[item];
                            }
                        }
                    }
                    #endregion


                    // enums cannot be left partial... we need to implement them
                    var __NestedEnums = SourceType.GetNestedTypes().Where(k => k.IsEnum).ToArray();
                    var __NestedEnumsConverted = context.TypeCache[__NestedEnums];

                    var _Interfaces = Enumerable.ToArray(
                        from k in SourceType.GetInterfaces()
                        where ShouldCopyType(k) || k.IsPublic
                        select context.TypeCache[k]
                    ).ToArray();

                    // explicit interfaces?

                    #region DefineMethodOverride
                    if (SourceType.IsInterface)
                    {
                        // System.ArgumentException: 'this' type cannot be an interface itself.
                    }
                    else
                    {
                        var __explicit =
                            from i in SourceType.GetInterfaces()
                            let map = SourceType.GetInterfaceMap(i)
                            from j in Enumerable.Range(0, map.InterfaceMethods.Length)
                            let TargetMethod = map.TargetMethods[j]

                            // abstract class with interfaces?
                            where TargetMethod != null

                            let InterfaceMethod = map.InterfaceMethods[j]
                            where TargetMethod.DeclaringType == SourceType
                            where !TargetMethod.IsPublic || TargetMethod.Name != InterfaceMethod.Name
                            select new { TargetMethod, InterfaceMethod };


                        foreach (var item in __explicit)
                        {
                            t.DefineMethodOverride(context.MethodCache[item.TargetMethod], context.MethodCache[item.InterfaceMethod]);
                        }
                    }
                    #endregion


                    // Method 'MoveNext' in type '<LoadReferencedAssemblies>d__0' from 
                    // assembly '20100313_jsc.installer, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
                    // does not have an implementation.

                    // do enums have to be created by now?

                    // CLR 4/2?
                    // An attempt was made to load a program with an incorrect format. (Exception from HRESULT: 0x8007000B)

                    // Could not load type 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement' from assembly 
                    // 'ScriptCoreLib, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.

                    // at System.Reflection.Emit.TypeBuilder._TermCreateClass(Int32 handle, Module module)
                    // at System.Reflection.Emit.TypeBuilder.CreateTypeNoLock()
                    // at System.Reflection.Emit.TypeBuilder.CreateType()

                    // if the CreateType call fails, then we may have
                    // missed a dependency?

                    Console.WriteLine("CreateType: " + SourceType.FullName);

                    // GenericArguments[0], 'ScriptCoreLib.JavaScript.DOM.XML.IXMLElement', 
                    // on 'ScriptCoreLib.JavaScript.DOM.IDocument`1[T]' 
                    // violates the constraint of type parameter 'T'.

                    // ? http://rolfkvinge.blogspot.com/2007_04_01_archive.html
                    // https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=324473&wa=wsignin1.0


                    //if (r != null)
                    //    r.WriteDiagnostics("CreateType " + t.Name);

                    // Type 'CircularGenericInterfaces.INode' from assembly 'CircularGenericInterfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' tried to override method 'System.Collections.IEnumerable.GetEnumerator' but does not implement or inherit that method.

                    // http://connect.microsoft.com/VisualStudio/feedback/details/270717/reflection-emit-chokes-on-method-type-parameters#details

                    // GenericArguments[0], 'System.Object', 
                    // on 'IDocument`1[TConstraint2]' violates the constraint of type parameter 'TConstraint2'.

                    // you better have CLR 4!

                    // An attempt was made to load a program with an incorrect format. (Exception from HRESULT: 0x8007000B)
                    
                    // Method 'GetEnumerator' in type 'ScriptCoreLib.JavaScript.DOM.ICommentNode' 
                    // from assembly 'ScriptCoreLib.dll.IDocument, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' 
                    // does not have an implementation.

                    // Access is denied: 'WhyIncorrectFormat.C'.
                    t.CreateType();


                    context.TypeCache.Flags[SourceType] = new object();

                    if (TypeCreated != null)
                        TypeCreated(t);
                };


            var AtTypeCreatedFilter = new List<Type>();

            if (SourceType.IsClass && SourceType.BaseType != typeof(object) && ShouldCopyType(SourceType.BaseType))
            {
                AtTypeCreatedFilter.Add(
                    SourceType.BaseType.TryGetGenericTypeDefinition()
                );
            }

            if (SourceType.IsNested /*&& SourceType.IsClass*/)
            {
                //Diagnostics("Delayed:  " + SourceType.FullName);
                if (SourceType.IsEnum)
                {
                    if (SourceType.DeclaringType.IsNested)
                        AtTypeCreatedFilter.Add(SourceType.DeclaringType);
                    // Enums are special! :)
                }
                else
                {
                    AtTypeCreatedFilter.Add(SourceType.DeclaringType);
                }
            }

            if (SourceType.IsGenericTypeDefinition)
            {
                AtTypeCreatedFilter.AddRange(
                    SourceType.GetGenericArguments().SelectMany(k => k.GetGenericParameterConstraints()).Where(ShouldCopyType)
                );
            }

            AtTypeCreatedFilter.AddRange(
                 from mm in SourceType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                 where mm.IsGenericMethodDefinition
                 from aa in mm.GetGenericArguments()
                 from xx in aa.GetGenericParameterConstraints()
                 where ShouldCopyType(xx)
                 select xx
            );

            // do we need this?
            AtTypeCreatedFilter.AddRange(
                from k in SourceType.GetFields(BindingFlags.NonPublic | BindingFlags.Static)
                where k.Name.StartsWith("__InternalTypeReferenceHint__")
                select k.FieldType
            );


            //Diagnostics("CreateType:  " + SourceType.FullName);
            AtTypeCreatedFilter_Trace = AtTypeCreatedFilter.ToArray();

            if (AtTypeCreatedFilter.Any(k => !(context.TypeCache.Flags.ContainsKey(k))))
            {
                r.TypeCreated +=
                    tt =>
                    {
                        if (AtTypeCreatedFilter == null)
                            return;

                        if (AtTypeCreatedFilter.Any(k => !(context.TypeCache.Flags.ContainsKey(k))))
                            return;

                        //Diagnostics("Delayed CreateType:  " + SourceType.FullName);

                        AtTypeCreatedFilter = null;
                        AtTypeCreated();
                    };
            }
            else
            {
                AtTypeCreated();
            }

        }

        public class CopyTypeDefinition
        {
            public RewriteToAssembly Command;

            public ILTranslationContext context;

            public Type SourceType;
            public ModuleBuilder m;
            //public VirtualDictionary<Type, Type> TypeCache;
            //public TypeBuilder OverrideDeclaringType;
            public VirtualDictionary<string, string> NameObfuscation;
            public Func<Type, bool> ShouldCopyType;
            public Func<string, string> FullNameFixup;
            public Action<string> Diagnostics;




            public TypeBuilder Invoke()
            {


                if (Diagnostics == null)
                    Diagnostics =
                    e =>
                    {
                        if (Debugger.IsAttached)
                        {
                            Debug.WriteLine(e);
                        }
                        else
                        {
                            Console.WriteLine(e);
                        }
                    };

                //Diagnostics("CopyTypeDefinition: " +
                //    SourceType.FullName
                //);

                // we should not reenter here!
                if (context.TypeDefinitionCache.BaseDictionary.ContainsKey(SourceType))
                    throw new InvalidOperationException();

                context.TypeDefinitionCache[SourceType] = null;

                Diagnostics("CopyTypeDefinition: " +
                    SourceType.FullName + " @ " + context.TypeDefinitionCache.BaseDictionary.Keys.Count.ToString("x8")
                );


                var _DeclaringType = (context.OverrideDeclaringType[SourceType] ?? (
                    SourceType.DeclaringType == null ? null :
                        (TypeBuilder)context.TypeDefinitionCache[SourceType.DeclaringType]

                    )
                );

                var TypeName =
                    _DeclaringType != null ?
                    context.TypeRenameCache[SourceType] ?? SourceType.Name :

                    // http://msdn.microsoft.com/en-us/library/system.type.fullname.aspx
                    // a null reference (Nothing in Visual Basic) if the current instance represents a 
                    // generic type parameter, an array type, pointer type, or byref type based on a 
                    // type parameter, or a generic type that is not a generic type definition
                    // but contains unresolved type parameters.
                    context.TypeRenameCache[SourceType] ?? SourceType.FullName;




                //var DeclaringTypeContinuation = default(Action);

                //if (SourceType.IsNested)
                //{
                //    Diagnostics("Should create " + SourceType.DeclaringType.Name + " before " + SourceType.Name);
                //}

                // We beed a separate TypeDeclarationCache for this to work:
                // Type { NestedType, Delegate1(NestedType) }




                var t = default(TypeBuilder);



                var TypeAttributes = SourceType.Attributes;

                TypeAttributes &= ~TypeAttributes.HasSecurity;

                // http://msdn.microsoft.com/en-us/library/system.reflection.typeattributes.aspx
                //                Bad type attributes. Reserved bits set on the type.
                //Public | BeforeFieldInit | HasSecurity



                // we might define as a nested type instead!
                try
                {
                    //Command.WriteDiagnostics("DefineType " + TypeName);
                    t = DefineType(_DeclaringType, TypeName, null, TypeAttributes);
                }
                catch (Exception ex)
                {
                    // TestSolutionBuilderWithViewer.Library.Templates.__InternalElementProxy
                    throw new InvalidOperationException(TypeName, ex);
                }

                context.TypeDefinitionCache[SourceType] = t;

                // interfaces dont have base types!
                var BaseType = SourceType.BaseType == null ? null : context.TypeDefinitionCache[SourceType.BaseType];

                if (BaseType != null)
                    t.SetParent(BaseType);



                #region Interfaces
                var _Interfaces = Enumerable.ToArray(

                    from k in SourceType.GetInterfaces()

                    where ShouldCopyType(k) || k.IsPublic

                    where SourceType.BaseType == null || (SourceType.BaseType != null && !SourceType.BaseType.GetInterfaces().Contains(k))

                    select context.TypeDefinitionCache[k]
                ).ToArray();



                foreach (var item in _Interfaces)
                {
                    // oh really?
                    t.AddInterfaceImplementation(item);
                }
                #endregion

                if (SourceType.IsGenericTypeDefinition)
                {
                    var ga = SourceType.GetGenericArguments();

                    //this.Command.WriteDiagnostics("DefineGenericParameters");
                    var gp = t.DefineGenericParameters(ga.Select(k => k.Name).ToArray());

                    for (int i = 0; i < gp.Length; i++)
                    {
                        context.TypeDefinitionCache[ga[i]] = gp[i];
                        context.TypeCache[ga[i]] = gp[i];


                        // http://msdn.microsoft.com/en-us/library/system.reflection.emit.generictypeparameterbuilder(v=VS.95).aspx


                        foreach (var item in ga[i].GetGenericParameterConstraints())
                        {
                            var Constraint = context.TypeDefinitionCache[item];


                            // any issues if circular referencing?
                            // Unable to change after type has been created.

                            if (item.IsInterface)
                                gp[i].SetInterfaceConstraints(Constraint);
                            else
                                gp[i].SetBaseTypeConstraint(Constraint);
                        }
                    }
                }

                //Diagnostics("TypeDefinitionCache: " + TypeName);

                return t;

            }

            private TypeBuilder DefineType(TypeBuilder _DeclaringType, string TypeName, Type BaseType, TypeAttributes TypeAttributes)
            {
                #region DefineType
                if (_DeclaringType != null)
                {
                    #region nested
                    var _NestedTypeName = NameObfuscation[TypeName];


                    //TypeAttributes = ReplaceTypeAttributes(TypeAttributes, TypeAttributes.NotPublic, TypeAttributes.NestedFamORAssem);
                    TypeAttributes = ReplaceTypeAttributes(TypeAttributes, TypeAttributes.Public, TypeAttributes.NestedPublic);


                    if (SourceType.StructLayoutAttribute != null && SourceType.StructLayoutAttribute.Size > 0)
                    {
                        return _DeclaringType.DefineNestedType(
                            _NestedTypeName,
                            TypeAttributes,
                             BaseType,
                            SourceType.StructLayoutAttribute.Size
                        );
                    }
                    else
                    {

                        return _DeclaringType.DefineNestedType(

                            _NestedTypeName,
                            TypeAttributes,
                            BaseType,
                             new Type[0]
                        );
                    }
                    #endregion

                }
                else
                {
                    var DefineTypeName = FullNameFixup(TypeName);

                    Func<IEnumerable<Type>> GetDuplicates =
                        () => context.TypeDefinitionCache.BaseDictionary.Values.Where(k => k != null).Where(
                            k => (
                                (string.IsNullOrEmpty(k.Namespace) ? "" : k.Namespace + ".") + k.Name
                        ) == DefineTypeName
                    );


                    while (GetDuplicates().Any())
                    {

                        if (SourceType.IsAnonymousType())
                        {
                            DefineTypeName += "´";
                        }
                        else
                        {
                            // have been merging types twice ?

                            // C = A + B
                            // D = C + B

                            var Conflicts = GetDuplicates().Concat(new[] { SourceType }).Select(k => k.Assembly.ToString()).ToArray();


                            throw new InvalidOperationException(
                                "Duplicate type name within an assembly.  "
                                + "Multiple projects shall reference one version of a component.  "
                                + SourceType.ToString()
                                + " at "
                                + string.Join(", ", Conflicts)
                            );
                        }
                    }

                    // "Object reference not set to an instance of an object."
                    //    at System.Reflection.Emit.SignatureHelper.AddOneArgTypeHelperWorker(Type clsArgument, Boolean lastWasGenericInst)

                    // Duplicate type name within an assembly.

                    //at System.Reflection.Emit.SignatureHelper.AddOneArgTypeHelperWorker(Type clsArgument, Boolean lastWasGenericInst)
                    //at System.Reflection.Emit.SignatureHelper.AddOneArgTypeHelperWorker(Type clsArgument, Boolean lastWasGenericInst)
                    //at System.Reflection.Emit.SignatureHelper.GetTypeSigToken(Module mod, Type type)
                    //at System.Reflection.Emit.ModuleBuilder.GetTypeTokenWorkerNoLock(Type type, Boolean getGenericDefinition)
                    //at System.Reflection.Emit.ModuleBuilder.GetTypeTokenInternal(Type type, Boolean getGenericDefinition)
                    //at System.Reflection.Emit.TypeBuilder.Init(String fullname, TypeAttributes attr, Type parent, Type[] interfaces, Module module, PackingSize iPackingSize, Int32 iTypeSize, TypeBuilder enclosingType)
                    //at System.Reflection.Emit.ModuleBuilder.DefineTypeNoLock(String name, TypeAttributes attr, Type parent, Type[] interfaces)
                    //at System.Reflection.Emit.ModuleBuilder.DefineType(String name, TypeAttributes attr, Type parent, Type[] interfaces)


                    return AtCodeTraceDefineType(BaseType, TypeAttributes, DefineTypeName);




                }
                #endregion
            }

            public Func<Type, TypeAttributes, string, TypeBuilder> AtCodeTraceDefineType;
            public Action AtCodeTraceDefineGenericParameters;
            public Action AtCodeTraceSetParent;

            private static TypeAttributes ReplaceTypeAttributes(TypeAttributes TypeAttributes, TypeAttributes _From, TypeAttributes _To)
            {
                if ((TypeAttributes & _From) == _From)
                    TypeAttributes = (TypeAttributes & ~_From) | _To;
                return TypeAttributes;
            }

        }

        internal static void CopyTypeMembers(
            Type SourceType,

            VirtualDictionary<string, string> NameObfuscation,
            TypeBuilder t,
            ILTranslationContext context
            )
        {





            foreach (var k in SourceType.GetConstructors(
                BindingFlags.DeclaredOnly |
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            {

                var km = context.ConstructorCache[k];
            }


            foreach (var k in SourceType.GetMethods(
                BindingFlags.DeclaredOnly |
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            {
                var km = context.MethodCache[k];
            }

            foreach (var k in SourceType.GetProperties(
                BindingFlags.DeclaredOnly |
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            {
                var km = context.PropertyCache[k];

            }


            foreach (var k in SourceType.GetEvents(
                BindingFlags.DeclaredOnly |
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            {
                var EventName = NameObfuscation[k.Name];
                var kp = t.DefineEvent(EventName, k.Attributes, context.TypeCache[k.EventHandlerType]);

                var _AddMethod = k.GetAddMethod(true);
                if (_AddMethod != null)
                    kp.SetAddOnMethod((MethodBuilder)context.MethodCache[_AddMethod]);

                var _GetRemoveMethod = k.GetRemoveMethod(true);
                if (_GetRemoveMethod != null)
                    kp.SetRemoveOnMethod((MethodBuilder)context.MethodCache[_GetRemoveMethod]);


            }


        }




    }
}

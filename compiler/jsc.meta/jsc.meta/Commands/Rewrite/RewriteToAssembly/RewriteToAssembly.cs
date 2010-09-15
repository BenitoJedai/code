using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;
using jsc.Languages.IL;
using jsc.Library;
using jsc.meta.Library;
using jsc.meta.Library.CodeTrace;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.meta.Commands.Rewrite
{
    public partial class RewriteToAssembly : CommandBase
    {
        public event Action<Assembly> AssemblyMergeLoadHint;

        public override void Invoke()
        {
            if (this.AttachDebugger)
                Debugger.Launch();

            InternalInvoke();
        }

        internal Func<Attribute, Attribute> SelectAssemblyMergeAttribute;

        public void InternalInvoke()
        {

            #region ExternalContext defaults...


            this.ExternalContext.TypeCache.Resolve +=
                source =>
                {
                    if (this.ExternalContext.TypeCache.BaseDictionary.ContainsKey(source))
                        return;

                    this.ExternalContext.TypeCache[source] = source;
                };

            this.ExternalContext.MethodCache.Resolve +=
                source =>
                {
                    if (this.ExternalContext.MethodCache.BaseDictionary.ContainsKey(source))
                        return;


                    this.ExternalContext.MethodCache[source] = source;
                };

            this.ExternalContext.ConstructorCache.Resolve +=
                source =>
                {
                    if (this.ExternalContext.ConstructorCache.BaseDictionary.ContainsKey(source))
                        return;


                    this.ExternalContext.ConstructorCache[source] = source;
                };

            #endregion


            var NameObfuscationRandom = new Random();

            #region NameObfuscation
            NameObfuscation.Resolve +=
                n =>
                {
                    if (!this.obfuscate)
                    {
                        NameObfuscation[n] = n;

                        return;
                    }

                    // see: http://www.cumps.be/obfuscation-making-reverse-engineering-harder/
                    // see: http://www.codesqueeze.com/careless-obfuscation-can-lose-you-business/

                    // should we add salt?
                    var salt_length = NameObfuscationRandom.Next(7);

                    var ObfuscatedName = new StringBuilder();

                    ObfuscatedName.Append((char)(0xFEFC - NameObfuscation.BaseDictionary.Count));

                    for (int i = 0; i < salt_length; i++)
                    {
                        ObfuscatedName.Append((char)(0xFEFC - NameObfuscationRandom.Next(0x1000)));
                    }

                    NameObfuscation[n] = ObfuscatedName.ToString();
                };
            #endregion



            if (this.assembly == null)
                this.staging = this.staging.CreateTemp();
            else
                this.staging = this.staging.Create(() => this.assembly.Directory.CreateSubdirectory("staging"));


            var assembly = this.assembly == null ? null : Assembly.LoadFile(this.assembly.FullName);
            //var assembly = this.assembly.LoadAssemblyAt(staging);
            _assembly = assembly;

            // load the rest of the references
            // maybe we shouldnt load those references which will be merged?
            if (assembly != null)
            {
                assembly.LoadReferencesAt(staging, this.assembly.Directory);
            }

            // AssemblyMerge will copy resources too... getting crowded!
            Action<AssemblyBuilder, ModuleBuilder> InvokeLater = delegate { };

            if (this.Output != null)
            {
                this.product = Path.GetFileNameWithoutExtension(this.Output.Name);
                this.productExtension = this.Output.Extension;
            }

            var Product_Name = (string.IsNullOrEmpty(this.product) ?
                    this.assembly.Name + ".Rewrite" :
                    this.product);


            #region PrimaryTypes AssemblyMerge
            if (this.PrimaryTypes.Length == 0)
            {
                this.PrimaryTypes = this.AssemblyMerge.SelectMany(
                    k =>
                    {
                        var shadow = Path.Combine(this.staging.FullName, Path.GetFileName(k.name));

                        var loaded = File.Exists(shadow);

                        if (!loaded)
                            File.Copy(
                                k.name,
                                shadow
                            );


                        // ? what? :)
                        var shadow_assembly = new FileInfo(shadow).ToAssemblyOrAppDomainAssembly();

                        if (this.AssemblyMergeLoadHint != null)
                            this.AssemblyMergeLoadHint(shadow_assembly);


                        if (!loaded)
                        {
                            var FolderToLoadFrom = Path.GetDirectoryName(k.name);

                            if (string.IsNullOrEmpty(FolderToLoadFrom))
                            {
                                FolderToLoadFrom = Path.GetDirectoryName(this.AssemblyMerge.First().name);
                                // ????
                            }
                            else
                            {
                                shadow_assembly.LoadReferencesAt(staging, new DirectoryInfo(FolderToLoadFrom));
                            }

                        }

                        InvokeLater +=
                            (__a, __m) =>
                            {
                                // should we copy attributes? should they be opt-out?

                                foreach (var item in
                                    from k_ in shadow_assembly.GetCustomAttributes(false)
                                    let kk = SelectAssemblyMergeAttribute == null ?
                                        (Attribute)k_ : SelectAssemblyMergeAttribute((Attribute)k_)

                                    where kk != null
                                    select kk.ToCustomAttributeBuilder()
                                    )
                                {
                                    __a.SetCustomAttribute(item(this.RewriteArguments.context));
                                }


                                foreach (var item in shadow_assembly.GetManifestResourceNames())
                                {
                                    var n = item;

                                    if (n.StartsWith(shadow_assembly.GetName().Name))
                                        n = Product_Name + n.Substring(shadow_assembly.GetName().Name.Length);

                                    __m.DefineManifestResource(
                                        n,
                                        shadow_assembly.GetManifestResourceStream(item), ResourceAttributes.Public
                                    );

                                }
                            };


                        return shadow_assembly.GetTypes();
                    }
                ).ToArray();
            }
            #endregion


            if (this.PrimaryTypes.Length == 0)
                if (assembly != null)
                    this.PrimaryTypes =
                        (string.IsNullOrEmpty(this.type) ?
                            (assembly.EntryPoint == null ? assembly.GetTypes() : new[] { assembly.EntryPoint.DeclaringType }) :
                                new[] { assembly.GetType(this.type) }
                        );

            #region __BCLImplementationMergeAssemblies
            var __BCLImplementationMergeAssemblies = Enumerable.ToLookup(
                from bb in this.BCLImplementationMergeAssemblies
                // or is it already loaded?
                let aa = Assembly.LoadFile(bb.name)
                from t in aa.GetTypes()
                let ss = t.ToScriptAttributeOrDefault()
                let ii = ss.Implements
                where ii != null
                select new { ii, t }
                , k => k.ii, k => k.t
            );
            #endregion


            var Product_Extension = this.assembly == null ? productExtension : this.assembly.Extension;

            var Product = new FileInfo(Path.Combine(staging.FullName, Product_Name + Product_Extension));

            // we might want to use temp path instead and later figure out if we are replacing input...
            var OutputUndefined = this.Output == null;

            if (OutputUndefined)
                this.Output = Product;



            if (OutputUndefined)
            {
                // we probably did not load the same file... and we can easly remove it!
                if (Product.Exists)
                    Product.Delete();
            }


            var a = default(AssemblyBuilder);
            var m = default(ModuleBuilder);

            // ct denotes CodeTrace prefix
            var _ct_Product_Name = Product.Name;
            var _ct_staging_FullName = staging.FullName;

            // ? Unable to add resource to transient module or transient assembly.
            var _ct_SaveName = OutputUndefined ? Product.Name : "~" + Product.Name;


            var OutputAssemblyName = new AssemblyName(Path.GetFileNameWithoutExtension(_ct_Product_Name))
            {
            };

            if (this.OutputStrongNameKeyPair != null)
                OutputAssemblyName.KeyPair = new StrongNameKeyPair(File.ReadAllBytes(this.OutputStrongNameKeyPair.FullName));

            a = AppDomain.CurrentDomain.DefineDynamicAssembly(
                OutputAssemblyName,
                AssemblyBuilderAccess.RunAndSave,
                _ct_staging_FullName
            );

            m = a.DefineDynamicModule(Path.GetFileNameWithoutExtension(_ct_Product_Name), _ct_SaveName);






            var OverrideDeclaringType = this.RewriteArguments.context.OverrideDeclaringType;

            var TypeDefinitionCache = this.RewriteArguments.context.TypeDefinitionCache;
            var TypeCache = this.RewriteArguments.context.TypeCache;
            var TypeRenameCache = this.RewriteArguments.context.TypeRenameCache;
            var MemberRenameCache = this.RewriteArguments.context.MemberRenameCache;
            var FieldCache = this.RewriteArguments.context.FieldCache;

            var ConstructorCache = this.RewriteArguments.context.ConstructorCache;
            var MethodCache = this.RewriteArguments.context.MethodCache;
            var MethodAttributesCache = this.RewriteArguments.context.MethodAttributesCache;
            var PropertyCache = this.RewriteArguments.context.PropertyCache;


            this.RewriteArguments.Assembly = a;
            this.RewriteArguments.Module = m;






            #region PropertyCache
            PropertyCache.Resolve +=
                SourceProperty =>
                {
                    var Source = TypeCache[SourceProperty.DeclaringType];

                    if (Source is TypeBuilder)
                    {
                        var k = SourceProperty;
                        var t = Source as TypeBuilder;

                        var PropertyName = NameObfuscation[k.Name];

                        var _SetMethod = k.GetSetMethod(true);
                        var _GetMethod = k.GetGetMethod(true);

                        // http://channel9.msdn.com/forums/TechOff/251252-Reflection-Emit-C-Indexer-problem/
                        var kp = t.DefineProperty(
                            PropertyName,
                            k.Attributes,
                            TypeCache[k.PropertyType],

                            TypeCache[k.GetIndexParameters().Select(kk => kk.ParameterType).ToArray()]

                        );

                        if (_SetMethod != null)
                            kp.SetSetMethod((MethodBuilder)MethodCache[_SetMethod]);

                        if (_GetMethod != null)
                            kp.SetGetMethod((MethodBuilder)MethodCache[_GetMethod]);

                        PropertyCache[SourceProperty] = kp;

                        return;
                    }

                    PropertyCache[SourceProperty] = Source.GetProperty(SourceProperty.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                };
            #endregion

            #region ConstructorCache
            ConstructorCache.Resolve +=
                SourceConstructor =>
                {
                    // This unit was resolved for us...
                    if (ExternalContext.ConstructorCache[SourceConstructor] != SourceConstructor)
                    {
                        ConstructorCache[SourceConstructor] = ExternalContext.ConstructorCache[SourceConstructor];
                        return;
                    }

                    //    L_0086: newobj instance void [System.Core]System.Func`2<class [ScriptCoreLib.Archive.ZIP]ScriptCoreLib.Archive.ZIP.ZIPFile/Entry, bool>::.ctor(object, native int)

                    var SourceType = SourceConstructor.DeclaringType;
                    var Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;


                    if (SourceType.IsGenericType)
                        if (!SourceType.IsGenericTypeDefinition)
                        {
                            // are we rewriting any part of this generic type? if we are not we need just to pass it!

                            if (!ShouldCopyType(SourceType))
                            {
                                ConstructorCache[SourceConstructor] = SourceConstructor;
                                return;
                            }

                            var Def = SourceType.GetGenericTypeDefinition().GetConstructors(Flags).Single(k => k.MetadataToken == SourceConstructor.MetadataToken);

                            // Define it in the TypeBuilder
                            var Def1 = ConstructorCache[Def];

                            var ResolvedType1 = TypeDefinitionCache[SourceType.GetGenericTypeDefinition()];

                            var ResolvedType2 = ResolvedType1.MakeGenericType(
                                TypeDefinitionCache[SourceType.GetGenericArguments()]
                            );

                            // http://connect.microsoft.com/VisualStudio/feedback/details/97424/confused-typebuilder-getmethod-constructor
                            // http://msdn.microsoft.com/en-us/library/ms145835.aspx

                            //var Def2 = ResolvedType.GetMethods(Flags).Single(k => k.MetadataToken == msource.MetadataToken);


                            // The specified method must be declared on the generic type definition of the specified type.
                            // Parameter name: type
                            var Def2 = default(ConstructorInfo);

                            //ResolvedType1 is TypeBuilder || TypeDefinitionCache[source.GetGenericArguments()].Any(k => k is TypeBuilder) ?

                            try
                            {
                                Def2 = TypeBuilder.GetConstructor(ResolvedType2, Def1);
                            }
                            catch
                            {
                                Def2 = ResolvedType2.GetConstructors(Flags).Single(k => k.MetadataToken == SourceConstructor.MetadataToken);
                            }



                            ConstructorCache[SourceConstructor] = Def2;
                            return;
                        }




                    if (ShouldCopyType(SourceType) && TypeCache[SourceType] is TypeBuilder)
                    {
                        var DeclaringType = (TypeBuilder)TypeCache[SourceType];

                        if (ConstructorCache.BaseDictionary.ContainsKey(SourceConstructor))
                            return;

                        CopyConstructor(
                            SourceConstructor,
                            DeclaringType,
                            NameObfuscation,
                            AtILOverride,
                            this.RewriteArguments.context
                        );
                        return;
                    }


                    ConstructorCache[SourceConstructor] = SourceConstructor;

                };
            #endregion


            #region MethodAttributesCache
            MethodAttributesCache.Resolve +=
                SourceMember =>
                {
                    if (MethodAttributesCache.BaseDictionary.ContainsKey(SourceMember))
                        return;

                    MethodAttributesCache[SourceMember] = SourceMember.Attributes;
                };
            #endregion

            #region MemberRenameCache
            MemberRenameCache.Resolve +=
                SourceMember =>
                {
                    if (MemberRenameCache.BaseDictionary.ContainsKey(SourceMember))
                        return;

                    MemberRenameCache[SourceMember] = default(string);
                };
            #endregion

            #region TypeRenameCache
            TypeRenameCache.Resolve +=
                SourceType =>
                {
                    if (TypeRenameCache.BaseDictionary.ContainsKey(SourceType))
                        return;

                    TypeRenameCache[SourceType] = default(string);
                };
            #endregion

            #region OverrideDeclaringType
            OverrideDeclaringType.Resolve +=
                SourceType =>
                {
                    if (OverrideDeclaringType.BaseDictionary.ContainsKey(SourceType))
                        return;

                    OverrideDeclaringType[SourceType] = default(TypeBuilder);
                };
            #endregion


            #region MethodCache
            MethodCache.Resolve +=
                SourceMethod =>
                {
                    //Console.WriteLine("MethodCache: " + msource.ToString());

                    // This unit was resolved for us...
                    if (ExternalContext.MethodCache[SourceMethod] != SourceMethod)
                    {
                        MethodCache[SourceMethod] = ExternalContext.MethodCache[SourceMethod];
                        return;
                    }

                    // http://msdn.microsoft.com/en-us/library/system.reflection.memberinfo.declaringtype.aspx
                    // If the MemberInfo object is a global member, (that is, it was obtained from Module.GetMethods,
                    // which returns global methods on a module), then the returned DeclaringType will be a 
                    // null reference (Nothing in Visual Basic).

                    var SourceType = SourceMethod.DeclaringType;
                    var Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

                    #region MakeGenericMethod
                    if (SourceType != null)
                        if (SourceType.IsGenericType)
                            if (!SourceType.IsGenericTypeDefinition)
                            {
                                var Def = SourceType.GetGenericTypeDefinition().GetMethods(Flags).Single(k => k.MetadataToken == SourceMethod.MetadataToken);

                                // Define it in the TypeBuilder
                                var Def1 = MethodCache[Def];

                                var ResolvedType1 = TypeDefinitionCache[SourceType.GetGenericTypeDefinition()];

                                var ResolvedType2 = ResolvedType1.MakeGenericType(
                                    TypeDefinitionCache[SourceType.GetGenericArguments()]
                                );

                                // http://connect.microsoft.com/VisualStudio/feedback/details/97424/confused-typebuilder-getmethod-constructor
                                // http://msdn.microsoft.com/en-us/library/ms145835.aspx

                                //var Def2 = ResolvedType.GetMethods(Flags).Single(k => k.MetadataToken == msource.MetadataToken);


                                // The specified method must be declared on the generic type definition of the specified type.
                                // Parameter name: type
                                var Def2 = default(MethodInfo);

                                // ResolvedType1 is TypeBuilder || TypeDefinitionCache[source.GetGenericArguments()].Any(k => k is TypeBuilder) ?
                                try
                                {
                                    Def2 = TypeBuilder.GetMethod(ResolvedType2, Def1);
                                }
                                catch
                                {
                                    Def2 = ResolvedType2.GetMethods(Flags).Single(k => k.MetadataToken == SourceMethod.MetadataToken);
                                }

                                var Def3 = Def2;

                                if (SourceMethod.IsGenericMethod)
                                    Def3 = Def2.MakeGenericMethod(
                                     TypeDefinitionCache[SourceMethod.GetGenericArguments()]
                                     );

                                MethodCache[SourceMethod] = Def3;
                                return;
                            }
                    #endregion


                    if (SourceMethod.IsGenericMethod)
                        if (!SourceMethod.IsGenericMethodDefinition)
                        {
                            MethodCache[SourceMethod] = MethodCache[SourceMethod.GetGenericMethodDefinition()].MakeGenericMethod(
                                 TypeDefinitionCache[SourceMethod.GetGenericArguments()]
                            );

                            return;
                        }


                    var DeclaringType = (
                        SourceType == null ? null : TypeCache[SourceType.TryGetGenericTypeDefinition()]
                    ) as TypeBuilder;

                    var IsGlobalMethodAndShouldCopy = (SourceType == null && ShouldCopyAssembly(SourceMethod.Module.Assembly));

                    #region ShouldCopyType - CopyMethod
                    if (IsGlobalMethodAndShouldCopy || DeclaringType != null)
                    {
                        var tb_source = DeclaringType;

                        CopyMethod(
                            a,
                            m,
                            SourceMethod,
                            tb_source,
                            NameObfuscation,
                            _assembly,
                            this.codeinjecton,
                            this.codeinjectonparams,

                            (MethodBase, e) =>
                            {



                                // enable further IL override
                                if (this.AtILOverride != null)
                                    this.AtILOverride(MethodBase, e);
                            },

                            (SourceMethod_, Method, GetILGenerator) =>
                            {
                                if (this.BeforeInstructions != null)
                                    this.BeforeInstructions(
                                         new BeforeInstructionsArguments
                                         {
                                             SourceType = SourceType,
                                             Type = tb_source,
                                             Assembly = a,
                                             Module = m,

                                             SourceMethod = SourceMethod,
                                             Method = Method,
                                             GetILGenerator = GetILGenerator,

                                             context = this.RewriteArguments.context
                                         }
                                    );
                            },
                            this.RewriteArguments.context,
                            this,

                            (string MethodName, MethodAttributes MethodAttributes, CallingConventions CallingConvention) =>
                            {
                                var DeclaringMethod = default(MethodBuilder);


                                return DeclaringMethod;
                            },

                            (MethodBuilder DeclaringMethod) =>
                            {

                            }
                        );
                        return;
                    }
                    #endregion


                    if (!SourceMethod.IsGenericMethodDefinition)
                    {
                        // do we need to redirect the type also?
                        if (SourceType.GetGenericArguments().Any(k => k != TypeCache[k]))
                        {
                            //var msource_gp = msource.GetGenericMethodDefinition().GetParameterTypes();


                            var GenericArguments = TypeDefinitionCache[SourceType.GetGenericArguments()];


                            var GenericTypeDefinition = SourceType.GetGenericTypeDefinition();
                            var GenericTypeDefinition_GetGenericArguments = GenericTypeDefinition.GetGenericArguments();

                            var GenericType = GenericTypeDefinition.MakeGenericType(GenericArguments);
                            var ParameterTypes =


                            SourceMethod.IsGenericMethod ? SourceMethod.GetGenericMethodDefinition().GetParameterTypes().Select(
                                k =>
                                {
                                    if (k.IsGenericTypeDefinition)
                                        return k;

                                    return TypeCache[k];
                                }
                                ).ToArray() : TypeDefinitionCache[SourceMethod.GetParameterTypes()];

                            ParameterTypes = ParameterTypes.Select(k =>
                            {

                                #region resolve Type Generics
                                for (int iii = 0; iii < GenericArguments.Length; iii++)
                                {
                                    if (GenericArguments[iii] == k)
                                        return GenericTypeDefinition_GetGenericArguments[iii];
                                }
                                #endregion



                                return k;
                            }).ToArray();
                            // Type must be a type provided by the runtime.
                            // Parameter name: types
                            var GenericTypeDefinitionMethod = GenericTypeDefinition.GetMethod(SourceMethod.Name, Flags, null, ParameterTypes, null);

                            var GenericTypeMethod = TypeBuilder.GetMethod(GenericType, GenericTypeDefinitionMethod);

                            var GenericTypeMethod__ = SourceMethod.IsGenericMethod ? GenericTypeMethod.MakeGenericMethod(
                                 TypeDefinitionCache[SourceMethod.GetGenericArguments()]
                                 ) : GenericTypeMethod;

                            MethodCache[SourceMethod] = GenericTypeMethod__;
                        }
                        else
                        {
                            MethodCache[SourceMethod] =
                                SourceMethod.IsGenericMethod ?

                                MethodCache[SourceMethod.GetGenericMethodDefinition()]

                                .MakeGenericMethod(
                                    TypeDefinitionCache[SourceMethod.GetGenericArguments()]
                                ) : SourceMethod;
                        }

                        return;
                    }

                    MethodCache[SourceMethod] = SourceMethod;
                };
            #endregion



            #region FieldCache
            FieldCache.Resolve +=
                SourceField =>
                {
                    // if the datastruct is actually pointing to
                    // a initialized data in .sdata
                    // then we have to redefine it in our version
                    // for some reason we cannot just copy this bit in current API

                    var DeclaringType_ = TypeCache[SourceField.DeclaringType];

                    // Things may have changed... abort?
                    if (FieldCache.BaseDictionary.ContainsKey(SourceField))
                        return;

                    var source = SourceField.DeclaringType;

                    #region IsGenericType
                    if (source.IsGenericType)
                        if (!source.IsGenericTypeDefinition)
                        {

                            if (!ShouldCopyType(source))
                            {
                                FieldCache[SourceField] = SourceField;
                                return;
                            }


                            var ResolvedType1 = TypeDefinitionCache[source.GetGenericTypeDefinition()];

                            var ResolvedType2 = ResolvedType1.MakeGenericType(
                                TypeDefinitionCache[source.GetGenericArguments()]
                            );



                            var Def1 = TypeBuilder.GetField(ResolvedType2, FieldCache[source.GetGenericTypeDefinition().GetField(
                                SourceField.Name,
                                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic
                                )]
                            );


                            FieldCache[SourceField] = Def1;

                            return;
                        }
                    #endregion


                    if (DeclaringType_ is TypeBuilder)
                    {
                        var DeclaringType = (TypeBuilder)DeclaringType_;
                        var FieldName = NameObfuscation[MemberRenameCache[SourceField] ?? SourceField.Name];


                        var FieldValue = default(byte[]);

                        if (DeclaringType.ContainsGenericParameters || !SourceField.IsStatic || SourceField.FieldType.IsEnum || SourceField.IsLiteral)
                        {
                            // Unhandled Exception: System.Reflection.TargetException: Non-static field requires a target.

                            // Unhandled Exception: System.InvalidOperationException: 
                            // Late bound operations cannot be performed on fields with types for which Type.ContainsGenericParameters is true.
                            // at System.Reflection.RtFieldInfo.GetValue(Object obj)
                        }
                        //else if (/*SourceField.FieldType == typeof(long) ||*/ SourceField.FieldType.IsInitializedDataFieldType())
                        else if (SourceField.FieldType.IsValueType && DeclaringType.Name.StartsWith("<PrivateImplementationDetails>"))
                        {
                            //An unhandled exception of type 'System.ExecutionEngineException' occurred in mscorlib.dll


                            var __Value = SourceField.GetValue(null);



                            FieldValue = __Value.StructAsByteArray();

                        }

                        if (FieldValue != null && FieldValue.Any(k => k > 0))
                        {

                            var ff = DeclaringType.DefineInitializedData(FieldName, FieldValue, SourceField.Attributes);

                            FieldCache[SourceField] = ff;
                        }
                        else
                        {

                            //this.WriteDiagnostics("DefineField " + FieldName);

                            var FieldType = TypeDefinitionCache[SourceField.FieldType];

                            if (FieldCache.BaseDictionary.ContainsKey(SourceField))
                            {
                            }
                            else
                            {
                                var FieldAttributes = SourceField.Attributes;

                                var DeclaringField = default(FieldBuilder);

                                DeclaringField = DeclaringType.DefineField(
                                     FieldName,
                                     FieldType,
                                     FieldAttributes
                                );

                                // should we copy attributes? should they be opt-out?
                                foreach (var item in SourceField.GetCustomAttributes(false).Select(kk => kk.ToCustomAttributeBuilder()))
                                {
                                    DeclaringField.SetCustomAttribute(item(this.RewriteArguments.context));
                                }


                                if (SourceField.IsLiteral)
                                {
                                    // should we enable constant value override? :)

                                    DeclaringField.SetConstant(SourceField.GetRawConstantValue());
                                }


                                FieldCache[SourceField] = DeclaringField;
                            }

                            FieldType = TypeCache[SourceField.FieldType];
                        }
                    }
                    else
                    {
                        // Specified method is not supported.
                        // http://msdn.microsoft.com/en-us/library/4ek9c21e.aspx

                        FieldCache[SourceField] = DeclaringType_.GetField(
                            SourceField.Name,
                            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic
                        );
                    }
                };
            #endregion

            // assemblies loaded at different locatiuons will have different hashes for types but the GUIDs are the same


            #region TypeDefinitionCache
            TypeDefinitionCache.Resolve +=
                (SourceType) =>
                {
                    if (SourceType.Assembly is AssemblyBuilder)
                    {
                        // not going to merge already merged type.
                        TypeDefinitionCache[SourceType] = SourceType;
                        return;
                    }

                    if (TypeCache.BaseDictionary.ContainsKey(SourceType))
                    {
                        // somebody wrote the type before us?
                        TypeDefinitionCache[SourceType] = TypeCache.BaseDictionary[SourceType];
                        return;
                    }

                    if (SourceType.IsGenericParameter)
                    {
                        TypeDefinitionCache[SourceType] = SourceType;
                        return;
                    }

                    if (SourceType.IsByRef)
                    {
                        TypeDefinitionCache[SourceType] = TypeDefinitionCache[SourceType.GetElementType()].MakeByRefType();
                        return;
                    }

                    if (SourceType.IsArray)
                    {
                        TypeDefinitionCache[SourceType] = TypeDefinitionCache[SourceType.GetElementType()].MakeArrayType();
                        return;
                    }

                    #region Generic
                    if (SourceType.IsGenericType)
                        if (!SourceType.IsGenericTypeDefinition)
                        {


                            // Operation is not valid due to the current state of the object.
                            var GenericTypeDefinition__ = SourceType.GetGenericTypeDefinition();

                            var GenericTypeDefinition = TypeDefinitionCache[GenericTypeDefinition__];
                            var GenericArguments = TypeDefinitionCache[SourceType.GetGenericArguments()];

                            TypeDefinitionCache[SourceType] =
                                GenericTypeDefinition.MakeGenericType(GenericArguments);


                            return;
                        }
                    #endregion


                    #region This unit was resolved for us...
                    if (ExternalContext.TypeCache[SourceType] != SourceType)
                    {
                        TypeDefinitionCache[SourceType] = ExternalContext.TypeCache[SourceType];

                        // was continuation honored?
                        if (TypeDefinitionCache[SourceType] is TypeBuilder)
                        {
                            TypeCache[SourceType] = TypeDefinitionCache[SourceType];
                            TypeCache.Flags[SourceType] = new object();
                            Console.WriteLine("CreateType:  " + SourceType.FullName);

                            if (TypeCreated != null)
                                TypeCreated(
                                    new TypeRewriteArguments
                                    {
                                        SourceType = SourceType,
                                        Type = (TypeBuilder)TypeCache[SourceType],
                                        Assembly = a,
                                        Module = m,

                                        context = this.RewriteArguments.context
                                    }
                                );
                        }

                        return;
                    }
                    #endregion

                    #region time to find the BCLImplementation?
                    {
                        // 
                        var Candidates = __BCLImplementationMergeAssemblies[SourceType].ToArray();
                        var ResolvedBCLImplementationMergeAssemblies = TypeCache[Candidates];
                    }
                    #endregion


                    var ContextType = SourceType;
                    if (ShouldCopyType(ContextType))
                    {
                        var ttt = new CopyTypeDefinition
                        {
                            Command = this,

                            context = this.RewriteArguments.context,

                            SourceType = SourceType,
                            m = m,

                            //OverrideDeclaringType = null,
                            NameObfuscation = NameObfuscation,
                            ShouldCopyType = ShouldCopyType,
                            FullNameFixup = FullNameFixup,
                            Diagnostics = null,


                            AtCodeTraceDefineType =
                                (Type BaseType, TypeAttributes TypeAttributes, string DefineTypeName) =>
                                {
                                    var DeclaringType = default(TypeBuilder);

                                    // base type is always null here and will be set separatly

                                    DeclaringType = m.DefineType(
                                        DefineTypeName,
                                        TypeAttributes,

                                        null,

                                        new Type[0]
                                    );



                                    return DeclaringType;
                                },




                            AtCodeTraceDefineGenericParameters =
                              delegate
                              {

                              }
                        };

                        var t = ttt.Invoke();


                        //TypeDefinitionCache[SourceType] = t;
                    }
                    else
                    {
                        if (SourceType.IsGenericType)
                        {
                            TypeDefinitionCache[SourceType] =

                            SourceType.GetGenericTypeDefinition().MakeGenericType(
                                 SourceType.GetGenericArguments().Select(
                                     k => TypeDefinitionCache[k]
                                 ).ToArray()
                            );
                        }
                        else
                        {
                            TypeDefinitionCache[SourceType] = SourceType;

                            //var _ct_SourceType = SourceType;
                        }
                    }

                };
            #endregion


            #region TypeCache
            TypeCache.Resolve +=
                (SourceType) =>
                {
                    if (SourceType.Assembly is AssemblyBuilder)
                    {
                        // not going to merge already merged type.
                        TypeCache[SourceType] = SourceType;
                        return;
                    }





                    if (SourceType.IsGenericParameter)
                    {
                        TypeCache[SourceType] = SourceType;
                        return;
                    }


                    if (SourceType.IsByRef)
                    {
                        TypeCache[SourceType] = TypeCache[SourceType.GetElementType()].MakeByRefType();
                        return;
                    }


                    if (SourceType.IsArray)
                    {
                        TypeCache[SourceType] = TypeCache[SourceType.GetElementType()].MakeArrayType();
                        return;
                    }

                    #region MakeGenericType
                    if (SourceType.IsGenericType)
                        if (!SourceType.IsGenericTypeDefinition)
                        {
                            var GenericArguments =
                              SourceType.GetGenericArguments().Select(
                                      k => TypeCache[k]
                                  ).ToArray();

                            var GenericTypeDefinition = TypeCache[SourceType.GetGenericTypeDefinition()];


                            TypeCache[SourceType] =
                                GenericTypeDefinition.MakeGenericType(
                                    GenericArguments
                                );
                            return;
                        }
                    #endregion

                    // should we actually copy the field type?
                    // simple rule - same assembly equals must copy


                    // did we got overrridden?
                    var __Definition = TypeDefinitionCache[SourceType];

                    if (TypeCache.BaseDictionary.ContainsKey(SourceType))
                    {
                        // seems like we are not supposed to resolve this type and use
                        // what has been inserted in the cache!
                        return;
                    }

                    if (ShouldCopyType(SourceType) && TypeDefinitionCache[SourceType] is TypeBuilder)
                    {
                        CopyType(
                            SourceType, a, m,
                            null,
                            TypeRenameCache,
                            NameObfuscation,
                            ShouldCopyType,
                            FullNameFixup,

                             t =>
                             {
                                 #region PostTypeRewrite
                                 if (PostTypeRewrite != null)
                                     PostTypeRewrite(
                                         new TypeRewriteArguments
                                         {
                                             SourceType = SourceType,
                                             Type = t,
                                             Assembly = a,
                                             Module = m,

                                             context = this.RewriteArguments.context
                                         }
                                     );
                                 #endregion
                             }
                            ,


                             t =>
                             {
                                 #region PreTypeRewrite
                                 if (PreTypeRewrite != null)
                                     PreTypeRewrite(
                                         new TypeRewriteArguments
                                         {
                                             SourceType = SourceType,
                                             Type = t,
                                             Assembly = a,
                                             Module = m,

                                             context = this.RewriteArguments.context
                                         }
                                     );
                                 #endregion

                             }
                             ,

                             t =>
                             {
                                 #region TypeCreated
                                 if (TypeCreated != null)
                                     TypeCreated(
                                         new TypeRewriteArguments
                                         {
                                             SourceType = SourceType,
                                             Type = t,
                                             Assembly = a,
                                             Module = m,

                                             context = this.RewriteArguments.context
                                         }
                                     );
                                 #endregion

                             },
                             this,
                             this.RewriteArguments.context,

                             delegate
                             {


                             }
                        );


                    }
                    else
                    {
                        TypeCache[SourceType] =

                            SourceType.IsGenericType ? SourceType.GetGenericTypeDefinition().MakeGenericType(
                                TypeCache[SourceType.GetGenericArguments()]
                            ) : SourceType;
                    }

                };
            #endregion

            if (assembly != null)
                foreach (var ka in assembly.GetCustomAttributes<ObfuscationAttribute>())
                {
                    a.SetCustomAttribute(
                        ka.ToCustomAttributeBuilder()(this.RewriteArguments.context)
                    );
                }

            if (PreAssemblyRewrite != null)
                PreAssemblyRewrite(
                    RewriteArguments
                );

            // we cannot be rewriting initialized data types...
            PrimaryTypes = PrimaryTypes.Where(k => !k.IsInitializedDataFieldType()).ToArray();

            Console.WriteLine("");
            Console.WriteLine("rewriting... primary types: " + PrimaryTypes.Length);
            Console.WriteLine("");

            #region HiddenEntryPoints
            var HiddenEntryPoints = Enumerable.ToArray(
                from t in PrimaryTypes
                from tm in t.GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
                where tm.DeclaringType.Assembly.EntryPoint == tm
                select tm
            );

            foreach (var item in HiddenEntryPoints)
            {
                MethodAttributesCache[item] = item.Attributes | MethodAttributes.Family;
            }
            #endregion

            // ask for our primary types to be copied
            var kt = TypeCache[PrimaryTypes];

            // did we define any type declarations which we did not actually create yet?
            // fixme: maybe we shold just close the unclosed TypeBuilders?

            var ClosePartialDefinitions = new VirtualDictionary<Type, object>();
            var ClosePartialDefinitionsFilter =
                TypeDefinitionCache.BaseDictionary.Keys.Except(TypeCache.BaseDictionary.Keys).Select(
                    item =>
                    new
                    {
                        item,
                        tb = TypeDefinitionCache[item] as TypeBuilder
                    }
                ).Where(k => k.tb != null).ToArray();

            var PartialDefinition = new { Visited = new object(), IsPartial = new object() };

            ClosePartialDefinitions.Resolve +=
                SourceType =>
                {
                    // ask us only once! :)
                    ClosePartialDefinitions[SourceType] = PartialDefinition.Visited;

                    if (TypeCache.BaseDictionary.ContainsKey(SourceType))
                        return;

                    var DeclaringType = ClosePartialDefinitionsFilter.Where(k => k.item == SourceType).Select(k => k.tb).FirstOrDefault();

                    if (DeclaringType == null)
                        return;



                    if (SourceType.IsEnum)
                    {
                        // enums cannot be left partial... we need to implement them
                        var __Enum = TypeCache[SourceType];

                    }

                    var SignatureTypes = new[] { SourceType.BaseType }.Concat(SourceType.GetInterfaces()).Where(k => k != null).Select(k => ClosePartialDefinitions[k]).ToArray();

                    ClosePartialDefinitions[SourceType] = PartialDefinition.IsPartial;

                    var PartialMethods = new List<object>();

                    #region GetInterfaceMap
                    if (DeclaringType.IsClass && !DeclaringType.IsAbstract)
                    {
                        // we need dummy implementation now because we cannot go back in time and make us abstract

                        var __explicit =
                            from i in SourceType.GetInterfaces()
                            let map = SourceType.GetInterfaceMap(i)
                            from j in Enumerable.Range(0, map.InterfaceMethods.Length)
                            let TargetMethod = map.TargetMethods[j]

                            // abstract class with interfaces?
                            where TargetMethod != null

                            let InterfaceMethod = map.InterfaceMethods[j]
                            where TargetMethod.DeclaringType == SourceType
                            select new { TargetMethod, InterfaceMethod };

                        foreach (var VirtualMethod_ in __explicit)
                        {
                            var VirtualMethod = VirtualMethod_.TargetMethod;

                            PartialMethods.Add(
                                new
                                {
                                    VirtualMethod.Name,
                                    VirtualMethod.ReturnType,
                                    GetParameterTypes = TypeDefinitionCache[VirtualMethod.GetParameterTypes()]
                                }
                            );

                            DeclaringType.DefineMethod(
                                VirtualMethod.Name,
                                VirtualMethod.Attributes,
                                VirtualMethod.CallingConvention,
                                VirtualMethod.ReturnType,
                                TypeDefinitionCache[VirtualMethod.GetParameterTypes()]
                            ).NotImplemented();
                        }
                    }
                    #endregion

                    TypeCache[SourceType] = DeclaringType;

                    var SourceTypeDefaultConstructor = SourceType.GetConstructor(new Type[0]);

                    if (SourceTypeDefaultConstructor != null)
                    {
                        ConstructorCache[SourceTypeDefaultConstructor] = DeclaringType.DefineDefaultConstructor(SourceTypeDefaultConstructor.Attributes);
                    }

                    if (SourceType.IsNested)
                    {
                        if (!TypeCache.Flags.ContainsKey(SourceType.DeclaringType))
                        {
                            var ParentPartial = ClosePartialDefinitions[SourceType.DeclaringType];
                        }
                    }

                    // do we need to implement some methods?
                    Console.WriteLine("Create Partial Type: " + DeclaringType.FullName);
                    DeclaringType.CreateType();

                    TypeCache.Flags[SourceType] = new object();

                    var TypeCreatedArguments =
                        new TypeRewriteArguments
                        {
                            SourceType = SourceType,
                            Type = (TypeBuilder)TypeCache[SourceType],
                            Assembly = a,
                            Module = m,

                            context = this.RewriteArguments.context
                        };

                    RaiseTypeCreated(TypeCreatedArguments);

                };

            // fixme: one partial type inherits another...

            Console.WriteLine("Closing partial types...");

            foreach (var item in ClosePartialDefinitionsFilter)
            {
                var _ = ClosePartialDefinitions[item.item];
            }



            DefineHiddenEntryPointsType(m, HiddenEntryPoints);


            #region maybe the rewriter wants to add some types at this point?
            if (PostAssemblyRewrite != null)
                PostAssemblyRewrite(
                    RewriteArguments
                );
            #endregion

            InvokeLater(a, m);


            Console.WriteLine("");
            Console.WriteLine("rewriting... done");
            Console.WriteLine("");

            // http://blogs.msdn.com/fxcop/archive/2007/04/27/correct-usage-of-the-compilergeneratedattribute-and-the-generatedcodeattribute.aspx




            m.CreateGlobalFunctions();

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

            a.Save(_ct_SaveName);

            // The type definition of the global function is not completed.
            if (OutputUndefined)
            {

            }
            else
            {
                // we probably loaded that assembly and now are trying to write to it...

                var Temp = Path.Combine(Product.Directory.FullName, "~" + Product.Name);

                // The process cannot access the file 'c:\util\jsc\bin\ScriptCoreLib.dll' because it is being used by another process.
                // http://stackoverflow.com/questions/1337961/powershell-unload-module-completely

                if (this.EnableDelayedFileMove)
                {
                    Program.DelayedMoveFile(Temp, this.Output.FullName);
                }
                else
                {
                    File.Copy(Temp, this.Output.FullName, true);
                    File.Delete(Temp);
                }
            }

            Product.Refresh();
        }

        public void RaiseTypeCreated(TypeRewriteArguments TypeCreatedArguments)
        {
            if (TypeCreated != null)
                TypeCreated(TypeCreatedArguments);
        }

        private void DefineHiddenEntryPointsType(ModuleBuilder m, MethodInfo[] HiddenEntryPoints)
        {
            if (HiddenEntryPoints.Any())
            {
                var HiddenEntryPointsType = m.DefineType("HiddenEntryPointsType", TypeAttributes.Abstract | TypeAttributes.Sealed | TypeAttributes.Public);

                var EntryPointIndex = 0;

                foreach (var HiddenEntryPoint in HiddenEntryPoints)
                {
                    var EntryPointMethod = HiddenEntryPointsType.DefineMethod(
                        "EntryPoint" + EntryPointIndex,
                        MethodAttributes.Public | MethodAttributes.Static,
                        HiddenEntryPoint.CallingConvention,
                        HiddenEntryPoint.ReturnType,
                        HiddenEntryPoint.GetParameterTypes()
                    );

                    var il = EntryPointMethod.GetILGenerator();

                    foreach (var item in HiddenEntryPoint.GetParameters())
                    {
                        il.Emit(OpCodes.Ldarg, (short)item.Position);
                    }
                    il.Emit(OpCodes.Call, this.RewriteArguments.context.MethodCache[HiddenEntryPoint]);
                    il.Emit(OpCodes.Ret);

                }

                HiddenEntryPointsType.CreateType();
            }
        }






        private static bool IsMarkedForMerge(Type t)
        {
            return t.Assembly.GetCustomAttributes<ObfuscationAttribute>().Any(k => k.Feature == "merge");
        }


        public RewriteToAssembly()
        {
            this.RewriteArguments = new AssemblyRewriteArguments { context = new ILTranslationContext() };
        }

        public AssemblyRewriteArguments RewriteArguments { get; private set; }

        public class AtShouldCopyTypeTuple
        {
            public Type ContextType;

            public bool DisableCopyType;
        }






        public string FullNameFixup(string n)
        {
            if (this.obfuscate)
                return NameObfuscation[n];

            return InternalFullNameFixup(n);
        }

        public string InternalFullNameFixup(string n)
        {


            if (this.rename != null)
                foreach (var k in this.rename)
                {
                    if (n.StartsWith(k.From))
                        return k.To + n.Substring(k.From.Length);
                }

            return n;
        }
    }
}

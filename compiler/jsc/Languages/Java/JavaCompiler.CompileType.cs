
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {
		public Action CompileType_WriteAdditionalMembers;

        public override bool CompileType(Type z)
        {
            if (IsNativeType(z))
                return false;

			if (z.Name.Contains("<PrivateImplementationDetails>") || (z.DeclaringType != null && z.DeclaringType.Name.Contains("<PrivateImplementationDetails>")))
				return false;

			// why would we do that?
			//if (IsEmptyImplementationType(z))
			//    return false;

            if (ScriptAttribute.IsAnonymousType(z))
                return false;

            //WriteMachineGeneratedWarning();
			WriteCommentLine(Path.GetFileName(z.Assembly.Location) );

            if (z.Namespace != null)
            {
                this.WriteIdent();
				this.WriteKeywordSpace(Keywords._package);
				this.Write(NamespaceFixup(z.Namespace, z) + ";");
				this.WriteLine();
				this.WriteLine();
            }

            this.WriteImportTypes(z);

            WriteLine();


            ScriptAttribute za = ScriptAttribute.Of(z, true);



            #region type summary
            XmlNode u = GetXMLNode(z);

            if (u != null)
                WriteBlockComment(u["summary"].InnerText);
            #endregion

			CompileType_WriteAdditionalMembers = delegate { };

            WriteTypeSignature(z, za);

            using (CreateScope())
            {
                WriteTypeFields(z, za);
                WriteLine();
                WriteTypeStaticConstructor(z, za);
                WriteLine();

				// why was this check here?
				//if (za.Implements == null)
				//{
                    WriteTypeInstanceConstructors(z);
                    WriteLine();
				//}

                WriteTypeInstanceMethods(z, za);
                WriteLine();
                WriteTypeStaticMethods(z, za);

				if (za.Implements == typeof(Delegate))
				{
					DelegateImplementationProvider.WriteExtensionMethodSupport(this, z);
				}

				CompileType_WriteAdditionalMembers();
            }

            //Thread.Sleep(100);

            return true;
        }

		protected override bool WriteMethodCustomBody(MethodBase m)
		{
			if (m.DeclaringType.IsDelegate())
			{
				if (m.IsConstructor)
				{
					DelegateImplementationProvider.WriteConstructor(this, (ConstructorInfo)m);
					return true;
				}

				if (m.Name == "BeginInvoke")
				{
					DelegateImplementationProvider.WriteBeginInvoke(this, (MethodInfo)m);
					return true;
				}

				if (m.Name == "EndInvoke")
				{
					DelegateImplementationProvider.WriteEndInvoke(this, (MethodInfo)m);
					return true;
				}

				if (m.Name == "Invoke")
				{
					DelegateImplementationProvider.WriteInvoke(this, (MethodInfo)m);
					return true;

				} 
			}

			if ((m.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PinvokeImpl)
			{
				var DllImport = m.GetCustomAttributes<DllImportAttribute>().Single();
				var ReturnType = ((MethodInfo)m).ReturnType;
				// cool.
				// do we have Platform Invocation Services?

				if (ReturnType == typeof(int))
				{
					Func<string, string, object[], int> _InvokeInt32 = PlatformInvocationServices.InvokeInt32;

					var _Resolved_InvokeInt32 = this.ResolveImplementationMethod(_InvokeInt32.Method.DeclaringType, _InvokeInt32.Method);

					if (_Resolved_InvokeInt32 == null)
						throw new NotSupportedException("PlatformInvocationServices.InvokeInt32 implementation was not found.");

					this.WriteIdent();

					this.WriteKeywordSpace(Keywords._return);
					this.WriteDecoratedTypeName(_Resolved_InvokeInt32.DeclaringType);
					this.Write(".");
					this.WriteDecoratedMethodName(_Resolved_InvokeInt32, false);
					this.Write("(");

					this.WriteQuotedLiteral(DllImport.Value);
					this.Write(", ");
					this.WriteQuotedLiteral(DllImport.EntryPoint);
					this.Write(", ");

					this.WriteKeywordSpace(Keywords._new);
					this.WriteDecoratedTypeName(typeof(object));
					this.WriteSpace();
					this.Write("[]");
					this.WriteSpace();
					this.Write("{");

					var p = m.GetParameters();
					for (int i = 0; i < p.Length; i++)
					{
						if (i > 0)
							this.Write(", ");

						this.WriteDecoratedMethodParameter(p[i], typeof(object));
					}

					this.Write("}");
					this.Write(")");
					this.Write(";");

					this.WriteLine();

					//WriteCommentLine("PinvokeImpl: " + DllImport.Value);

					//Debugger.Launch();
					//Debugger.Break();

					return true;

				}

				throw new NotSupportedException("PlatformInvocationServices for " + ReturnType.FullName + " not implemented");
			}

			return false;
		}
    }
}

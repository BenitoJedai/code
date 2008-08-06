using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler
    {
		
        private void WriteCustomAttributes(ICustomAttributeProvider zfn)
        {
            foreach (var v in from i in zfn.GetCustomAttributes(false)
                              let type = i.GetType()
                              let meta = ScriptAttribute.Of(type)
                              where meta != null
                              let name = type.Name.Substring(0, type.Name.Length - "Attribute".Length)
                              let fields = type.GetFields()
                              select new { name, type, i, meta, fields })
            {
				var AttributeName = v.name;
				var AttributeRef = v.i;
				var AttributeFields = v.fields;

				WriteCustomAttribute(AttributeName, AttributeRef, AttributeFields);
            }
        }

		private void WriteCustomAttribute(string AttributeName, object AttributeRef, FieldInfo[] AttributeFields)
		{
			WriteIdent();

			Write("[");
			WriteSafeLiteral(AttributeName);
			Write("(");



			WriteCustomAttributeNamedParameters(AttributeRef, AttributeFields);


			Write(")");
			Write("]");
			WriteLine();
		}

		private void WriteCustomAttributeNamedParameters(object AttributeRef, FieldInfo[] AttributeFields)
		{
			AttributeFields.Aggregate("",
							(seed, f) =>
							{
								if (f.IsLiteral)
									return seed;

								if (f.FieldType == typeof(string))
								{
									var value = (string)f.GetValue(AttributeRef);

									if (value == null)
										return seed;

									Write(seed);

									Write(f.Name);
									WriteAssignment();

									WriteQuotedLiteral(value);


								}
								else if (f.FieldType == typeof(uint))
								{
									Write(seed);

									Write(f.Name);
									WriteAssignment();

									var value = (uint)f.GetValue(AttributeRef);

									var HexA = f.GetCustomAttributes(typeof(HexAttribute), false).Cast<HexAttribute>().SingleOrDefault();

									if (HexA != null)
										Write(string.Format("0x{0:x8}", value));
									else
										Write((value).ToString());
								}
								else if (f.FieldType == typeof(int))
								{
									Write(seed);

									Write(f.Name);
									WriteAssignment();

									var value = (int)f.GetValue(AttributeRef);

									var HexA = f.GetCustomAttributes(typeof(HexAttribute), false).Cast<HexAttribute>().SingleOrDefault();

									if (HexA != null)
										Write(string.Format("0x{0:x8}", value));
									else
										Write((value).ToString());
								}
								else
									throw new NotImplementedException();

								return ", ";
							}
						);
		}



    }
}

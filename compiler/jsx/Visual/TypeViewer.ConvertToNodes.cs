using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace jsx.Visual
{

    partial class TypeViewer
    {
        public class ConverterSwitch
        {
            public Action<string> CreateNode;
            public ActionParams<string> BindImage;
            public ActionParams<object> SmartAddChildren;

            public TreeNode n;



        }

        public IEnumerable<TreeNode> ConvertToNodes(TreeView t, Action<Action> BindParentAction, params object[] root)
        {
            if (root == null)
                yield break;

            foreach (var v in root)
            {
                if (v == null)
                    continue;

                TreeNode n = null;

                var cs = new ConverterSwitch {
                    BindImage = delegate(string[] e)
                    {
                        var z = from i in t.ImageList.Images.Keys.ToSequence()
                                let isplit = i.Split('.')
                                let iintersect = e.Intersect(isplit).ToArray()
                                let ivalue = isplit.Length - iintersect.Length
                                orderby ivalue
                                select i;

                        var zimg = n.SelectedImageKey = n.ImageKey = z.FirstOrDefault();
                    }
                };



                #region bind

                Action<Action> BindSmartClick =
                    delegate(Action h)
                    {
                        if (BindParentAction == null)
                        {

                            h();
                        }
                        else
                        {
                            OnlyOnce bpc = new OnlyOnce();


                            BindParentAction(
                                delegate
                                {
                                    if (bpc++) return;


                                    h();
                                });
                        }
                    };

                Action<Action> BindBeforeSelect =
                    delegate(Action h)
                    {
                        t.BeforeSelect +=
                            delegate(object sender, TreeViewCancelEventArgs e)
                            {

                                if (e.Node != n) return;

                                if (h != null)
                                    h();
                            };
                    };

                Action<Action> BindBeforeExpand =
                    delegate(Action h)
                    {

                        t.BeforeExpand +=
                            delegate(object sender, TreeViewCancelEventArgs e)
                            {

                                if (e.Node != n) return;

                                if (h != null)
                                    h();
                            };

                        // listen for removals...

                    };

                ActionParams<object> SmartAddChildren =
                    delegate(object[] o)
                    {
                        BindSmartClick(
                            delegate
                            {
                                AddNodesTo(n.Nodes, t, BindBeforeExpand, o);
                            }
                        );

                    };





                var v0 = v;

                Action<string> CreateNode =
                    delegate(string text)
                    {
                        n = new TreeNode(InstructionAnalysis.GetSymbolName(text));
                        n.Tag = v0;

                    };

                ActionParams<string, Action<TreeNode, ActionParams<object>>, string>
                    AddCustomNode = ( Text, Update, ImageKeys) => SmartAddChildren(new CustomNode { Text = Text, Update = Update, ImageKeys = ImageKeys });

                ActionParams<string, object[], string>
                    AddSimpleCustomNodes =
                    delegate(string Text, object[] Elements, string[] ImageKeys)
                    {
                        if (Elements.Length > 0)
                            AddCustomNode(Text, ( cn, h) => h(Elements), ImageKeys);
                    };

                #endregion




                if (v is CustomNode)
                {
                    var x = (CustomNode)v;
                    CreateNode(x.Text);
                    cs.BindImage(x.ImageKeys);
                    x.Update(n, SmartAddChildren);
                }
                else if (v is StackUsage.Value)
                {
                    var x = (StackUsage.Value)v;

                    Debug.Assert(x.ComplexFlag == false);


                    CreateNode(string.Format("stack: 0x{0:x4} {1}", x.Provider.Offset, x.Provider.Value.Name));
                    cs.BindImage("undo");

                    n.NodeFont = new Font(FontFamily.GenericMonospace, t.Font.Size);

                    SmartAddChildren(
                        x.Provider
                    );
                }
                else if (v is InstructionAnalysisNode)
                {
                    var x = (InstructionAnalysisNode)v;

                    CreateNode(String.Format("msil: 0x{0:x8}", x.Value.Value.MetadataToken));
                    n.NodeFont = new Font(FontFamily.GenericMonospace, t.Font.Size);

                    try
                    {
                        x.Analysis = Cache.InstructionAnalysis[x.Value];
                    }
                    catch
                    {
                        n.ForeColor = Color.Red;
                    }


                    if (x.Analysis != null)
                    {
                        AddSimpleCustomNodes(
                            "Referenced Called Methods",
                            x.Analysis.ReferencedCallMethods,
                            "redo"
                        );

                        AddSimpleCustomNodes(
                            "Referenced Delegates",
                            x.Analysis.ReferencedDelegateMethods,
                            "redo"
                        );


                        AddSimpleCustomNodes(
                            "Referenced Fields",
                            x.Analysis.ReferencedFields,
                            "redo"
                        );

                        AddSimpleCustomNodes(
                            "Entry",
                            new [] { x.Analysis.EntryPoint },
                            "property"
                        );

                        Func<Instruction, CustomNode[]> GetBranchOutNodes = null;

                        GetBranchOutNodes =
                            delegate(Instruction i)
                            {
                                var zio = x.Analysis.BranchOutByOffset[i.Offset];

                                if (zio == null)
                                    return new CustomNode[] { };

                                return zio.Select<Instruction, CustomNode>(
                                delegate(Instruction ij)
                                {
                                    return new CustomNode {
                                        Text = string.Format("0x{0:x4} {1} -> 0x{2:x4} {3}", 
                                            i.Offset, i.Value.Name,
                                            ij.Offset, ij.Value.Name),
                                        ImageKeys = new [] { 
                                            ij.BranchOut == null ? "undo" :
                                            "redo" 
                                        },
                                        Update = delegate(TreeNode tn, ActionParams<object> ap)
                                        {
                                            tn.NodeFont = new Font( FontFamily.GenericMonospace, t.Font.Size);

                                            var su = x.Analysis.StackUsage.ByClient[ij.Offset];

                                            ap(su);

                                            if (ij.BranchOut != null)
                                                foreach (var vij in ij.BranchOut)
	                                            {
                                        		     ap(GetBranchOutNodes(vij));
	                                            }
                                        }
                                    };
                                }
                                ).ToArray();
                            };


                        AddSimpleCustomNodes(
                            "BranchOut",
                            GetBranchOutNodes(x.Analysis.EntryPoint),
                            "redo"
                        );
                    }
                }
                else if (v is Instruction)
                {
                    var x = (Instruction)v;


                    CreateNode(string.Format("0x{0:x4} {1}", x.Offset, x.Value.Name));

                    n.NodeFont = new Font(FontFamily.GenericMonospace, t.Font.Size);


                    if (x.Value == OpCodes.Switch)
                    {
                        AddSimpleCustomNodes(
                               "Operands",
                               x.OperandArguments.Select(i=>"" + i).ToArray(),
                               "references"
                           );
                    }

                    if (x.TargetVariableLoadIndex != null)
                    {
                        var locusage = x.Analysis.Locals.Values[(int)x.TargetVariableLoadIndex];

                        var locproviders = locusage.Clients.Where(i => i.TargetInstruction == x).Single().Providers.Select(i => i.EntryPoint).ToArray();

                        AddSimpleCustomNodes(
                               "Providers",
                               locproviders,
                               "redo"
                           );
                    }

                    var su = x.Analysis.StackUsage.ByClient[x.Offset];

                    SmartAddChildren(su);

                    SmartAddChildren(
                          x.BranchOut
                      );

                }
                else if (v is InterfaceMapping)
                {
                    var x = (InterfaceMapping)v;

                    CreateNode("Interface Mapping : " + x.InterfaceType.Name);
                    cs.BindImage("interface");

                    AddSimpleCustomNodes(
                        x.InterfaceType.Name,
                        x.InterfaceMethods,
                        "redo"
                    );

                    AddSimpleCustomNodes(
                                            x.TargetType.Name,
                                            x.TargetMethods,
                                            "undo"
                                        );

                }
                else if (v is AssemblyName)
                {
                    var x = (AssemblyName)v;

                    CreateNode(x.FullName);
                    cs.BindImage("reference");

                    SmartAddChildren(
                        Assembly.Load(x)
                    );

                }
                else if (v is EventInfo)
                {
                    var x = (EventInfo)v;

                    CreateNode(x.Name);
                    cs.BindImage("event");

                    SmartAddChildren(x.Attributes);

                    SmartAddChildren(
                        x.GetAddMethod(true),
                        x.GetRemoveMethod(true),
                        x.GetRaiseMethod(true)
                    );

                    SmartAddChildren(
                        x.GetOtherMethods(true)
                    );
                }
                else if (v is PropertyInfo)
                {
                    var x = (PropertyInfo)v;



                    CreateNode(x.Name);
                    cs.BindImage("property");

                    SmartAddChildren(x.Attributes);

                    SmartAddChildren(
                        x.GetGetMethod(true),
                        x.GetSetMethod(true)
                    );


                }
                else if (v is MethodBase)
                {
                    var x = (MethodBase)v;

                    CreateNode(x.Name);


                    if (jsx.Extenstions.IsExtensionMethod(x))
                        n.ForeColor = Color.Green;
                    else if (jsx.Extenstions.IsCompilerGenerated(x))
                        n.ForeColor = Color.Blue;
                    else if (!x.IsPublic)
                        n.ForeColor = Color.Gray;

                    if (x.IsSpecialName)
                        n.NodeFont = new Font(t.Font, FontStyle.Italic);

                    if (v is ConstructorInfo)
                    {
                        cs.BindImage("ctor");

                    }
                    else
                    {
                        cs.BindImage("method");
                    }

                    SmartAddChildren(x.Attributes);


                    if (x.IsGenericMethod)
                    {
                        AddSimpleCustomNodes(
                            "Generic Parameters",
                            x.GetGenericArguments(),
                            "redo"
                        );
                    }

                    if (x.DeclaringType != null)
                    {
                        AddSimpleCustomNodes(
                            "Declaring Type : " + x.DeclaringType.Name,
                            new [] { x.DeclaringType },
                            "undo"
                        );

                    }

                    AddSimpleCustomNodes(
                        "Attributes",
                        x.GetCustomAttributes(false),
                        "undo"
                    );

                    if (x is MethodInfo)
                    {
                        var xm = (MethodInfo)x;

                        AddSimpleCustomNodes(
                            "Return Type",
                            new [] { xm.ReturnType },
                            "reference"
                        );
                    }

                    if (x.GetMethodBody() != null)
                        SmartAddChildren(new InstructionAnalysisNode { Value = x });

                }
                else if (v is FieldInfo)
                {
                    var x = (FieldInfo)v;

                    CreateNode(x.Name);

                    cs.BindImage("field");

                    SmartAddChildren(x.Attributes);

                    AddSimpleCustomNodes(
                        "Field Type",
                        new [] { x.FieldType },
                        "reference"
                    );
                }
                else if (v is Type)
                {
                    var x = (Type)v;

                    CreateNode(x.Name);

                    if (jsx.Extenstions.IsCompilerGenerated(x))
                        n.ForeColor = Color.Blue;
                    else if (!x.IsPublic)
                        n.ForeColor = Color.Gray;

                    if (x.IsInterface)
                    {
                        cs.BindImage("interface");
                    }
                    else if (x.BaseType == typeof(MulticastDelegate))
                    {
                        cs.BindImage("delegate");
                    }
                    else
                    {
                        if (x.IsValueType)
                        {
                            cs.BindImage("struct");
                        }
                        else
                        {
                            cs.BindImage("class");
                        }

                    }

                    SmartAddChildren(x.Attributes);


                    AddCustomNode(
                        "Base Types",
                        delegate(TreeNode cn, ActionParams<object> h)
                        {
                            if (!x.IsInterface &&
                                x != typeof(object))
                            {
                                h(x.BaseType);
                            }

                            h(x.GetInterfaces());
                        } 
                            ,
                        "redo"
                    );




                    if (x.IsGenericType)
                    {

                        AddSimpleCustomNodes(
                            "Generic Parameters",
                            x.GetGenericArguments(),
                            "redo"
                        );


                    }

                    AddCustomNode(
                        "Module",
                        delegate(TreeNode cn, ActionParams<object> h)
                        {
                            h(x.Module);
                        } 
                            ,
                        "references"
                    );



                    if (x.DeclaringType != null)
                    {
                        AddCustomNode(
                            "Declaring Type",
                            delegate(TreeNode cn, ActionParams<object> h)
                            {
                                h(x.DeclaringType);
                            } 
                            ,
                            "undo"
                        );


                    }

                    if (x.IsGenericParameter)
                        if (x.DeclaringMethod != null)
                        {
                            AddCustomNode(
                                "Declaring Method",
                                delegate(TreeNode cn, ActionParams<object> h)
                                {
                                    h(x.DeclaringMethod);
                                } 
                            ,
                                "undo"
                            );


                        }


                    AddSimpleCustomNodes(
                        "Custom Attributes",
                        x.GetCustomAttributes(false),
                        "references"
                    );

                    if (!x.IsInterface)
                    {
                        AddSimpleCustomNodes(
                            "Interface Mapping",
                            x.GetInterfaceMap().Cast<object>().ToArray(),
                            "references"
                        );
                    }

                    var DeclaredBinding =
                        BindingFlags.DeclaredOnly |
                        BindingFlags.NonPublic |
                        BindingFlags.Public |
                        BindingFlags.Instance |
                        BindingFlags.Static;

                    SmartAddChildren(x.GetNestedTypes(DeclaredBinding));

                    SmartAddChildren(x.GetFields(DeclaredBinding));
                    SmartAddChildren(x.GetProperties(DeclaredBinding));
                    SmartAddChildren(x.GetEvents(DeclaredBinding));
                    SmartAddChildren(x.GetConstructors(DeclaredBinding));
                    SmartAddChildren(x.GetMethods(DeclaredBinding));

                }
                else if (v is Module)
                {
                    var x = (Module)v;

                    CreateNode(x.ScopeName);

                    cs.BindImage("module");

                    AddCustomNode(
                        "Assembly",
                        delegate(TreeNode cn, ActionParams<object> h)
                        {
                            h(x.Assembly);
                        } 
                            ,
                        "reference"
                    );



                    SmartAddChildren(
                        x.GetFields()
                    );

                    SmartAddChildren(
                        x.GetMethods()
                    );

                    foreach (var y in x.GetTypes().Where(i => !i.IsNested).GroupBy(i => i.Namespace).OrderBy(i => i.Key))
                    {

                        var y0 = y;

                        AddCustomNode(
                            y.Key ?? "-",
                            delegate(TreeNode cn, ActionParams<object> h)
                            {
                                if (y.Key == null)
                                    cn.ForeColor = Color.Gray;

                                h(y0.ToArray());
                            } 
                            ,
                            "namespace"
                        );



                    }

                }
                else if (v is Assembly)
                {
                    var x = (Assembly)v;

                    CreateNode(x.FullName);
                    cs.BindImage("assembly");

                    AddSimpleCustomNodes(
                        "References",
                        x.GetReferencedAssemblies(),
                        "references"
                    );



                    SmartAddChildren(x.GetModules());


                    if (x.EntryPoint != null)
                    {
                        SmartAddChildren(x.EntryPoint);
                    }



                }
                else if (v is Enum)
                {
                    var x = (Enum)v;

                    CreateNode(x.ToString());
                    cs.BindImage("enum");

                    SmartAddChildren(v.GetType());
                }
                else
                {
                    CreateNode(
                        v.GetType() + " : " + v
                    );

                    SmartAddChildren(v.GetType());

                    n.ForeColor = Color.Red;
                }

                if (n != null)
                {
                    BindBeforeSelect(
                        delegate
                        {
                            if (this.Preview != null)
                                this.Preview(v0);
                        }
                    );

                    yield return n;
                }
            }


            yield break;

        }
    }
}

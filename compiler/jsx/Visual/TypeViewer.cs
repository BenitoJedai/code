using System;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace jsx.Visual
{

    public class CustomNode
    {
        public string Text;

        public string[] ImageKeys;

        public Action<TreeNode, ActionParams<object>> Update;
    }


    class InstructionAnalysisNode
    {
        public Nonnullable<MethodBase> Value;

        public InstructionAnalysis Analysis;


        public static implicit operator InstructionAnalysis(InstructionAnalysisNode e)
        {
            return e.Analysis;
        }
    }

    partial class TypeViewer
    {

        public event Action<object> Preview;

        public ReflectionCache Cache = ReflectionCache.Default;

        public readonly InstructionAnalysis.ToConsoleOptions Options = 
            new InstructionAnalysis.ToConsoleOptions
            {
                ShowStackUsage = false,
                ShowLocalsUsage = false,
                ShowParamsUsage = false 
            };



        void AddOption(ToolStripItemCollection c, string text, Action<bool> h, bool value)
        {
            var e = new ToolStripMenuItem(text);
            e.CheckOnClick = true;
            e.CheckedChanged += delegate
            {
                h(e.Checked);
            };
            c.Add(e);
            e.Checked = value;
        }

        public Thread ShowDialogThreaded(params object[] root)
        {
            var t = new Thread( () => ShowDialog(root) );

            t.SetApartmentState(ApartmentState.STA);

            t.Start();

            return t;
        }

        void ShowDialog(params object[] root)
        {
            var HistoryDisabled = false;

            using (var f = new TypeViewerForm())
            {


                foreach (var v in from i in typeof(InstructionAnalysis.ToConsoleOptions).GetFields()
                                let itype = i.FieldType
                                where itype == typeof(bool)
                                let idesc = (System.ComponentModel.DescriptionAttribute)i.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false).FirstOrDefault()
                                where idesc != null
                                select new { i , idesc.Description })
                {
                    FieldInfo i0 = v.i;

                    AddOption(f.OptionsMenu.Items, v.Description, i =>  i0.SetValue(Options, i), (bool)i0.GetValue(Options));
                }


                f.tree.Font = SystemFonts.MenuFont;

                f.tree.Nodes.Clear();
                f.TreeContext.Opening +=
                    delegate
                    {
                        f.AddToRoot.Visible = false;
                        f.RemoveFromRoot.Visible = false;

                        if (f.tree.SelectedNode != null)
                        {
                            if (f.tree.SelectedNode.Parent != null)
                            {
                                f.AddToRoot.Visible = true;
                            }
                            else
                            {

                                f.RemoveFromRoot.Visible = true;
                            }
                        }



                    };

                ActionParams<object> AddNodesToRoot =
                    delegate(object[] items)
                    {
                        AddNodesTo(f.tree.Nodes, f.tree, null, items);

                    };

                f.AddToRoot.Click +=
                    delegate
                    {
                        // todo: replace node based onclick with tag based.

                        AddNodesToRoot(f.tree.SelectedNode.Tag);

                    };

                f.RemoveFromRoot.Click +=
                    delegate
                    {
                        // todo: remove history

                        var x = f.tree.SelectedNode;


                        f.back.DropDownItems.RemoveWhere(i => i.Tag == x);
                        f.forward.DropDownItems.RemoveWhere(i => i.Tag == x);

                        x.Remove();
                    };


                f.tree.BeforeSelect +=
                    delegate
                    {
                        if (HistoryDisabled)
                            return;

                        if (f.tree.SelectedNode != null)
                        {
                            f.forward.DropDownItems.Clear();
                            f.forward.Enabled = false;

                            f.back.Enabled = true;

                            var t = new ToolStripMenuItem(f.tree.SelectedNode.Text);

                            t.Tag = f.tree.SelectedNode;

                            f.back.DropDownItems.Insert(0, t);
                        }
                    };

                f.forward.Enabled = false;
                f.back.Enabled = false;


                Action UpdateHistory =
                    delegate
                    {
                        if (f.forward.DropDownItems.Count == 0)
                            f.forward.Enabled = false;

                        if (f.back.DropDownItems.Count == 0)
                            f.back.Enabled = false;
                    };

                Action<ToolStripSplitButton, TreeNode> AddHistory =
                    delegate(ToolStripSplitButton e, TreeNode n)
                    {
                        if (n == null)
                            throw new NullReferenceException();

                        HistoryDisabled = true;

                        var t = new ToolStripMenuItem(f.tree.SelectedNode.Text);
                        t.Tag = f.tree.SelectedNode;

                        e.DropDownItems.Insert(0, t);
                        e.Enabled = true;

                        f.tree.SelectedNode = n;

                        HistoryDisabled = false;


                        UpdateHistory();
                    };

                f.forward.ButtonClick +=
                    delegate
                    {
                        if (f.forward.DropDownItems.Count == 0)
                            throw new NotSupportedException();

                        AddHistory(f.back, f.forward.DropDownItems.PopAsTag<TreeNode>(0));
                    };

                f.back.ButtonClick +=
                    delegate
                    {
                        if (f.back.DropDownItems.Count == 0)
                            throw new NotSupportedException();

                        AddHistory(f.forward, f.back.DropDownItems.PopAsTag<TreeNode>(0));
                    };




                AddNodesToRoot(root);

                f.ShowDialog();
            }
        }

        public void AddNodesTo(TreeNodeCollection c, TreeView t, Action<Action> BindParentClick, params object[] root)
        {
            c.AddRange(ConvertToNodes(t, BindParentClick, root).ToArray());
        }



    }
}

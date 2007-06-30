using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace jsx
{


    public partial class InstructionAnalysis
    {
        public static string GetSymbolName(string e)
        {
            string xname = "";

            foreach (var xv in e.ToCharArray())
            {
                if ((int)xv > 0xFF)
                {
                    xname += string.Format(@"\u{0:x4}", (int)xv);
                }
                else
                    xname += xv;
            }

            return xname;
        }

        public class ToConsoleOptions
        {
            public Func<Instruction, bool> InstructionFilter;

            [Description("Show Stack Usage")]
            public bool ShowStackUsage = true;

            [Description("Show Locals Usage")]
            public bool ShowLocalsUsage = true;

            [Description("Show Params Usage")]
            public bool ShowParamsUsage = true;

            [Description("Show Branch Endpoint")]
            public bool ShowBranchEndpoint = false;

            [Description("Show info for switch")]
            public bool ShowInfoForSwitch = true;

            [Description("Show info for loop")]
            public bool ShowInfoForLoop = true;


        }

        public void ToConsole()
        {
            ToConsole(new ToConsoleOptions());
        }


        public void ToConsole(ToConsoleOptions options)
        {
            var filter = options.InstructionFilter;

            var ShowStackChange = false;

            int BranchInMax = Instructions.Where(i => i.BranchIn != null).Select(i => i.BranchIn.Count()).Max();

            if (BranchInMax > 2)
                BranchInMax = 2;

            int OpCodeMax = Instructions.Select(i => (i.Value.Name.Length + (i.StackSizeOut ?? 0) * 2)).Max();
            int FlowControlMax = Instructions.Select(i => i.Value.FlowControl.ToString().Length + 1 + i.Value.OpCodeType.ToString().Length).Max();


            Action WriteIdent = () => Console.Write(new string(' ', BranchInMax * 7));

            if (BranchUsage != null)
                BranchUsage.ToConsole();

            WriteIdent();
            MethodToConsole(Value);

            #region params
            Console.WriteLine();

            if (this.Parameters != null)
            {
                foreach (VariableUsage.Parameter v in this.Parameters.Values)
                {
                    WriteIdent();
                    Console.Write("parameter ");

                    using (new ConsoleColorText(ConsoleColor.Yellow))
                        Console.Write("arg+" + v.Index + " ");

                    using (new ConsoleColorText(v.TargetType.IsCompilerGenerated() ? ConsoleColor.Cyan : ConsoleColor.DarkCyan))
                        Console.Write(GetSymbolName(v.TargetType.Name) + " ");

                    Console.Write(new String(' ', 24 - v.TargetType.Name.Length) + "providers: " + v.Providers.Length + " clients: " + v.Clients.Length);
                    Console.WriteLine();
                }
            }


            #endregion

            #region rdm
            Console.WriteLine();

            foreach (var v in this.ReferencedDelegateMethods.OrderBy(
                i => i.IsStatic).ThenBy(
                i => i.DeclaringType.Name).ThenBy(
                i => i.Name
                ))
            {
                WriteReferencedCall(v, "delegate");
            }


            #endregion

            #region rcm
            Console.WriteLine();

            foreach (MethodBase v in this.ReferencedCallMethods.OrderBy(
                 i => i.IsStatic).ThenBy(
                 i => (i.DeclaringType == null ? null : i.DeclaringType.Name)).ThenBy(
                 i => i.Name
                 ))
            {

                WriteReferencedCall(v, "call");
            }


            #endregion

            #region rf
            Console.WriteLine();

            foreach (FieldInfo v in this.ReferencedFields.OrderBy(
                 i => i.IsStatic).ThenBy(
                 i => (i.DeclaringType == null ? null : i.DeclaringType.Name)).ThenBy(
                 i => i.Name
                 ))
            {

                WriteReferencedField(v);
            }


            #endregion

            #region locals
            Console.WriteLine();

            if (this.Locals != null)
            {
                foreach (VariableUsage.LocalVariable v in this.Locals.Values)
                {
                    WriteIdent();
                    Console.Write("local ");

                    using (new ConsoleColorText(ConsoleColor.Yellow))
                        Console.Write("var+" + v.Index + " ");

                    Console.Write("0x{0:x8} ", v.TargetVariable.LocalType.MetadataToken);


                    using (new ConsoleColorText(v.TargetType.IsCompilerGenerated() ? ConsoleColor.Cyan : ConsoleColor.DarkCyan))
                        Console.Write(GetSymbolName(v.TargetType.Name) + " ");

                    Console.Write(new String(' ', 30 - v.TargetType.Name.Length) + "providers: " + v.Providers.Length + " clients: " + v.Clients.Length);


                    Console.WriteLine();
                }
            }
            #endregion

            Console.WriteLine();
            var all = Instructions.AsEnumerable();

            if (filter != null)
                all = all.Where(filter);

            foreach (Instruction i in all)
            {

                var is_try = Body.ExceptionHandlingClauses.Any(v => v.TryOffset == i.Offset);
                var try_handler = Body.ExceptionHandlingClauses.SingleOrDefault(v => v.HandlerOffset == i.Offset);
                var is_handler = try_handler != null;


                if (is_try)
                    using (new ConsoleColorText(ConsoleColor.Blue))
                        Console.WriteLine(new string(' ', BranchInMax * 7) + ".try");
                if (is_handler)
                    using (new ConsoleColorText(ConsoleColor.Blue))
                        Console.WriteLine(new string(' ', BranchInMax * 7) + ".handler " + try_handler.Flags);

                var is_jumpin = i.BranchIn != null && i.BranchIn.Length > 1;

                if (is_jumpin)
                    Console.WriteLine();

                #region switch case

                if (i.BranchIn != null)
                {
                    foreach (var vn in i.BranchIn.Distinct().Where(vi => vi.Value == OpCodes.Switch))
                    {



                        for (int xi = 0; xi < vn.BranchOut.Length; xi++)
                        {
                            if (vn.BranchOut[xi] == i)
                            {
                                if (xi == 0)
                                    using (ConsoleColorText.Cyan)
                                        Console.WriteLine("[0x{0:x4}] default: ", vn.Offset);
                                else
                                    using (ConsoleColorText.Cyan)
                                        Console.WriteLine("[0x{0:x4}] case {1}: ", vn.Offset, xi - 1);
                            }
                        }

                    }
                }

                #endregion


                var BranchInPad = BranchInMax - (i.BranchIn == null ? 0 : i.BranchIn.Count());

                if (BranchInPad > 0)
                    Console.Write(new string(' ', BranchInPad * 7));

                if (i.BranchIn != null)
                {
                    for (int vi = 0; vi < i.BranchIn.Length; vi++)
                    {
                        var v = i.BranchIn[vi];

                        if (vi > 0 && (vi % BranchInMax == 0))
                            Console.WriteLine();


                        using (new ConsoleColorText(v.Value.FlowControl == FlowControl.Cond_Branch ? ConsoleColor.Cyan : ConsoleColor.DarkCyan))
                            //Console.Write("0x{0:x4} -> 0x{1:x4} ",  this.BranchInByOffset[i.Offset][vi].Offset, v.Offset);
                            Console.Write("0x{0:x4} ", v.Offset);

                    }

                    if (i.BranchIn.Length > BranchInMax && (i.BranchIn.Length % BranchInMax != 0))
                    {
                        Console.WriteLine();
                        Console.Write(new string(' ', BranchInMax * 7));
                    }
                }



                if (i == EntryPoint)
                    using (new ConsoleColorText(ConsoleColor.White))
                        Console.Write("0x{0:x4}", i.Offset);
                else if (Body.ExceptionHandlingClauses.Any(v => v.HandlerOffset == i.Offset))
                    using (new ConsoleColorText(ConsoleColor.Red))
                        Console.Write("0x{0:x4}", i.Offset);
                else if (i.StackSizeIn > 0 && i.StackSizeOut == 0)
                    using (new ConsoleColorText(ConsoleColor.Yellow))
                        Console.Write("0x{0:x4}", i.Offset);
                else
                    using (new ConsoleColorText(ConsoleColor.Gray))
                        Console.Write("0x{0:x4}", i.Offset);



                using (new ConsoleColorText(ConsoleColor.Red))
                    Console.Write(i.StackSizeIn == null ? " -" : " " + i.StackSizeIn);

                if (ShowStackChange)
                    using (new ConsoleColorText(ConsoleColor.White))
                        Console.Write((i.Value.StackBehaviourPop + " " + i.StackPopCount).PadLeft(24));

                var StackSizeIn = ((i.StackSizeIn ?? 0) <= 0 ? 0 : i.StackSizeIn.Value * 2);
                var StackSizeOut = ((i.StackSizeOut ?? 0) <= 0 ? 0 : i.StackSizeOut.Value * 2);

                using (new ConsoleColorText(i.StackPushCount == 0 ?
                    (i.StackPopCount == 0 ? ConsoleColor.Blue :
                    ConsoleColor.Red) : ConsoleColor.Yellow))
                    Console.Write(" " + new string(' ', StackSizeIn) + i.Value.Name);

                var OpCodeNamePad = i.Value.Name.Length;

                var UPad = OpCodeMax - OpCodeNamePad - StackSizeIn;

                if (UPad > 0)
                    Console.Write(new string(' ', UPad));

                using (new ConsoleColorText(ConsoleColor.Green))
                    Console.Write(" " + i.Value.FlowControl);

                Console.Write(new string(' ', FlowControlMax
                    - i.Value.FlowControl.ToString().Length
                    - i.Value.OpCodeType.ToString().Length
                    - 1
                     ));



                using (new ConsoleColorText(ConsoleColor.Green))
                    Console.Write(" " + i.Value.OpCodeType);



                if (ShowStackChange)
                    using (new ConsoleColorText(ConsoleColor.White))
                        Console.Write((i.Value.StackBehaviourPush + " " + i.StackPushCount).PadLeft(24));

                using (new ConsoleColorText(ConsoleColor.Red))
                    Console.Write(i.StackSizeOut == null ? " -" : " " + i.StackSizeOut);




                if (i.BranchOut != null)
                    using (new ConsoleColorText(i.Value.FlowControl == FlowControl.Cond_Branch ? ConsoleColor.Green : ConsoleColor.DarkGreen))
                        for (int xv = 0; xv < i.BranchOut.Length; xv++)
                        {
                            var br = i.BranchOut[xv];
                            var brx = i.DirectBranchOut[xv];

                            if (options.ShowBranchEndpoint && br != brx)
                            {
                                using (ConsoleColorText.Magenta)
                                    Console.Write(" 0x{0:x4}", brx.Offset);
                            }
                            else
                                Console.Write(" 0x{0:x4}", br.Offset);

                            //Console.Write(" 0x{0:x4}", i.BranchOut[xv].Offset);
                            //Console.Write(" 0x{0:x4}", i.BranchOut[xv].Offset);

                            //Console.Write(" 0x{0:x4} -> 0x{1:x4}", i.BranchOut[xv].Offset, this.BranchOutByOffset[i.Offset][xv].Offset);
                        }


                if (i.TargetInteger != null)
                {
                    Console.Write(" int {0}", i.TargetInteger);
                }

                WriteAdditionalInfo(i, i);

                Console.WriteLine();

                if (options.ShowInfoForSwitch)
                    if (i.Value == OpCodes.Switch)
                        ShowInfoForSwitch(i);

                if (options.ShowInfoForLoop)
                    if (i.Value.FlowControl == FlowControl.Cond_Branch)
                        if (i.Value != OpCodes.Switch)
                            ShowInfoForLoop(i);


                var is_endtry = Body.ExceptionHandlingClauses.Any(v => v.TryOffset + v.TryLength == i.NextOffset);
                var is_endhandler = Body.ExceptionHandlingClauses.Any(v => v.HandlerOffset + v.HandlerLength == i.NextOffset);
                var is_jump = i.BranchOut == null || !i.BranchOut.All(vi => vi.Offset == i.NextOffset);



                if (is_endtry)
                    using (new ConsoleColorText(ConsoleColor.Blue))
                        Console.WriteLine(new string(' ', BranchInMax * 7) + ".endtry");
                else if (is_endhandler)
                    using (new ConsoleColorText(ConsoleColor.Blue))
                        Console.WriteLine(new string(' ', BranchInMax * 7) + ".endhandler");
                else if (is_jump)
                {
                    Console.WriteLine();
                }
            }

            if (Locals != null)
            {
                if (options.ShowLocalsUsage)
                    Locals.ToConsole();

                if (options.ShowParamsUsage)
                    Parameters.ToConsole();

            }

            if (options.ShowStackUsage)
                if (StackUsage != null)
                    StackUsage.ToConsole(this);

            foreach (ExceptionHandlingClause v in this.Body.ExceptionHandlingClauses)
            {
                using (new ConsoleColorText(ConsoleColor.Blue))
                    Console.Write(".try ");

                Console.Write("0x{0:x4} ", v.TryOffset);

                using (new ConsoleColorText(ConsoleColor.Blue))
                    Console.Write("to ");

                Console.Write("0x{0:x4} ", v.TryOffset + v.TryLength);

                using (new ConsoleColorText(ConsoleColor.Blue))
                    Console.Write(v.Flags + " ");

                Console.Write("0x{0:x4} ", v.HandlerOffset);

                using (new ConsoleColorText(ConsoleColor.Blue))
                    Console.Write("to ");

                Console.Write("0x{0:x4} ", v.HandlerOffset + v.HandlerLength);

                Console.WriteLine();
            }


            MyPerformance.ToConsole();

        }

        private void ShowInfoForLoop(Instruction i)
        {
            if (i.DirectBranchOut.Length == 2)
            {
                if (i.DirectBranchOut[0].Offset < i.Offset)
                {
                    Console.WriteLine("  while (...)");
                    Console.WriteLine("  {");

                    Console.WriteLine("    {0}", i.DirectBranchOut[0]);

                    Console.WriteLine("  }");

                    Console.WriteLine("  {0}", i.DirectBranchOut[1]);
                }
            }


        }

        private static void ShowInfoForSwitch(Instruction i)
        {
            var groups = InstructionInfo.GetSwitchCases(i).GroupBy(xi => xi.IsReturnStatementArgument).ToDictionary(xi => xi.Key);

            if (!groups.ContainsKey(true) || !groups.ContainsKey(false))
                return;

            Console.WriteLine("switch (...)");
            Console.WriteLine("{");




            foreach (var vcase in groups[true].GroupBy(xi => xi.Target))
            {
                foreach (var vcasex in vcase)
                    if (vcasex.Index == -1)
                        Console.WriteLine("  default:");
                    else
                        Console.WriteLine("  case {0}:", vcasex.Index);

                Console.WriteLine("    return {0};", vcase.Key);
            }

            var vcases = groups[false].ToArray();

            foreach (var vcase in vcases)
                if (vcase.Index == -1)
                    Console.WriteLine("  default:");
                else
                    Console.WriteLine("  case {0}:", vcase.Index);



            // this code now shares the same ending and returns





            if (vcases.Length == 2)
            {
                var a = vcases[0];
                var b = vcases[1];

                var c = InstructionInfo.FindJoinPoint(a.Target, b.Target);

                if (c != null)
                {
                    Console.WriteLine("    switch (...) ");
                    Console.WriteLine("    { ");
                    Console.WriteLine("      case {0}:", a.Index);
                    Console.WriteLine("        //block: {0} -> {1}", a.Target, c);
                    Console.WriteLine("        break;");
                    Console.WriteLine("      case {0}:", b.Index);
                    Console.WriteLine("        //block: {0} -> {1}", b.Target, c);
                    Console.WriteLine("        break;");
                    Console.WriteLine("    } ");

                    var f = InstructionInfo.FindBranchOutInstructions(c).GetBranchInInstruction().Where(InstructionInfo.IsReturnStatementArgument).Single();

                    Console.WriteLine("    //block: {0} -> {1}", c, f);
                }
            }
            else if (vcases.Length == 3)
            {
                var a = vcases[0];
                var b = vcases[1];

                var c = InstructionInfo.FindJoinPoint(a.Target, b.Target);

                if (c != null)
                {
                    var d = vcases[2];
                    var e = InstructionInfo.FindJoinPoints(c, d.Target).WhereNot(InstructionInfo.IsReturnStatementArgument).Single();


                    Console.WriteLine("    switch (...) ");
                    Console.WriteLine("    { ");
                    Console.WriteLine("      case {0}:", a.Index);
                    Console.WriteLine("      case {0}:", b.Index);

                    Console.WriteLine("        switch (...) ");
                    Console.WriteLine("        { ");
                    Console.WriteLine("          case {0}:", a.Index);
                    Console.WriteLine("            //block: {0} -> {1}", a.Target, c);
                    Console.WriteLine("            break;");
                    Console.WriteLine("          case {0}:", b.Index);
                    Console.WriteLine("            //block: {0} -> {1}", b.Target, c);
                    Console.WriteLine("            break;");
                    Console.WriteLine("        } ");

                    Console.WriteLine("        //block: {0} -> {1}", c, e);
                    Console.WriteLine("        break;");


                    Console.WriteLine("      case {0}:", d.Index);


                    Console.WriteLine("        //block: {0} -> {1}", d.Target, e);

                    Console.WriteLine("        break;");
                    Console.WriteLine("    } ");

                    var bout = InstructionInfo.FindBranchOutInstructions(e).GetBranchInInstruction();
                    var vret = bout.Where(InstructionInfo.IsReturnStatementArgument);



                    var f = vret.SingleOrDefault();

                    if (f != null)
                        Console.WriteLine("    //block: {0} -> {1}", e, f);
                    else
                        Console.WriteLine("    // ???");

                }
            }

            

            Console.WriteLine("    ... ");


            Console.WriteLine("}");
        }

        private void WriteAdditionalInfo(StackUsage.Value i, Instruction context)
        {
            if (i.ComplexFlag)
                Console.Write("...");
            else
            {
                if (i.Provider == null)
                {
                    Console.Write("(null)");
                }
                else
                    WriteAdditionalInfo((Instruction)i, context);
            }
        }

        private void WriteAdditionalInfo(Instruction i, Instruction context)
        {
            // if i is used, skip it

            var s = StackUsage[context.Offset];

            if (s.ByProvider == null)
                return;

            if (s.ByProvider.Length > 0)
            {
                if (s.ByProvider.Length == 1)
                {
                    var p = s.ByProvider[0];
                    var c = p.ClientList;

                    if (p.ClientList.Count > 0)
                        return;
                }
            }

            var args = StackUsage.ByClient[i.Offset];

            if (i.Value == OpCodes.Ldnull)
            {
                using (ConsoleColorText.Blue)
                    Console.Write("null");
            }
            else if (i.Value == OpCodes.Pop)
            {
                using (ConsoleColorText.Blue)
                    Console.Write(" pop");

                WriteAdditionalInfo(args[0], context);
            }
            else if (i.Value == OpCodes.Conv_I4)
            {
                using (ConsoleColorText.Blue)
                    Console.Write(" (int)");

                WriteAdditionalInfo(args[0], context);
            }
            else if (i.Value == OpCodes.Dup)
            {
                WriteAdditionalInfo(args[0], context);
            }
            else if (i.Value == OpCodes.Ldlen)
            {
                WriteAdditionalInfo(args[0], context);

                using (ConsoleColorText.Blue)
                    Console.Write(".length");

            }
            else if (i.Value == OpCodes.Add)
            {
                Console.Write("(");

                WriteAdditionalInfo(args[0], context);
                Console.Write(" + ");

                WriteAdditionalInfo(args[1], context);
                Console.Write(")");
            }
            else if (i.Value == OpCodes.Sub)
            {
                Console.Write("(");

                WriteAdditionalInfo(args[0], context);
                Console.Write(" - ");

                WriteAdditionalInfo(args[1], context);
                Console.Write(")");
            }
            //else if (i.Value == OpCodes.Ldelem)
            //{

            //}

            if (i.TargetVariableIndex != null)
                using (new ConsoleColorText(ConsoleColor.White))
                {


                    if (i.TargetVariableStoreIndex != null)
                    {
                        Console.Write(" var {0}", i.TargetVariableIndex);

                        Console.Write(" =");

                        WriteAdditionalInfo(args[0], context);

                    }
                    else if (i.TargetVariableLoadIndex != null)
                    {
                        Console.Write("var {0}", i.TargetVariableIndex);

                        VariableUsage.ClientInfo u = Locals.Values[i.TargetVariableLoadIndex.Value].Clients.Single(v => v.TargetInstruction == i);

                        if (u.Providers.Length == 1)
                        {
                            Console.Write(" = 0x{0:x4}", u.Providers[0].EntryPoint.Offset);

                            //WriteStackIntegerValue(StackUsage.ByClient[u.Providers[0].EntryPoint.Offset].Single(), Locals.Values[i.TargetVariableLoadIndex.Value]);

                            //WriteAdditionalInfo(args[0], context);
                        }
                    }
                }

            if (i.TargetField != null)
                using (new ConsoleColorText(ConsoleColor.White))
                {


                    if (i.Value == OpCodes.Ldfld)
                    {


                        WriteAdditionalInfo(args[0], context);

                        //Console.Write("->0x{0:x8}", i.TargetField.MetadataToken);
                        Console.Write(".{0}", i.TargetField.Name);

                    }
                    else if (i.Value == OpCodes.Stfld)
                    {


                        WriteAdditionalInfo(args[0], context);

                        Console.Write(".{0}", i.TargetField.Name);
                        Console.Write(" =");

                        WriteAdditionalInfo(args[1], context);
                    }
                    else
                    {
                        Console.Write(" ({1}) 0x{0:x8}", i.TargetField.MetadataToken, args.Length);
                    }
                }

            if (i.TargetInteger != null)
                using (new ConsoleColorText(ConsoleColor.White))
                {
                    var xs = StackUsage.ByProvider[i.Offset][0];

                    if (xs.ClientList.Count == 1)
                    {
                        var xsc = xs.ClientList[0];

                        Type xst = null;

                        if (xsc.TargetVariableStoreIndex != null)
                            xst = Locals.Values[xsc.TargetVariableStoreIndex.Value].TargetType;

                        if (xsc.TargetField != null)
                            xst = xsc.TargetField.FieldType;


                        if (xst != null)
                        {
                            if (xst == typeof(bool))
                            {
                                if (i.TargetInteger == 0)
                                    Console.Write(" false");
                                else
                                    Console.Write(" true");
                            }
                            else if (xst == typeof(int))
                            {
                                Console.Write(" " + i.TargetInteger);
                            }
                            else
                                Console.Write(" (int) " + i.TargetInteger);
                        }
                        else
                            Console.Write(" (int) " + i.TargetInteger);
                    }
                    else
                        Console.Write(" (int) " + i.TargetInteger);
                }

            if (i.TargetString != null)
                using (new ConsoleColorText(ConsoleColor.White))
                    Console.Write(" '" + InstructionAnalysis.GetSymbolName(i.TargetString) + "'");

            if (i.TargetParameterIndex != null)
                using (new ConsoleColorText(ConsoleColor.White))
                {
                    if (i.TargetParameterIndex == -1)
                        Console.Write(" this");
                    else
                    {
                        Console.Write(" arg " + i.TargetParameterIndex);

                    }

                }


            if (i.Value.FlowControl == FlowControl.Cond_Branch)
            {
                Console.Write(" branch(");

                WriteAdditionalInfo(args[0], context);

                Console.Write(" )");
            }

            if (i.Value.FlowControl == FlowControl.Call)
            {
                using (new ConsoleColorText(ConsoleColor.White))
                {
                    var m = i.TargetMethod;

                    if (i.Value == OpCodes.Callvirt)
                    {
                        if (m.IsStatic)
                        {
                            Console.Write(" 0x{0:x8}()", i.TargetMethod.MetadataToken);
                        }
                        else
                        {
                            Console.Write(" ");
                            Console.Write("(");

                            WriteAdditionalInfo(args[0], context);
                            //Console.Write("->0x{0:x8}()", i.TargetMethod.MetadataToken);

                            Console.Write(")");

                            Console.Write(".");

                            ConsoleWriteNameOrMetadata(i.TargetMethod.MetadataToken, i.TargetMethod.Name);

                            Console.Write("(");

                            var param = i.TargetMethod.GetParameters();

                            if (param.Length > 0)
                            {
                                for (int xi = 0; xi < param.Length; xi++)
                                {
                                    if (xi > 0)
                                        using (ConsoleColorText.Magenta)
                                            Console.Write(", ");
                                    WriteAdditionalInfo(args[xi + 1], context);

                                }
                            }

                            Console.Write(")");
                        }
                    }
                    else
                    {
                        if (i.Value == OpCodes.Newobj)
                        {
                            using (ConsoleColorText.Blue)
                                Console.Write(" new ");

                            ConsoleWriteNameOrMetadata(i.TargetMethod.DeclaringType.MetadataToken, i.TargetMethod.DeclaringType.Name);
                            Console.Write(".");
                            ConsoleWriteNameOrMetadata(i.TargetMethod.MetadataToken, i.TargetMethod.Name);
                            Console.Write("(");

                            var param = i.TargetMethod.GetParameters();

                            if (param.Length > 0)
                            {
                                for (int xi = 0; xi < param.Length; xi++)
                                {
                                    if (xi > 0)
                                        using (ConsoleColorText.Magenta)
                                            Console.Write(", ");
                                    WriteAdditionalInfo(args[xi], context);

                                }
                            }

                            Console.Write(")");



                        }
                        else
                        {
                            Console.Write(" static ");

                            ConsoleWriteNameOrMetadata(i.TargetMethod.MetadataToken, i.TargetMethod.Name);


                            Console.Write("(");

                            var param = i.TargetMethod.GetParameters();

                            if (param.Length > 0)
                            {
                                for (int xi = 0; xi < param.Length; xi++)
                                {
                                    if (xi > 0)
                                        using (ConsoleColorText.Magenta)
                                            Console.Write(", ");

                                    WriteAdditionalInfo(args[xi], context);

                                }
                            }

                            Console.Write(")");
                        }
                    }
                }

            }

            if (i.Value == OpCodes.Ret)
            {
                Console.Write(" return ");

                if (HasReturnParameter)
                {
                    WriteAdditionalInfo(args[0], context);
                }
            }
        }

        private void ConsoleWriteNameOrMetadata(int meta, string name)
        {
            if (name.Length > 2)
                using (new ConsoleColorText(ConsoleColor.Yellow)) Console.Write(name);
            else
                using (new ConsoleColorText(ConsoleColor.Cyan)) Console.Write("0x{0:x8}", meta);
        }


        private void WriteReferencedField(FieldInfo v)
        {
            using (new ConsoleColorText(v.Module == Value.Module ? ConsoleColor.Yellow : ConsoleColor.DarkYellow))
                Console.Write("{0} 0x{1:x8} field ", v.Module.Name.PadLeft(32), v.MetadataToken);

            if (v.IsStatic)
                Console.Write("static ");


            using (new ConsoleColorText(v.DeclaringType.IsCompilerGenerated() ? ConsoleColor.Cyan : ConsoleColor.DarkCyan))
                Console.Write(v.DeclaringType == null ? "global " : v.DeclaringType.Name + " ");

            using (new ConsoleColorText(v.FieldType.IsCompilerGenerated() ? ConsoleColor.White : ConsoleColor.Gray))
                Console.Write(v.FieldType.Name + " ");

            using (new ConsoleColorText(v.IsCompilerGenerated() ? ConsoleColor.Cyan : ConsoleColor.DarkCyan))
                Console.Write(GetSymbolName(v.Name));




            Console.WriteLine();
        }

        private void WriteReferencedCall(MethodBase v, string text)
        {
            using (new ConsoleColorText(v.Module == Value.Module ? ConsoleColor.Yellow : ConsoleColor.DarkYellow))
                Console.Write("{0} 0x{1:x8} {2} ", v.Module.Name.PadLeft(32), v.MetadataToken, text);

            if (v.IsStatic) { }
            Console.Write("static ");


            using (new ConsoleColorText(v.DeclaringType.IsCompilerGenerated() ? ConsoleColor.Cyan : ConsoleColor.DarkCyan))
                Console.Write(v.DeclaringType == null ? "global " : GetSymbolName(v.DeclaringType.Name) + " ");

            using (new ConsoleColorText(v.IsCompilerGenerated() ? ConsoleColor.Cyan : ConsoleColor.DarkCyan))
                Console.Write(GetSymbolName(v.Name));




            Console.WriteLine();
        }

        public static void MethodToConsole(MethodBase m)
        {
            Console.Write("{0} 0x{1:x8} ", m.Module.ScopeName, m.MetadataToken);
            using (new ConsoleColorText(ConsoleColor.White))
                Console.Write((m.DeclaringType == null ? "global" : GetSymbolName(m.DeclaringType.FullName)) + " " + GetSymbolName(m.Name));

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ConvertASToCS.js.Any
{
    [Script]
    public class ProxyProvider
    {
        [Script]
        public class MethodParametersInfo
        {
            [Script]
            public class ParamInfo
            {
                public string Name;
                public string TypeName;

       
            }

            public readonly ParamInfo[] Parameters;

            public MethodParametersInfo(string e)
            {
                Parameters = e.Split(',').Select(i => i.Trim()).Where(i => !string.IsNullOrEmpty(i)).Select(
                    text =>
                    {
                       
                        var z = text.Split(' ');

                  
                        return new ParamInfo
                        {
                            Name = z[1].Trim(),
                            TypeName = z[0].Trim(),
                        };
                    }
                ).ToArray();
            }
        }

        [Script]
        public class MethodDefinition
        {
            public string Signature;
            public string Name;

            public MethodParametersInfo ParametersInfo { get; private set; }


            static public MethodDefinition Parse(string z)
            {
                // scan this line


                var a = z.IndexInfoOf(" ");
                
                if (a.Index == -1)
                    return null;

                // name
                var b = a.IndexInfoOf("(");

                if (b.Index == -1)
                    return null;

                // params
                var c = b.IndexInfoOf(")");

                if (c.Index == -1)
                    return null;

                return new MethodDefinition
                {
                     Name = a.SubString(b),
                     Signature = z,
                     ParametersInfo = new MethodParametersInfo(b.SubString(c))
                };
            }
        }

        public readonly List<MethodDefinition> MethodDefinitions = new List<MethodDefinition>();

        public ProxyProvider(string text)
        {
            var p = text.IndexInfoOf("");

            while (p.Index != -1)
            {
                var t = p.IndexInfoOf("\n");

                if (t.Index != -1)
                {
                    var z = p.SubString(t).Trim();

                    Console.WriteLine("try parse " + z);

                    var n = MethodDefinition.Parse(z);

                    if (n != null)
                    {
                        MethodDefinitions.Add(n);
                        Console.WriteLine("ok: " + z);
                    }

                    
                }

                p = t;
            }

        }
    }
}

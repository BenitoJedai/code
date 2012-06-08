using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;

namespace GEXFxPAST
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var f = @"C:\Users\Arvo\Documents\foo.gexf";
            var g = XElement.Load(f);

            var graph = g.Element("graph");
            var nodes = graph.Element("nodes").Elements("node").Select(
                k => new
                {
                    id = int.Parse(k.Attribute("id").Value),
                    label = k.Attribute("label").Value
                }
            ).GroupBy(k => k.id).OrderBy(k => k.Key).ToArray();

            var edges = graph.Element("edges").Elements("edge").Select(
                k => new { source = int.Parse(k.Attribute("source").Value), target = int.Parse(k.Attribute("target").Value) }
            ).GroupBy(k => k.source).OrderBy(k => k.Key).ToArray();

            var w = new StringBuilder();

            w.Append(".");

            foreach (var node in nodes)
            {
                w.Append("\t");
                w.Append("\"" + node.First().label + "\"");
            }

            w.AppendLine();

            foreach (var node in nodes)
            {
                w.Append("\"" + node.First().label + "\"");

                foreach (var targetnode in nodes)
                {
                    w.Append("\t");

                    var edge_source_by_source = edges.FirstOrDefault(k => k.Key == node.Key);

                    if (edge_source_by_source == null)
                    {
                        w.Append("0");
                        continue;
                    }

                    var edge_target_by_source = edge_source_by_source.FirstOrDefault(k => k.target == targetnode.Key);

                    if (edge_target_by_source == null)
                        w.Append("0");
                    else
                        w.Append("1");
                }

                w.AppendLine();
            }

            File.WriteAllText(f + ".dat", w.ToString());
        }

    }
}

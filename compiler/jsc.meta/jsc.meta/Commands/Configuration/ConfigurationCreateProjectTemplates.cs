using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.Ultra.Studio;
using System.IO;
using ScriptCoreLib.Ultra.Studio.Languages;
using jsc.meta.Commands.Rewrite.RewriteToVSProjectTemplate;
using ScriptCoreLib.Extensions;

namespace jsc.meta.Commands.Configuration
{
    [Description("This command will create project templates to be installed with jsc. Templates can be created anytime, before install, after and at install.")]
    public class ConfigurationCreateProjectTemplates : CommandBase
    {
        // Project to template:
        // RewriteToMVSProjectTemplate.cs

        // Actually the studio.jsc-solutions.net could benefit from a New Project dialog :)

        public override void Invoke()
        {
            Action<SolutionBuilder> Create =

                sln => (
                        //from DefaultToOrcas in new[] { true, false }
                        from Language in KnownLanguages.GetLanguages()

                        // exclude F# from 2008
                        //where !DefaultToOrcas || (DefaultToOrcas && Language != KnownLanguages.VisualFSharp)

                        select (Action)(() => RewriteToMVSProjectTemplate(sln, Language, DefaultToOrcas: false))
                ).Invoke();


            Create(
                new SolutionBuilder
                {
                    // Ultra?
                    Name = "Browser Application",
                }
            );

            Create(
                new SolutionBuilder
                {
                    // Ultra?
                    Name = "Browser Avalon Application",
                }.WithCanvas()
            );


            Create(
                new SolutionBuilder
                {
                    // Ultra?
                    Name = "Browser Avalon Application With Adobe Flash",

                }.WithCanvasAdobeFlash()
            );

            Create(
                new SolutionBuilder
                {
                    // Ultra?
                    Name = "Browser Application With Java Applet",
                }.WithJavaApplet()
            );

            Create(
                new SolutionBuilder
                {
                    // Ultra?
                    
                    Name = "Browser Application With Adobe Flash",
                }.WithAdobeFlash()
            );
        }

        private static void RewriteToMVSProjectTemplate(SolutionBuilder sln, SolutionProjectLanguage Language, bool DefaultToOrcas)
        {
            var staging = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(staging);

            sln.Language = Language;

            var ProjectFileName = default(FileInfo);
            sln.WriteTo(
                f =>
                {
                    var p = Path.Combine(staging, f.Name);
                    var d = Path.GetDirectoryName(p);

                    Directory.CreateDirectory(d);

                    Console.WriteLine(p);

                    File.WriteAllText(p, f.Content);

                    if (f.Name == sln.SolutionProjectFileName)
                    {
                        ProjectFileName = new FileInfo(p);
                    }
                }
            );

            new RewriteToMVSProjectTemplate
            {
                ProjectFileName = ProjectFileName,
                DefaultToOrcas = DefaultToOrcas,
                AssemblyAttributes = new RewriteToMVSProjectTemplate.AssemblyAttributesType
                {
                    Company = sln.Company,
                    Description = sln.Description,
                    Title = sln.Name
                }
            }.Invoke();

            // The directory is not empty.
            Directory.Delete(staging, true);
        }
    }
}

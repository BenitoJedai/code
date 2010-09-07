using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.Extensions;
using System.Diagnostics;

namespace jsc.meta.Commands.Configuration
{
    public class ConfigurationClean : CommandBase
    {
        public DirectoryInfo Target;

        public override void Invoke()
        {
            var errors = new { t = default(DirectoryInfo), e = default(Exception) }.ToEmptyList();

            Action<DirectoryInfo> TakeAction =
                t =>
                {
                    var Actions = t.GetFiles().SelectAction(f => () => f.Delete()).Concat(
                        t.GetDirectories().SelectAction(f => () => f.Delete(true))
                    );

                    var svn = t.GetDirectories(".svn").Any();

                    if (svn)
                    {
                        Console.WriteLine("svn: " + t.FullName);
                        return;
                    }

                    if (Actions.Any())
                        try
                        {
                            Console.WriteLine("clean: " + t.FullName);

                            Actions.Invoke();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("error: " + t.FullName);
                            errors.Add(
                                new { t, e }
                            );
                        }
                };

            Action<DirectoryInfo> Clean = null;

            Clean = t =>
            {
                t.GetDirectories().WithEach(
                    s =>
                    {
                        if (s.Name == "bin")
                        {
                            TakeAction(s);
                            return;
                        }

                        if (s.Name == "obj")
                        {
                            TakeAction(s);
                            return;
                        }

                        Clean(s);
                    }
                );
            };

            Clean(Target);
        }
    }
}

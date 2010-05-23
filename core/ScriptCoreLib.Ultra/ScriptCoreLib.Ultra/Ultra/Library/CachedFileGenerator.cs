﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Archive.ZIP;
using ScriptCoreLib.Extensions;
using System.IO;
using System.Diagnostics;

namespace ScriptCoreLib.Ultra.Library
{
    public class CachedFileGenerator : CachedFileGeneratorBase
    {
        public DirectoryInfo SDK;

        public static new CachedFileGeneratorBase Create(CachedFileGeneratorBase.Arguments Arguments)
        {
            return new CachedFileGenerator(Arguments);
        }

        public CachedFileGenerator(CachedFileGeneratorBase.Arguments Arguments)
            : base(Arguments)
        {
            // http://stackoverflow.com/questions/867485/c-getting-the-path-of-appdata

            var CommonApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            var CacheFolder = new DirectoryInfo(
                Path.Combine(
                    CommonApplicationData,
                    "jsc/cache/" + this.ConstructorArguments.AssamblyFile.Name + "/" + this.ConstructorArguments.Language.ToString()
                )
            );



            var Cache = new FileInfo(
                Path.Combine(
                    CacheFolder.FullName,
                    Arguments.AssamblyFile.LastWriteTimeUtc.Ticks + ".zip"
                )
            );


            this.AtValidate +=
                delegate
                {
                    // time to extract the zip file if ready and emit the local token

                    if (Cache.Exists)
                    {
                        //Debugger.Launch();

                        var zip = Cache.ToZIPFile();

                        foreach (var item in zip.Entries)
                        {
                            var FilePath = Path.Combine(
                                this.ConstructorArguments.TargetDirectory.FullName,
                                item.FileName
                            );

                            this.Add(
                                FilePath,
                                item.Text
                            );

                        }

                        this.WriteLocalTokens();
                    }
                    else
                    {
                        if (this.SourceVersion.Exists)
                            this.SourceVersion.Delete();

                        CacheFolder.Create();
                        CacheFolder.Clear();
                    }
                };

            this.AtWriteTokens +=
                delegate
                {
                    // time to write the zip file

                    if (Cache.Exists)
                        return;

                    CacheFolder.Create();
                    CacheFolder.Clear();

                    var zip = new ZIPFile();

                    foreach (var item in this.Files)
                    {
                        var RelativeFileName = item.FileName.Replace("\\", "/").SkipUntilIfAny(this.ConstructorArguments.TargetDirectory.FullName.Replace("\\", "/") + "/");

                        zip.Add(RelativeFileName, item.Content);
                    }

                    // should we mark NTFS it compressable?

                    //Debugger.Launch();



                    zip.WriteToFile(Cache);

                    if (this.SDK != null)
                    {
                        var SDKCacheFolder = new DirectoryInfo(
                              Path.Combine(
                                  SDK.FullName,
                                  "cache/" + this.ConstructorArguments.AssamblyFile.Name + "/" + this.ConstructorArguments.Language.ToString()
                              )
                          );


                        SDKCacheFolder.Create();
                        SDKCacheFolder.Clear();
                        var SDKCache = new FileInfo(
                            Path.Combine(
                                SDKCacheFolder.FullName,
                                Arguments.AssamblyFile.LastWriteTimeUtc.Ticks + ".zip"
                            )
                        );
                  

                        zip.WriteToFile(SDKCache);

                    }
                };


        }
    }
}

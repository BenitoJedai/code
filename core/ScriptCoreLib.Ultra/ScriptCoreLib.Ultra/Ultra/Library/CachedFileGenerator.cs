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
        // should this be moved into .Liibrary assembly?

        public DirectoryInfo SDK;

        public static new CachedFileGeneratorBase Create(CachedFileGeneratorBase.Arguments Arguments)
        {
            return new CachedFileGenerator(Arguments);
        }

        [Obsolete("This is not safe, .NET keeps updating itself. Cache will break!")]
        public static new CachedFileGeneratorBase CreateForUnqualifiedEnvironment(CachedFileGeneratorBase.Arguments Arguments)
        {
            return new CachedFileGenerator(Arguments, true);
        }

        public CachedFileGenerator(CachedFileGeneratorBase.Arguments Arguments, bool UnqualifiedEnvironment = false)
            : base(Arguments)
        {
            // http://stackoverflow.com/questions/867485/c-getting-the-path-of-appdata
            // http://support.microsoft.com/kb/2600217#UpdateReplacement

            var CommonApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            var Version = Environment.Version.ToString();

            if (UnqualifiedEnvironment)
            {
                Version = Version.TakeUntilLastIfAny(".");
            }

            var CacheFolder = new DirectoryInfo(
                Path.Combine(
                    CommonApplicationData,
                    "jsc/"
                    + "cache/"
                    + Version
                    + "/"
                    + this.ConstructorArguments.AssamblyFile.Name
                    + "/"
                    + this.ConstructorArguments.Language.ToString()
                )
            );


            // next new cache name
            var Cache = new FileInfo(
                Path.Combine(
                    CacheFolder.FullName,
                   this.ConstructorArguments.AssamblyFile.Name + ".zip"
                )
            );


            this.AtValidate +=
                delegate
                {
                    // time to extract the zip file if ready and emit the local token

                    if (Cache.Exists)
                    {
                        // great. now compare the times

                        if (this.ConstructorArguments.AssamblyFile.LastWriteTime > Cache.LastWriteTime)
                        {
                            // no dice. the target is newer than our cache.

                            Cache.Delete();
                            Cache.Refresh();
                        }
                    }

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
                        this.WriteLocalFiles();
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
                    // if the cache still exists it's time to write the zip file

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


                    #region SDK
                    if (this.SDK != null)
                    {
                        var SDKCacheFolder = new DirectoryInfo(
                              Path.Combine(
                                  SDK.FullName,
                                   "cache/"
                                    + Version
                                    + "/"
                                  + this.ConstructorArguments.AssamblyFile.Name
                                  + "/"
                                  + this.ConstructorArguments.Language.ToString()
                              )
                          );


                        SDKCacheFolder.Create();
                        SDKCacheFolder.Clear();
                        var SDKCache = new FileInfo(
                            Path.Combine(
                                SDKCacheFolder.FullName,
                                this.ConstructorArguments.AssamblyFile.Name + ".zip"
                            )
                        );


                        zip.WriteToFile(SDKCache);

                    }
                    #endregion
                };


        }
    }
}

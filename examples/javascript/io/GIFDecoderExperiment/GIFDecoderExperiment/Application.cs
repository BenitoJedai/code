using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GIFDecoderExperiment;
using GIFDecoderExperiment.Design;
using GIFDecoderExperiment.HTML.Pages;
using System.Windows.Controls;
using System.IO;
using System.Diagnostics;
using System.Net;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Ultra.Library.Extensions;
using GIFDecoderExperiment.Avalon.Images;

namespace Abstractatech
{
    [Obsolete("this is the first nuget to be used within jsc analyzer, and runs as javascript.")]
    public class GIFDecoder
    {
        enum GIFFunctionCode : byte
        {
            PlaintextExtension = 1,
            LocalDescriptorExtension = 249,
            CommentExtension = 254,
            ApplicationExtension = 255
        }


        public readonly byte[][] GlobalColorMap;

        public GIFDecoder(
            byte[] filebytes,

            ItemCollection parent,

            Func<ItemCollection, Image, TextBlock, TreeViewItem> AddNodeDirect
            )
        {



            var m = new BinaryReader(
                new MemoryStream(
                //File.ReadAllBytes(path)
                    filebytes
                )
            );


            #region X:\jsc.svn\examples\javascript\io\GIFDecoderExperiment\GIFDecoderExperiment\Application.cs
            var GIF_signature = m.ReadBytes(3);
            var GIF_version = m.ReadBytes(3);
            // 6

            // { GIF_width = 0, GIF_height = 0, GIF_GlobalColorTable = 0 }
            var GIF_width = m.ReadUInt16();
            var GIF_height = m.ReadUInt16();
            // 10

            //Console.WriteLine(new { m.BaseStream.Position, m.BaseStream.Length, GIF_width, GIF_height });

            var GIF_flags = m.ReadByte();
            // 11

            var GIF_flags_GlobalColorMap = (GIF_flags >> 7) & 0x1;
            var GIF_flags_PixelBits = GIF_flags & 7; // z111

            // "C:\Users\Arvo\Videos\Wildebeest.gif"
            //var GIF_GlobalColorTable = GIF_flags_GlobalColorMap == 0 ? 0 : (int)Math.Pow(2, (GIF_flags_PixelBits + 1));

            var GIF_GlobalColorTable = 0;
            if (GIF_flags_GlobalColorMap != 0)
                GIF_GlobalColorTable = (int)Math.Pow(2, (GIF_flags_PixelBits + 1));

            //Console.WriteLine(new { m.BaseStream.Position, m.BaseStream.Length, GIF_flags_GlobalColorMap, GIF_GlobalColorTable });

            var GIF_backgroundColorIndex = m.ReadByte();
            var GIF_aspectRatio = m.ReadByte();

            var GIF_GlobalColorTableSize = 3 * GIF_GlobalColorTable;


            this.GlobalColorMap = Enumerable.Range(0, GIF_GlobalColorTable).Select(
                i => m.ReadBytes(3)
            ).ToArray();

            var GIFNode = AddNodeDirect(
                parent,
                new ClassWithoutMethods(),
                new TextBlock { Text = new { GIF_width, GIF_height, GIF_GlobalColorTable }.ToString() }
            );

            var xTerminator = m.ReadByte();

            #region GIFFunctionCode
            Action DoGIFFunctionCode = delegate
            {


                while (xTerminator == 0x21)
                {
                    // http://www.let.rug.nl/kleiweg/gif/netscape.html

                    var Function = (GIFFunctionCode)m.ReadByte();
                    var FunctionLength = m.ReadByte();
                    var FunctionData = m.ReadBytes(FunctionLength);


                    var ApplicationExtensionData = new MemoryStream();
                    var Text = "";
                    var ApplicationExtensionDataBlocks = default(byte);

                    // http://www.let.rug.nl/kleiweg/gif/GIF89a.html#application
                    if (Function == GIFFunctionCode.ApplicationExtension)
                    {
                        Text = Encoding.UTF8.GetString(FunctionData);

                        xTerminator = m.ReadByte();
                        while (xTerminator != 0)
                        {
                            ApplicationExtensionDataBlocks++;
                            m.ReadBytes(xTerminator).With(bytes => ApplicationExtensionData.Write(bytes, 0, bytes.Length));
                            xTerminator = m.ReadByte();
                        }

                    }
                    else
                    {
                        if (Function == GIFFunctionCode.CommentExtension)
                        {
                            Text = Encoding.UTF8.GetString(FunctionData);
                        }


                        xTerminator = m.ReadByte();
                    }

                    var xNode = AddNodeDirect(
                          GIFNode.Items,
                          new LanguageKeyword(),
                          new TextBlock
                          {
                              Text = new
                              {
                                  Function,
                                  FunctionLength,
                                  Text,
                                  ApplicationExtensionDataBlocks,
                                  ApplicationExtensionData.Length
                              }.ToString()
                          }
                      );

                    #region ApplicationExtension
                    if (Function == GIFFunctionCode.ApplicationExtension)
                    {
                        if (Text == "XMP DataXMP")
                        {
                            //var xmpString = Encoding.UTF8.GetString(
                            //        ApplicationExtensionData.ToBytes()
                            //     );

                            //var xmp = XElement.Parse(
                            //xmpString    
                            //);


                            //AddXElementTo(xmp, null, xNode.Items);

                        }
                    }
                    #endregion

                    xTerminator = m.ReadByte();


                    if (xTerminator == 0)
                        Debugger.Break();
                }
            };
            #endregion

            #region DoRasterDataBlock
            Action DoRasterDataBlock = delegate
            {

                while (xTerminator == 0x2c)
                {
                    var PosX = m.ReadUInt16();
                    var PosY = m.ReadUInt16();
                    var Width = m.ReadUInt16();
                    var Height = m.ReadUInt16();

                    // <Packed Fields> 
                    var frame_flags = m.ReadByte();
                    var frame_flags_LocalColorMap = (frame_flags >> 7) & 0x1;
                    var frame_flags_PixelBits = frame_flags & 7; // z111

                    //var frame_LocalColorTable = frame_flags_LocalColorMap == 0 ? 0 : (int)Math.Pow(2, (frame_flags_PixelBits + 1));
                    var frame_LocalColorTable = 0;
                    if (frame_flags_LocalColorMap != 0)
                        frame_LocalColorTable = (int)Math.Pow(2, (frame_flags_PixelBits + 1));

                    var frame_GlobalColorTableSize = 3 * frame_LocalColorTable;

                    var frame_LocalColorMap = Enumerable.Range(0, frame_LocalColorTable).Select(
                        i => m.ReadBytes(3)
                    ).ToArray();

                    // ? ColorDepth
                    var frame_InitialCodeSize = m.ReadByte();

                    var frame_blocks = 0;

                    //Console.WriteLine(new { m.BaseStream.Position, m.BaseStream.Length, frame_blocks, frame_InitialCodeSize });
                    // 0:8301ms { Position = 81, frame_blocks = 0, frame_InitialCodeSize = 48 } 

                    var frame_Data = new MemoryStream();

                    // { PosX = 0, PosY = 0, Width = 23, Height = 19, frame_GlobalColorTableSize = 6, frame_blocks = 11, Length = 998 }

                    //while ((xTerminator = m.ReadByte()) != 0)

                    //0:83ms { frame_blocks = 0, frame_InitialCodeSize = 48 } view-source:37729
                    //0:84ms { xTerminator = 202, frame_blocks = 1 } 

                    xTerminator = m.ReadByte();
                    while (xTerminator != 0)
                    {
                        frame_blocks++;

                        //Console.WriteLine(new { xTerminator, frame_blocks });

                        m.ReadBytes(xTerminator).With(bytes => frame_Data.Write(bytes, 0, bytes.Length));
                        xTerminator = m.ReadByte();
                    }

                    AddNodeDirect(
                        GIFNode.Items,
                        new LanguageKeyword(),
                        new TextBlock
                        {
                            Text = new
                            {
                                PosX,
                                PosY,
                                Width,
                                Height,
                                frame_GlobalColorTableSize,
                                frame_blocks,
                                frame_Data.Length
                            }.ToString()
                        }
                    );

                    xTerminator = m.ReadByte();

                    if (xTerminator == 0)
                        Debugger.Break();
                }
            };
            #endregion



            //02000024 GIFDecoderExperiment.Application+<>c__DisplayClassc+<>c__DisplayClasse
            //script: error JSC1000: unknown while condition at Void <.ctor>b__6(). Maybe you did not turn off c# compiler 'optimize code' feature?

            var ok_275 = true;

            while (ok_275)
            {
                //Console.WriteLine(new { m.BaseStream.Position, m.BaseStream.Length, ok_275, xTerminator });
                DoGIFFunctionCode();
                //Console.WriteLine(new { m.BaseStream.Position, m.BaseStream.Length, ok_275, xTerminator });
                DoRasterDataBlock();

                ok_275 = (xTerminator == 0x21);
            }

            if (xTerminator != 0x3b)
                Debugger.Break();
            #endregion






        }
    }
}

namespace GIFDecoderExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {




        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //Could not load file or assembly 'ScriptCoreLibJava.AppEngine, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.

            #region AddNodeDirect
            Func<ItemCollection, Image, TextBlock, TreeViewItem> AddNodeDirect =
                (parent, image, x) =>
                {
                    // X:\jsc.svn\core\ScriptCoreLib.Avalon\ScriptCoreLib.Avalon\JavaScript\BCLImplementation\System\Windows\Controls\TextBlock.cs
                    //var x = new TextBlock
                    //{
                    //    Text = "hello " + new { bytes.Length }
                    //};

                    //<div><div name="__Panel" style="width: 600px; height: 400px; position: absolute; left: 0px; top: 0px; z-index: 0;"><div style="position: relative;"><div name="__TextBlock" style="position: absolute; left: 0px; top: 0px;"><label>{ PosX = 0, PosY = 0, Width = 23, Height = 19, frame_GlobalColorTableSize = 6, frame_blocks = 11, Length = 998 }</label></div></div></div></div>

                    //var p = new Canvas();

                    //x.AttachTo(p);

                    var i = new IHTMLImage { };

                    ((__Image)image).InternalBitmapChanged +=
                        delegate
                        {
                            i.src = ((__Image)image).InternalBitmap.src;
                        };

                    var div = new IHTMLDiv {
                        
                        i,

                        x.Text }.AttachToDocument();

                    //p.AttachToContainer(div);

                    return new TreeViewItem { };
                };
            #endregion




            //Uncaught InvalidAccessError: Failed to set the 'responseType' property on 'XMLHttpRequest': The response type can only be changed for asynchronous HTTP requests made from a document. 

            #region AtGIFBytes
            Action<byte[]> AtGIFBytes =
                filebytes =>
                {
                    // what to prefix this with?
                    // data:[<MIME-type>][;charset=<encoding>][;base64],
                    new IHTMLImage { src = "data:image/gif;base64," + Convert.ToBase64String(filebytes) }

                        .AttachToDocument();

                    // fake it
                    var n = new TreeViewItem { };


                    var g = new Abstractatech.GIFDecoder(
                        filebytes,

                        n.Items,

                        AddNodeDirect
                    );

                    new IHTMLPre
                    {
                        new{ GlobalColorMap = g.GlobalColorMap.Length }
                    }.AttachToDocument();

                    g.GlobalColorMap.WithEach(
                        rgb =>
                        {
                            // http://gamedev.stackexchange.com/questions/67724/how-can-i-simulate-a-limited-256-color-palette-in-opengl
                            // https://twitter.com/notch/status/393803588134109184

                            new IStyle(new IHTMLDiv { }.AttachToDocument())
                            {
                                width = "1em",
                                height = "1em",
                                whiteSpace = IStyle.WhiteSpaceEnum.pre,

                                display = IStyle.DisplayEnum.inline_block,

                                backgroundColor = "rgb(" + rgb[0] + ", " + rgb[1] + ", " + rgb[2] + ")"

                            };

                        }
                    );

                };
            #endregion

            // how can we comment that the API is wrong?
            // go online and just do that? :)

            //new dance().bytes.ContinueWithResult(

      



            // X:\jsc.svn\examples\javascript\Test\TestScriptApplicationIntegrity\TestScriptApplicationIntegrity\Application.cs
            //new IXMLHttpRequest(
            //    ScriptCoreLib.Shared.HTTPMethodEnum.GET,
            //    new HTML.Images.FromAssets.dance().src,
            //    true
            //).bytes.ContinueWithResult(AtGIFBytes);


            new WebClient().DownloadDataTaskAsync(
                // like nameof, jsc could optimize the newobj out and keep the src as const instead!
                new HTML.Images.FromAssets.dance().src
            ).ContinueWithResult(AtGIFBytes);


            // jsc can we also do drag n drop?
            // "X:\jsc.svn\examples\javascript\io\DropFileForMD5Experiment\DropFileForMD5Experiment.sln"



            #region ondragover
            Native.document.documentElement.ondragover +=
                e =>
                {
                    //Console.WriteLine("ondragover");

                    e.stopPropagation();
                    e.preventDefault();

                    e.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.


                    page.body.style.backgroundColor = "cyan";


                    // this wont work
                    //e.dataTransfer.setDragImage(
                    //    new IHTMLDiv { 
                    //        "drop here"
                    //    }, 0, 0
                    //);

                };

            // Error	7	Predefined type 'System.Runtime.CompilerServices.IAsyncStateMachine' is not defined or imported	X:\jsc.svn\examples\javascript\io\GIFDecoderExperiment\GIFDecoderExperiment\CSC	GIFDecoderExperiment
            Native.document.documentElement.ondrop +=
                //async 
                   e =>
                   {
                       // X:\jsc.svn\examples\javascript\io\WebApplicationSelectingFile\WebApplicationSelectingFile\Application.cs

                       page.body.style.backgroundColor = "";

                       Console.WriteLine("ondrop");

                       e.stopPropagation();
                       e.preventDefault();
                       FileList x = e.dataTransfer.files; // FileList object.

                       for (uint i = 0; i < x.length; i++)
                       {
                           var f = x[i];

                           var s = Stopwatch.StartNew();

                           //Method not found: 'Void ScriptCoreLib.JavaScript.DOM.DataTransferItem.getAsString(ScriptCoreLib.JavaScript.DOM.IFunction)'.
                           // do redux rebuild!

                           //var bytes = await f.readAsBytes();
                           f.readAsBytes().ContinueWithResult(
                               bytes =>
                               {

                                   var md5 = bytes.ToMD5Bytes();
                                   var md5hex = md5.ToHexString();

                                   new IHTMLPre {
                                        new { 
                                            f.type,
                                        f.name, 
                                        f.size,
                                        md5hex,
                                        s.ElapsedMilliseconds
                                        }
                                    }.AttachToDocument();

                                   if (f.name.EndsWith(".gif"))
                                       AtGIFBytes(bytes);
                               }
                           );

                       }
                   };
            #endregion



        }

    }
}

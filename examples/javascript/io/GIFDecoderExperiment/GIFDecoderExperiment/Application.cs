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
using GIFDecoderExperiment.HTML.Images.FromAssets;
using System.Windows.Controls;
using System.IO;
using System.Diagnostics;
using System.Net;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls;

namespace GIFDecoderExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        enum GIFFunctionCode : byte
        {
            PlaintextExtension = 1,
            LocalDescriptorExtension = 249,
            CommentExtension = 254,
            ApplicationExtension = 255
        }

        public class xNode
        {
            public object Items;
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //Could not load file or assembly 'ScriptCoreLibJava.AppEngine, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.

            #region AddNodeDirect
            Func<object, Image, TextBlock, xNode> AddNodeDirect =
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


                    var div = new IHTMLDiv {
                        
                        ((__Image)image).InternalBitmap,

                        x.Text }.AttachToDocument();

                    //p.AttachToContainer(div);

                    return new xNode { };
                };
            #endregion


            // how can we comment that the API is wrong?
            // go online and just do that? :)

            //new dance().bytes.ContinueWithResult(

            // not yet available?
            //new WebClient().DownloadDataAsync(



            //Uncaught InvalidAccessError: Failed to set the 'responseType' property on 'XMLHttpRequest': The response type can only be changed for asynchronous HTTP requests made from a document. 

            new IXMLHttpRequest(
                ScriptCoreLib.Shared.HTTPMethodEnum.GET,
                new dance().src,
                true
            ).bytes.ContinueWithResult(
                filebytes =>
                {
                    // fake it
                    var n = new xNode { };

                    var m = new BinaryReader(
                        new MemoryStream(
                        //File.ReadAllBytes(path)
                            filebytes
                        )
                    );


                    #region X:\jsc.svn\examples\javascript\io\GIFDecoderExperiment\GIFDecoderExperiment\Application.cs
                    var GIF_signature = m.ReadBytes(3);
                    var GIF_version = m.ReadBytes(3);

                    // { GIF_width = 0, GIF_height = 0, GIF_GlobalColorTable = 0 }
                    var GIF_width = m.ReadUInt16();
                    var GIF_height = m.ReadUInt16();

                    var GIF_flags = m.ReadByte();
                    var GIF_flags_GlobalColorMap = (GIF_flags >> 7) & 0x1;
                    var GIF_flags_PixelBits = GIF_flags & 7; // z111

                    // "C:\Users\Arvo\Videos\Wildebeest.gif"
                    //var GIF_GlobalColorTable = GIF_flags_GlobalColorMap == 0 ? 0 : (int)Math.Pow(2, (GIF_flags_PixelBits + 1));

                    var GIF_GlobalColorTable = 0;
                    if (GIF_flags_GlobalColorMap != 0)
                        GIF_GlobalColorTable = (int)Math.Pow(2, (GIF_flags_PixelBits + 1));


                    var GIF_backgroundColorIndex = m.ReadByte();
                    var GIF_aspectRatio = m.ReadByte();

                    var GIF_GlobalColorTableSize = 3 * GIF_GlobalColorTable;

                    var GlobalColorMap = Enumerable.Range(0, GIF_GlobalColorTable).Select(
                        i => m.ReadBytes(3)
                    ).ToArray();

                    var GIFNode = AddNodeDirect(
                        n.Items,
                        new Avalon.Images.ClassWithoutMethods(),
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
                                //while ((xTerminator = m.ReadByte()) != 0)
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
                                  new Avalon.Images.LanguageKeyword(),
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
                            //impl:type: ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__BinaryReader 818a3699-984d-30aa-8562-925d8bedb620  - System.IO.BinaryReader 2484afda-7b47-3cd7-97b5-951f5c6ab5b6
                            //script: error JSC1000: No implementation found for this native method, please implement [System.IO.BinaryReader.ReadUInt16()]

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
                            if (frame_flags_LocalColorMap != null)
                                frame_LocalColorTable = (int)Math.Pow(2, (frame_flags_PixelBits + 1));

                            var frame_GlobalColorTableSize = 3 * frame_LocalColorTable;

                            var frame_LocalColorMap = Enumerable.Range(0, frame_LocalColorTable).Select(
                                i => m.ReadBytes(3)
                            ).ToArray();

                            // ? ColorDepth
                            var frame_InitialCodeSize = m.ReadByte();

                            var frame_blocks = 0;

                            var frame_Data = new MemoryStream();

                            //while ((xTerminator = m.ReadByte()) != 0)

                            xTerminator = m.ReadByte();
                            while (xTerminator != 0)
                            {
                                frame_blocks++;
                                m.ReadBytes(xTerminator).With(bytes => frame_Data.Write(bytes, 0, bytes.Length));
                                xTerminator = m.ReadByte();
                            }

                            AddNodeDirect(
                                GIFNode.Items,
                                new Avalon.Images.LanguageKeyword(),
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
                        DoGIFFunctionCode();
                        DoRasterDataBlock();

                        ok_275 = (xTerminator == 0x21);
                    }

                    if (xTerminator != 0x3b)
                        Debugger.Break();
                    #endregion




                }
            );
        }

    }
}

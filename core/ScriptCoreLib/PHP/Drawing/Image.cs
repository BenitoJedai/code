using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.PHP.IO;


using ScriptCoreLib.Shared;

namespace ScriptCoreLib.PHP.Drawing
{
    [Script, System.Obsolete]
    internal partial class Image : global::System.IDisposable
    {

        public string FileName
        {
            get
            {
                return this.ImageFile.FileName;
            }
        }

        public int Width
        {
            get
            {
                object[] n = ImageSize;

                var d = (double)n[0];

                d *= Zoom;

                return (int)d;
            }
        }

        public int Height
        {
            get
            {
                object[] n = ImageSize;

                var d = (double)n[1];

                d *= Zoom;

                return (int)d;
            }
        }


        object[] ImageSize
        {
            get
            {
                return API.getimagesize(ImageFile.FullPath);
            }
        }

        public double Zoom = 1;

        [Script]
        public enum ImageTypeEnum
        {
            Unknown, GIF, JPG, PNG, SWF, SWC, PSD, TIFF, BMP, IFF, JP2, JPX, JB2, JPC, XBM, WBMP
        };

        public ImageTypeEnum ImageType
        {
            get
            {
                object[] n = ImageSize;

                return (ImageTypeEnum)n[2];
            }
        }

        public object Handle;


        

        public void Open()
        {
            if (Exists)
            {
                Native.Log("Image opened from {" + FileName + "} type: " + ImageType);

                if (ImageType == ImageTypeEnum.JPG)
                {
                    Handle = API.imagecreatefromjpeg(ImageFile.FullPath);

                }
                else if (ImageType == ImageTypeEnum.GIF)
                {
                    Handle = API.imagecreatefromgif(ImageFile.FullPath);

                }
                else if (ImageType == ImageTypeEnum.PNG)
                {
                    Handle = API.imagecreatefrompng(ImageFile.FullPath);
                }
                else
                {
                    Handle = null;
                    Native.Log("not a valid Image");
                }
            }
        }

        public void SaveJPG(string filename, int quality)
        {
            Native.Log("Image save as {" + filename + "}");

            API.imagejpeg(Handle, filename, quality);
        }

        public void Close()
        {
            if (Handle == null)
                return;

            API.imagedestroy(Handle);
        }

        public FileInfo ImageFile;

        public bool Exists
        {
            get
            {
                return ImageFile.IsReadable;
            }
        }

        public static Image Of(FileInfo f)
        {
            Image n = new Image();

            n.ImageFile = f;

            n.Open();

            return n;
        }
        
        public static Image Of(string filename)
        {
            return Of(FileInfo.OfPath(filename));
        }


        public void SaveThumbnailAs(FileInfo fileInfo, int width)
        {
            int height = width * 3 / 4;


            object t1 = Native.API.imagecreatetruecolor(width, height);


            if (Height > Width)
            {
                int osh = Width * 3 / 4;

                Native.API.imagecopyresampled(t1, Handle, 0, 0, 0, osh / 2, width, height, Width, osh);

            }
            else
            {
                int osw = Height * 4 / 3;


                Native.API.imagecopyresampled(t1, Handle, 0, 0, 0, 0, width, height, osw, Height);
            }

            Native.API.imagejpeg(t1, fileInfo.FullPath, 100);

        }

        /// <summary>
        /// var.A is the source image, and var.B will be the thumbnail
        /// </summary>
        /// <param name="var"></param>
        /// <param name="p">size of the thumbnail</param>
        public static void EnsureThumbnail(Pair<FileInfo> v, int p)
        {
            if (!v.B.IsReadable)
            {

                using (Image i = Image.Of(v.A))
                {
                    i.SaveThumbnailAs(v.B, p);
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Close();
        }

        #endregion
    }
}

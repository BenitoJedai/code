using ScriptCoreLib;
using ScriptCoreLib.PHP.IO;

using ScriptCoreLib.Shared;

namespace ScriptCoreLib.PHP.Drawing
{
    public partial class Image
    {
        [Script]
        internal static class API
        {
            #region array getimagesize ( string filename [, array &imageinfo] )

            /// <summary>
            /// The getimagesize() function will determine the size of any GIF, JPG, PNG, SWF, SWC, PSD, TIFF, BMP, IFF, JP2, JPX, JB2, JPC, XBM, or WBMP image file and return the dimensions along with the file type and a height/width text string to be used inside a normal HTML &lt;IMG&gt; tag. 
            /// </summary>
            /// <param name="_filename">string filename</param>
            [Script(IsNative = true)]
            public static object[] getimagesize(string _filename) { return default(object[]); }

            #endregion


            #region resource imagecreatefromgif ( string filename )

            /// <summary>
            /// imagecreatefromgif() returns an image identifier representing the image obtained from the given filename. 
            /// </summary>
            /// <param name="_filename">string filename</param>
            [Script(IsNative = true)]
            public static object imagecreatefromgif(string _filename) { return default(object); }

            #endregion

            #region resource imagecreatefrompng ( string filename )

            /// <summary>
            /// imagecreatefrompng() returns an image identifier representing the image obtained from the given filename. 
            /// </summary>
            /// <param name="_filename">string filename</param>
            [Script(IsNative = true)]
            public static object imagecreatefrompng(string _filename) { return default(object); }

            #endregion

            #region resource imagecreatefromjpeg ( string filename )

            /// <summary>
            /// imagecreatefromjpeg() returns an image identifier representing the image obtained from the given filename.
            /// </summary>
            /// <param name="_filename">string filename</param>
            [Script(IsNative = true)]
            public static object imagecreatefromjpeg(string _filename) { return default(object); }

            #endregion

            #region bool imagejpeg ( resource image [, string filename [, int quality]] )

            /// <summary>
            /// imagejpeg() creates the JPEG file in filename from the image image. The image argument is the return from the imagecreatetruecolor() function. 
            /// </summary>
            /// <param name="_image">resource image</param>
            [Script(IsNative = true)]
            public static bool imagejpeg(object _image) { return default(bool); }

            #endregion

            #region bool imagejpeg ( resource image , string filename , int quality )

            /// <summary>
            /// imagejpeg() creates the JPEG file in filename from the image image. The image argument is the return from the imagecreatetruecolor() function. 
            /// </summary>
            /// <param name="_image">resource image</param>
            /// <param name="_filename">string filename</param>
            /// <param name="_quality">int quality</param>
            [Script(IsNative = true)]
            public static bool imagejpeg(object _image, string _filename, int _quality) { return default(bool); }

            #endregion

            #region bool imagedestroy ( resource image )

            /// <summary>
            /// imagedestroy() frees any memory associated with image image. image is the image identifier returned by one of the image create functions, such as imagecreatetruecolor(). 
            /// </summary>
            /// <param name="_image">resource image</param>
            [Script(IsNative = true)]
            public static bool imagedestroy(object _image) { return default(bool); }

            #endregion

        }



       
    }
}

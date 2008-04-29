﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript
{
    // http://livedocs.adobe.com/flex/2/docs/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Parts&file=00000970.html
    // http://www.actionscript.org/forums/showthread.php3?t=165106
    // http://livedocs.adobe.com/flex/2/docs/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Parts&file=00000789.html
    // http://www.adobe.com/cfusion/communityengine/index.cfm?event=showdetails&productId=2&postId=8046
    // http://www.adobe.com/devnet/flex/quickstart/embedding_assets/
    [Script(IsNative = true)]
    public sealed class EmbedAttribute : Attribute
    {
        /// <summary>
        /// Specifies the name and path of the asset to embed; either an absolute path or a path relative to the file containing the embed statement. The embedded asset must be a locally stored asset. Therefore, you cannot specify a URL for an asset to embed.
        /// </summary>
        public string source;

        /// <summary>
        /// Specifies the mime type of the asset.
        /// Supported values: 
        /// <example>
        ///     *  application/octet-stream
        /// * application/x-font
        /// * application/x-font-truetype
        /// * application/x-shockwave-flash
        /// * audio/mpeg
        /// * image/gif
        /// * image/jpeg
        /// * image/png
        /// * image/svg
        /// * image/svg-xml
        /// </example>
        /// </summary>
        public string mimeType;

        // font
        public string systemFont;
        public string fontName;
        public string fontWeight;
        public string fontStyle;
        public string fontFamily;
        public string unicodeRange;

        // image

        /// <summary>
        /// Specifies the distance, in pixels, of the upper dividing line from the top of the image in a scale-9 formatting system. The distance is relative to the original, unscaled size of the image.
        /// </summary>
        public string scaleGridTop;
        /// <summary>
        /// Specifies the distance in pixels of the lower dividing line from the top of the image in a scale-9 formatting system. The distance is relative to the original, unscaled size of the image.
        /// </summary>
        public string scaleGridBottom;
        /// <summary>
        /// Specifies the distance in pixels of the left dividing line from the left side of the image in a scale-9 formatting system. The distance is relative to the original, unscaled size of the image.
        /// </summary>
        public string scaleGridLeft;
        /// <summary>
        /// Specifies the distance in pixels of the right dividing line from the left side of the image in a scale-9 formatting system. The distance is relative to the original, unscaled size of the image.
        /// </summary>
        public string scaleGridRight;




    }
}

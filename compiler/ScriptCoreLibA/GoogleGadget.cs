using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib
{
    // http://code.google.com/apis/gadgets/
    // http://googlegadgetsapi.blogspot.com/
    // http://code.google.com/apis/gadgets/docs/gs.html
    // http://www.google.com/ig/directory?url=www.google.com/ig/modules/gge.xml
    
    /// <summary>
    /// Google Gadgets are simple HTML and JavaScript mini-applications served in iFrames that can be embedded in webpages and other apps.
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class GoogleGadgetAttribute : Attribute
    {
        #region ModulePrefs

        public string category { get; set; }
        public string category2 { get; set; }

        /// <summary>
        ///   	Optional string that provides the title of the gadget. This title is displayed in the gadget title bar on iGoogle. If this string contains user preference substitution variables and you are planning to submit your gadget to the content directory, you should also provide a directory_title for directory display.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Optional string that provides the title that should be displayed for your gadget in the content directory. Should contain only the literal text to be displayed, not user preference substitution variables. This is because the content directory displays a static view of your gadget, and therefore can't perform the necessary substitution to produce a reasonable title. For example, if your gadget's title is "Friends for __UP_name__", the directory is not able to perform the substitution to provide a reasonable value for " __UP_name__". So you might set your directory_title to be simply "Friends".
        /// </summary>
        public string directory_title { get; set; }
        /// <summary>
        /// Optional string that provides a URL that the gadget title links to. For example, you could link the title to a webpage related to the gadget.
        /// </summary>
        public string title_url { get; set; }

        /// <summary>
        /// Optional string that describes the gadget.
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Optional string that lists the author of the gadget.
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// Optional string that provides the gadget author's email address. You can use any email system, but you should not use a personal email address because of spam. One approach is to use an email address of the form helensmith.feedback+coolgadget@gmail.com in your gadget spec. Gmail drops everything after the plus sign (+), so this email address is interpreted as helensmith.feedback@gmail.com.
        /// </summary>
        public string author_email { get; set; }

        /// <summary>
        /// Optional string such as "Google" that indicates the author's affiliation. This attribute is required for gadgets that are included in the content directory.
        /// </summary>
        public string author_affiliation { get; set; }
        /// <summary>
        /// The author's geographical location, such as "Mountain View , CA, USA ".
        /// </summary>
        public string author_location { get; set; }
        /// <summary>
        /// Optional string that gives the URL of a gadget screenshot. This must be an image on a public web site that is not blocked by robots.txt. PNG is the preferred format, though GIF and JPG are also acceptable. Gadget screenshots should be 280 pixels wide. The height of the screenshot should be the "natural" height of the gadget when it's in use. For more discussion of screenshot guidelines, see Publishing to the Content Directory.
        /// </summary>
        public string screenshot { get; set; }
        /// <summary>
        /// Optional string that gives the URL of a gadget thumbnail. This must be an image on a public web site that is not blocked by robots.txt. PNG is the preferred format, though GIF and JPG are also acceptable. Gadget thumbnails should be 120x60 pixels. For more discussion of thumbnail guidelines, see Publishing to the Content Directory.
        /// </summary>
        public string thumbnail { get; set; }

        /// <summary>
        /// Optional positive integer that specifies the height of the area in which the gadget runs. The default height is 200.
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// Optional positive integer that specifies the width of the area in which the gadget runs. This setting only applies to syndicated gadgets. The default width is 320.
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// Optional boolean that specifies whether the aspect ratio (height-to-width ratio) of the gadget is maintained when the browser is resized. Gadgets that can automatically scale vertically should set this to true, but gadgets which have a fixed height should set this to false. The default is true.
        /// </summary>
        public bool scaling { get; set; }
        /// <summary>
        /// Optional boolean that provides vertical and/or horizontal scrollbars if the content exceeds the space provided. If false, then the content is clipped to the height and width provided (not that width is not configurable). The default is false.
        /// </summary>
        public bool scrolling { get; set; }
        /// <summary>
        /// Optional boolean that specifies whether users can add a gadget multiple times from a directory. The default is true, meaning that by default, gadgets can only be added once. Directories can handle this attribute however they choose. For example, the content directory handles singleton="true" by graying out and displaying the text "Added" for gadgets that have already been added. Note that changes to this attribute may not be picked up by directories right away. This attribute doesn't prevent users from adding gadgets multiple times through the developer gadget or Add by URL. Consequently, you still need to write your gadget to support multiple instances.
        /// </summary>
        public bool singleton { get; set; }


        /// <summary>
        /// For the authors page, a URL to a photo (70x100 PNG format preferred, but JPG/GIF are also supported).
        /// </summary>
        public string author_photo { get; set; }
        /// <summary>
        /// For the authors page, a statement about yourself (try to keep to ~500 characters).
        /// </summary>
        public string author_aboutme { get; set; }
        /// <summary>
        /// For the authors page, a link to your website, blog, etc.
        /// </summary>
        public string author_link { get; set; }
        /// <summary>
        /// For the authors page, a quote you'd like to include (try to keep to ~300 characters).
        /// </summary>
        public string author_quote { get; set; }
        #endregion

        public GoogleGadgetAttribute()
        {
            this.height = 240;
            this.width = 320;
            this.scaling = true;
            this.scrolling = false;
            this.singleton = true;

        }
    }
}

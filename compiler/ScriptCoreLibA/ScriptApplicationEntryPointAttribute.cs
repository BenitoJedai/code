using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib
{
    // http://dotnet.org.za/kevint/pages/Flags.aspx
    [Flags]
    public enum SerializedDataFormat
    {
        none = 0,
        json = 1,
        xml = 2,
        tlv = 4
    }

    /// <summary>
    /// Will generate an entrypoint to this class with the default value of field 'DefaultData'
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ScriptApplicationEntryPointAttribute : Attribute
    {
		public string Feed;

        /// <summary>
        /// Application will be loaded from a href
        /// </summary>
        public bool IsClickOnce;

        /// <summary>
        /// formats in which the data will be serialized and inserted inline into html document
        /// </summary>
        public SerializedDataFormat Format = /*SerializedDataFormat.json |*/ SerializedDataFormat.xml;

        /// <summary>
        /// Suffix on the filename will be omitted.
        /// </summary>
        public SerializedDataFormat DefaultFormat = SerializedDataFormat.xml;

        /// <summary>
        /// script files are loaded dynamically
        /// </summary>
        public bool ScriptedLoading;

        public int Width { get; set; }
        public int Height { get; set; }

        public const int DefaultWidth = 500;
        public const int DefaultHeight = 380;

        public ScriptApplicationEntryPointAttribute()
        {
            Width = DefaultWidth;
            Height = DefaultHeight;
        }

		public bool Background;
		public uint BackgroundColor;

		public bool AlignToCenter;

		/// <summary>
		/// By setting this property to true, this type will be responsible
		/// for assets. For ActionScript this would mean registering them with 
		/// the resource collector. In the future versions this property might
		/// be true by default.
		/// </summary>
		public bool WithResources;
    }

}

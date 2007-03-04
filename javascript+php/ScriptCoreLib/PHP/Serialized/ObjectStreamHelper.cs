using ScriptCoreLib;

using ScriptCoreLib.PHP.Runtime;

using ScriptCoreLib.Shared.Serialized;

namespace ScriptCoreLib.PHP.Serialized
{

    [Script]
    public class ObjectStreamHelper<T> : IObjectStreamHelper<T>
        where T : class
    {


        string _Stream;

        /// <summary>
        /// will decode json base64 steam to an item object
        /// </summary>
        public string Stream
        {
            get { return _Stream; }
            set
            {
                _Stream = value;
                _Item = JSON.DecodeFromBase64String<T>(value);
            }
        }

        T _Item;

        /// <summary>
        /// will encode an item object to json base64
        /// </summary>
        public T Item
        {
            get { return _Item; }
            set
            {
                _Item = value;
                _Stream = JSON.EncodeToBase64String(value);
            }
        }

        /// <summary>
        /// echoes the stream to the client, 
        /// encapsulationg the data within a hidden iput from a specific class
        /// </summary>
        /// <param name="p"></param>
        public void ToStream(string p)
        {
            Native.echo("<div><input type='hidden' class='" + p + "' value='" + Stream + "' /></div>");

        }
    }

}

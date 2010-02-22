using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Runtime;

using ScriptCoreLib.Shared.Serialized;

namespace ScriptCoreLib.JavaScript.Serialized
{
    [Script]
	[System.Obsolete("To be moved out of CoreLib or removed")]
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
                _Item = Convert.FromJSON<T>(value, true);
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
                _Stream = Convert.ToBase64String(Expando.Of(_Item).ToJSON());
            }
        }

        //public void ToConsole()
        //{
        //    Console.Log(Stream);
        //}
    }

}

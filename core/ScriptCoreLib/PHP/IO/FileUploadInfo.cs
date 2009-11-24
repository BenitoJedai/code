using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;

using Serializable = System.SerializableAttribute;

namespace ScriptCoreLib.PHP.IO
{


    [Serializable]
	[Script, System.Obsolete]
    public class FileUploadInfo
    {
        public string name;
        public string type;
        public string tmp_name;
        public int error;
        public int size;


        public static FileUploadInfo Of(string file)
        {
            return Expando.Copy(new FileUploadInfo(), (IArray)Native.SuperGlobals.Files[file]);


        }
    }
}

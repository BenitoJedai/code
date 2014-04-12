using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;

namespace ScriptCoreLib.ActionScript.Nonoba.api
{
    [Script(IsNative = true)]
    public class Message
    {
        public string Type;

        public int length
        {
            get
            {
                return default(int);
            }
        }

        public Message(string type)
        {

        }

        public void Add(object e)
        {

        }

        public void Clone(object to)
        {
        }

        public string GetString(uint index)
        {
            return default(string);
        }

        public int GetInt(uint index)
        {
            return default(int);
        }

        public double GetNumber(uint index)
        {
            return default(double);
        }

		public ByteArray GetByteArray(uint index)
		{
			return default(ByteArray);
		}

        [Script(NotImplementedHere = true)]
        public static Message CloneFrom(object e)
        {
            return default(Message);
        }
    }
}

using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget="Date")]
    public class IDate
    {
        /*
         * ref: http://www.w3schools.com/jsref/jsref_obj_date.asp
         * 
         */

        public IDate()
        {
        }

        public IDate(double i)
        {
        }

        public int getSeconds() { return default(int); }
        public int getMinutes() { return default(int); }
        public int getHours() { return default(int); }
        public int getDate() { return default(int); }
        public int getDay() { return default(int); }
        public int getMonth() { return default(int); }
        public int getFullYear() { return default(int); }


        public long getTime()
        {
            return default(long);
        }



        public string toGMTString()
        {
            return default(string);
        }

        public string toLocaleString()
        {
            return default(string);
        }

        /* 
         * defines static alias
         */
        static public IDate Now
        {
            get
            {
                return new IDate();
            }
        }

        public static implicit operator double(IDate e)
        {
            return e.getTime();
        }

    }
}

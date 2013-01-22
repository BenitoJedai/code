using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


[assembly: TestGUIDAttributeOnAField.x212(0, field1 = "1", prop2 = "2")]
[module: TestGUIDAttributeOnAField.x212(0, field1 = "1", prop2 = "2")]

namespace TestGUIDAttributeOnAField
{
    //001c:0001 XTestGUIDAttributeOnAField define TestGUIDAttributeOnAField.TimeZoneInformation
    //error: System.ArgumentException: Invalid custom attribute provided: 'MarshalAs attribute has fields set that are not valid for the specified unmanaged type.'

    struct TimeZoneInformation
    {
        [MarshalAs(
            unmanagedType: UnmanagedType.ByValTStr,

            SizeConst = 32
            )
        ]
        public string standardName;
    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class x212 : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly Guid m_guid;

        // This is a positional argument
        public x212(uint a)
        {
            m_guid = new Guid(a, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        public int goo;

        public string field1;

        public string prop2 { get; set; }
    }

    class foo
    {
        [x212(0, field1 = "1", prop2 = "2", goo = default(int))]
        public string f;

        [x212(0, field1 = "1", prop2 = "2")]
        public foo()
        {

        }
        [x212(0, field1 = "1", prop2 = "2")]
        public void ffoo()
        {

        }

        [x212(0, field1 = "1", prop2 = "2")]
        public string ff
        {
            [x212(0, field1 = "1", prop2 = "2")]
            get { return f; }
            [x212(0, field1 = "1", prop2 = "2")]

            set { f = value; }
        }

        [x212(0, field1 = "1", prop2 = "2")]
        public event Action fff
        {
            [x212(0, field1 = "1", prop2 = "2")]
            add { }
            [x212(0, field1 = "1", prop2 = "2")]

            remove { }
        }
    }

    [x212(0, field1 = "1", prop2 = "2")]
    public enum e196
    {
        [x212(0, field1 = "1", prop2 = "2")]
        Other
    }
}

using ScriptCoreLib.GLSL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Data
{
    using NotImplementedException = Exception;

    /// <summary>
    /// This class provides an easy way to use object.property
    /// syntax with a DataReader by wrapping a DataReader into
    /// a dynamic object.
    /// 
    /// The class also automatically fixes up DbNull values
    /// (null into .NET and DbNUll)
    /// </summary>
    public class DynamicDataReader : DynamicObject, IDataReader /* = this.DataReader */
    {
        // tested by "X:\jsc.svn\examples\javascript\Test\TestSQLiteParameter\TestSQLiteParameter\TestSQLiteParameter.csproj"

        // http://www.west-wind.com/weblog/posts/2011/Dec/06/Creating-a-Dynamic-DataReader-for-easier-Property-Access

        #region /* IDataReader = this.DataReader */
        public void Close()
        {
            throw new NotImplementedException();
        }

        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get { throw new NotImplementedException(); }
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }

        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int FieldCount
        {
            get { throw new NotImplementedException(); }
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return this.DataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            return this.DataReader.GetFieldType(i);
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            return this.DataReader.GetOrdinal(name);
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            throw new NotImplementedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public object this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        public object this[int i]
        {
            get { throw new NotImplementedException(); }
        }
        #endregion


        /// <summary>
        /// Cached Instance of DataReader passed in
        /// </summary>
        public IDataReader DataReader;

        /// <summary>
        /// Pass in a loaded DataReader
        /// </summary>
        /// <param name="dataReader">DataReader instance to work off</param>
        public DynamicDataReader(IDataReader dataReader)
        {
            DataReader = dataReader;
        }

        /// <summary>
        /// Returns a value from the current DataReader record
        /// If the field doesn't exist null is returned.
        /// DbNull values are turned into .NET nulls.
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var Name = binder.Name;

            result = null;
            var retvalue = true;
            // 'Implement' common reader properties directly
            if (binder.Name == "IsClosed")
                result = DataReader.IsClosed;
            else if (binder.Name == "RecordsAffected")
                result = DataReader.RecordsAffected;
            // lookup column names as fields
            else
            {
                var FieldNamesE = DataReader.FieldNames();
                var FieldNames = FieldNamesE.ToArray();

                if (FieldNames.Contains(Name))
                {
                    //Console.WriteLine("TryGetMember: " + Name);

                    try
                    {
                        // { Message = 'System.Data.DynamicDataReader' does not contain a definition for 'xyz', S


                        result = DataReader[Name];

                        if (result == DBNull.Value)
                            result = null;


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("TryGetMember error: " + new { Name, ex.Message, ex.StackTrace });


                        result = null;
                        retvalue = false;
                    }

                }
                else
                {
                    Console.WriteLine("Field not found: " + Name);

                    foreach (var item in DataReader.FieldNames())
                    {
                        Console.WriteLine("" + new { item, Name });
                    }

                    // otherwise check if we can map to GLSL?
                    ////if (Name == "xyz")
                    ////{
                    ////    var ivec3 = new ivec3();

                    ////    // sqlite defaults to long
                    ////    // while glsl does not have long vec.
                    ////    var x = DataReader["x"];
                    ////    var y = DataReader["y"];
                    ////    var z = DataReader["z"];

                    ////    ivec3.x = (int)(long)x;
                    ////    ivec3.y = (int)(long)y;
                    ////    ivec3.z = (int)(long)z;

                    ////    result = ivec3;
                    ////}
                }
            }

            return retvalue;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            // Implement most commonly used method
            if (binder.Name == "Read")
                result = DataReader.Read();
            else if (binder.Name == "Close")
            {
                DataReader.Close();
                result = null;
            }
            else
                // call other DataReader methods using Reflection (slow - not recommended)
                // recommend you use full DataReader instance

                throw new Exception("TryInvokeMember");
            //typeof(IDataReader).InvokeMember(binder.Name, 
            //result = ReflectionUtils.CallMethod(DataReader, binder.Name, args);

            return true;
        }



    }

}

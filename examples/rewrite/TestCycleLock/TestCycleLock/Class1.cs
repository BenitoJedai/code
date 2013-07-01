using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCycleLock
{
    //public struct TypeInfo
    //{
    //    public Conversion ImplicitConversion;
    //}

    //public struct Conversion
    //{
    //    public UserDefinedConversionResult conversionResult;

    //}

    public struct UserDefinedConversionResult
    {
        public ReadOnlyArray<object> Results;
        //public IComparable<UserDefinedConversionAnalysis> Results;
        //public ReadOnlyArray<UserDefinedConversionAnalysis> Results;
    }

  //  public class UserDefinedConversionAnalysis
  //  {
  //      //Error	1	Struct member 'TestCycleLock.Conversion.c' of type 'TestCycleLock.UserDefinedConversionResult' causes a cycle in the struct layout	X:\jsc.svn\examples\rewrite\TestCycleLock\TestCycleLock\Class1.cs	16	44	TestCycleLock
  //      //Error	2	Struct member 'TestCycleLock.UserDefinedConversionResult.c' of type 'TestCycleLock.UserDefinedConversionAnalysis' causes a cycle in the struct layout	X:\jsc.svn\examples\rewrite\TestCycleLock\TestCycleLock\Class1.cs	22	46	TestCycleLock
  //      //Error	3	Struct member 'TestCycleLock.UserDefinedConversionAnalysis.c' of type 'TestCycleLock.Conversion' causes a cycle in the struct layout	X:\jsc.svn\examples\rewrite\TestCycleLock\TestCycleLock\Class1.cs	27	27	TestCycleLock

  ////      __Diagnostics_waiting 4

  ////TestCycleLock.UserDefinedConversionAnalysis
  ////TestCycleLock.TypeInfo
  ////2 types above waiting for TestCycleLock.Conversion
  ////  TestCycleLock.UserDefinedConversionResult
  ////    TestCycleLock.UserDefinedConversionAnalysis
  ////      TestCycleLock.Conversion

  ////TestCycleLock.UserDefinedConversionResult
  ////1 types above waiting for TestCycleLock.UserDefinedConversionAnalysis
  ////  TestCycleLock.Conversion

  ////TestCycleLock.Conversion
  ////1 types above waiting for TestCycleLock.UserDefinedConversionResult


  //      //public Conversion SourceConversion;
  //  }

    public struct ReadOnlyArray<T>
    {
        public T t;

    }


}

using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
//using System.Core?

namespace TestIQueryable
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // http://msdn.microsoft.com/en-us/library/system.linq.iqueryable(v=vs.110).aspx
        // Provides functionality to evaluate queries against a specific data source wherein the type of the data is not specified.
        // http://blogs.msdn.com/b/mattwar/archive/2007/07/30/linq-building-an-iqueryable-provider-part-i.aspx
        // http://msdn.microsoft.com/en-us/library/cc190116(v=vs.110).aspx
        // http://msdn.microsoft.com/en-us/library/bb311040.aspx

        public void WebMethod2()
        {
            //var x = new Book1Sheet1();

            //var f = x.Where(k => k.Foo == "xxx");
            //var g = f.Where(k => k.Foo == "xxx");

            var xx =
                from k in new Book1Sheet1()
                where k.Foo == "xxx"
                where k.Goo == "xxx"
                orderby k.Timestamp descending
                select k;
            //select k.Key;
            //select new { k.Key, k.Foo };

            // http://stackoverflow.com/questions/10591010/remove-orderby-from-an-iqueryablet
            // Error	3	foreach statement cannot operate on variables of type 'TestIQueryable.IBook1Sheet1Queryable'
            // because 'TestIQueryable.IBook1Sheet1Queryable' does not contain a public definition for 'GetEnumerator'	X:\jsc.svn\examples\javascript\Test\TestIQueryable\TestIQueryable\ApplicationWebService.cs	43	13	TestIQueryable

            //{ sql = select Key, Foo, Goo, Timestamp
            //from Book1Sheet1
            // where Foo = xxx where Goo = xxx
            //order by Timestamp descending
            //limit 10
            // }


            //{ sql = select Key
            //from Book1Sheet1
            // where Foo = xxx where Goo = xxx
            //order by Timestamp descending
            //limit 10
            // }

            //{ sql = select Key, Foo, Goo, Timestamp
            //from Book1Sheet1
            // where Foo = xxx where Goo = xxx
            //order by Timestamp descending
            //limit 10
            // }

            foreach (var item in xx.Take(10))
            {

            }

            Debugger.Break();
        }

    }



    public class Book1Sheet1Row
    {
        public long Key;

        public string Foo;
        public string Goo;

        public long Timestamp;
    }

    // providing a nice name
    public class Book1Sheet1 : IBook1Sheet1Queryable
    {
        // what fields we need anyhow? AsEnumerable
        public string SelectCommand = "select Key, Foo, Goo, Timestamp";
        public string FromCommand = "from Book1Sheet1";
        public string WhereCommand = "";
        public string OrderByCommand = "";

        // http://stackoverflow.com/questions/2497677/using-the-limit-statement-in-a-sqlite-query
        // http://php.about.com/od/mysqlcommands/g/Limit_sql.htm
        // sqlite and mysql are ok with this
        public string LimitCommand = "";
    }


    // should this be an inteface instead?
    public class IBook1Sheet1Queryable
    {


        // like the css selector
        public IBook1Sheet1Queryable Context;

        #region Where
        public Action<Book1Sheet1> ApplyWhereExpression;

        public IBook1Sheet1Queryable Where(Expression<Func<Book1Sheet1Row, bool>> f)
        {
            Console.WriteLine("IBook1Sheet1Queryable.Where");

            // we are like a stringbuilder
            // like dynamic keyword support 

            return new IBook1Sheet1Queryable
            {
                Context = this,
                ApplyWhereExpression = x =>
                {
                    var ff = f;

                    //                    +		f	{k => (k.Foo == "xxx")}	System.Linq.Expressions.Expression<System.Func<TestIQueryable.Book1Sheet1Row,bool>>
                    //        f.Name	null	string
                    //        f.NodeType	Lambda	System.Linq.Expressions.ExpressionType
                    //+		f.Parameters	Count = 1	System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> {System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.ParameterExpression>}
                    //+		f.ReturnType	{Name = "Boolean" FullName = "System.Boolean"}	System.Type {System.RuntimeType}
                    //+		f.Parameters[0]	{k}	System.Linq.Expressions.ParameterExpression {System.Linq.Expressions.TypedParameterExpression}
                    //+		f.Parameters[1]	'f.Parameters[1]' threw an exception of type 'System.ArgumentOutOfRangeException'	System.Linq.Expressions.ParameterExpression {System.ArgumentOutOfRangeException}
                    //+		f.Body	{(k.Foo == "xxx")}	System.Linq.Expressions.Expression {System.Linq.Expressions.MethodBinaryExpression}
                    //+		(f.Body as MethodBinaryExpression)	{(k.Foo == "xxx")}	System.Linq.Expressions.MethodBinaryExpression
                    //+		((MethodBinaryExpression)f.Body).Left	{k.Foo}	System.Linq.Expressions.Expression {System.Linq.Expressions.FieldExpression}
                    //+		((MethodBinaryExpression)f.Body).Right	{"xxx"}	System.Linq.Expressions.Expression {System.Linq.Expressions.ConstantExpression}
                    //+		((MethodBinaryExpression)f.Body).Right	{"xxx"}	System.Linq.Expressions.Expression {System.Linq.Expressions.ConstantExpression}
                    //+		(ConstantExpression)((MethodBinaryExpression)f.Body).Right	{"xxx"}	System.Linq.Expressions.ConstantExpression
                    //        ((ConstantExpression)((MethodBinaryExpression)f.Body).Right).Value	"xxx"	object {string}
                    //        ((FieldExpression)((MethodBinaryExpression)f.Body).Left).Member.Name	"Foo"	string

                    //var f_Body_as_MethodBinaryExpression = ((MethodBinaryExpression)f.Body);
                    var f_Body_as_BinaryExpression = ((BinaryExpression)f.Body);

                    // http://stackoverflow.com/questions/9241607/whats-wrong-with-system-linq-expressions-logicalbinaryexpression-class
                    var f_Body_Left_as_MemberExpression = (MemberExpression)f_Body_as_BinaryExpression.Left;
                    var f_Body_Right_as_ConstantExpression = (ConstantExpression)f_Body_as_BinaryExpression.Right;

                    Console.WriteLine("IBook1Sheet1Queryable.Where " + new { f_Body_as_BinaryExpression.Method });

                    x.WhereCommand += " where " + f_Body_Left_as_MemberExpression.Member.Name + " = " + f_Body_Right_as_ConstantExpression.Value;

                }
            };
        }
        #endregion


        #region OrderByDescending
        //-- Error	2	Could not find an implementation of the query pattern for source type 'TestIQueryable.IBook1Sheet1Queryable'.  
        // 'OrderByDescending' not found.	X:\jsc.svn\examples\javascript\Test\TestIQueryable\TestIQueryable\ApplicationWebService.cs	36	27	TestIQueryable

        public Action<Book1Sheet1> ApplyOrderByDescending;

        public IBook1Sheet1Queryable OrderByDescending(Expression<Func<Book1Sheet1Row, long>> f)
        {
            Console.WriteLine("IBook1Sheet1Queryable.OrderByDescending");
            // we are like a stringbuilder
            // like dynamic keyword support 

            return new IBook1Sheet1Queryable
            {
                Context = this,
                ApplyOrderByDescending =
                x =>
                {
                    var ff = f;

                    // http://social.msdn.microsoft.com/Forums/en-US/ab528f6a-a60e-4af6-bf31-d58e3f373356/resolving-propertyexpressions-and-fieldexpressions-in-a-custom-linq-provider?forum=linqprojectgeneral

                    //var f_Body_as_FieldExpression = ((FieldExpression)f.Body);
                    var f_Body_as_MemberExpression = ((MemberExpression)f.Body);

                    //+		f	{k => k.Timestamp}	System.Linq.Expressions.Expression<System.Func<TestIQueryable.Book1Sheet1Row,long>>
                    //        f.Name	null	string
                    //        f.NodeType	Lambda	System.Linq.Expressions.ExpressionType
                    //+		f.Parameters	Count = 1	System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.ParameterExpression> {System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.ParameterExpression>}
                    //+		f.ReturnType	{Name = "Int64" FullName = "System.Int64"}	System.Type {System.RuntimeType}
                    //+		f.Parameters[0]	{k}	System.Linq.Expressions.ParameterExpression {System.Linq.Expressions.TypedParameterExpression}
                    //+		f.Parameters[1]	'f.Parameters[1]' threw an exception of type 'System.ArgumentOutOfRangeException'	System.Linq.Expressions.ParameterExpression {System.ArgumentOutOfRangeException}
                    //+		f.Body	{k.Timestamp}	System.Linq.Expressions.Expression {System.Linq.Expressions.FieldExpression}
                    //+		(FieldExpression)f.Body	{k.Timestamp}	System.Linq.Expressions.FieldExpression
                    //+		((FieldExpression)f.Body)	{k.Timestamp}	System.Linq.Expressions.FieldExpression
                    //        f_Body_as_MemberExpression.Member.Name	"Timestamp"	string



                    x.OrderByCommand += "order by " + f_Body_as_MemberExpression.Member.Name + " descending";
                }
            };
        }
        #endregion

        #region Take
        public Action<Book1Sheet1> ApplyTake;

        // we only support take as the last command
        public IBook1Sheet1Queryable Take(int c)
        {
            Console.WriteLine("IBook1Sheet1Queryable.Take");
            // we are like a stringbuilder
            // like dynamic keyword support 

            return new IBook1Sheet1Queryable
            {
                Context = this,
                ApplyTake =
                x =>
                {
                    // http://php.about.com/od/mysqlcommands/g/Limit_sql.htm
                    x.LimitCommand += "limit " + c;
                }
            };
        }
        #endregion

        //Error	2	Could not find an implementation of the query pattern for source type 'TestIQueryable.IBook1Sheet1Queryable'.  
        // 'Select' not found.	X:\jsc.svn\examples\javascript\Test\TestIQueryable\TestIQueryable\ApplicationWebService.cs	37	27	TestIQueryable

        public Action<Book1Sheet1> ApplySelect;

        // select only the columns from our table we want. unless we want to select data 
        // from outside of our table..
        public IBook1Sheet1Queryable Select<TAny>(Expression<Func<Book1Sheet1Row, TAny>> f)
        {
            Console.WriteLine("IBook1Sheet1Queryable.Select");
            // we are like a stringbuilder
            // like dynamic keyword support 

            return new IBook1Sheet1Queryable
            {
                Context = this,
                ApplySelect =
                x =>
                {
                    var ff = f;

                    // this allows us to filter down the fields we need,
                    // yet which type to we want to use?
                    // keep full row type or use the new TAny?

                    //   select new { k.Key, k.Foo };
                    //                    +		f	{k => new <>f__AnonymousType0`2(Key = k.Key, Foo = k.Foo)}	System.Linq.Expressions.Expression<System.Func<TestIQueryable.Book1Sheet1Row,<>f__AnonymousType0<long,string>>>
                    //+		f.Body	{new <>f__AnonymousType0`2(Key = k.Key, Foo = k.Foo)}	System.Linq.Expressions.Expression {System.Linq.Expressions.NewExpression}
                    //        f.Body.NodeType	New	System.Linq.Expressions.ExpressionType
                    //+		(NewExpression)f.Body	{new <>f__AnonymousType0`2(Key = k.Key, Foo = k.Foo)}	System.Linq.Expressions.NewExpression
                    //+		((NewExpression)f.Body).Arguments	Count = 2	System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> {System.Runtime.CompilerServices.TrueReadOnlyCollection<System.Linq.Expressions.Expression>}
                    //        ((MemberExpression)((NewExpression)f.Body).Arguments[0]).Member.Name	"Key"	string
                    //+		((NewExpression)f.Body).Arguments[1]	{k.Foo}	System.Linq.Expressions.Expression {System.Linq.Expressions.FieldExpression}

                    //                    +		f	{k => k.Key}	System.Linq.Expressions.Expression<System.Func<TestIQueryable.Book1Sheet1Row,long>>
                    //+		f.Body	{k.Key}	System.Linq.Expressions.Expression {System.Linq.Expressions.FieldExpression}
                    //        f.Body.NodeType	MemberAccess	System.Linq.Expressions.ExpressionType
                    //+		((MemberExpression)((f.Body)))	{k.Key}	System.Linq.Expressions.MemberExpression {System.Linq.Expressions.FieldExpression}


                    //// http://php.about.com/od/mysqlcommands/g/Limit_sql.htm
                    x.SelectCommand = "select ";

                    if (f.Body.NodeType == ExpressionType.MemberAccess)
                    {
                        x.SelectCommand += ((MemberExpression)f.Body).Member.Name;
                    }
                    else if (f.Body.NodeType == ExpressionType.New)
                    {
                        ((NewExpression)f.Body).Arguments.WithEachIndex(
                            (k, i) =>
                            {
                                if (i > 0)
                                    x.SelectCommand += ", ";

                                x.SelectCommand += ((MemberExpression)k).Member.Name;
                            }
                        );
                    }

                }
            };
        }

        #region AsEnumerable
        // finalize the query. what about count?
        public IEnumerable<Book1Sheet1Row> AsEnumerable()
        {
            Console.WriteLine("IBook1Sheet1Queryable.AsEnumerable");

            var sql = ToSQL(this);

            Console.WriteLine();
            Console.WriteLine(new { sql });

            return new Book1Sheet1Row[] { };

        }

        // enabling the foreach?
        public IEnumerator<Book1Sheet1Row> GetEnumerator()
        {
            // Object reference not set to an instance of an object.
            return AsEnumerable().GetEnumerator();
        }
        #endregion


        public static string ToSQL(IBook1Sheet1Queryable q)
        {
            // first find the root and then work backwards to add the new filters

            Book1Sheet1 c = null;

            Action<IBook1Sheet1Queryable> y = null;

            y = x =>
            {
                // we wen too far, bail
                if (x == null)
                    return;

                // we found the root!
                if (x is Book1Sheet1)
                {
                    c = (Book1Sheet1)x;
                    return;
                }

                y(x.Context);

                // we will be modifying the original mutable object fields. reset for reuse!
                // ok. time to do the modify

                // what are we? a where clause?
                // can we merge the apply methods into one?
                if (x.ApplyWhereExpression != null)
                {
                    x.ApplyWhereExpression(c);
                    return;
                }

                if (x.ApplyOrderByDescending != null)
                {
                    x.ApplyOrderByDescending(c);
                    return;
                }

                if (x.ApplyTake != null)
                {
                    x.ApplyTake(c);
                    return;
                }

                if (x.ApplySelect != null)
                {
                    x.ApplySelect(c);
                    return;
                }
            };

            y(q);

            var w = new StringBuilder();

            w.AppendLine(c.SelectCommand);
            w.AppendLine(c.FromCommand);
            w.AppendLine(c.WhereCommand);
            w.AppendLine(c.OrderByCommand);
            w.AppendLine(c.LimitCommand);

            return w.ToString();
        }
    }
}

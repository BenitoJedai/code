using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace ScriptCoreLib.JSON.UnitTest
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class UnitTest1
	{
		public UnitTest1()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void TestMethod1()
		{
			var source = @"
 [
  [""Jack Smith"",""22.33.44.55:6666""],
  [""John Doe"",""22.33.44.11:6666""]
  ]			
			
			";

			JSONDocument.Parse(source,
				new JSONDocument.ParseArguments
				{
					FoundArray = a =>
					{
						// before root element
						a.Invoke(
							new JSONDocument.ParseArguments
							{
								FoundArray = b =>
								{
									// inner string list starting

									// whenever we find a string...

									b(
										new JSONDocument.ParseArguments
										{
											FoundString =
												text =>
												{
													Console.WriteLine(text);
												}
										}
									);

									// inner string list done
								}
							}
						);
						// after root element
					}
				}
			);
		}



		[TestMethod]
		public void TestMethod2()
		{
			var source = @"
 [
  [""Jack Smith"",""22.33.44.55:6666""],
  [""John Doe"",""22.33.44.11:6666""]
  ]			
			
			";

			JSONDocument.ParseArray(source,
				new JSONDocument.ParseArrayArguments
				{
					FoundString =
						(rank, index, text) =>
						{
							Console.WriteLine(new { rank, index, text }.ToString());
						}
				}
			);
		}


		[TestMethod]
		public void TestMethod3()
		{
			var source = @"
 [
  [""Jack Smith"",""22.33.44.55:6666""],
  [""John Doe"",""22.33.44.11:6666""]
  ]			
			
			";

			var a = new List<Info>();

			JSONDocument.ParseMatrix(source,
				delegate
				{
					var n = new Info();

					a.Add(n);

					return new JSONDocument.ParseArguments[] 
					{
						(JSONDocument.StringAction)(Name => n.Name = Name),
						(JSONDocument.StringAction)(Target => n.Target = Target)
					};
				}
			);

			foreach (var k in a)
			{
				Console.WriteLine(new { k.Name, k.Target });
			}
		}

		class Info
		{
			public string Name;
			public string Target;
		}


		[TestMethod]
		public void TestMethod4()
		{
			var source = @"
 [
  [""Jack Smith"",""22.33.44.55:6666""],
  [""John Doe"",""22.33.44.11:6666""]
  ]			
			
			";

			var a = new List<Info>();

			JSONDocument.ParseMatrix(source,
				delegate
				{
					var n = new Info();

					a.Add(n);

					return new JSONDocument.ParseArguments[] 
					{
						(JSONDocument.StringAction)(Name => n.Name = Name),
						(JSONDocument.StringAction)(Target => n.Target = Target)
					};
				}
			);

			foreach (var k in a)
			{
				Console.WriteLine(new { k.Name, k.Target });
			}

			// talk about dynamic contexts... :)
			Console.WriteLine(
				JSONDocument.ToString(
					delegate
					{
						var i = -1;

						return delegate
						{
							i++;

							if (i < a.Count)
							{
								var c = a[i];

								return new[] { c.Name, c.Target };
							}

							return null;
						};
					}
				)
			);
		}



		class Info2
		{
			public string Name;
			public string Target;


			public static Info2[] Parse(string source)
			{
				var a = new ArrayList();

				JSONDocument.ParseMatrix(source,
					delegate
					{
						var n = new Info2();

						a.Add(n);

						return new JSONDocument.ParseArguments[] 
							{
								(JSONDocument.StringAction)(Name => n.Name = Name),
								(JSONDocument.StringAction)(Target => n.Target = Target)
							};
					}
				);

				return (Info2[])a.ToArray(typeof(Info2));
			}

			public static string ToString(Info2[] a)
			{
				return JSONDocument.ToString(
					delegate
					{
						var i = -1;

						return delegate
						{
							i++;

							if (i < a.Length)
							{
								var c = a[i];

								return new[] { c.Name, c.Target };
							}

							return null;
						};
					}
				);
			}
		}

		

		[TestMethod]
		public void TestMethod5()
		{
			var source = @"
 [
  [""Jack Smith"",""22.33.44.55:6666""],
  [""John Doe"",""22.33.44.11:6666""]
  ]			
			
			";

			var a = Info2.Parse(source);


			foreach (var k in a)
			{
				Console.WriteLine(new { k.Name, k.Target });
			}

			// talk about dynamic contexts... :)
			Console.WriteLine(Info2.ToString(a.ToArray()));
		}
	}
}

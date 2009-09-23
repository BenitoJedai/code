using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace ScriptCoreLib.JSON
{
	public class JSONDocument
	{
		// http://www.json.org/

		public delegate bool BooleanContinuation(ParseArguments a);
		public delegate void Continuation(ParseArguments a);
		public delegate void Notification(Continuation a);
		public delegate void StringAction(string a);
		public delegate bool BooleanFunc();

		public class ParseArguments
		{
			public Notification FoundArray;
			public Notification FoundObject;
			public StringAction FoundString;
			public Notification FoundNull;
			public Notification FoundNumeric;

			public static implicit operator ParseArguments(StringAction FoundString)
			{
				return new ParseArguments { FoundString = FoundString };
			}
		}

		public class ParseArrayArguments
		{

			public delegate void FoundStringAction(int rank, int index, string value);
			public FoundStringAction FoundString;

			//public Notification FoundObject;
			//public Notification FoundNull;
			//public Notification FoundNumeric;
		}

		public delegate ParseArguments[] ParseArgumentsArrayFunc();

		/// <summary>
		/// This method can be used to parse a list of objects with sequential fields to objects
		/// </summary>
		/// <param name="source"></param>
		/// <param name="f"></param>
		public static void ParseMatrix(string source, ParseArgumentsArrayFunc f)
		{
			var c = default(ParseArguments[]);

			ParseArray(source,
				new ParseArrayArguments
				{
					FoundString =
						(rank, index, value) =>
						{
							if (rank == 2)
							{
								if (index == 0)
								{
									c = f();
								}

								c[index].FoundString(value);
							}
						}
				}
			);
		}


		public delegate string[] StringArrayFunc();
		public delegate StringArrayFunc StringArrayFunc2();

		/// <summary>
		/// ToString function is to be used with a matrix of strings. For example an array of instances of an array of strings.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static string ToString(StringArrayFunc2 context)
		{
			var w = new StringBuilder();


			var next = context();
			var k = next();

			w.AppendLine("[");

			while (k != null)
			{
				w.Append("[");

				for (int i = 0; i < k.Length; i++)
				{
					if (i > 0)
						w.Append(",");

					w.Append("\"" + k[i] + "\"");
				}

				w.Append("]");

				k = next();

				if (k == null)
					w.AppendLine();
				else
					w.AppendLine(",");
			}


			w.AppendLine("]");

			return w.ToString();
		}

	
		public static void ParseArray(string source, ParseArrayArguments args)
		{
			// how many dimensions?
			var rank = 0;

			Notification FoundArray = null;



			FoundArray =
				a =>
				{
					rank++;

					var index = -1;

					a(
						new ParseArguments
						{
							FoundArray = FoundArray,
							FoundString = value =>
							{
								index++;

								args.FoundString(rank, index, value);
							}
						}
					);

					rank--;
				};

			Parse(source,
				new ParseArguments
				{
					FoundArray = FoundArray,
					FoundString = value =>
					{
						args.FoundString(rank, 0, value);
					}
				}
			);
		}

		public static void Parse(string source, ParseArguments args)
		{
			var i = 0;


			#region SkipWhiteSpaces
			Action SkipWhiteSpaces =
				delegate
				{
					while (char.IsWhiteSpace(source, i))
					{
						i++;

						// eof
						if (i == source.Length)
							break;
					}
				};
			#endregion




			BooleanContinuation MoveNext = null;

			#region FoundString
			Continuation FoundString =
				a =>
				{
					// lets remember how our string was qouted
					var q = source[i];
					var w = new StringBuilder();

					while (true)
					{
						i++;
						// now we need to parse a json string which should be in utf8 encoing and might be escaped

						var c = source[i];

						if (c == q)
						{
							a.FoundString(w.ToString());

							i++;
							return;
						}

						// are we entering escape mode?
						if (c == '\\')
						{
							throw new NotImplementedException();
						}

						w.Append(c);
					}
				};
			#endregion

			#region FoundArray
			Continuation FoundArray =
				a =>
				{
					i++;

					// first element?
					while (MoveNext(a))
					{
						SkipWhiteSpaces();

						if (source[i] != ',')
							break;

						i++;
					}

					SkipWhiteSpaces();

					// we have a state here...

					if (source[i] == ']')
					{
						i++;
						return;
					}

					throw new NotSupportedException();
				};
			#endregion


			MoveNext =
				a =>
				{
					SkipWhiteSpaces();

					if (source[i] == '\"')
					{
						FoundString(a);
						return true;
					}

					if (source[i] == '[')
					{
						a.FoundArray(FoundArray);
						return true;
					}

					return false;
				};


			if (MoveNext(args))
			{
				SkipWhiteSpaces();

				// we should now be at the end!

				return;
			}

			throw new NotSupportedException();
		}


	}
}

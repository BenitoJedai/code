using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLINQJoinConcat
{
	class Program
	{
		static void Main(string[] args)
		{
			var cNoBlockComment = Enumerable.ToArray(
				from i in Enumerable.Range(0, 9)
				let mod2 = i % 2
				let z = new { i, mod2 }
				group z by new
				{
					mod2
				} into g

				let count = g.Count()

				select new { count, g.Key.mod2, g }
			);

			//var cNoBlockCommenty = Enumerable.ToArray(
			//	from g in cNoBlockComment
			//	where !(g.mod2 == 1)
			//	select g
			//);

			var cNoBlockCommentx = cNoBlockComment;

			//cNoBlockCommentx.Apply(
			//	from g in cNoBlockComment
			//	where !(g.mod2 == 1)
			//	select g
			//);

			//ParallelEnumerable.ToArray(
			//cNoBlockCommentx = Enumerable.ToArray(
			cNoBlockCommentx = xEnumerable.SelectManyToArray(
				from gg in cNoBlockComment
					// what if we only want to work on mod2=1?
					// can we do the first where multithreaded?
					//where gg.mod2 == 1


				select gg.mod2 == 0 ? new[] { gg } :

				from c in gg.g

					// update
				let i = c.i + 1000

				// keep
				let mod2 = c.i % 2

				// work on this group

				let z = new { i, mod2 }

				group z by new
				{
					mod2
				} into g


				let count = g.Count()
				select new { count, g.Key.mod2, g }


			////join y in (from g in cNoBlockComment where !(g.mod2 == 1) select g) on true equals true
			////join y in cNoBlockComment on true equals !(y.mod2 == 1) 

			//let y = from g in cNoBlockComment
			//		where !(g.mod2 == 1)
			//		select g


			//select new[] { x }.Concat(y)
			);

			Debugger.Break();
		}
	}

	static class xEnumerable
	{
		//Error CS1101   The parameter modifier 'ref' cannot be used with 'this' 	TestLINQJoinConcat X:\jsc.svn\examples\merge\test\TestLINQJoinConcat\TestLINQJoinConcat\Program.cs	81
		//public static void Apply<T>(this ref IEnumerable<T> e)
		//{

		//}

		public static T[] SelectManyToArray<T>(this IEnumerable<IEnumerable<T>> source)
		{
			return source.SelectMany(x => x).ToArray();
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace FlashAvalonQueryExample.Shared
{
	[Script]
	public static class MySharedExtensions
	{
		/// <summary>
		/// Host names are restricted to a small subset of the ASCII character set known as LDH, 
		/// the Letters A–Z in upper and lower case, 
		/// Digits 0–9, Hyphen, and the dot to separate LDH-labels; see RFC 3696 section 2 for details
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static bool LooksLikeValidCName(this string e)
		{
			// http://en.wikipedia.org/wiki/Domain_Name_System

			var dot_before = 0;
			var dot = 0;
			var dot_after = 0;

			Func<char, char, Func<char, bool>> f =
				(from, to) => c => c.IsInCharArray(from, to);

			var _AZ = f('A', 'Z');
			var _az = f('a', 'z');
			var _09 = f('0', '9');

			Func<char, bool> IsValid =
				c =>
				{
					if (_AZ(c))
						return true;
					if (_az(c))
						return true;
					if (_az(c))
						return true;
					if (c == '.')
						return true;
					return false;
				};

			foreach (var c in e)
			{
				if (c == '.')
					dot++;
				else if (dot == 0)
					dot_before++;
				else
					dot_after++;


				if (!IsValid(c))
				{
					dot = -1;
					break;
				}
			}

			if (dot != 1)
				return false;

			if (dot_before == 0)
				return false;

			if (dot_after == 0)
				return false;

			return true;
		}

		public static bool IsInCharArray(this char e, char from, char to)
		{
			if (e < from)
				return false;

			if (e > to)
				return false;

			return true;
		}





	}
}

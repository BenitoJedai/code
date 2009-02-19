﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace MovieBlog.Server
{
	[Script]
	public class BasicFileNameParser
	{
		// House S05 E15 Unfaithful HDTV XviD-FQM [VTV]-NoRAR
		// Heroes.S03E16.HDTV.XviD-XOR.avi
		// Lost.S05E06.HDTV.XviD-XOR.avi
		// Bones.S04E15.HDTV.XviD-LOL.avi
		// Dexter Season 3 HDTV
		// Heroes.S03E14.HDTV.XviD-LOL.avi
		// Lost.S05E04.HDTV.XviD-XOR.avi
		// Lost.S05E05.HDTV.XviD-McCain.avi
		// Battlestar.Galactica.S04E15.HDTV.XviD-0TV.avi
		// Heroes.S03E15.HDTV.XviD-LOL

		// Thick.As.Thieves[2009]DvDrip-aXXo
		// Extreme.Movie.2008.STV.DVDRip.XviD.MOTION-NoRar¤
		// Australia[2008]DvDrip-aXXo
		// The International CAM XVID 2009.PrisM-NoRar¤
		// The.Day.The.Earth.Stood.Still.2008.DvDrip-NoRar¤
		// The.Librarian.The.Curse.Of.The.Judas.Chalice.DVDRip-NoRar¤
		// Slumdog.Millionaire.DVDSCR.XviD-NoGrp©
		// Slumdog millionaire [DVDSCREENER][Xvid][Spanish]
		// In.the.Electric.Mist.2009.FESTiVAL.DVDRip.XviD.NODLABS-NoRar¤
		// Yes.Man.Scr.XViD-BaLD (NO RARS)
		// Yes.Man.[2008.Eng].DVDScr.DivX-LTT
		// Yes..Man.TS-TELESYNC.Xvid-NoRar¤
		// Rachel Getting Married[2008]DvDrip[Eng]-FXG
		// Taken[2008]DvDrip-aXXo
		// Gran.Torino.2008.DvDRip-FxM
		// seven pounds.[2008.Eng].DVDScr.DivX-LTT
		// Un.Chihuahua.En.Beverly.Hills.[2009].[Spanish].[DVD-Screener].[X
		// Valkiria.[2009].[Spanish].[TS-Screener].[XviD]
		// Choke[2008]DvDrip[Eng]-FXG
		// Bolt(2008)(DVDSCR)
		// Australia.[2008].[Spanish].[DVD-Screener].[XviD]
		// La.Duda.[2009].[Spanish].[DVD-Screener].[XviD]
		// Australia[2008]DvDrip[Eng]-FXG
		// Blindness[2008]DvDrip[Eng]-FXG
		// Max.Payne.2008.Unrated.Edition.DvDrip-NoRar¤
		// 007.James.Bond-Quantum.of.Solace.DVDSCR.XviD-COALiTiON©
		// 7 Almas [DVDSCREENER][Xvid][Castellano][TDT]
		// Transporter 3
		// Billu Barber 2009 PDVDRip XviD[Hindi](No Rars)
		// RocknRolla[2008]DvDrip-aXXo
		// Quarantine.2008.DvDRip-FxM
		// Frozen River[2008]DvDrip[Eng]-FXG
		// W.[2008]DvDrip-aXXo
		// Miracle.At.St.Anna[2008]DvDrip-aXXo
		// Top Gear Back in the Fast Lane KLAXXON
		// End Game KLAXXON
		// Friday.The.13th.TS.XviD-COALiTiON
		// Robocop 2 KLAXXON
		// Seven Pounds[2008]DvDrip[Eng]-FXG

		// to test: 
		// NCIS S6 E16 HDTV-LOL [VTV]-NoRAR
		// Pride.And.Glory.DvDrip-aXXo 
		// Yes.Man.Scr.XViD-BaLD (NO RARS)
		// Yes.Man.Scr.XViD-BaLD.[Movie-Torrentz]
		// CSI.New.York.S05E15.HDTV.XviD-0TV.avi
		// XIII-The.Conspiracy[2008]DvDrip-aXXo
		// Valkyrie Cam XviD (2008) [NNC] LynksÂ©

		public readonly ColoredText ColoredText;

		public string Year;

		public BasicFileNameParser(string e)
		{
			// rule #1 - dots, underspaces are spaces
			// rule #2 - there may be season (Season 3, S03) and/or episode tag
			// rule #3 - there may be a year
			// rule #4 - words with two or more upper case chars may be tags 
			//           if they do not represent roman numbers
			//           if they do not appear before season/episode tag (CSI)
			// rule #5 - a year may come after a tag
			// rule #6 - a year may be enclosed within brackets
			// rule #7 - name may start or container a number
			// rule #8 - name does not span beyond year or tag, or a bracket
			// rule #9 - imdb may not know about the name
			// rule #10 - episode title may appear after episode tag


			var c = new ColoredText(e);

			this.ColoredText = c;

			var BackgroundToGray = "#c0c0c0".FixLastParam<int, int, string>(c.SetBackground);
			var BackgroundToRed = "red".FixLastParam<int, int, string>(c.SetBackground);
			var BackgroundToYellow = "yellow".FixLastParam<int, int, string>(c.SetBackground);
			var BackgroundToCyan = "cyan".FixLastParam<int, int, string>(c.SetBackground);

			#region rule # 1
			e.FindSubstrings(".",
				(offset, length) =>
				{
					BackgroundToYellow(offset, length);

					return offset + length;
				}
			);

			e.FindSubstrings("_",
				(offset, length) =>
				{
					BackgroundToYellow(offset, length);

					return offset + length;
				}
			);
			#endregion

			Func<int, int, int> Discard =
				(offset, length) =>
				{
					BackgroundToGray(offset, e.Length - offset);

					return e.Length;
				};


			// rule #3 
			// rule #8 
			e.FindDigits(4,
				(offset, length) =>
				{
					BackgroundToCyan(offset, length);

					this.Year = e.Substring(offset, length);

					Discard(offset + length, length);
				}
			);

			#region FindEpisodeInfo
			Action<string, Action<string, int, int>> FindEpisodeInfo =
				(prefix, handler) =>
					e.FindSubstrings(prefix,
						(offset, length) =>
						{
							int i = 1;

							for (; i + offset < e.Length; i++)
							{
								if (!char.IsNumber(e[offset + i]))
								{
									break;
								}
							}

							if (i > 1)
							{
								handler(prefix, offset, i);
							}

							return offset + i;
						}
					);
			#endregion

			#region season tag
			var SeasonEndOffset = -1;
			FindEpisodeInfo("S",
				(prefix, offset, length) =>
				{
					Season = e.Substring(offset + prefix.Length, length - prefix.Length);
					SeasonEndOffset = offset;
					BackgroundToRed(offset, length);
				}
			);

			var EpisodeEndOffset = -1;
			FindEpisodeInfo("E",
				(prefix, offset, length) =>
				{
					Episode = e.Substring(offset + prefix.Length, length - prefix.Length);
					EpisodeEndOffset = offset;
					BackgroundToRed(offset, length);
				}
			);

			var SeasonTagEndOffset = EpisodeEndOffset.Max(SeasonEndOffset);
			#endregion


			// rule #4 
			// rule #8 
			e.FindUpperCase(2,
				(offset, length) =>
				{
					// http://en.wikipedia.org/wiki/Roman_numerals
					// roman numbers may be part of the name
					if (e.EnsureChars(offset, length, "IVXLCDM"))
						return offset + length;

					if (SeasonTagEndOffset >= 0)
						if (offset < SeasonTagEndOffset)
							return offset + length;

					return Discard(offset, length);
				}
			);

			e.FindSubstrings("[", Discard);
			e.FindSubstrings("(", Discard);

			

		
			var TitleBuilder = new StringBuilder();
			var SubTitleBuilder = new StringBuilder();

			Func<int, StringBuilder> GetStream =
				offset =>
				{
					if (SeasonTagEndOffset < 0)
						return TitleBuilder;

					if (offset < SeasonTagEndOffset)
						return TitleBuilder;

					return SubTitleBuilder;
				};

			for (int i = 0; i < this.ColoredText.Source.Length; i++)
			{
				if (!string.IsNullOrEmpty(this.ColoredText.Background[i]))
					GetStream(i).Append(" ");
				else
					GetStream(i).Append(this.ColoredText.Source[i]);
			}

			this.Title = TitleBuilder.ToString().Trim();
			this.SubTitle = SubTitleBuilder.ToString().Trim();
		}

		public string Season;
		public string Episode;

		public string Title;
		public string SubTitle;

	}
}

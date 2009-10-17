using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace jsc // .CodeModel
{
	public class ILLoopConstruct
	{
		// while
		//      (
		public ILInstruction CFirst;
		/// <summary>
		/// condition
		/// </summary>
		public ILInstruction CLast;
		//      )
		public ILInstruction Branch;
		// [optional]
		// {
		public ILInstruction BodyFirst;
		public ILInstruction BodyLast;
		// }
		// [optional]
		public ILInstruction Join;



		public bool IsConditionOnly
		{
			get { return BodyFirst == null && BodyLast == null; }
		}

		public bool IsContinue(ILInstruction e)
		{
			if (e != Branch && e != null)
			{
				if (e.TargetInstruction == CFirst)
				{
					return true;
				}
			}

			return false;
		}

		public bool IsBreak(ILInstruction e)
		{
			if (e != Branch && e != null)
			{
				if (e.TargetInstruction == Join)
				{
					return true;
				}
			}

			return false;
		}

		public bool IsBody(ILInstruction i)
		{
			if (IsContinue(i))
				return false;

			if (IsBreak(i))
				return false;


			return true;
		}
		public string ToString(ILInstruction e)
		{
			try
			{
				StringWriter w = new StringWriter();


				if (IsContinue(e))
				{
					w.Write("continue");
					return w.ToString();
				}

				if (IsBreak(e))
				{
					w.Write("break");
					return w.ToString();
				}


				w.Write("while ( [0x{0:x4} to 0x{1:x4}] ) "
					  , CFirst.Offset
					  , CLast.Offset);

				if (!IsConditionOnly)
				{
					w.Write("[0x{0:x4} to 0x{1:x4}] "
						  , BodyFirst.Offset
						  , BodyLast.Offset
						  );
				}

				if (Join == null)
				{
					w.Write("nojoin");
				}
				else
				{
					w.Write("join [0x{0:x4}]"
						, Join.Offset);
				}

				return w.ToString();
			}
			catch
			{
				return "error";
			}
		}

		public override string ToString()
		{
			return ToString(null);


		}
	}

}

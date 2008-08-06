using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;

namespace FlashSpaceInvaders.ActionScript
{
	[Script]
	public class DefenseBlock : SolidColorShape, IFragileEntity
	{
		public const int BlockSize = 16;
		public const uint BlockColor = Colors.Green;

		public DefenseBlock() : base(BlockSize, BlockColor)
		{

		}

		#region ITakeDamage Members

		public void TakeDamage(double damage)
		{
			this.alpha -= damage;
		}

		#endregion

		#region IWithLocation Members

		public ScriptCoreLib.ActionScript.flash.geom.Point Location
		{
			get { return this.ToPoint(); }
		}

		#endregion

		#region IHitPoints Members

		public double HitPoints
		{
			get { return this.alpha; }
		}

		#endregion



		#region IHitRange Members

		public double HitRange
		{
			get { return BlockSize; }
		}

		#endregion
	}
}

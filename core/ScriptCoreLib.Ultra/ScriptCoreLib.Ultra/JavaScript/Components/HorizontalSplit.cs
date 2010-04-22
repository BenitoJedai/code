using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.Components
{
	public class HorizontalSplitBase
	{
		// to enable vertical split? :)

		public readonly IHorizontalSplitConcept Split;
		readonly IHorizontalSplitAreaConcept SplitArea;

		public class Arguments
		{
			public IHorizontalSplitConcept Split;
			public IHorizontalSplitAreaConcept SplitArea;

			public IHTMLImage SplitImage;

			public int SplitImageWidth;
			public int SplitImageHeight;
		}

		public double Minimum = 0.2;
		public double Maximum = 0.8;

		double InternalValue;
		public double Value
		{
			set
			{
				this.InternalValue = value;
				this.InternalSetValue(Convert.ToInt32(value * 100));
			}
			get
			{
				return this.InternalValue;
			}
		}
		Action<int> InternalSetValue;

		public IHTMLDiv LeftContainer
		{
			get
			{
				return this.Split.LeftContainer;
			}
			set
			{
				this.Split.LeftContainer = value;
			}
		}

		public IHTMLDiv RightContainer
		{
			get
			{
				return this.Split.RightContainer;
			}
			set
			{
				this.Split.RightContainer = value;
			}
		}


		public HorizontalSplitBase(Arguments args)
		{
			this.Split = args.Split;
			this.SplitArea = args.SplitArea;

			var hs = args.Split;
			var hsArea = args.SplitArea;

			var hsa = args.SplitImage;

			var hsm = new IHTMLDiv();
			hsm.AttachTo(hs.Splitter);
			hsm.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			hsm.style.left = "1px";
			hsm.style.top = "50%";
			hsm.style.marginTop = (-args.SplitImageHeight / 2) + "px";

			hsa.AttachTo(hsm);


			hsArea.Abort.style.Opacity = 0.05;


			var dragmode = false;

			hsArea.Target.onmousedown +=
				ee =>
				{
					hsArea.Target.style.backgroundColor = ScriptCoreLib.JavaScript.Runtime.JSColor.System.Highlight;
					dragmode = true;

					ee.PreventDefault();
					hsArea.Abort.style.Opacity = 0.05;
				};

			hsArea.PageContainer.onmousemove +=
				ee =>
				{
					var OffsetX = ee.GetOffsetX(hsArea.PageContainer);


					if (!dragmode)
						return;

					var p = System.Convert.ToInt32(OffsetX * 100 / hsArea.PageContainer.offsetWidth);

					if (p < Convert.ToInt32(Minimum * 100))
						p = Convert.ToInt32(Minimum * 100);
					if (p > Convert.ToInt32(Maximum * 100))
						p = Convert.ToInt32(Maximum * 100);

					hsArea.Target.style.left = p + "%";
				};

			InternalSetValue =
				p =>
				{
					if (p < Convert.ToInt32(Minimum * 100))
						p = Convert.ToInt32(Minimum * 100);
					if (p > Convert.ToInt32(Maximum * 100))
						p = Convert.ToInt32(Maximum * 100);

					hsArea.Target.style.left = p + "%";
					hs.Right.style.left = p + "%";
					hs.Right.style.width = (100 - p) + "%";
					hs.Left.style.width = p + "%";
				};

			hsArea.PageContainer.onmouseup +=
				ee =>
				{
					if (!dragmode)
						return;

					var OffsetX = ee.GetOffsetX(hsArea.PageContainer);

					dragmode = false;
					var p = System.Convert.ToInt32(OffsetX * 100 / hsArea.PageContainer.offsetWidth);

					Value = p * 0.01;

					hsArea.Abort.style.Opacity = 0;
					hsArea.Target.style.backgroundColor = ScriptCoreLib.JavaScript.Runtime.JSColor.None;

				};

			hsArea.Abort.onmousemove +=
				ee =>
				{
					if (dragmode)
					{
						return;
					}

					hsArea.Target.style.backgroundColor = ScriptCoreLib.JavaScript.Runtime.JSColor.None;
					hsArea.PageContainer.Orphanize();
				};

			hs.Splitter.onmouseover +=
				delegate
				{
					hsArea.Abort.style.Opacity = 0.05;

					hsArea.PageContainer.AttachTo(hs.ContentContainer);
				};
		}

		public IHTMLDiv Container
		{
			get
			{
				return this.Split.ContentContainer;
			}
		}
	}

}

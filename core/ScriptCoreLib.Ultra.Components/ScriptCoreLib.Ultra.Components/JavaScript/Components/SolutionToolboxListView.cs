using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Lambda;
using System.ComponentModel;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Ultra.Studio;

namespace ScriptCoreLib.JavaScript.Components
{
	public class SolutionToolboxListView : IEnumerable<SolutionToolboxListViewTab>
	{
		public IHTMLDiv Container { get; private set; }

		public BindingListWithEvents<SolutionToolboxListViewTab> Tabs { get; private set; }

		public void Add(SolutionToolboxListViewTab e)
		{
			this.Tabs.Source.Add(e);
		}

		public SolutionToolboxListView()
		{
			this.Container = new IHTMLDiv();
			this.Container.style.minWidth = "12em";
			
			this.Tabs = new BindingList<SolutionToolboxListViewTab>().WithEvents(
				NewTab =>
				{
					var ItemBackground = NewTab.Icon;

					var ItemContainer = new IHTMLDiv().AttachTo(Container);

					ItemContainer.style.position = IStyle.PositionEnum.relative;
					ItemContainer.style.height = "50px";
					ItemContainer.style.borderWidth = "1px";
					ItemContainer.style.borderStyle = "solid";
					ItemContainer.style.borderColor = Color.White;

					ItemContainer.onmouseover +=
						delegate
						{
							ItemContainer.style.borderColor = Color.Gray;
						};

					ItemContainer.onmouseout +=
						delegate
						{
							ItemContainer.style.borderColor = Color.White;
						};


					var ItemContent = new IHTMLDiv().AttachTo(ItemContainer);

					ItemContent.style.position = IStyle.PositionEnum.absolute;
					ItemContent.style.left = "1px";
					ItemContent.style.right = "1px";
					ItemContent.style.top = "1px";
					ItemContent.style.bottom = "1px";
					ItemContent.style.overflow = IStyle.OverflowEnum.hidden;

					var ItemCenter = new IHTMLDiv().AttachTo(ItemContent);

					ItemCenter.style.position = IStyle.PositionEnum.absolute;
					ItemCenter.style.left = "50%";
					ItemCenter.style.right = "50%";
					ItemCenter.style.marginLeft = "-200px";
					ItemCenter.style.marginTop = ((300 - 48) / -2) + "px";

					var ItemCenterContainer = new IHTMLDiv().AttachTo(ItemCenter);

					ItemCenterContainer.style.SetSize(400 - 2, 300 - 2);
					ItemCenterContainer.style.overflow = IStyle.OverflowEnum.hidden;
					ItemCenterContainer.style.position = IStyle.PositionEnum.relative;

					var ItemCenterInfoContainer = new IHTMLDiv().AttachTo(ItemCenterContainer);

					ItemCenterInfoContainer.style.SetLocation(-1, -1);
					ItemCenterInfoContainer.style.SetSize(400, 300);

					var ItemCenterInfo = new IHTMLDiv().AttachTo(ItemCenterInfoContainer);

					ItemCenterInfo.style.position = IStyle.PositionEnum.absolute;
					//ItemCenterInfo.innerText = "Windows Forms Control";
					ItemCenterInfo.style.left = "30%";
					ItemCenterInfo.style.top = "50%";
					//ItemCenterInfo.style.SetSize(48, 48);
					//ItemCenterInfo.style.backgroundColor = Color.Yellow;

					var ItemCenterInfoContent = new IHTMLDiv().AttachTo(ItemCenterInfo);

					ItemCenterInfoContent.style.position = IStyle.PositionEnum.absolute;
					ItemCenterInfoContent.style.display = IStyle.DisplayEnum.inline_block;
					ItemCenterInfoContent.style.left = "32px";
					ItemCenterInfoContent.style.top = "-0.5em";
					//ItemCenterInfoContent.style.backgroundColor = Color.Red;
					ItemCenterInfoContent.style.whiteSpace = IStyle.WhiteSpaceEnum.pre;
					ItemCenterInfoContent.style.fontFamily = IStyle.FontFamilyEnum.Tahoma;
					ItemCenterInfoContent.innerText = NewTab.Text;

					NewTab.Changed +=
						delegate
						{
							ItemCenterInfoContent.innerText = NewTab.Text;
						};

					var ItemCenterImageContainer = new IHTMLDiv().AttachTo(ItemCenterContainer);

					ItemCenterImageContainer.style.SetLocation(-1, -1);
					ItemCenterImageContainer.style.SetSize(400, 300);


					var ItemImage = new StockToolboxImageTransparent64().AttachTo(ItemCenterImageContainer);

					// safari seems to drag and drop only img.src
					ItemImage.src += "?" + SolutionBuilderInteractive.DataTypeAttribute + "=" + NewTab.DataType;
				

					ItemImage.style.SetSize(400, 300);
					ItemImage.style.borderWidth = "1px";
					ItemImage.style.borderStyle = "solid";
					ItemImage.style.borderColor = Color.Gray;
					ItemImage.style.backgroundPosition = "30% 50%";
					ItemImage.style.cursor = IStyle.CursorEnum.move;
					ItemImage.title = NewTab.Title;
					ItemImage.id = NewTab.Name;

					// http://ejohn.org/blog/html-5-data-attributes/
					ItemImage.setAttribute(SolutionBuilderInteractive.DataTypeAttribute, NewTab.DataType);

					ItemBackground.ToBackground(ItemImage.style);

					ItemImage.style.backgroundRepeat = "no-repeat";

					return delegate
					{
						ItemContainer.Orphanize();
					};
				}
			);
		}

		#region IEnumerable<SolutionToolboxListViewTab> Members

		public IEnumerator<SolutionToolboxListViewTab> GetEnumerator()
		{
			return this.Tabs.Source.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.Tabs.Source.GetEnumerator();
		}

		#endregion
	}
}

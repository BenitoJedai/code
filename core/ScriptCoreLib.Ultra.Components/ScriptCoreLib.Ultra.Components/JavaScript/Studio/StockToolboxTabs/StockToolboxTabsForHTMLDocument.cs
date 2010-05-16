using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.Ultra.Studio;

namespace ScriptCoreLib.JavaScript.Studio.StockToolboxTabs
{
	public class StockToolboxTabsForHTMLDocument : IEnumerable<SolutionToolboxListViewTab>
	{
		public readonly SolutionToolboxListViewTab Page;

		public readonly SolutionToolboxListViewTab UserControl;

		public readonly SolutionToolboxListViewTab Canvas;

		public readonly SolutionToolboxListViewTab AppletUserControl;

		public readonly SolutionToolboxListViewTab Applet;

		public readonly SolutionToolboxListViewTab SpriteCanvas;

		public readonly SolutionToolboxListViewTab Sprite;

		public readonly SolutionToolboxListViewTab SpriteTransparent;

		public StockToolboxTabsForHTMLDocument()
		{
			Page = new SolutionToolboxListViewTab
			{
				Icon = new StockToolboxImageForHTMLDocument(),
				Name = "Page1",
				Title = "HTML Document",
				Text = "HTML Document"
			};

			UserControl = new SolutionToolboxListViewTab
			{
				Icon = new StockToolboxImageForFormsControl(),
				Name = "UserControl1",
				Title = "Windows Forms UserControl",
				Text = "UserControl"
			};

			Canvas = new SolutionToolboxListViewTab
			{
				Icon = new StockToolboxImageForAvalonCanvas(),
				Name = "Canvas1",
				Title = "Windows Presentation Foundation Canvas",
				Text = "Canvas"
			};

			AppletUserControl = new SolutionToolboxListViewTab
			{
				Icon = new StockToolboxImageForJavaAppletFormsControl(),
				Name = "AppletUserControl1",
				Title = "Java Applet with Windows Forms UserControl",
				Text = "Applet UserControl"
			};

			Applet = new SolutionToolboxListViewTab
			{
				Icon = new StockToolboxImageForJavaApplet(),
				Name = "Applet1",
				Title = "Java Applet",
				Text = "Applet"
			};

			SpriteCanvas = new SolutionToolboxListViewTab
			{
				Icon = new StockToolboxImageForFlashSpriteAvalonCanvas(),
				Name = "SpriteCanvas1",
				Title = "Flash Sprite with Windows Presentation Foundation Canvas",
				Text = "Flash Canvas"
			};


			Sprite = new SolutionToolboxListViewTab
			{
				Icon = new StockToolboxImageForFlashSprite(),
				Name = "Sprite1",
				Title = "Flash Sprite",
				Text = "Flash"
			};

			SpriteTransparent = new SolutionToolboxListViewTab
			{
				Icon = new StockToolboxImageForFlashSprite(),
				Name = "SpriteTransparent1",
				Title = "Flash Sprite Transparent",
				Text = "Flash Transparent"
			};

			InternalArray = new[]
			{
				Page,
				UserControl,
				Canvas,
				AppletUserControl,
				Applet,
				SpriteCanvas,
				Sprite,
				SpriteTransparent
			};

			foreach (var item in InternalArray)
			{
				
				item.DataType = item.Name;
			}
		}

		readonly SolutionToolboxListViewTab[] InternalArray;

		#region IEnumerable<SolutionToolboxListViewTab> Members

		public IEnumerator<SolutionToolboxListViewTab> GetEnumerator()
		{
			return this.InternalArray.AsEnumerable().GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.InternalArray.AsEnumerable().GetEnumerator(); 
		}

		#endregion
	}
}

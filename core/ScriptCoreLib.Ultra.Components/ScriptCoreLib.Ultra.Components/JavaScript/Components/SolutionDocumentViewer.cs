using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using System.ComponentModel;
using ScriptCoreLib.Shared.Lambda;

namespace ScriptCoreLib.JavaScript.Components
{
	public class SolutionDocumentViewer : IEnumerable<SolutionDocumentViewerTab>
	{
		readonly SolutionDocumentViewerPage dv = new SolutionDocumentViewerPage();


		public IHTMLDiv Container { get; private set; }

		public IHTMLDiv Content
		{
			get
			{
				return dv.Content;
			}
			set
			{
				dv.Content.ReplaceWith(value);
			}
		}


		public BindingListWithEvents<SolutionDocumentViewerTab> Tabs { get; private set; }

		public void Add(SolutionDocumentViewerTab e)
		{
			this.Tabs.Source.Add(e);
		}

		
		

		public SolutionDocumentViewer()
		{


			dv.TabContainer.Clear();
			this.Container = dv.Container;

			Action Deselect = delegate { };

			dv.TabContainer.onmouseover +=
				delegate
				{
					dv.TabContainer.style.height = "3em";
					dv.TabContainer.style.overflowY = IStyle.OverflowEnum.hidden;
					dv.TabContainer.style.overflowX = IStyle.OverflowEnum.auto;

				};

			dv.TabContainer.onmouseout +=
				delegate
				{
					dv.TabContainer.style.height = "";
					dv.TabContainer.style.overflowY = IStyle.OverflowEnum.visible;
					dv.TabContainer.style.overflowX = IStyle.OverflowEnum.hidden;
				};


			this.Tabs = new BindingList<SolutionDocumentViewerTab>().WithEvents(
				NewTab =>
				{
					var Text = NewTab.Text;

					#region create

					var Current = new IHTMLDiv().AttachTo(dv.TabContainer);

					Current.style.display = IStyle.DisplayEnum.inline_block;

					var Prototype = new SolutionDocumentViewerPage();

					var ActiveTab = Prototype.ActiveTab.AttachTo(Current);
					var CandidateTab = Prototype.CandidateTab.AttachTo(Current);
					var InactiveTab = Prototype.InactiveTab.AttachTo(Current);

					ActiveTab.style.display = IStyle.DisplayEnum.none;
					CandidateTab.style.display = IStyle.DisplayEnum.none;

					Prototype.ActiveTabText.innerText = Text;
					Prototype.CandidateTabText.innerText = Text;
					Prototype.InactiveTabText.innerText = Text;

					var IsActive = false;

					Current.onmouseover +=
						delegate
						{
							if (IsActive)
								return;


							InactiveTab.style.display = IStyle.DisplayEnum.none;
							CandidateTab.style.display = IStyle.DisplayEnum.empty;

						};

					Current.onmouseout +=
						delegate
						{
							if (IsActive)
								return;

							InactiveTab.style.display = IStyle.DisplayEnum.empty;
							CandidateTab.style.display = IStyle.DisplayEnum.none;
						};

					Action Activate =
						delegate
						{
							if (!IsActive)
							{
								IsActive = true;
								ActiveTab.style.display = IStyle.DisplayEnum.empty;
								CandidateTab.style.display = IStyle.DisplayEnum.none;
								InactiveTab.style.display = IStyle.DisplayEnum.none;

								Deselect();
								Deselect = delegate
								{
									IsActive = false;
									ActiveTab.style.display = IStyle.DisplayEnum.none;
									InactiveTab.style.display = IStyle.DisplayEnum.empty;
								};

								NewTab.RaiseActivated();
							}
						};

					NewTab.Activate = Activate;

					Current.onclick +=
						delegate
						{
							Activate();
						};

					#endregion

					return delegate
					{
						Current.Orphanize();
					};
				}
			);



			//this.Add("Tab1");
			//this.Add("Tab2");
			//this.Add("Tab3");
		}

		public void Clear()
		{
			this.Tabs.Source.Clear();
		}

		#region IEnumerable<Tab> Members

		public IEnumerator<SolutionDocumentViewerTab> GetEnumerator()
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

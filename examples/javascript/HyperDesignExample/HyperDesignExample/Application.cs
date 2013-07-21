using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HyperDesignExample.HTML.Pages;
using System.Collections.Generic;
using ScriptCoreLib.Shared.Drawing;

namespace HyperDesignExample
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApplication page)
        {
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );

            InitializeContent(page);
        }

		public class Employee
		{
			public int Number;

            public string FirstName = "John";
            public string LastName = "Doe";

			public string Bio = "No Bio yet!";

			// new data field in the details view

			public string Location = "Default Location";

		}

        public void InitializeContent(IApplication n)
		{
            //var n = new HyperDesignExample.HTML.Pages.Application();
			
            //n.Container.AttachToDocument();

            
            n.Read.onclick +=
                delegate
                {
                    n.FirstName.value = "John";
                    n.LastName.value = "Doe";
                };

			// more code...
			// lets make some changes to the template code...
			// nice huh? :D

			var Items = new IHTMLDiv();

			Items.style.height = "300px";
			Items.style.overflow = IStyle.OverflowEnum.auto;
			Items.style.backgroundColor = Color.System.ButtonFace;
			Items.style.borderWidth = "2px";
			Items.style.borderStyle = "solid";
			Items.style.borderColor = Color.System.ButtonShadow;

			Items.AttachToDocument();

			var List = new List<Employee>();

			n.Add.onclick +=
				delegate
				{
					var i = new HyperDesignExample.HTML.Pages.Summary();
					var j = new Employee
					{
						Number = List.Count + 1,
						FirstName = n.FirstName.value,
						LastName = n.LastName.value
					};
					List.Add(j);

					Action Update =
						delegate
						{
							i.Number.value = "#" + j.Number;
							i.FirstName.value = j.FirstName;
							i.LastName.value = j.LastName;
						};

					Update();

					i.Delete.onclick +=
						delegate
						{
							i.Container.Orphanize();
						};

					i.Container.AttachTo(Items);

					i.Edit.onclick +=
						delegate
						{
							i.Edit.disabled = true;

							var details = new HyperDesignExample.HTML.Pages.Details();

							details.FirstName.value = i.FirstName.value;
							details.LastName.value = i.LastName.value;
							details.Bio.value = j.Bio;
							details.Location.value = j.Location;

							details.Container.AttachTo(i.Details);

							details.Discard.onclick +=
								delegate
								{
                                    details.Container.Orphanize();
									i.Edit.disabled = false;
								};

							details.Save.onclick +=
								delegate
								{
                                    details.Container.Orphanize();
									i.Edit.disabled = false;

									j.Bio = details.Bio.value;
									j.FirstName = details.FirstName.value;
									j.LastName = details.LastName.value;
									j.Location = details.Location.value;

									Update();
								};
						};
				};
		}

    }
}

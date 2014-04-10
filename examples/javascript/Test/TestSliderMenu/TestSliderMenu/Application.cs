using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSliderMenu;
using TestSliderMenu.Design;
using TestSliderMenu.HTML.Pages;
using TestSliderMenu.TTF;

namespace TestSliderMenu
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            bool isVisible = false;
            var fontAwsome = new TTF.fontawesome_webfont();
            
            var dashboard = new MenuButton();
            dashboard.ButtonText.innerText = "Dashboard";
            dashboard.MenuButton.css.hover.style.backgroundColor = "#97829a";

            dashboard.ButtonText.css.before.style.fontFamily = fontAwsome;
            dashboard.ButtonText.css.before.contentText = "\xf009";
            dashboard.ButtonText.css.before.style.paddingRight = "5px";

            var games = new MenuButton();
            games.ButtonText.innerText = "games";
            games.MenuButton.css.hover.style.backgroundColor = "#97829a";

            games.ButtonText.css.before.style.fontFamily = fontAwsome;
            games.ButtonText.css.before.contentText = "\xf11b";
            games.ButtonText.css.before.style.paddingRight = "5px";


            var menu = new SliderMenu();
            menu.MenuHeader.innerText = "Abstractatech";
            menu.SliderMenu.style.fontFamily = new TTF.OpenSans_Semibold();


            dashboard.AttachTo(menu.SliderMenu);
            games.AttachTo(menu.SliderMenu);

            menu.AttachTo(page.MenuHolder);


            page.Click.onclick += delegate
            {
                if (!isVisible)
                {
                    isVisible = true;
                    page.MenuHolder.style.width = "15em";
                    menu.SliderMenu.style.width = "15em";
                }
                else
                {
                    isVisible = false;
                    page.MenuHolder.style.width = "0em";
                    menu.SliderMenu.style.width = "0em";
                }
            };

            #region Styles

            new IStyle(menu.SliderMenu)
            {
                transition = "width 0.5s ease-in-out",
                Opacity = 1,
                width = "0em"
            };
            new IStyle(page.MenuHolder)
            {
                transition = "width 0.5s ease-in-out",
                Opacity = 1,
                width = "0em"
            };


            page.Click.style.fontFamily = fontAwsome;
            page.Click.innerText = "\xf00b";

            new IStyle(page.Click)
            {
                border = "0",
                color="#fff",
                fontSize = "2em",
                padding = "10px 10px 7px 10px",
                backgroundColor = "#97829a"
            };
            #endregion

        }

    }
}

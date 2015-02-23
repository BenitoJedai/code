extern alias xglobal;

using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using xglobal::chrome;


namespace chrome
{
    //chrome.pageAction





    public class Tab_pageAction_async
    {
        // https://github.com/darwin/chromium-src-chrome-browser/blob/master/extensions/page_action_controller.h
        // https://github.com/darwin/chromium-src-chrome-browser/blob/master/extensions/page_action_controller.cc

        public Tab_pageAction pageAction;

        public Task<Tab_pageAction> onclick
        {
            get
            {
                // tested by
                // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionWithWorker\ChromeExtensionWithWorker\Application.cs

                var c = new TaskCompletionSource<Tab_pageAction>();


                // show our icon?
                pageAction.Tab.id.show();

                xglobal::chrome.pageAction.Clicked +=
                    xtab =>
                    {
                        if (xtab.id != pageAction.Tab.id)
                            return;


                        pageAction.Tab.id.hide();

                        c.SetResult(pageAction);
                    };


                return c.Task;
            }
        }

    }

    public class Tab_pageAction
    {
        public Tab_pageAction_async async
        {
            get
            {
                return new Tab_pageAction_async { pageAction = this };
            }
        }

        public Tab Tab;
    }

    public class Tab
    {
        // https://github.com/darwin/chromium-src-chrome-browser/blob/master/extensions/extension_tab_util.cc
        // https://github.com/darwin/chromium-src-chrome-browser/blob/master/extensions/extension_tab_util_android.cc

        // https://github.com/darwin/chromium-src-chrome-browser/blob/master/extensions/api/tabs/tabs_api.cc
        // https://github.com/darwin/chromium-src-chrome-browser/blob/master/extensions/api/tabs/tabs_api.h

        // extensions for android in 2015?

        public xglobal.chrome.TabIdInteger id;

        // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionWithWorker\ChromeExtensionWithWorker\Application.cs

        public Tab_pageAction pageAction
        {
            [method: Script(DefineAsStatic = true)]
            get
            {
                return new Tab_pageAction { Tab = this };
            }
        }
    }
}

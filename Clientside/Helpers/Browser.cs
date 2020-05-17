using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RAGE;
using RAGE.Ui;

namespace Clientside.Helpers {
    class Browser : Events.Script {
        private static List<HtmlWindow> _customBrowsers = new List<HtmlWindow>();

        public Browser() {
            Events.Add("createBrowser", CreateBrowserEvent);
            Events.Add("destroyBrowser", DestroyBrowserEvent);

            Events.Add("executeFunction", ExecuteFunctionEvent);
        }

        public static void CreateBrowserEvent(object[] args) {
            string url = args[0].ToString();

            var browser = _customBrowsers.FirstOrDefault(x => x.Url == url);

            if (browser == null) {
                browser = new HtmlWindow(url);
                _customBrowsers.Add(browser);
            }

            if (args.Length > 1) {
                ExecuteFunctionEvent(args);
            }
        }

        public static void ExecuteFunctionEvent(object[] args) {
            var url = args[0].ToString();
            var function = args[1].ToString();

            var eventArgs = new List<object>();

            if (args.Length > 2) {
                eventArgs.AddRange(args.Skip(2));
            }

            var input = string.Empty;
            foreach (var arg in eventArgs) {
                input += input.Length > 0 ? (", '" + arg.ToString() + "'") : ("'" + arg.ToString() + "'");
            }

            var functionWithParams = $"{function}({input});";

            var browser = _customBrowsers.FirstOrDefault(x => x.Url == url);
            if (browser != null) {
                browser.ExecuteJs(functionWithParams);
            }
        }

        public static void DestroyBrowserEvent(object[] args) {
            var url = args[0].ToString();

            var browser = _customBrowsers.FirstOrDefault(x => x.Url == url);

            if (browser != null) {
                browser.Destroy();

                _customBrowsers.Remove(browser);
            }
        }

        public static void SetCursorVisible(bool visible) {
            Cursor.Visible = visible;
        }

        //public static void MarkAsChat() {
        //    if (customBrowser != null) {
        //        customBrowser.MarkAsChat();
        //    }
        //}
    }
}

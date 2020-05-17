using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clientside.Enums;
using Clientside.Helpers;
using RAGE;
using RAGE.Elements;
using static RAGE.Events;

namespace Clientside.Controllers {
    public class ClientBrowser : Script {
        public ClientBrowser() {
            Events.Add("ExecuteRemoteEvent", ExecuteRemoteEvent);

            Events.Add("CreateBrowserEvent", CreateBrowserEvent);
            Events.Add("DestroyBrowserEvent", DestroyBrowserEvent);

            Events.Add("ExecuteBrowserFunction", ExecuteBrowserFunction);
        }

        private void ExecuteRemoteEvent(object[] args) {
            try {
                var eventName = args[0].ToString();

                if (args.Length > 1) {
                    var eventArgs = args.Skip(1).ToArray();

                    Events.CallRemote(eventName, eventArgs);
                }
                else {
                    Events.CallRemote(eventName);
                }
            }
            catch (Exception ex) {
                Chat.Output($"ExecuteRemoteEvent: {ex.Message}");
            }
        }

        private void CreateBrowserEvent(object[] args) {
            try {
                var url = args[0].ToString();
                var cursorVisible = (bool)args[1];

                var eventArgs = new List<object>();
                eventArgs.Add(url);

                if (args.Length > 2) {
                    eventArgs.AddRange(args.Skip(2));
                }

                Browser.CreateBrowserEvent(eventArgs.ToArray());

                Browser.SetCursorVisible(cursorVisible);
            }
            catch (Exception ex) {
                Chat.Output($"ExecuteBrowserEvent: {ex.Message}");
            }
        }

        private void DestroyBrowserEvent(object[] args) {
            try {
                var url = args[0].ToString();
                var cursorVisible = (bool)args[1];

                Browser.DestroyBrowserEvent(args);

                Browser.SetCursorVisible(cursorVisible);
            }
            catch (Exception ex) {
                Chat.Output($"ExecuteBrowserEvent: {ex.Message}");
            }
        }

        private void ExecuteBrowserFunction(object[] args) {
            try {
                Browser.ExecuteFunctionEvent(args);
            }
            catch (Exception ex) {
                Chat.Output($"ExecuteBrowserFunction: {ex.Message}");
            }
        }
    }
}

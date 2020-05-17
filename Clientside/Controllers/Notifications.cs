using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.Elements;
using static RAGE.Events;

namespace Clientside.Controllers {
    public class Notifications : Script {
        public Notifications() {
            Events.Add("ShowNotification", ShowNotification);
        }

        private void ShowNotification(object[] args) {
            throw new NotImplementedException();
        }
    }
}

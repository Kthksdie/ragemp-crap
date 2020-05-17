using System;
using System.Collections.Generic;
using System.Text;

namespace Serverside.Entities {
    public class IPAddress {
        public IPAddress() {
            Timestamp = DateTime.UtcNow;
        }

        public string Address { get; set; }

        public DateTime Timestamp { get; set; }
    }
}

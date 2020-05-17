using System;
using System.Collections.Generic;
using System.Text;

namespace Serverside.Interfaces {
    public interface IItem {
        string Id { get; set; }

        bool Stackable { get; set; }

        int Uses { get; set; }

        int Amount { get; set; }

        int MaxStack { get; set; }
    }
}

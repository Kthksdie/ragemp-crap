using System;
using System.Collections.Generic;
using System.Text;

namespace Serverside.Entities {
    public class AnimationSet {
        public AnimationSet() {
            ActorAnims = new List<int>();
            ObjectAnims = new List<int>();
            Objects = new List<string>();
        }

        public int Index { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public bool ActorsAligned { get; set; }

        public float DeltaZ { get; set; }

        public List<int> ActorAnims { get; set; }

        public List<int> ObjectAnims { get; set; }

        public List<string> Objects { get; set; }
    }
}

using System;

namespace BlockMoveGame.Things {
    public class Cords : ICloneable {
        public int X { get; set; }
        public int Y { get; set; }

        public object Clone() {
            return new Cords {X = X, Y = Y};
        }
    }
}
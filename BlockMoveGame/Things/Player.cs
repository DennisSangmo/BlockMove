using System;

namespace BlockMoveGame.Things
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Color { get; set; }
        public int YPosition { get; set; }
        public int XPosition { get; set; }

        public Player(string color) {
            Id = Guid.NewGuid();
            Color = color;
            YPosition = 0;
            XPosition = 0;
        }
    }
}

using System;

namespace BlockMoveGame.Things
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
        public int Score { get; set; }
        public Cords Cords { get; set; }

        public Player(string color, int size, Cords cords) {
            Id = Guid.NewGuid();
            Color = color;
            Size = size;
            Cords = cords;
        }
    }
}

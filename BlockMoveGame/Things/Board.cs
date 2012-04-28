using System;
using System.Collections.Generic;
using System.Linq;
using BlockMoveGame.Doers;

namespace BlockMoveGame.Things {
    public class Board {
        private int Width { get; set; }
        private int Height { get; set; }
        private List<Player> Players { get; set; }
        public PointBlip PointBlip { get; set; }

        public Board(int width, int height) {
            Width = width;
            Height = height;
            Players = new List<Player>();
            NewPointBlip();
        }

        public void Reset()
        {
            Players = new List<Player>();
            NewPointBlip();
        }

        public Player AddPlayer(string color) {
            var playerSize = Settings.PlayerSize;
            var cords = RandomPosition(playerSize);
            var player = new Player(color, playerSize, cords);
            Players.Add(player);
            return player;
        }

        public Player MovePlayer(string id, string direction) {
            var player = Players.SingleOrDefault(x => x.Id.ToString() == id);

            var distance = Settings.StepDistance;

            var cords = player.Cords.Clone() as Cords;

            switch (direction)
            {
                case Directions.Up:
                    cords.Y -= distance;
                    break;
                case Directions.Right:
                    cords.X += distance;
                    break;
                case Directions.Down:
                    cords.Y += distance;
                    break;
                case Directions.Left:
                    cords.X -= distance;
                    break;
            }

            cords = WallCollition(cords, player.Size);

            if (PointCollition(cords, player.Size)) {
                NewPointBlip();
                player.Score++;
            }

            player.Cords = cords;
            return player;
        }

        private void NewPointBlip() {
            PointBlip = new PointBlip(Settings.PlayerSize, RandomPosition(Settings.PlayerSize));
        }

        public PointBlip GetPointBlip() {
            return PointBlip;
        }

        public IEnumerable<Player> GetPlayers() {
            return Players;
        }

        private Cords RandomPosition(int objectSize) {
            var random = new Random();
            var randX = random.Next((Width - objectSize)/objectSize);
            var randY = random.Next((Height - objectSize)/objectSize);
            return new Cords{X = randX*objectSize, Y = randY*objectSize};
        }

        // Collition handlers

        private bool PointCollition(Cords playerCords, int playerSize)
        {
            for (var y = 0; y < playerSize; y++)
            {
                for (var x = 0; x < playerSize; x++)
                {
                    var currentY = playerCords.Y + y;
                    var currentX = playerCords.X + x;
                    if (
                        currentY >= PointBlip.Cords.Y &&
                        currentY < PointBlip.Cords.Y + PointBlip.Size &&
                        currentX >= PointBlip.Cords.X &&
                        currentX < PointBlip.Cords.X + PointBlip.Size
                      )
                        return true;
                }
            }
            return false;
        }

        private Cords WallCollition(Cords cords, int objectSize)
        {
            // Up
            if (cords.Y < 0) cords.Y = 0;

            // Right
            if (cords.X + objectSize > Width) cords.X = Width - objectSize;

            // Down
            if (cords.Y + objectSize > Height) cords.Y = Height - objectSize;

            // Left
            if (cords.X < 0) cords.X = 0;

            return cords;
        }
    }
}
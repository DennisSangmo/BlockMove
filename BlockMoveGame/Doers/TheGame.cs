using System.Collections.Generic;
using System.Linq;
using BlockMoveGame.Things;

namespace BlockMoveGame.Doers {
    public class TheGame {
        private static TheGame _theGame;
        public static TheGame TheGameObject { get { return _theGame ?? (_theGame = new TheGame()); } }

        public Board Board { get; set; }
        public List<Player> Players { get; set; }

        public bool Initialized { get; private set; }

        public TheGame()
        {
            Players = new List<Player>();
        }

        public Player AddPlayer(string color) {
            if (Players.Any(x => x.Color == color))
                return null;

            var player = new Player(color);
            Players.Add(player);
            return player;
        }

        public Player GetPlayer(string id) {
            return Players.SingleOrDefault(x => x.Id.ToString() == id);
        }

        public void Initialize(int width, int height) {
            Board = new Board(width, height);
        }

        public Player MovePlayer(string id, string direction) {
            var player = GetPlayer(id);

            if (player == null)
                return null;

            var distance = Settings.StepDistance;
            
            switch (direction)
            {
                case Directions.Up:
                    player.YPosition -= distance;
                    break;
                case Directions.Right:
                    player.XPosition += distance;
                    break;
                case Directions.Down:
                    player.YPosition += distance;
                    break;
                case Directions.Left:
                    player.XPosition -= distance;
                    break;
            }

            return player;
        }
    }
}
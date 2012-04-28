using System.Collections.Generic;
using BlockMoveGame.Things;

namespace BlockMoveGame.Doers {
    public class TheGame {
        private static TheGame _theGame;
        public static TheGame TheGameObject { get { return _theGame ?? (_theGame = new TheGame()); } }

        public Board Board { get; set; }

        public bool Initialized { get; private set; }

        public void Initialize(int width, int height)
        {
            if (Initialized) return;
            Board = new Board(width, height);
            Initialized = true;
        }

        public Player AddPlayer(string color) {
            // TODO validate color
            return Board.AddPlayer(color);
        }

        public Player MovePlayer(string id, string direction) {
            return Board.MovePlayer(id, direction);
        }

        public IEnumerable<Player> GetPlayers() {
            return Board.GetPlayers();
        }

        public PointBlip GetPointBlip() {
            return Board.GetPointBlip();
        }

        public void Reset() {
            Board.Reset();
        }
    }
}
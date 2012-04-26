using System;
using System.Collections.Generic;
using System.Linq;
using SignalR.Hubs;
using SignalR;
using SignalR.Hosting.AspNet;
using SignalR.Infrastructure;

namespace BlockMoveGame
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

    public class TheGame {
        private static TheGame _theGame;
        public static TheGame TheGameObject { get { return /*new TheGame();*/_theGame ?? (_theGame = new TheGame()); } }

        public List<Player> Players { get; set; }

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
    }

    public class BlockMove : Hub {
        public TheGame TheGame { get; set; }
        public BlockMove() {
            TheGame = TheGame.TheGameObject;
        }

        public IEnumerable<Player> GetPlayers() {
            return TheGame.Players;
        }

        public Player Join(string color) {
            var player = TheGame.AddPlayer(color);
            Clients.playerJoin(player);
            return player;
        }

        public Player Move(string id, string direction) {
            var player = TheGame.GetPlayer(id);

            if (player == null)
                return null;

            switch (direction) {
                case "up":
                    player.YPosition -= 10;
                    break;
                case "right":
                    player.XPosition += 10;
                    break;
                case "down":
                    player.YPosition += 10;
                    break;
                case "left":
                    player.XPosition -= 10;
                    break;
            }

            Clients.movePlayer(player);
            return player;
        }
    }
}

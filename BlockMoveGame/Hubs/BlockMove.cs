using System.Collections.Generic;
using BlockMoveGame.Doers;
using BlockMoveGame.Things;
using SignalR.Hubs;

namespace BlockMoveGame.Hubs {
    public class BlockMove : Hub {
        public TheGame TheGame { get; set; }
        public BlockMove() {
            TheGame = TheGame.TheGameObject;
        }

        public IEnumerable<Player> Initialize(int width, int height) {
            TheGame.Initialize(width, height);
            CheckPointBlip();
            return TheGame.GetPlayers();
        }

        public Player Join(string color) {
            var player = TheGame.AddPlayer(color);
            Clients.playerJoin(player);
            return player;
        }

        public Player Move(string id, string direction) {
            var player = TheGame.MovePlayer(id, direction);
            if(player!= null) Clients.movePlayer(player);

            CheckPointBlip();
            
            return player;
        }

        public void Reset() {
            TheGame.Reset();
            CheckPointBlip();
        }

        private void CheckPointBlip()
        {
            var pointBlip = TheGame.GetPointBlip();
            if (pointBlip != null)
                Clients.updatePointBlip(pointBlip);
        }
    }
}
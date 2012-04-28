using BlockMoveGame.Things;
using NUnit.Framework;

namespace CollectTests
{
    [TestFixture]
    public class BoardTests
    {
        public Board Board { get; set; }
        public Player Player { get; set; }
        public int Size = 10;

        [SetUp]
        public void SetUp() {
            Board = new Board(100, 100);
            Player = Board.AddPlayer("red");
        }

        [TearDown]
        public void TearDown() {
            Board = null;
        }

        [Test]
        public void Should_collide_when_move_up () {
            Player.Cords.Y = 0;
            var player = Board.MovePlayer(Player.Id.ToString(), Directions.Up);

            Assert.IsNull(player);
        }

        [Test]
        public void Should_collide_when_move_right() {
            Player.Cords.X = 90;
            var player = Board.MovePlayer(Player.Id.ToString(), Directions.Right);

            Assert.IsNull(player);
        }

        [Test]
        public void Should_collide_when_move_down() {
            Player.Cords.Y = 90;
            var player = Board.MovePlayer(Player.Id.ToString(), Directions.Down);

            Assert.IsNull(player);
        }

        [Test]
        public void Should_collide_when_move_left() {
            Player.Cords.X = 0;
            var player = Board.MovePlayer(Player.Id.ToString(), Directions.Left);

            Assert.IsNull(player);
        }
    }
}

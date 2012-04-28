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
            Player = null;
        }

        [Test]
        public void Should_collide_when_move_up () {
            Player.Cords.Y = 0;
            var player = Board.MovePlayer(Player.Id.ToString(), Directions.Up);

            Assert.AreEqual(0, player.Cords.Y);
        }

        [Test]
        public void Should_collide_when_move_right() {
            Player.Cords.X = 90;
            var player = Board.MovePlayer(Player.Id.ToString(), Directions.Right);

            Assert.AreEqual(90, player.Cords.X);
        }

        [Test]
        public void Should_collide_when_move_down() {
            Player.Cords.Y = 90;
            var player = Board.MovePlayer(Player.Id.ToString(), Directions.Down);

            Assert.AreEqual(90, player.Cords.Y);
        }

        [Test]
        public void Should_collide_when_move_left() {
            Player.Cords.X = 0;
            var player = Board.MovePlayer(Player.Id.ToString(), Directions.Left);

            Assert.AreEqual(0, player.Cords.X);
        }

        [Test]
        public void Should_recieve_point() {
            Player.Cords.X = 0;
            Player.Cords.Y = 0;

            Board.PointBlip = new PointBlip(10, new Cords { X = 10, Y = 0 });

            var player = Board.MovePlayer(Player.Id.ToString(), Directions.Right);

            Assert.AreEqual(1, player.Score);
        }

        [Test]
        public void Should_generate_new_pointBlip()
        {
            Player.Cords.X = 0;
            Player.Cords.Y = 0;

            var pointBlip = new PointBlip(10, new Cords { X = 10, Y = 0 });

            Board.PointBlip = pointBlip;

            Board.MovePlayer(Player.Id.ToString(), Directions.Right);

            Assert.AreNotEqual(pointBlip, Board.PointBlip);
        }
    }
}

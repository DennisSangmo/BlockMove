namespace BlockMoveGame.Things {
    public class PointBlip {
        public int Size { get; set; }
        public Cords Cords { get; set; }

        public PointBlip(int objectSize, Cords cords) {
            Size = objectSize;
            Cords = cords;
        }
    }
}
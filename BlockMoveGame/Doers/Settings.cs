using System.Configuration;

namespace BlockMoveGame.Doers {
    public static class Settings {
        public static int StepDistance { 
            get {
                var stringDist = ConfigurationManager.AppSettings["StepDistance"];
                int dist;
                return int.TryParse(stringDist, out dist) ? dist : 10;
            }
        }

        public static int PlayerSize
        {
            get
            {
                var stringSize = ConfigurationManager.AppSettings["PlayerSize"];
                int size;
                return int.TryParse(stringSize, out size) ? size: 10;
            }
        }
    }
}
using System.Configuration;

namespace BlockMoveGame.Doers {
    public static class Settings {
        public static int StepDistance { 
            get {
                var distance = ConfigurationManager.AppSettings["StepDistance"];
                int dist;
                return int.TryParse(distance, out dist) ? dist : 10;
            }
        }
    }
}
namespace HuntTheMonster.LevelGeneration
{
    public class Level
    {
        public Room[,] rooms { get; }
        
        public Level(int width, int length)
        {
            rooms = new Room[width, length];
        }
    }
}
namespace Quoridor_MVC.Models
{
    abstract class AbstractCharacter
    {
        public int Walls { get; protected set; } = 10;
        
        public string Name { get; protected set; }

        public Coords CurrentPosition { get; set; }

        public abstract void Move(Coords coords);

        public abstract void SpendWall();
    }
}

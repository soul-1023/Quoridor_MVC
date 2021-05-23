namespace Quoridor_MVC.Models
{
    abstract class AbstractCharacter
    {
        public int Walls { get; protected set; } = 10;
        
        public string Name { get; protected set; }

        public Coords CurrentPosition { get; protected set; }

        public void Move(Coords coords) => CurrentPosition = coords;

        public void SpendWall()
        {
            if (Walls != 0) Walls--;
        }
    }
}

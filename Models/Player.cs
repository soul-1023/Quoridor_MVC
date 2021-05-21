namespace Quoridor_MVC.Models
{
    class Player : AbstractCharacter
    {
        public Player(string name, Coords coords)
        {
            Name = name;
            CurrentPosition = coords;
        }

        public override void Move(Coords coords)
        {
            CurrentPosition = coords;
        }

        public override void SpendWall()
        {
            Walls--;
        }
    }
}

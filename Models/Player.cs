namespace Quoridor_MVC.Models
{
    class Player : AbstractCharacter
    {
        public Player(string name,)
        {
            Name = 
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

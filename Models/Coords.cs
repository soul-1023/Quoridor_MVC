namespace Quoridor_MVC.Models
{
    sealed class Coords
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public bool IsTemporary { get; set; } = false;

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

}

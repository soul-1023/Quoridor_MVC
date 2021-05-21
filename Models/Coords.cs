namespace Quoridor_MVC
{

    sealed class Coords
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

}

namespace Quoridor_MVC.Controllers
{
    abstract class AbstractGameActivities
    {
        IGraphController LinkManager { get; set; }

        IActivitiesChecker ActivitiesChecker { get; set; }

        ICharactersManager CharactersManager { get; set; }

        public abstract bool PlaceWall();
    }
}

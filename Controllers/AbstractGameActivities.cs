namespace Quoridor_MVC.Controllers
{
    abstract class AbstractGameActivities
    {
        IGraphController LinkManager { get; set; }

        IActivitiesChecker ActivitiesChecker { get; set; }

        AbstractCharactersManager CharactersManager { get; set; }

        public abstract bool PlaceWall();
    }
}

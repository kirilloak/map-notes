namespace MapNotes.DAL.Concrete
{
    public class BaseRepository
    {
        protected readonly DataContext Context;

        public BaseRepository()
        {
            Context = new DataContext();
        }
    }
}

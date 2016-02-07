using Autofac;
using MapNotes.BLL.Abstract.Managers;
using MapNotes.DAL.Abstract;

namespace MapNotes.BLL.Concrete.Managers
{
    public class NoteManager : INoteManager
    {
        public INoteRepository Repository { get; set; }

        public NoteManager()
        {
            Repository = IoC.Instance.Resolve<INoteRepository>();
        }
    }
}

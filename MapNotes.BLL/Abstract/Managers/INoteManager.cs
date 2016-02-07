using MapNotes.DAL.Abstract;

namespace MapNotes.BLL.Abstract.Managers
{
    public interface INoteManager
    {
        INoteRepository Repository { get; set; }
    }
}

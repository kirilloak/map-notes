using System.Linq;
using MapNotes.DTO.Models;

namespace MapNotes.DAL.Abstract
{
    public interface INoteRepository
    {
        IQueryable<NoteModel> GetBy();

        NoteModel GetById(int id);
        IQueryable<NoteModel> GetAll();
    }
}

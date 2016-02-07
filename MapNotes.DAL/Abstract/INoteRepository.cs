using System.Linq;
using MapNotes.DTO.Models.Note;

namespace MapNotes.DAL.Abstract
{
    public interface INoteRepository
    {
        IQueryable<NoteModel> GetBy();

        IQueryable<NoteModel> GetAll();
        NoteModel GetById(int id);
        IQueryable<NoteModel> GetByUserId(string userId);
    }
}

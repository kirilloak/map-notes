using System.Linq;
using MapNotes.DAL.Abstract;
using MapNotes.DTO.Models.Note;

namespace MapNotes.DAL.Concrete
{
    public class NoteRepository : BaseRepository, INoteRepository
    {
        public IQueryable<NoteModel> GetBy()
        {
            return Context.Note.Select(x => new NoteModel
            {
                Id = x.Id,
                UserId = x.UserId,
                IsActive = x.IsActive,
                Title = x.Title,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                DateCreated = x.DateCreated
            });
        }

        public NoteModel GetById(int id)
        {
            return GetBy().FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<NoteModel> GetByUserId(string userId)
        {
            return GetBy().Where(x => x.IsActive && x.UserId == userId);
        }

        public IQueryable<NoteModel> GetAll()
        {
            return GetBy().Where(x => x.IsActive);
        }
    }
}

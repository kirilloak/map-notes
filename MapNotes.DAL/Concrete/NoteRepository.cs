using System.Linq;
using MapNotes.DAL.Abstract;
using MapNotes.DAL.Entities;
using MapNotes.DTO.Models;

namespace MapNotes.DAL.Concrete
{
    public class NoteRepository : BaseRepository, INoteRepository
    {
        public static NoteModel MapToModel(NoteEntity x)
        {
            var model = new NoteModel
            {
                Id = x.Id,
                IsActive = x.IsActive,
                Title = x.Title,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                DateCreated = x.DateCreated
            };

            return model;
        }

        public IQueryable<NoteModel> GetBy()
        {
            return Context.Note.Select(x => new NoteModel
            {
                Id = x.Id,
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

        public IQueryable<NoteModel> GetAll()
        {
            return GetBy().Where(x => x.IsActive);
        }
    }
}

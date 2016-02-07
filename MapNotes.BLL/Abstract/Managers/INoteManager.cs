using System.Collections.Generic;
using MapNotes.DAL.Abstract;
using MapNotes.DTO.Models.Note;

namespace MapNotes.BLL.Abstract.Managers
{
    public interface INoteManager
    {
        INoteRepository Repository { get; set; }

        IEnumerable<NearestNoteModel> GetNearest(string userId, double latitude, double longitude, int distance);
        void RebuildIndex(string userId);
    }
}

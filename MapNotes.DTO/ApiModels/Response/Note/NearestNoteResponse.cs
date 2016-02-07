using System.Collections.Generic;
using MapNotes.DTO.Models.Note;

namespace MapNotes.DTO.ApiModels.Response.Note
{
    public class NearestNoteResponse
    {
        public IEnumerable<NearestNoteModel> Notes { get; set; }
    }
}

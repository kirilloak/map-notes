namespace MapNotes.DTO.Models
{
    public class NoteModel : BaseModel
    {
        public string Title { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string DateCreated { get; set; }
    }
}

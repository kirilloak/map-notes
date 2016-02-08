namespace MapNotes.DTO.ApiModels.Request.Note
{
    public class CreateNoteRequest
    {
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
        public string Title { get; set; }
    }
}

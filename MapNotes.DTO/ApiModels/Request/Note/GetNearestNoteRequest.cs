namespace MapNotes.DTO.ApiModels.Request.Note
{
    public class GetNearestNoteRequest
    {
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }
    }
}

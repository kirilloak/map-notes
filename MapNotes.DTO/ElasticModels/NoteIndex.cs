using Nest;

namespace MapNotes.DTO.ElasticModels
{
    public class NoteIndex
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        [ElasticProperty(Type = FieldType.GeoPoint)]
        public Location Location { get; set; }
    }
}

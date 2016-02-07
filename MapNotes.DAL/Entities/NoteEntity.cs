using System.ComponentModel.DataAnnotations.Schema;
using MapNotes.DAL.Identity;

namespace MapNotes.DAL.Entities
{
    [Table("Note")]
    public class NoteEntity : BaseEntity
    {
        public string UserId { get; set; }

        public string Title { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string DateCreated { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}

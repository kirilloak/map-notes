using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MapNotes.DAL.Identity;

namespace MapNotes.DAL.Entities
{
    [Table("Note")]
    public class NoteEntity : BaseEntity
    {
        [Required]
        public string UserId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}

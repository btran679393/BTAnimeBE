using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTAnime.Models
{
    [Table("Animes")]
    public class AnimeModel
    {
        [Key]
        public int AnimeID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        // Correct type for navigation property
        public ICollection<Favorite> Favorites { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTAnime.Models
{
    [Table("Seasons")]
    public class SeasonsModel
    {
        [Key]
        public int SeasonID { get; set; }

        [Required]
        public int AnimeID { get; set; }

        [Required]
        public int SeasonNumber { get; set; }

        // Optional: Navigation property to AnimeModel (if you're using relationships)
        // public AnimeModel Anime { get; set; }
    }
}

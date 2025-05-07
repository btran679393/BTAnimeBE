using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BTAnime.Models
{
    public class Favorite
    {
        [Key]
        public int FavoriteID { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int AnimeID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties (optional but useful)
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("AnimeID")]
        public AnimeModel Anime { get; set; }
    }
}

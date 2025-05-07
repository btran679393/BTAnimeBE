using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTAnime.Models
{
    [Table("Episodes")]
    public class EpisodesModel
    {
        [Key]
        public int EpisodeID { get; set; }

        [Required]
        public int SeasonID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public int? EpisodeNumber { get; set; }

        
    }
}

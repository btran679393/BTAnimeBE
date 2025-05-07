using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTAnime.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }  // e.g., "User", "VIP"

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}

namespace BTAnime.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Feature { get; set; } // e.g., "Favorite Shows"
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}

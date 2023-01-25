using OnePageProject1.Models.Base;

namespace OnePageProject1.Models
{
    public class Post:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Image { get; set; }
    }
}

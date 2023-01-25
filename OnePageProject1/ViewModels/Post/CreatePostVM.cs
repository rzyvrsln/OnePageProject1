namespace OnePageProject1.ViewModels.Post
{
    public class CreatePostVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}

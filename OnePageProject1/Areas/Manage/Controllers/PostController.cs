using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePageProject1.DAL;
using OnePageProject1.Models;
using OnePageProject1.ViewModels.Post;

namespace OnePageProject1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PostController : Controller
    {

        readonly AppDbContext _context;
        readonly IWebHostEnvironment _webHostEnvironment;

        public PostController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _context.Posts.ToListAsync());

        [HttpGet]
        public async Task<IActionResult> Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostVM createPostVM)
        {
            if (!ModelState.IsValid) return View();

            IFormFile file = createPostVM.Image;

            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image",$"{file.FileName} falyi sekil deyil");
                return View();
            }

            if (file.Length > 200 * 1024)
            {
                ModelState.AddModelError("Image",$"{file.FileName} falyinin hecmi 2kb dan artiq ola bilmez.");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + file.FileName;

            using (var stream = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "post", fileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Post post = new Post
            {
                Title = createPostVM.Title,
                Description = createPostVM.Description,
                CreatedAt = DateTime.Now,
                Image = fileName
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), "Post");
        }

        [HttpGet]
        public async Task<IActionResult> View(int? id)
        {
            if (!ModelState.IsValid) return View();
            if (id is null) return BadRequest();
            var post = await _context.Posts.FindAsync(id);
            if (post is null) return NotFound();

            ViewBag.Title = post.Title;
            ViewBag.Description = post.Description;
            ViewBag.CreatedAt = post.CreatedAt;
            ViewBag.Image = post.Image;

            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!ModelState.IsValid) return View();
            if (id is null) return BadRequest();
            var post = await _context.Posts.FindAsync(id);
            if (post is null) return NotFound();
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), "Post");
        }
    }
}

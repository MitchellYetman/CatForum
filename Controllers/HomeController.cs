using System.Diagnostics;
using CatForum.Data;
using CatForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly CatForumContext _context;

        public HomeController(CatForumContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //add orderby here and get rid of sort
            var discussions = await _context.Discussion.Include(d => d.Comments).Include(d => d.ApplicationUser).OrderByDescending(d => d.CreateDate).ToListAsync();
            return View(discussions);
        }

        public IActionResult Privacy()
        {
            return View();        
        }

        public async Task<IActionResult> GetDiscussion(int? id)
        {
            var discussion = await _context.Discussion.Include(d => d.Comments).Include(d => d.ApplicationUser).FirstOrDefaultAsync(d => d.DiscussionId == id);

            if (discussion != null && discussion.Comments != null)
            {
                discussion.Comments.Sort((x, y) => y.CreateDate.CompareTo(x.CreateDate));
            }

            return View(discussion);
        }

        public async Task<IActionResult> Profile(string? id)
        {
            var user = await _context.Users.Include(u => u.Discussions).FirstOrDefaultAsync(u => u.Id == id);

            return View(user);

        }


    }
}

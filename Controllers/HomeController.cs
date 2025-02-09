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
            var discussions = await _context.Discussion.ToListAsync();

            //sort syntax from ChatGPT
            discussions.Sort((x, y) => y.CreateDate.CompareTo(x.CreateDate));

            //syntax for dictionary and fetch, as well as idea to use dictionary, from ChatGPT. my original thought was to pass an object that held DiscussionId's and comment counts
            var discussionComments = new Dictionary<int, int>();
            foreach(var d in discussions)
            {
                var commentCount = await _context.Comment.Where(c => c.DiscussionId == d.DiscussionId).CountAsync();
                discussionComments.Add(d.DiscussionId, commentCount);
            }

            ViewBag.DiscussionComments = discussionComments;

            return View(discussions);
        }

        public IActionResult Privacy()
        {
            return View();        
        }

        public IActionResult GetDiscussion()
        {
            return View();
        }


    }
}

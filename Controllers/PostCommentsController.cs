using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogSite.Data;
using BlogSite.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlogSite.Controllers
{
    public class PostCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PostComments
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostComment.ToListAsync());
        }

        [Authorize]
        // GET: PostComments/Create
        public IActionResult Create()
        {
            ViewBag.Postid = TempData["id"];
            return View();
        }
        [Authorize]
        public IActionResult RedirectToCreate(int? Id)
        {
            TempData["id"] = Id;
            return RedirectToAction("Create");
        }

        // POST: PostComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostId,OriginalPoster,Comment_Day_published,CommentText")] PostComment postComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postComment);
                await _context.SaveChangesAsync();
                return RedirectToAction("ShowBlogPost", "BlogPosts", new { id= postComment.PostId });
            }
            return View(postComment);
        }

        // GET: PostComments/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postComment = await _context.PostComment.FindAsync(id);
            if (postComment == null)
            {
                return NotFound();
            }
            return View(postComment);
        }

        // POST: PostComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostId,OriginalPoster,Comment_Day_published,CommentText")] PostComment postComment)
        {
            if (id != postComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostCommentExists(postComment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ShowBlogPost", "BlogPosts", new { id = postComment.PostId });
            }
            return View(postComment);
        }

        // GET: PostComments/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postComment = await _context.PostComment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postComment == null)
            {
                return NotFound();
            }

            return View(postComment);
        }

        // POST: PostComments/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postComment = await _context.PostComment.FindAsync(id);
            _context.PostComment.Remove(postComment);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowBlogPost", "BlogPosts", new { id = postComment.PostId });
        }

        private bool PostCommentExists(int id)
        {
            return _context.PostComment.Any(e => e.Id == id);
        }
    }
}

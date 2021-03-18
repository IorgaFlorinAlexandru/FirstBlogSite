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
    public class BlogPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BlogPosts
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogPost.ToListAsync());
        }

        public async Task<IActionResult> ShowSearchResults(String SearchWord)
        {
            return View("Index", await _context.BlogPost.Where(m => m.Title.Contains(SearchWord)).ToListAsync());
        }

        // GET: BlogPosts/Details/5
        [Authorize]
        public async Task<IActionResult> ShowBlogPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var blogPost = await _context.BlogPost
                .FirstOrDefaultAsync(m => m.Id == id);

            if (blogPost == null)
            {
                return NotFound();
            }

            ViewBag.BlogPost = blogPost;
            ViewBag.Comments = _context.PostComment;
            return View();
        }

        // GET: BlogPosts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Day_published,Text,ImageUrl")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Day_published,Text,ImageUrl")] BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _context.BlogPost.FindAsync(id);
            _context.BlogPost.Remove(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPost.Any(e => e.Id == id);
        }
    }
}

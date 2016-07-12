using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Snippy.App.Models.ViewModels;
using Snippy.Data;

namespace Snippy.App.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data.UnitOfWork;
    using Microsoft.AspNet.Identity;
    using Models.BindingModels;
    using Snippy.Models;

    [Authorize]
    public class SnippetsController : BaseController
    {
        public SnippetsController(ISnippyData data)
            : base(data)
        {
        }


        [AllowAnonymous]
        public ActionResult Index(int page = 1, int count = 5)
        {
            var snippets = this.Data.Snippets.All();
            int snippetsCount = snippets.Count();
            snippets = snippets
                .Include(s => s.Labels)
                .OrderByDescending(s => s.CreatedOn)
                .Skip((page - 1) * count)
                .Take(count);

            this.ViewBag.TotalPages = (snippetsCount + count - 1) / count;
            this.ViewBag.CurrentPage = page;

            var model = Mapper.Map<IEnumerable<ConciseSnippetViewModel>>(snippets);

            return this.View(model);
        }


        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var snippet = this.Data.Snippets.All()
                .Include(s => s.Author)
                .Include(s => s.Labels)
                .Include(s => s.Comments)
                .FirstOrDefault(s => s.Id == id);

            if (snippet == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<DetailsSnippetViewModel>(snippet);
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(CommentInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                model.AuthorId = this.User.Identity.GetUserId();
                var author = this.Data.Users.All()
                    .FirstOrDefault(u => u.Id == model.AuthorId);

                var comment = new Comment()
                {
                    Content = model.Content,
                    AuthorId = model.AuthorId,
                    Author = author,
                    CreatedOn = DateTime.Now
                };

                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                var commentDb = this.Data.Comments.All()
                    .FirstOrDefault(c => c.Id == comment.Id);

                var modelComment = Mapper.Map<ConciseCommentViewModel>(commentDb);

                return this.PartialView("DisplayTemplates/ConciseCommentViewModel", modelComment);
                //return Redirect("/Index");
            }
            return this.Json("Error");
        }



        //// GET: Snippets/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Snippets/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Title,CreatedOn")] ConciseSnippetViewModel conciseSnippetViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ConciseSnippetViewModels.Add(conciseSnippetViewModel);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(conciseSnippetViewModel);
        //}

        //// GET: Snippets/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ConciseSnippetViewModel conciseSnippetViewModel = db.ConciseSnippetViewModels.Find(id);
        //    if (conciseSnippetViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(conciseSnippetViewModel);
        //}

        //// POST: Snippets/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Title,CreatedOn")] ConciseSnippetViewModel conciseSnippetViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(conciseSnippetViewModel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(conciseSnippetViewModel);
        //}

        //// GET: Snippets/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ConciseSnippetViewModel conciseSnippetViewModel = db.ConciseSnippetViewModels.Find(id);
        //    if (conciseSnippetViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(conciseSnippetViewModel);
        //}

        //// POST: Snippets/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ConciseSnippetViewModel conciseSnippetViewModel = db.ConciseSnippetViewModels.Find(id);
        //    db.ConciseSnippetViewModels.Remove(conciseSnippetViewModel);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}

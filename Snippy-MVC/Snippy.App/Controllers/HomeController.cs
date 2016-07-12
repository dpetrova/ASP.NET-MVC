using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snippy.App.Controllers
{
    using System.Data.Entity;
    using AutoMapper;
    using Data.UnitOfWork;
    using Models.ViewModels;

    public class HomeController : BaseController
    {
        public HomeController(ISnippyData data)
            : base(data)
        {
        }


        public ActionResult Index()
        {
            var latestSnippets = this.Data.Snippets.All()
                .Include(s => s.Labels)
                .OrderByDescending(s => s.CreatedOn)
                .Take(5);

            var latestComments = this.Data.Comments.All()
                .Include(c => c.Author)
                .Include(c => c.Snippet)
                .OrderByDescending(c => c.CreatedOn)
                .Take(5);

            var bestLabels = this.Data.Labels.All()
                .Include(l => l.Snippets)
                .OrderByDescending(l => l.Snippets.Count)
                .Take(5);
            
            var model = new HomePageViewModel()
            {
                LatestSnippets = Mapper.Map<IEnumerable<ConciseSnippetViewModel>>(latestSnippets),
                LatestComments = Mapper.Map<IEnumerable<ConciseCommentViewModel>>(latestComments),
                BestLabels = Mapper.Map<IEnumerable<ConciseLabelViewModel>>(bestLabels)
            };
            return this.View(model);
            
        }

       
    }
}
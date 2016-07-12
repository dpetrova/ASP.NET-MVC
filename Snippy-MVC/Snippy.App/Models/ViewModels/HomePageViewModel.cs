using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippy.App.Models.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<ConciseSnippetViewModel> LatestSnippets { get; set; }

        public IEnumerable<ConciseCommentViewModel> LatestComments { get; set; }

        public IEnumerable<ConciseLabelViewModel> BestLabels { get; set; }
    }
}
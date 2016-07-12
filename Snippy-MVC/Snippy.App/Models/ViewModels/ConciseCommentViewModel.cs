using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippy.App.Models.ViewModels
{
    using Snippy.Models;

    public class ConciseCommentViewModel
    {
        public int Id { get; set; }
    
        public string Content { get; set; }
   
        public string AuthorUsername { get; set; }

        public DateTime CreatedOn { get; set; }
   
        public int SnippetId { get; set; }

        public string SnippetTitle { get; set; }
    }
}
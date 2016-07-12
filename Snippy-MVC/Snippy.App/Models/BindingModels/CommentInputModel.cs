using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippy.App.Models.BindingModels
{
    public class CommentInputModel
    {
        public int SnippetId { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }
    }
}
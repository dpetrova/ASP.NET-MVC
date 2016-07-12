using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippy.App.Models.ViewModels
{
    using Snippy.Models;

    public class DetailsSnippetViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public DateTime CreatedOn { get; set; }

        public string LanguageName { get; set; }

        public string AuthorUsername { get; set; }

        public virtual ICollection<Label> Labels { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippy.App.Models.ViewModels
{
    using Snippy.Models;

    public class ConciseSnippetViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Label> Labels { get; set; }
    }
}
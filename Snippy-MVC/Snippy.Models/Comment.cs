using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public virtual User Author { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int SnippetId { get; set; }

        [Required]
        public virtual Snippet Snippet { get; set; }
    }
}

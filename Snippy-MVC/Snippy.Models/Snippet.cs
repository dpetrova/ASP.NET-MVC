using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Snippet
    {
        public Snippet()
        {
            this.Labels = new HashSet<Label>();
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public virtual Language Language { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public virtual User Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Label> Labels { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}

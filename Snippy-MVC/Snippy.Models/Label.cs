using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Label
    {
        public Label()
        {
            this.Snippets = new HashSet<Snippet>();
        }

        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public virtual ICollection<Snippet> Snippets { get; set; }
    }
}

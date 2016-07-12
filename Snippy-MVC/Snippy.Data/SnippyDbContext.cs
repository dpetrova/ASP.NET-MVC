using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.Data
{
    using System.Data.Entity;
    using System.Text.RegularExpressions;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class SnippyDbContext : IdentityDbContext<User>
    {
        public SnippyDbContext()
            : base("SnippyConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Label> Labels { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Snippet> Snippets { get; set; }

        public static SnippyDbContext Create()
        {
            return new SnippyDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Snippets)
                .WithRequired(p => p.Author)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        //public System.Data.Entity.DbSet<Snippy.App.Models.ViewModels.ConciseSnippetViewModel> ConciseSnippetViewModels { get; set; }
    }
}

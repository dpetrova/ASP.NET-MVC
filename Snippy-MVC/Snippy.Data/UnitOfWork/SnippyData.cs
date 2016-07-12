using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.Data.UnitOfWork
{
    using System.Data.Entity;
    using Models;
    using Repositories;

    public class SnippyData : ISnippyData
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        public SnippyData()
            : this(new SnippyDbContext())
        {
        }

        public SnippyData(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.repositories = new Dictionary<Type, object>();
        }

        public Repositories.IRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public Repositories.IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public Repositories.IRepository<Label> Labels
        {
            get
            {
                return this.GetRepository<Label>();
            }
        }

        public Repositories.IRepository<Language> Languages
        {
            get
            {
                return this.GetRepository<Language>();
            }
        }

        public Repositories.IRepository<Snippet> Snippets
        {
            get
            {
                return this.GetRepository<Snippet>();
            }
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EntityFrameworkRepository<T>);
                this.repositories.Add(
                    typeof(T),
                    Activator.CreateInstance(type, this.dbContext));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}

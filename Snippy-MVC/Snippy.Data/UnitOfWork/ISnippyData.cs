using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.Data.UnitOfWork
{
    using System.Text.RegularExpressions;
    using Models;
    using Repositories;

    public interface ISnippyData
    {
        IRepository<User> Users { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Label> Labels { get; }

        IRepository<Language> Languages { get; }

        IRepository<Snippet> Snippets { get; }

        int SaveChanges();
    }
}

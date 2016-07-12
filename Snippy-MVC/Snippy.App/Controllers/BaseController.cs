using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snippy.App.Controllers
{
    using System.Web.Routing;
    using Data.UnitOfWork;
    using Snippy.Models;

    public abstract class BaseController : Controller
    {
        private ISnippyData data;
        private User userProfile;

        protected BaseController(ISnippyData data)
        {
            this.Data = data;
        }

        protected BaseController(ISnippyData data, User userProfile)
            : this(data)
        {
            this.UserProfile = userProfile;
        }

        protected ISnippyData Data { get; private set; }

        protected User UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
                this.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}
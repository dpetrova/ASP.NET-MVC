namespace ASP_NET_MVC_Identity_Homework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<ASP_NET_MVC_Identity_Homework.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ASP_NET_MVC_Identity_Homework.Models.ApplicationDbContext context)
        {
            // makes an Administrator role if doesn't exist
            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                roleManager.Create(new IdentityRole { Name = "Administrator" });
            }

            // if user doesn't exist, create one and add it to the Administrator role
            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com"
                };

                userManager.Create(user, "123456");

                userManager.AddToRole(user.Id, "Administrator");

            }
        }
    }
}

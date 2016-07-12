namespace Snippy.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<Snippy.Data.SnippyDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        //not all data have been seeded; just lack of time
        protected override void Seed(Snippy.Data.SnippyDbContext context)
        {
            if (!context.Users.Any())
            {
                this.SeedUsers(context);
            }

            if (!context.Labels.Any())
            {
                this.SeedLabels(context);
            }

            if (!context.Languages.Any())
            {
                this.SeedLanguages(context);
            }

            if (!context.Snippets.Any())
            {
                this.SeedSnippets(context);
            }

            if (!context.Comments.Any())
            {
                this.SeedComments(context);
            }
        }

        private void SeedUsers(SnippyDbContext context)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            var userToInsert = new User { UserName = "admin", Email = "admin@snippy.softuni.com" };
            userManager.Create(userToInsert, "adminPass123");
            userStore = new UserStore<User>(context);
            userManager = new UserManager<User>(userStore);
            userToInsert = new User { UserName = "someUser", Email = "someUser@example.com" };
            userManager.Create(userToInsert, "someUserPass123");
            userStore = new UserStore<User>(context);
            userManager = new UserManager<User>(userStore);
            userToInsert = new User { UserName = "newUser", Email = "new_user@gmail.com" };
            userManager.Create(userToInsert, "userPass123");
        }

        private void SeedLabels(SnippyDbContext context)
        {
            var stringOfLabels = "bug, funny, jquery, mysql, useful, web, geometry, back-end, front-end, games";
            var listOfLabels = stringOfLabels.Split(',').ToList();
            foreach (var label in listOfLabels)
            {
                var labelDb = new Label()
                {
                    Text = label.Trim()
                };
                context.Labels.Add(labelDb);
            }
            context.SaveChanges();
        }

        private void SeedLanguages(SnippyDbContext context)
        {
            var stringOfLanguages = "C#, JavaScript, Python, CSS, SQL, PHP";
            var listOfLanguages = stringOfLanguages.Split(',').ToList();
            foreach (var language in listOfLanguages)
            {
                var languageDb = new Language()
                {
                    Name = language.Trim()
                };
                context.Languages.Add(languageDb);
            }
            context.SaveChanges();
        }

        private void SeedSnippets(SnippyDbContext context)
        {
            var snippet1 = new Snippet()
            {
                Title = "Ternary Operator Madness",
                Description = "This is how you DO NOT user ternary operators in C#!",
                Code = "bool X = Glob.UserIsAdmin ? true : false;",
                Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                CreatedOn = new DateTime(2015, 10, 26, 17, 20, 33),
                Language = context.Languages.FirstOrDefault(l => l.Name == "C#"),
                Labels = new List<Label> {context.Labels.FirstOrDefault(l => l.Text == "funny")}
            };
             
             var snippet2 = new Snippet()
            {
                Title = "Points Around A Circle For GameObject Placement",
                Description = "Determine points around a circle which can be used to place elements around a central point",
                Code = "private Vector3 DrawCircle(float centerX, float centerY, float radius, float totalPoints, float currentPoint)" +
                       "{" +
	                            "float ptRatio = currentPoint / totalPoints;" +
	                            "float pointX = centerX + (Mathf.Cos(ptRatio * 2 * Mathf.PI)) * radius;" +
	                            "float pointY = centerY + (Mathf.Sin(ptRatio * 2 * Mathf.PI)) * radius;" +
	                            "Vector3 panelCenter = new Vector3(pointX, pointY, 0.0f);" +
                                "return panelCenter;" +
                        "}",
                Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                CreatedOn = new DateTime(2015, 10, 26, 20, 15, 30),
                Language = context.Languages.FirstOrDefault(l => l.Name == "C#"),
                Labels = new List<Label> {context.Labels.FirstOrDefault(l => l.Text == "geometry"), context.Labels.FirstOrDefault(l => l.Text == "games")}
            };
            
            var snippet3 = new Snippet()
            {
                Title = "forEach. How to break?",
                Description = "Array.prototype.forEach You can't break forEach. So use \"some\" or \"every\". Array.prototype.some some is pretty much the same as forEach but it break when the callback returns true. Array.prototype.every every is almost identical to some except it's expecting false to break the loop.",
                Code = "var ary = [\"JavaScript\", \"Java\", \"CoffeeScript\", \"TypeScript\"];" +
                        "ary.some(function (value, index, _ary) {" +
                        "console.log(index + \": \" + value);" +
	                    "return value === \"CoffeeScript\";" +
                        "});" +
                        "// output:" +
                        "// 0: JavaScript" +
                        "// 1: Java" +
                        "// 2: CoffeeScript" +
                        "ary.every(function(value, index, _ary) {" +
	                    "console.log(index + \": \" + value);" +
	                    "return value.indexOf(\"Script\") > -1;" +
                        "});" +
                        "// output:" +
                        "// 0: JavaScript" +
                        "// 1: Java",
                Author = context.Users.FirstOrDefault(u => u.UserName == "newUser"),
                CreatedOn = new DateTime(2015, 10, 27, 10, 53, 20),
                Language = context.Languages.FirstOrDefault(l => l.Name == "JavaScript"),
                Labels = new List<Label> {context.Labels.FirstOrDefault(l => l.Text == "jquery"), context.Labels.FirstOrDefault(l => l.Text == "useful"), context.Labels.FirstOrDefault(l => l.Text == "web"), context.Labels.FirstOrDefault(l => l.Text == "front-end")}
            };

            context.Snippets.Add(snippet1);
            context.Snippets.Add(snippet2);
            context.Snippets.Add(snippet3);
            
            context.SaveChanges();
        }

        private void SeedComments(SnippyDbContext context)
        {
            var comment1 = new Comment()
            {
                Content = "Now that's really funny! I like it!",
                Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                CreatedOn = new DateTime(2015, 10, 30, 11, 50, 38),
                Snippet = context.Snippets.FirstOrDefault(s => s.Title == "Ternary Operator Madness")
            };

            var comment2 = new Comment()
            {
                Content = "Here, have my comment!",
                Author = context.Users.FirstOrDefault(u => u.UserName == "newUser"),
                CreatedOn = new DateTime(2015, 11, 1, 15, 52, 42),
                Snippet = context.Snippets.FirstOrDefault(s => s.Title == "Ternary Operator Madness")
            };

            var comment3 = new Comment()
            {
                Content = "I didn't manage to comment first :(",
                Author = context.Users.FirstOrDefault(u => u.UserName == "someUser"),
                CreatedOn = new DateTime(2015, 11, 2, 05, 30, 20),
                Snippet = context.Snippets.FirstOrDefault(s => s.Title == "Ternary Operator Madness")
            };

            context.Comments.Add(comment1);
            context.Comments.Add(comment2);
            context.Comments.Add(comment3);

            context.SaveChanges();
        }
       

    }
}

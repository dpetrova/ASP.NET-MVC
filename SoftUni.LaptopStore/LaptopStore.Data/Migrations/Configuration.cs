namespace LaptopStore.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Runtime.Remoting.Channels;

    public sealed class Configuration : DbMigrationsConfiguration<LaptopStore.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "LaptopStore.Data.ApplicationDbContext";
        }

        protected override void Seed(LaptopStore.Data.ApplicationDbContext context)
        {
           ;
        }
    }
}

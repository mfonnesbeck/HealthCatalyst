using System;
using System.Data.Entity.Migrations;

namespace HealthCatalystWebApp.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Models.PeopleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "HealthCatalystWebApp.Models.PeopleContext";
        }

        protected override void Seed(Models.PeopleContext context)
        {
            //This method will be called after migrating to the latest version.
            //Using the DbSet<T>.AddOrUpdate() helper extension method to avoid creating duplicate seed data.
            context.People.AddOrUpdate(
                p => p.PID, 
                new Models.PeopleModel
                {
                    PID = Guid.NewGuid(),
                    firstName = "James",
                    lastName = "Johnson",
                    address = "100 N Main, SLC",
                    age = 30,
                    interests = "Skiing, snowboarding, skateboarding",
                    picture = "images/1-jamesjohnson.jpg",
                },
                new Models.PeopleModel
                {
                    PID = Guid.NewGuid(),
                    firstName = "Patricia",
                    lastName = "Ruth",
                    address = "200 W Center St, SLC",
                    age = 67,
                    interests = "Cross Stiching, Painting",
                    picture = "images/2-patriciaruth.jpg",
                },
                new Models.PeopleModel
                {
                    PID = Guid.NewGuid(),
                    firstName = "Jefferson",
                    lastName = "Howell",
                    address = "300 E State St, SLC",
                    age = 42,
                    interests = "Rifle Shooting, Hunting",
                    picture = "images/3-jeffhowell.jpg",
                });
        }
    }
}

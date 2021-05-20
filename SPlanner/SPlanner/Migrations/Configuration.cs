namespace SPlanner.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SPlanner.Models;
    using System.Collections.Generic;


    internal sealed class Configuration : DbMigrationsConfiguration<SPlanner.DAL.SPlannerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SPlanner.DAL.SPlannerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var user = new List<User>
            {
                new User{ UserID =1,FirstName="Ewa",LastName="Bączek",EmailAddress="ebaczek@gmail.com",Password="baczek1",College="Harvard University",RolaID=2},
                new User{ UserID =2,FirstName="Maria",LastName="Piechota",EmailAddress="mpiechota@gmail.com",Password="piechota2",College="Stanford University",RolaID=2},
                new User{ UserID =3,FirstName="Tomek",LastName="Lew",EmailAddress="tlew@gmail.com",Password="lew1",College="Yale University",RolaID=2},
                new User{ UserID =4,FirstName="Marek",LastName="Preis",EmailAddress="mpreis@gmail.com",Password="preis1",College="Columbia University",RolaID=2},
                new User{ UserID =5,FirstName="Magda",LastName="Smołucha",EmailAddress="msmoluch@gmail.com",Password="smoluch1",College="Harvard University",RolaID=2},
                new User{ UserID =6,FirstName="Lucian",LastName="Olewniczak",EmailAddress="lolewniczak@gmail.com",Password="olewniczak1",College="Columbia University",RolaID=1}
            };
            user.ForEach(s => context.Users.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();
            var events = new List<Event>
            {
                new Event{ EventID =1,StartDate=DateTime.Parse("2021-03-24"),EndDate=DateTime.Parse("2021-03-29"),Thema="event1",Description="Description1",CategoryID=1,UserID = 1},
                new Event{ EventID =2,StartDate=DateTime.Parse("2021-03-12"),EndDate=DateTime.Parse("2021-03-13"),Thema="event2",Description="Description2",CategoryID=2,UserID = 2},
                new Event{ EventID =3,StartDate=DateTime.Parse("2021-03-01"),EndDate=DateTime.Parse("2021-03-05"),Thema="event3",Description="Description3",CategoryID=3,UserID = 3},
                new Event{ EventID =4,StartDate=DateTime.Parse("2021-03-25"),EndDate=DateTime.Parse("2021-03-27"),Thema="event4",Description="Description4",CategoryID=4,UserID = 4},
                new Event{ EventID =5,StartDate=DateTime.Parse("2021-03-15"),EndDate=DateTime.Parse("2021-03-17"),Thema="event5",Description="Description5",CategoryID=5,UserID = 5},
                new Event{ EventID =6,StartDate=DateTime.Parse("2021-03-19"),EndDate=DateTime.Parse("2021-03-19"),Thema="event6",Description="Description6",CategoryID=6,UserID = 6}
            };
            events.ForEach(s => context.Events.AddOrUpdate(p => p.Thema, s));
            context.SaveChanges();

            var role = new List<Rola>
            {
                new Rola{RolaID = 1, Name = "Admin"},
                new Rola{RolaID = 2, Name = "User"}
            };
            role.ForEach(s => context.Rolas.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var subject = new List<Subject>
            {
                new Subject{SubjectID=1, Name="Math"},
                new Subject{SubjectID=2, Name="Polish"},
                new Subject{SubjectID=3, Name="Physics"}
            };
            subject.ForEach(s => context.Subjects.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var grade = new List<Grade>
            {
                new Grade{GradeID=1, Gradee= 3.6m, UserID=2, SubjectID=2 },
                new Grade{GradeID=1, Gradee= 2.6m, UserID=1, SubjectID=3 },
                new Grade{GradeID=1, Gradee= 4.6m, UserID=2, SubjectID=1 }
            };
            grade.ForEach(s => context.Grades.AddOrUpdate(p => p.Gradee, s));
            context.SaveChanges();

            var category = new List<Category>
            {
                new Category{ CategoryID =1,Name="Exam"},
                new Category{ CategoryID =2,Name="Quiz"},
                new Category{ CategoryID =3,Name="Webinar"},
                new Category{ CategoryID =4,Name="Studning"},
                new Category{ CategoryID =5,Name="STUDYING"},
                new Category{ CategoryID =6,Name="Test"}
            };
            category.ForEach(s => context.Categories.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

        }
    }
}

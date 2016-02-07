using System.Data.Entity.Migrations;
using MapNotes.DAL.Entities;
using MapNotes.DAL.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MapNotes.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataContext context)
        {
            var userId = SeedUser(context);

            if (!string.IsNullOrWhiteSpace(userId))
            {
                SeedNotes(context, userId);
            }
        }

        private void SeedNotes(DataContext context, string userId)
        {
            context.Note.AddOrUpdate(x => x.Id,
                new NoteEntity { Id = 1, Title = "Рыба-ёж", Latitude = "55.01483239352023", Longitude = "82.95087408212277", UserId = userId },
                new NoteEntity { Id = 2, Title = "Стул-трон с цепями", Latitude = "55.018075136743114", Longitude = "82.94791292337035", UserId = userId },
                new NoteEntity { Id = 3, Title = "Двор, в котором растут игрушки", Latitude = "55.01627501523843", Longitude = "82.9364116111145", UserId = userId },
                new NoteEntity { Id = 4, Title = "Часовня-памятник иконы Владимирской Божией Матери", Latitude = "55.01195932489116", Longitude = "82.92484592584225", UserId = userId },
                new NoteEntity { Id = 5, Title = "Памятник трамваю", Latitude = "55.02239016595399", Longitude = "82.93412993846984", UserId = userId },
                new NoteEntity { Id = 6, Title = "Цирк во дворе НИИ Гидрометеорологии", Latitude = "55.01030013614618", Longitude = "82.94477378811163", UserId = userId },
                new NoteEntity { Id = 7, Title = "Лежачая абстракция", Latitude = "55.02977220838242", Longitude = "82.92400044573324", UserId = userId },
                new NoteEntity { Id = 8, Title = "Кощей Бессмертный и Змей Горыныч", Latitude = "55.027565845641746", Longitude = "82.9187004007198", UserId = userId },
                new NoteEntity { Id = 9, Title = "Памятник колбасе", Latitude = "55.011035841530486", Longitude = "82.94257016102254", UserId = userId },
                new NoteEntity { Id = 10, Title = "Сердце для замочков любви", Latitude = "55.024700861537475", Longitude = "82.92921629426047", UserId = userId }
            );
        }

        private string SeedUser(DataContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new ApplicationUserManager(store);

            var email = "admin@site.com";
            var password = "Test1!";
            var admin = manager.FindByEmail(email);

            if (admin != null)
                return admin.Id;

            var user = new ApplicationUser { Email = email, UserName = email };
            var isCreated = manager.Create(user, password);

            if (isCreated.Succeeded)
                return user.Id;

            return null;
        }
    }
}

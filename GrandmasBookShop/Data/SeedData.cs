using GrandmasBookShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace GrandmasBookShop.Data
{
    public static class SeedData
    {
        public static async Task SeedRolesAndAdminAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Define the roles you need
            string[] roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create a default admin user if it doesn't exist
            var adminEmail = "admin@example.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Password123!"); // Ensure this is a strong password
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

        public static async Task SeedBooksAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Ensure the database is created and migrations are applied
            await context.Database.MigrateAsync();

            // Only seed books if none exist
            if (!context.Books.Any())
            {
                var books = new List<Book>{
                    new Book{ Name="Book1",Author="Author1",Price=370,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description1",Genre="Thriller"},
                    new Book{ Name="Book2",Author="Author2",Price=260,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description2",Genre="Action"},
                    new Book{ Name="Book3",Author="Author3",Price=450,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description3",Genre="Action"},
                    new Book{ Name="Book4",Author="Author4",Price=450,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description4",Genre="Sci-Fi"},
                    new Book{ Name="Book5",Author="Author5",Price=420,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description5",Genre="Romance"},
                    new Book{ Name="Book6",Author="Author6",Price=410,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description6",Genre="Action"},
                    new Book{ Name="Book7",Author="Author7",Price=120,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description7",Genre="Sci-Fi"},
                    new Book{ Name="Book8",Author="Author8",Price=320,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description8",Genre="Thriller"},
                    new Book{ Name="Book9",Author="Author9",Price=350,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description9",Genre="Sci-Fi"},
                    new Book{ Name="Book10",Author="Author10",Price=370,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description10",Genre="Action"},
                    new Book{ Name="Book11",Author="Author11",Price=150,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description11",Genre="Thriller"},
                    new Book{ Name="Book12",Author="Author12",Price=230,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description12",Genre="Horror"},
                    new Book{ Name="Book13",Author="Author13",Price=190,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description13",Genre="Horror"},
                    new Book{ Name="Book14",Author="Author14",Price=200,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description14",Genre="Sci-Fi"},
                    new Book{ Name="Book15",Author="Author15",Price=120,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description15",Genre="Romance"},
                    new Book{ Name="Book16",Author="Author16",Price=460,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description16",Genre="Horror"},
                    new Book{ Name="Book17",Author="Author17",Price=430,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description17",Genre="Thriller"},
                    new Book{ Name="Book18",Author="Author18",Price=450,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description18",Genre="Action"}
                };

                context.Books.AddRange(books);
                await context.SaveChangesAsync();
            }
        }
    }
}

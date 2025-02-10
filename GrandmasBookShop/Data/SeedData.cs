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
                    new Book{ Name="Book1",Author="Author1",Price=290,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description1",Genre="Action"},
                    new Book{ Name="Book2",Author="Author2",Price=160,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description2",Genre="Romance"},
                    new Book{ Name="Book3",Author="Author3",Price=410,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description3",Genre="Thriller"},
                    new Book{ Name="Book4",Author="Author4",Price=130,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description4",Genre="Thriller"},
                    new Book{ Name="Book5",Author="Author5",Price=470,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description5",Genre="Action"},
                    new Book{ Name="Book6",Author="Author6",Price=450,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description6",Genre="Sci-Fi"},
                    new Book{ Name="Book7",Author="Author7",Price=350,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description7",Genre="Action"},
                    new Book{ Name="Book8",Author="Author8",Price=490,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description8",Genre="Romance"},
                    new Book{ Name="Book9",Author="Author9",Price=110,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description9",Genre="Sci-Fi"},
                    new Book{ Name="Book10",Author="Author10",Price=180,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description10",Genre="Horror"},
                    new Book{ Name="Book11",Author="Author11",Price=320,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description11",Genre="Horror"},
                    new Book{ Name="Book12",Author="Author12",Price=380,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description12",Genre="Sci-Fi"},
                    new Book{ Name="Book13",Author="Author13",Price=320,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description13",Genre="Sci-Fi"},
                    new Book{ Name="Book14",Author="Author14",Price=330,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description14",Genre="Horror"},
                    new Book{ Name="Book15",Author="Author15",Price=320,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description15",Genre="Sci-Fi"},
                    new Book{ Name="Book16",Author="Author16",Price=180,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description16",Genre="Thriller"},
                    new Book{ Name="Book17",Author="Author17",Price=360,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description17",Genre="Action"},
                    new Book{ Name="Book18",Author="Author18",Price=420,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description18",Genre="Horror"},
                    new Book{ Name="Book19",Author="Author19",Price=400,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description19",Genre="Thriller"},
                    new Book{ Name="Book20",Author="Author20",Price=290,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description20",Genre="Sci-Fi"},
                    new Book{ Name="Book21",Author="Author21",Price=360,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description21",Genre="Action"},
                    new Book{ Name="Book22",Author="Author22",Price=110,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description22",Genre="Romance"},
                    new Book{ Name="Book23",Author="Author23",Price=280,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description23",Genre="Sci-Fi"},
                    new Book{ Name="Book24",Author="Author24",Price=240,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description24",Genre="Thriller"},
                    new Book{ Name="Book25",Author="Author25",Price=390,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description25",Genre="Horror"},
                    new Book{ Name="Book26",Author="Author26",Price=370,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description26",Genre="Thriller"},
                    new Book{ Name="Book27",Author="Author27",Price=120,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description27",Genre="Romance"},
                    new Book{ Name="Book28",Author="Author28",Price=460,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description28",Genre="Horror"},
                    new Book{ Name="Book29",Author="Author29",Price=130,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description29",Genre="Romance"},
                    new Book{ Name="Book30",Author="Author30",Price=500,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description30",Genre="Thriller"},
                    new Book{ Name="Book31",Author="Author31",Price=300,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description31",Genre="Thriller"},
                    new Book{ Name="Book32",Author="Author32",Price=260,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description32",Genre="Thriller"},
                    new Book{ Name="Book33",Author="Author33",Price=290,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description33",Genre="Horror"},
                    new Book{ Name="Book34",Author="Author34",Price=420,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description34",Genre="Thriller"},
                    new Book{ Name="Book35",Author="Author35",Price=380,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description35",Genre="Horror"},
                    new Book{ Name="Book36",Author="Author36",Price=190,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description36",Genre="Thriller"},
                    new Book{ Name="Book37",Author="Author37",Price=240,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description37",Genre="Horror"},
                    new Book{ Name="Book38",Author="Author38",Price=220,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description38",Genre="Thriller"},
                    new Book{ Name="Book39",Author="Author39",Price=110,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description39",Genre="Romance"},
                    new Book{ Name="Book40",Author="Author40",Price=110,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description40",Genre="Romance"},
                    new Book{ Name="Book41",Author="Author41",Price=240,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description41",Genre="Thriller"},
                    new Book{ Name="Book42",Author="Author42",Price=250,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description42",Genre="Romance"},
                    new Book{ Name="Book43",Author="Author43",Price=100,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description43",Genre="Romance"},
                    new Book{ Name="Book44",Author="Author44",Price=110,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description44",Genre="Sci-Fi"},
                    new Book{ Name="Book45",Author="Author45",Price=200,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description45",Genre="Action"},
                    new Book{ Name="Book46",Author="Author46",Price=150,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description46",Genre="Sci-Fi"},
                    new Book{ Name="Book47",Author="Author47",Price=490,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description47",Genre="Action"},
                    new Book{ Name="Book48",Author="Author48",Price=390,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description48",Genre="Romance"},
                    new Book{ Name="Book49",Author="Author49",Price=350,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description49",Genre="Sci-Fi"},
                    new Book{ Name="Book50",Author="Author50",Price=240,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description50",Genre="Thriller"},
                    new Book{ Name="Book51",Author="Author51",Price=270,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description51",Genre="Thriller"},
                    new Book{ Name="Book52",Author="Author52",Price=420,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description52",Genre="Horror"},
                    new Book{ Name="Book53",Author="Author53",Price=360,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description53",Genre="Romance"},
                    new Book{ Name="Book54",Author="Author54",Price=350,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description54",Genre="Sci-Fi"},
                    new Book{ Name="Book55",Author="Author55",Price=300,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description55",Genre="Romance"},
                    new Book{ Name="Book56",Author="Author56",Price=300,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description56",Genre="Romance"},
                    new Book{ Name="Book57",Author="Author57",Price=110,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description57",Genre="Thriller"},
                    new Book{ Name="Book58",Author="Author58",Price=180,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description58",Genre="Action"},
                    new Book{ Name="Book59",Author="Author59",Price=470,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description59",Genre="Sci-Fi"},
                    new Book{ Name="Book60",Author="Author60",Price=450,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description60",Genre="Horror"},
                    new Book{ Name="Book61",Author="Author61",Price=140,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description61",Genre="Sci-Fi"},
                    new Book{ Name="Book62",Author="Author62",Price=490,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description62",Genre="Horror"},
                    new Book{ Name="Book63",Author="Author63",Price=230,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description63",Genre="Action"},
                    new Book{ Name="Book64",Author="Author64",Price=340,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description64",Genre="Thriller"},
                    new Book{ Name="Book65",Author="Author65",Price=270,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description65",Genre="Horror"},
                    new Book{ Name="Book66",Author="Author66",Price=420,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description66",Genre="Thriller"},
                    new Book{ Name="Book67",Author="Author67",Price=420,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description67",Genre="Horror"},
                    new Book{ Name="Book68",Author="Author68",Price=280,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description68",Genre="Sci-Fi"},
                    new Book{ Name="Book69",Author="Author69",Price=210,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description69",Genre="Horror"},
                    new Book{ Name="Book70",Author="Author70",Price=250,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description70",Genre="Action"},
                    new Book{ Name="Book71",Author="Author71",Price=500,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description71",Genre="Sci-Fi"},
                    new Book{ Name="Book72",Author="Author72",Price=130,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description72",Genre="Thriller"},
                    new Book{ Name="Book73",Author="Author73",Price=260,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description73",Genre="Horror"},
                    new Book{ Name="Book74",Author="Author74",Price=450,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description74",Genre="Romance"},
                    new Book{ Name="Book75",Author="Author75",Price=100,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description75",Genre="Thriller"},
                    new Book{ Name="Book76",Author="Author76",Price=200,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description76",Genre="Sci-Fi"},
                    new Book{ Name="Book77",Author="Author77",Price=100,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description77",Genre="Action"},
                    new Book{ Name="Book78",Author="Author78",Price=340,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description78",Genre="Horror"},
                    new Book{ Name="Book79",Author="Author79",Price=240,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description79",Genre="Action"},
                    new Book{ Name="Book80",Author="Author80",Price=230,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description80",Genre="Action"},
                    new Book{ Name="Book81",Author="Author81",Price=470,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description81",Genre="Horror"},
                    new Book{ Name="Book82",Author="Author82",Price=100,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description82",Genre="Sci-Fi"},
                    new Book{ Name="Book83",Author="Author83",Price=480,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description83",Genre="Thriller"},
                    new Book{ Name="Book84",Author="Author84",Price=210,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description84",Genre="Thriller"},
                    new Book{ Name="Book85",Author="Author85",Price=340,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description85",Genre="Action"},
                    new Book{ Name="Book86",Author="Author86",Price=230,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description86",Genre="Action"},
                    new Book{ Name="Book87",Author="Author87",Price=430,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description87",Genre="Sci-Fi"},
                    new Book{ Name="Book88",Author="Author88",Price=270,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description88",Genre="Romance"},
                    new Book{ Name="Book89",Author="Author89",Price=110,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description89",Genre="Thriller"},
                    new Book{ Name="Book90",Author="Author90",Price=420,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description90",Genre="Action"},
                    new Book{ Name="Book91",Author="Author91",Price=240,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description91",Genre="Sci-Fi"},
                    new Book{ Name="Book92",Author="Author92",Price=320,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description92",Genre="Romance"},
                    new Book{ Name="Book93",Author="Author93",Price=480,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description93",Genre="Romance"},
                    new Book{ Name="Book94",Author="Author94",Price=320,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description94",Genre="Horror"},
                    new Book{ Name="Book95",Author="Author95",Price=210,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description95",Genre="Sci-Fi"},
                    new Book{ Name="Book96",Author="Author96",Price=270,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description96",Genre="Thriller"},
                    new Book{ Name="Book97",Author="Author97",Price=460,CopiesAvailable=2,PhotoUrl="/images/book.png",Description="Description97",Genre="Action"},
                    new Book{ Name="Book98",Author="Author98",Price=360,CopiesAvailable=3,PhotoUrl="/images/book.png",Description="Description98",Genre="Horror"},
                    new Book{ Name="Book99",Author="Author99",Price=300,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description99",Genre="Sci-Fi"},
                    new Book{ Name="Book100",Author="Author100",Price=270,CopiesAvailable=1,PhotoUrl="/images/book.png",Description="Description100",Genre="Sci-Fi"}
                };

                context.Books.AddRange(books);
                await context.SaveChangesAsync();
            }
        }
    }
}

using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product {
                        Name = "Kayak",
                        Description = "A boat for one person",
                        Category = "Watersports",
                        Price=275m },
                    new Product {
                        Name = "Lifejacket",
                        Description = "Protective and fashionable",
                        Category = "Watersports",
                        Price=48.95m },
                    new Product {
                        Name = "Soccer Ball",
                        Description = "FIFA-approved size and weight",
                        Category = "Soccer",
                        Price=19.50m },
                    new Product {
                        Name = "Corner Flags",
                        Description = "Give your playing field a professional touch",
                        Category = "Soccer",
                        Price=34.95m },
                    new Product {
                        Name = "Game Day Jersey",
                        Description = "Take to the field in style!",
                        Category = "Soccer",
                        Price=45m },
                    new Product {
                        Name = "Chess Strategy Guide",
                        Description = "Improve your chess game efficiency by 75%",
                        Category = "Chess",
                        Price=35.50m },
                    new Product {
                        Name = "Chess Game Timer",
                        Description = "Choose quick-set options for the most popular controls",
                        Category = "Chess",
                        Price=24m },
                    new Product {
                        Name = "European Chess Board",
                        Description = "Beautiful chess set with hand carved board and pieces",
                        Category = "Chess",
                        Price=80.50m },
                    new Product {
                        Name = "Metal Chess Set",
                        Description = "Elegant metal chess pieces",
                        Category = "Chess",
                        Price=46.75m }
                );

                context.SaveChanges();
            }
        }
    }
}
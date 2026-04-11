namespace PlatformsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("Seeding data...");
                context.Platforms.AddRange(
                    new Models.Platform() { Name = "Dot Net", Publisher = 1, Cost = 0 },
                    new Models.Platform() { Name = "SQL Server Express", Publisher = 1, Cost = 0 },
                    new Models.Platform() { Name = "Kubernetes", Publisher = 2, Cost = 0 }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data");
            }
        }
    }
}
namespace PlatformsService.Data
{
    public static class PrepDb
    {
        // This method is called during application startup to ensure the database is populated with initial data
        public static void PrepPopulation(IApplicationBuilder app)
        {
            // Create a new scope to get an instance of the AppDbContext from the service provider
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // Call the SeedData method to populate the database with initial data
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            // Check if there are any platforms already in the database. If not, add some initial platforms.
            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data...");
                
                // Add some sample platforms to the database
                context.Platforms.AddRange(
                    new Models.Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Models.Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Models.Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                );

                // Save the changes to the database
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
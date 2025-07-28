using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data.Seeding
{
    public static class DbSeeder
    {
        public static async Task SeedCareerPathsAsync(IServiceProvider serviceProvider, string jsonPath)
        {
            await using StepWiseDbContext dbContext = serviceProvider
                .GetRequiredService<StepWiseDbContext>();

            try
            {
                string jsonInput = await File
                    .ReadAllTextAsync(jsonPath, Encoding.Unicode, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                throw;
            }
        }
    }
}

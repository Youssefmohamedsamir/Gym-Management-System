using GymManagementDAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeeding
{
    public static class GymSeeding
    {
        public static bool SeedData(GymDbContext dbContext)
        {
            try
            {
                var HasPlans = dbContext.Plans.Any();
                var HasCategorys = dbContext.Categories.Any();
                if (HasPlans && HasCategorys) return false;

                if (!HasPlans)
                {
                    var Plans = LoadDataFromJsonFile<Entity.Plan>("Plans.json");
                    if (Plans.Any())
                        dbContext.Plans.AddRange(Plans);
                }

                if (!HasCategorys)
                {
                    var Categorys = LoadDataFromJsonFile<Entity.Category>("Categorys.json");
                    if (Categorys.Any())
                        dbContext.Categories.AddRange(Categorys);
                }
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while seeding the database.", ex);
                return false;
            }
        }

        private static List<T> LoadDataFromJsonFile<T>(string fileName)
        {

            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);
            if (!File.Exists(FilePath))
            throw new FileNotFoundException($"The file {fileName} was not found at path {FilePath}");
            string Data = File.ReadAllText(FilePath);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<T>>(Data, options) ?? new List<T>();
        }
    }
}

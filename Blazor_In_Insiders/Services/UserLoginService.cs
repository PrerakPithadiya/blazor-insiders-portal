using MongoDB.Driver;
using Blazor_In_Insiders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor_In_Insiders.Services
{
    public static class UserLoginService
    {
        public static async Task<List<UserLogin>> GetAllUserLoginsAsync(MongoDbService mongoDbService)
        {
            var collection = mongoDbService.GetCollection<UserLogin>("UserLogins");
            var logins = await collection.FindAsync(_ => true);
            return await logins.ToListAsync();
        }

        public static async Task<UserLogin?> FindUserLoginAsync(MongoDbService mongoDbService, string email, string passwordHash)
        {
            var collection = mongoDbService.GetCollection<UserLogin>("UserLogins");
            var cursor = await collection.FindAsync(x => x.Email == email && x.PasswordHash == passwordHash);
            return await cursor.FirstOrDefaultAsync();
        }

        public static async Task<UserLogin?> FindUserLoginByEmailAsync(MongoDbService mongoDbService, string email)
        {
            var collection = mongoDbService.GetCollection<UserLogin>("UserLogins");
            var cursor = await collection.FindAsync(x => x.Email == email);
            return await cursor.FirstOrDefaultAsync();
        }

        public static async Task<UserLogin?> FindUserLoginByPasswordAsync(MongoDbService mongoDbService, string passwordHash)
        {
            var collection = mongoDbService.GetCollection<UserLogin>("UserLogins");
            var cursor = await collection.FindAsync(x => x.PasswordHash == passwordHash);
            return await cursor.FirstOrDefaultAsync();
        }
    }
}

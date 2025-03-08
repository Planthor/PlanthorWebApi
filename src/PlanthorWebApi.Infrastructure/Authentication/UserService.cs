using System;
using System.Threading.Tasks;

namespace PlanthorWebApi.Infrastructure.Authentication;

public interface IUserService
{
    Task<User?> ValidateUserAsync(string email, string password);
}
public class UserService : IUserService
{
    public async Task<User?> ValidateUserAsync(string email, string password)
    {
        if (string.Equals(email, "alice@planthor.com", StringComparison.OrdinalIgnoreCase)
            && string.Equals(password, "Planthor@123", StringComparison.OrdinalIgnoreCase))
        {
            return await Task.FromResult(new User
            {
                Email = email,
                Password = password,
                FirstName = "Test",
                LastName = "User",
                Id = 1,
            });
        }

        return await Task.FromResult<User?>(null);
    }
}

using DataAccess.Model.User;
using Microsoft.AspNetCore.Identity;

namespace UserModule.Abstraction;

public interface IRoleManager
{ Task<string> GetHighestSystemRoleAsync(Teammate teammate);
}
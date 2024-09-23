using System.Security;
using DataAccess.Enums;

namespace UserModule.Records;

public record AppUser(
    string Username,
    string Password,
    string Email,
    string Nickname = "",
    ScrumRole? Role = null,
    string Id = "");
using System.Security;
using DataAccess.Enums;

namespace UserModule.Records;

public record SimpleUserModel(
    string Username,
    string Password,
    string Email,
    string teamGuid = "",
    string Nickname = "",
    ScrumRole? Role = null,
    string Id = "");
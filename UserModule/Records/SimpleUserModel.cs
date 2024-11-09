
using DataAccess.Enums;
using UserModule.Informations;

namespace UserModule.Records;

public record SimpleUserModel(
    string Username,
    string Password,
    string Email,
    string SystemRole = SystemRole.User,
    string TeamGuid = "",
    string Nickname = "",
    ScrumRole? Role = null,
    string Id = "");
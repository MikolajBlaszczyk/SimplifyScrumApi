namespace DataAccess.Model.User;

public static class TeammateExtensions
{
    public static void Update(this Teammate origin, Teammate partial)
    {
        origin.ScrumRole = partial.ScrumRole ?? origin.ScrumRole;
        origin.Nickname = ChooseValueToUpdate(partial.Nickname, origin.Nickname)!;
        origin.TeamGUID = partial.TeamGUID ?? origin.TeamGUID;
        origin.Email =  ChooseValueToUpdate(partial.Email, origin.Email);
        origin.UserName = ChooseValueToUpdate(partial.UserName, origin.UserName);
        origin.NewUser = partial.NewUser;
    }

    private static string ChooseValueToUpdate(string? newValue, string? oldValue) =>
        string.IsNullOrEmpty(newValue) ? oldValue : newValue;
}
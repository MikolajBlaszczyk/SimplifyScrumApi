namespace UserModule.Utils.Texts;

public abstract class ValidationTexts
{
    public const string UsernameIsEmpty = "Username cannot be empty.";
    public const string PasswordIsEmpty = "Password cannot be empty.";
    public const string UsernameDoesNotMeetRequirements = "Username can contain only lowercase letters, uppercase letters, digits and special characters like:  '-', '.', '_', '*'. It must have at least 3 characters. It cannot be more than 256.";
    public const string NicknameDoesNotMeetRequirements = "Nickname can contain lowercase letters, uppercase letters, digits, spaces and special characters like: '-', '_', '!'. It must be at least 3 characters. It cannot be more than 50 characters.";

}
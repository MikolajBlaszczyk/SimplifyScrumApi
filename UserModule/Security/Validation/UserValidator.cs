using System.Security;
using System.Text.RegularExpressions;
using UserModule.Records;
using UserModule.Utils.Texts;
using UserModule.Validation;

namespace UserModule.Security.Validation;

public class UserValidator
{
   private static readonly Regex ValidNickname = new Regex(@"^([a-zA-Z0-9-_!\s]){3,50}$");
   private static readonly Regex ValidUsername = new Regex(@"^([a-zA-Z0-9-._*]){3,256}$");
   public ValidationResult ValidateBeforeSignIn(SimpleUserModel userModel)
   {
      if (NicknameIsValid(userModel.Nickname, out string nicknameErrorMessage) == false)
      {
         return ValidationResultFactory.CreateFailedResult(nicknameErrorMessage);
      }

      if (UsernameIsValid(userModel.Username, out string usernameErrorMessage) == false)
      {
         return ValidationResultFactory.CreateFailedResult(usernameErrorMessage);
      }

      if (PasswordIsNotEmpty(userModel.Password, out string passwordErrorMessage) == false)
      {
         return ValidationResultFactory.CreateFailedResult(passwordErrorMessage);
      }
      
      return ValidationResultFactory.CreateSuccessResult();
   }

   private bool NicknameIsValid(string nickname, out string errorMessage)
   {
      if (ValidNickname.IsMatch(nickname) == false)
      {
         errorMessage = ValidationTexts.NicknameDoesNotMeetRequirements;
         return false;
      }
      
      errorMessage = string.Empty;
      return true;
   }

   private bool UsernameIsValid(string username, out string errorMessage)
   {
      if (ValidUsername.IsMatch(username) == false)
      {
         errorMessage = ValidationTexts.UsernameDoesNotMeetRequirements;
         return false;
      }
      
      errorMessage = string.Empty;
      return true;
   }
   
   public ValidationResult ValidateBeforeLogin(SimpleUserModel userModel)
   {
      if (UsernameIsNotEmpty(userModel.Username, out string errorUsernameMessage) == false)
      {
         return ValidationResultFactory.CreateFailedResult(errorUsernameMessage);
      }

      if (PasswordIsNotEmpty(userModel.Password, out string errorPasswordMessage) == false)
      {
         return ValidationResultFactory.CreateFailedResult(errorPasswordMessage);
      }
         
      return ValidationResultFactory.CreateSuccessResult();
   }

   private bool PasswordIsNotEmpty(string password, out string errorMessage)
   {
      if (password.Length == 0)
      {
         errorMessage = ValidationTexts.PasswordIsEmpty;
         return false;
      }

      errorMessage = string.Empty;
      return true;
   }

   private bool UsernameIsNotEmpty(string username, out string errorMessage)
   {
      if (string.IsNullOrWhiteSpace(username))
      {
         errorMessage = ValidationTexts.UsernameIsEmpty;
         return false;
      }

      errorMessage = string.Empty;
      return true;
   }
}
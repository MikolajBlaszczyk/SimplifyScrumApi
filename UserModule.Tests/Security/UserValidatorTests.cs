using DataAccess.Enums;
using UserModule.Records;
using UserModule.Security.Validation;
using UserModule.Validation;

namespace UserModuleTests.Security;

[TestFixture]
public class UserValidatorTests
{

    public static IEnumerable<TestCaseData> InvalidUserDueToUserName
    {
        get
        {
            yield return new TestCaseData(AppUserFactory.CreateAppUser("", "secretePassword1"));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("", "secretePassword2", "test nickname2"));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("", "secretePassword3", "test nickname3", ScrumRole.ProjectOwner));
        }
    }
    
    [Test]
    [TestCaseSource(nameof(InvalidUserDueToUserName))]
    public void ValidateUserBeforeLogin_ShouldFailValidationDueToEmptyUsername(AppUser user)
    {
        UserValidator validator = new UserValidator();

        var actual = validator.ValidateBeforeLogin(user);
        
        Assert.IsTrue(actual.IsFailure, "Should invalidate user due to empty username ");
    }
    
    public static IEnumerable<TestCaseData> InvalidUserDueToUserPassword
    {
        get
        {
            yield return new TestCaseData(AppUserFactory.CreateAppUser("Username1", ""));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("Username2", "", "test nickname2"));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("Username3", "", "test nickname3", ScrumRole.ProjectOwner));
        }
    }
    
    [Test]
    [TestCaseSource(nameof(InvalidUserDueToUserPassword))]
    public void ValidateUserBeforeLogin_ShouldFailValidationDueToEmptyPassword(AppUser user)
    {
        UserValidator validator = new UserValidator();

        var actual = validator.ValidateBeforeLogin(user);
        
        Assert.IsTrue(actual.IsFailure, "Should invalidate user due to emptypassword ");
    }
    
    public static IEnumerable<TestCaseData> ValidUsers
    {
        get
        {
            yield return new TestCaseData(AppUserFactory.CreateAppUser("TestUsername1", "SecretPassword1"));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("TestUsername2", "SecretPassword2", "Nickname2" ));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("TestUsername3", "SecretPassword","Nickname3", ScrumRole.ProjectOwner));
        }
    }
    
    [Test]
    [TestCaseSource(nameof(ValidUsers))]
    public void ValidateUserBeforeLogin_ShouldSuccessfullyValidateUser(AppUser user)
    {
        UserValidator validator = new UserValidator();

        var actual = validator.ValidateBeforeLogin(user);
        
        Assert.IsTrue(actual.IsSuccess, "User should be successfully validated.");
    }



    private static IEnumerable<TestCaseData> InvalidNewUsersDueToUsername
    {
        get
        {
            yield return new TestCaseData(AppUserFactory.CreateAppUser("", "SecretPassword123", "Test Nickname",
                ScrumRole.ScrumMaster));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("Incorrect User", "SecretPassword123", "Test Nickname",
                ScrumRole.ScrumMaster));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("Incorrect!!!???User", "SecretPassword123", "Test Nickname",
                ScrumRole.ScrumMaster));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("§§§ewasdsads", "SecretPassword123", "Test Nickname",
                ScrumRole.ScrumMaster));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("A", "SecretPassword123", "Test Nickname",
                ScrumRole.ScrumMaster));
        }
    }
    
    
    [Test]
    [TestCaseSource(nameof(InvalidNewUsersDueToUsername))]
    public void ValidateUserBeforeSignIn_ShouldFailValidationDueToIncorrectUsernameRequirements(AppUser user)
    {
        UserValidator validator = new UserValidator();

        var actual = validator.ValidateBeforeSignIn(user);
        
        Assert.IsTrue(actual.IsFailure, "Username does not meet all the requirements. ");
    }

    private static IEnumerable<TestCaseData> InvalidUsersDueToNickname
    {
        get
        {
            yield return new TestCaseData(AppUserFactory.CreateAppUser("TestUsername_", "Secret password", ""));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("_TestUsername_", "Secret password", "Ni"));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("_TestUsername_", "Secret password", "Nickanme."));

        }
    }

    [Test]
    [TestCaseSource(nameof(InvalidUsersDueToNickname))]
    public void ValidateUserBeforeSignIn_ShouldFailValidationDueToIncorrectNicknameRequirements(AppUser user)
    {
        UserValidator validator = new UserValidator();

        var actual = validator.ValidateBeforeSignIn(user);
        
        Assert.IsTrue(actual.IsFailure);
    }
    public static IEnumerable<TestCaseData> ValidNewUsers
    {
        get
        {
            yield return new TestCaseData(AppUserFactory.CreateAppUser("UserAbcd", "SecretPassword123", "User ABCD",
                ScrumRole.DevelopmentTeam));
        }
    }
    
    [Test]
    [TestCaseSource(nameof(ValidNewUsers))]
    public void ValidateUserBeforeSignIn_ShouldSuccessfullyValidateUser(AppUser user)
    {
        UserValidator validator = new UserValidator();

        var actual = validator.ValidateBeforeSignIn(user);
        
        Assert.IsTrue(actual.IsSuccess, $"User should be successfully validated. {actual.Message}");
    }
}
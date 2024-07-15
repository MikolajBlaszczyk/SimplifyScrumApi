using DataAccess.Enums;
using DataAccess.Model.User;
using UserModule.Records;
using UserModule.Security.Models.Converters;

namespace UserModuleTests.Security;

[TestFixture]
public class UserConverterTests
{
    private static IEnumerable<TestCaseData> AppUserModels
    {
        get
        {
            yield return new TestCaseData(AppUserFactory.CreateAppUser("ABC", "DEF", "UDASHDHSAIHUD",
                ScrumRole.DevelopmentTeam, "testemail@example.com"));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("Test username", "test password", "testnickname",
                ScrumRole.ProjectOwner, "someemail@wp.pl"));
            yield return new TestCaseData(AppUserFactory.CreateAppUser("", ""));
        }
    }
    
    [Test]
    [TestCaseSource(nameof(AppUserModels))]
    public void ConvertToTeammate_ShouldConvertAppUserIntoTeammatesModels(AppUser user)
    {
        UserModelConverter converter = new UserModelConverter();

        var teammate = converter.ConvertToTeammate(user);
        
        Assert.That(teammate, Is.TypeOf(typeof(Teammate)));
    }
    
}
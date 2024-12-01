using DataAccess.Enums;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;
using DataAccess.Models.Factories;
using DataAccess.Models.Projects;
using Task = DataAccess.Models.Projects.Task;
using TaskFactory = DataAccess.Models.Factories.TaskFactory;

namespace SimplifyScrumApi.Tests;

public static class TestData
{
    public static Meeting Meeting = MeetingFactory.Create(
        Guid.NewGuid().ToString(),
        "Simple",
        "",
        "",
        DateTime.Now,
        TimeSpan.FromHours(3),
        MeetingType.Custom);

    public static Sprint Sprint = SprintFactory.Create(
        Guid.NewGuid().ToString(),
        "Simple",
        "Goal",
        1,
        DateTime.Now,
        "",
        "me",
        DateTime.Today);

    public static SprintNote SprintNote = SprintNoteFactory.Create(
        Random.Shared.Next(),
        new SprintNoteValue(),
        "",
        "");

    public static Feature Feature = FeatureFactory.Create(
        Guid.NewGuid().ToString(),
        "Simple",
        "",
        ExtendedStatus.New,
        1,
        null,
        "me",
        DateTime.Now);

    public static Project Project = ProjectFactory.Create(
        Guid.NewGuid().ToString(),
        "Simple",
        "",
        StandardStatus.New,
        "",
        "me",
        DateTime.Today);

    public static Task Task = TaskFactory.Create(
        Random.Shared.Next(),
        "Simple",
        SimpleStatus.Done,
        "",
        "",
        "me",
        DateTime.Now);

    public static Team Team = TeamFactory.Create(
        Guid.NewGuid().ToString(),
        "Simple",
        "");

    public static Teammate User = new Teammate
    {
        Id = Guid.NewGuid().ToString(),
        UserName = "User",
        Email = "example@abc.com",
        Nickname = "user",
        ScrumRole = ScrumRole.DevelopmentTeam,
        TeamGUID = "",
    };

}
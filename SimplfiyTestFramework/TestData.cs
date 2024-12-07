using BacklogModule.Models;
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

    #region Sprint Management
    
    public static Project Project_Success_PlanSprint_ShouldReturnNotNull 
    {
        get
        {
            return ProjectFactory.Create(Guid.NewGuid().ToString(), "Test", "", StandardStatus.New, "412432141241234", "Me", DateTime.Now);
        }
    }
    public static IEnumerable<Feature> Features_Success_PlanSprint_ShouldReturnNotNull
    {
        get
        {
            yield return FeatureFactory.Create(Guid.NewGuid().ToString(), "Test", "Test", ExtendedStatus.New, 1,
                Project_Success_PlanSprint_ShouldReturnNotNull.GUID, "Me", DateTime.Now, "me", DateTime.Now);
            
            yield return FeatureFactory.Create(Guid.NewGuid().ToString(), "Test2", "Test", ExtendedStatus.New, 1,
                Project_Success_PlanSprint_ShouldReturnNotNull.GUID, "Me", DateTime.Now, "me", DateTime.Now);
        }
    }
    public static SprintRecord Sprint_Success_PlanSprint_ShouldReturnNotNull = SprintRecord.Create("", "Test",
        "Some Goal", 1, DateTime.Now.AddDays(28), TestData.Project.GUID, "Me", DateTime.Now);
    
    public static SprintRecord Sprint_Fail_PlanSprint_NotSprintInfo_ShouldReturnNull = SprintRecord.Create("", "",
        "", -1, DateTime.Now.AddDays(29), TestData.Project.GUID, "Me", DateTime.Now);

        
    #endregion
}
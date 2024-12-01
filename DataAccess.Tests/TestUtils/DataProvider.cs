using DataAccess.Enums;
using DataAccess.Models.Projects;

namespace DataAccess.Tests.TestUtils;

public static class DataProvider
{
    public static IEnumerable<Project> Projects
    {
        get
        {
            yield return new Project()
            {
                GUID = "1",
                Name = "First",
                Description = "No desc",
                State = StandardStatus.New,
                TeamGUID = "Mine",
                CreatedBy = "Me",
                CreatedOn = DateTime.MinValue,
                LastUpdatedBy = "Me",
                LastUpdateOn = DateTime.MinValue
            };
            yield return new Project()
            {
                GUID = "2",
                Name = "Second",
                Description = "No desc",
                State = StandardStatus.New,
                TeamGUID = "Mine",
                CreatedBy = "Me",
                CreatedOn = DateTime.MinValue,
                LastUpdatedBy = "Me",
                LastUpdateOn = DateTime.MinValue
            };
            yield return new Project()
            {
                GUID = "Second",
                Name = "First",
                Description = "No desc",
                State = StandardStatus.New,
                TeamGUID = "Mine",
                CreatedBy = "Me",
                CreatedOn = DateTime.MinValue,
                LastUpdatedBy = "Me",
                LastUpdateOn = DateTime.MinValue
            };
        }
    }
}
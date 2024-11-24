using BacklogModule.Models;
using BacklogModule.Models.Results;

namespace BacklogModule.Abstraction;

public interface IManageSprint
{
    BacklogResult GetSprintInfoForProject(string projectGUID);
}
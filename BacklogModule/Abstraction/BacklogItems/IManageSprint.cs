using BacklogModule.Models;
using BacklogModule.Utils.Results;

namespace BacklogModule.Abstraction.BacklogItems;

public interface IManageSprint
{
    Task<BacklogResult> GetSprintInfoByProjectGUID(string projectGUID);

    Task<BacklogResult> PlanSprint(PlanSprintRecord record);
}
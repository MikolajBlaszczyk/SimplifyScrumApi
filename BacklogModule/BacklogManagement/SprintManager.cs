using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Models;
using BacklogModule.Utils.Results;
using DataAccess.Abstraction.Storage;

namespace BacklogModule.BacklogManagement;

public class SprintManager(ISprintStorage sprintStorage) : IManageSprint
{
    public async Task<BacklogResult> GetSprintInfoByProjectGUID(string projectGUID)
    {
        SprintRecord record = sprintStorage.GetSprintInfoByProjectGUID(projectGUID);
        return record;
    }
}
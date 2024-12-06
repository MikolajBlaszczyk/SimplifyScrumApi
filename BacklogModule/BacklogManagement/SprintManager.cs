using BacklogModule.Abstraction.BacklogItems;
using BacklogModule.Models;
using BacklogModule.Utils.Results;
using DataAccess.Abstraction.Storage;

namespace BacklogModule.BacklogManagement;

public class SprintManager(ISprintStorage sprintStorage) : IManageSprint
{
    public async Task<BacklogResult> GetSprintInfoByProjectGUID(string projectGUID)
    {
        var model  = sprintStorage.GetSprintInfoByProjectGUID(projectGUID);
        if (model is null)
            return BacklogResult.SuccessWithoutData();

        SprintRecord record = model;
        return record;
    }
}
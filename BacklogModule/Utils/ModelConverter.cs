using BacklogModule.Models;
using DataAccess.Models.Factories;
using DataAccess.Models.Projects;

namespace BacklogModule.Utils;

public static class ModelConverter
{
    public static SprintRecord ConvertSprintModelToRecord(Sprint model)
    {
        return new SprintRecord(
            model.GUID,
            model.Name,
            model.Goal,
            model.Iteration,
            model.End,
            model.ProjectGUID,
            model.Creator,
            model.LastUpdate
            );
    }

    public static Sprint ConvertSprintRecordToModel(SprintRecord record)
    {
        return SprintFactory
            .CreateSprintWithGuid(
                record.GUID,
                record.Creator,
                record.LastUpdate,
                record.Name,
                record.Goal,
                record.IterationNumber,
                record.End,
                record.ProjectGUID
                );
    }
}
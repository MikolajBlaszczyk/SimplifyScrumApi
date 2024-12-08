using DataAccess.Enums;
using DataAccess.Models.Factories;
using DataAccess.Models.Projects;

namespace BacklogModule.Models;

public record SprintNoteRecord(
    int ID,
    SprintRateValue Value,
    string TeammateGUID,
    string SprintGUID)
{
    public static SprintNoteRecord Create(
        int ID,
        SprintRateValue Value,
        string TeammateGUID,
        string SprintGUID)
    {
        return new SprintNoteRecord(
            ID,
            Value,
            TeammateGUID,
            SprintGUID);
    }

    public static implicit operator SprintNote(SprintNoteRecord record)
    {
        return SprintNoteFactory.Create(
            record.ID,
            record.Value,
            record.TeammateGUID,
            record.SprintGUID);
    }

    public static implicit operator SprintNoteRecord(SprintNote model)
    {
        return Create(
            model.ID,
            model.Value,
            model.TeammateGUID,
            model.SprintGUID);
    }
}
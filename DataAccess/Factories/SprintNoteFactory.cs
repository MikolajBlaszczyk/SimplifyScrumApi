using DataAccess.Enums;
using DataAccess.Models.Projects;

namespace DataAccess.Models.Factories;

public static class SprintNoteFactory
{
   
    
    public static SprintNote Create(int ID, SprintRateValue value, string teammateGUID, string sprintGUID)
    {
        var newSprintNote =  new SprintNote
        {
            ID = ID,
            Value = value,
            TeammateGUID = teammateGUID,
            SprintGUID = sprintGUID,
        };
        

        return newSprintNote;
    }
}
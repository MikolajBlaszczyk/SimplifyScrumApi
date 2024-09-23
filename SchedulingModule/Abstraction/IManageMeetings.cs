using SchedulingModule.Models;
using SchedulingModule.Records;

namespace SchedulingModule.Abstraction;

public interface IManageMeetings
{
    Task<ScheduleResult> UpsertMeeting(SimpleMeetingModel simpleMeeting);
    
    Task<ScheduleResult> DeleteMeeting(SimpleMeetingModel simpleMeetingToDelete);
}
using DataAccess.Model.Meetings;
using SchedulingModule.Records;

namespace SchedulingModule.Utils;

public class MeetingGrouper(ModelConverter converter)
{
    public IList<IGrouping<int, SimpleMeetingModel>> GroupMeetingsByDayOfMonth(List<Meeting> meetings) =>
        meetings.AsEnumerable()
            .Select(converter.ConvertIntoRecord)
            .GroupBy(mr => mr.Start.Day)
            .ToList();
}
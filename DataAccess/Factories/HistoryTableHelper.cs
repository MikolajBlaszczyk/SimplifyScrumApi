using DataAccess.Abstraction.Tables;

namespace DataAccess.Models.Factories;

public static class HistoryTableHelper
{
    public static void PopulateMissingValues(HistoryTable table, string createdBy, DateTime createdOn, string lastUpdatedBy, DateTime lastUpdateOn )
    {
        table.CreatedBy = createdBy;
        table.CreatedOn = createdOn;
        table.LastUpdatedBy = lastUpdatedBy;
        table.LastUpdateOn = lastUpdateOn;
    }
}
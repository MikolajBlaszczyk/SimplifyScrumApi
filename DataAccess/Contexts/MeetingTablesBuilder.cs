using DataAccess.Model.ConnectionTables;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public static class MeetingTablesBuilder
{
    public static void BuildMeetingRelatedTables(ModelBuilder builder)
    {
        builder.Entity<TeammateMeetings>()
            .HasKey(tm => new { TeammateGuid = tm.TeammateGUID, MeetingGuid = tm.MeetingGUID })
            .IsClustered(false);

        builder.Entity<TeammateMeetings>()
            .HasOne(tm => tm.Meeting)
            .WithMany(m => m.TeammateMeetings)
            .HasForeignKey(tm => tm.MeetingGUID)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<TeammateMeetings>()
            .HasOne(tm => tm.Teammate)
            .WithMany(t => t.TeammateMeetings)
            .HasForeignKey(tm => tm.TeammateGUID)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<MeetingAttachments>()
            .HasKey(ma => new { MeetingGuid = ma.MeetingGUID, AttachmentId = ma.AttachmentID })
            .IsClustered(false);

        builder.Entity<MeetingAttachments>()
            .HasOne(ma => ma.Attachment)
            .WithMany(a => a.MeetingAttachments)
            .HasForeignKey(ma => ma.AttachmentID)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<MeetingAttachments>()
            .HasOne(ma => ma.Meeting)
            .WithMany(m => m.MeetingAttachments)
            .HasForeignKey(ma => ma.MeetingGUID)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
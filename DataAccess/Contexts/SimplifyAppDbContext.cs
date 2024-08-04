using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Attachment = DataAccess.Model.Meetings.Attachment;

namespace DataAccess.Context;

public class SimplifyAppDbContext : IdentityDbContext<Teammate>
{
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    
    public DbSet<TeammateMeetings> TeammateMeetings { get; set; }
    public DbSet<MeetingAttachments> MeetingAttachments { get; set; }
    
    public SimplifyAppDbContext(DbContextOptions<SimplifyAppDbContext> options) : base(options) {}


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<TeammateMeetings>()
            .HasKey(tm => new { tm.TeammateGuid, tm.MeetingGuid })
            .IsClustered(false);

        builder.Entity<TeammateMeetings>()
            .HasOne(tm => tm.Meeting)
            .WithMany(m => m.TeammateMeetings)
            .HasForeignKey(tm => tm.MeetingGuid)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<TeammateMeetings>()
            .HasOne(tm => tm.Teammate)
            .WithMany(t => t.TeammateMeetings)
            .HasForeignKey(tm => tm.TeammateGuid)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<MeetingAttachments>()
            .HasKey(ma => new { ma.MeetingGuid, ma.AttachmentId })
            .IsClustered(false);

        builder.Entity<MeetingAttachments>()
            .HasOne(ma => ma.Attachment)
            .WithMany(a => a.MeetingAttachments)
            .HasForeignKey(ma => ma.AttachmentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<MeetingAttachments>()
            .HasOne(ma => ma.Meeting)
            .WithMany(m => m.MeetingAttachments)
            .HasForeignKey(ma => ma.MeetingGuid)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
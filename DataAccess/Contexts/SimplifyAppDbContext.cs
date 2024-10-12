using DataAccess.Model.ConnectionTables;
using DataAccess.Model.Meetings;
using DataAccess.Model.User;
using DataAccess.Models.Projects;
using DataAccess.Models.Tracking;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Attachment = DataAccess.Model.Meetings.Attachment;
using Task = DataAccess.Models.Projects.Task;

namespace DataAccess.Context;

public class SimplifyAppDbContext : IdentityDbContext<Teammate>
{
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Sprint> Sprints { get; set; }
    public DbSet<SprintNote> SprintNotes { get; set; }
    public DbSet<ActionHistory> ActionHistories { get; set; }
    
    
    public DbSet<TeammateMeetings> TeammateMeetings { get; set; }
    public DbSet<MeetingAttachments> MeetingAttachments { get; set; }
    
    public SimplifyAppDbContext(DbContextOptions<SimplifyAppDbContext> options) : base(options) {}


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        MeetingTablesBuilder.BuildMeetingRelatedTables(builder);
        ProjectTablesBuilder.BuildProjectRelatedTables(builder);
    }
}
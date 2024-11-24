using DataAccess.Model.User;
using DataAccess.Models.Projects;
using DataAccess.Models.Tracking;
using Microsoft.EntityFrameworkCore;
using Task = DataAccess.Models.Projects.Task;

namespace DataAccess.Context;

public static class ProjectTablesBuilder
{
    public static void BuildProjectRelatedTables(ModelBuilder builder)
    {
        builder.Entity<Project>()
            .HasOne(p => p.ProjectTeam)
            .WithMany(t => t.Projects)
            .HasForeignKey(p => p.TeamGUID)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Project>()
            .HasOne<Teammate>()
            .WithMany()
            .HasForeignKey(p => p.CreatedBy)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Project>()
            .HasOne<Teammate>()
            .WithMany()
            .HasForeignKey(p => p.LastUpdatedBy)  
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Feature>()
            .HasOne(f => f.ParentProject)
            .WithMany(p => p.Features)
            .HasForeignKey(f => f.ProjectGUID);

        builder.Entity<Feature>()
            .HasOne<Teammate>()
            .WithMany()
            .HasForeignKey(f => f.CreatedBy)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Feature>()
            .HasOne<Teammate>()
            .WithMany()
            .HasForeignKey(t => t.LastUpdatedBy)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Entity<Feature>()
            .ToTable(f => f.HasCheckConstraint("CK_Features_Points", "[Points] IN (-1, 1, 2, 3, 5, 8, 13)"));

        builder.Entity<Task>()
            .HasOne(t => t.ParentFeature)
            .WithMany(f => f.Tasks)
            .HasForeignKey(t => t.FeatureGUID);

        builder.Entity<Task>()
            .HasOne(t => t.AssignedTeammate)
            .WithMany(tm => tm.Tasks)
            .HasForeignKey(t => t.Assignee);

        builder.Entity<Task>()
            .HasOne<Teammate>()
            .WithMany()
            .HasForeignKey(t => t.CreatedBy)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Task>()
            .HasOne<Teammate>()
            .WithMany()
            .HasForeignKey(t => t.LastUpdatedBy)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Sprint>()
            .HasOne(s => s.Project)
            .WithMany(p => p.Sprints)
            .HasForeignKey(s => s.ProjectGUID);

        builder.Entity<Sprint>()
            .HasOne<Teammate>()
            .WithMany()
            .HasForeignKey(s => s.CreatedBy)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Sprint>()
            .HasOne<Teammate>()
            .WithMany()
            .HasForeignKey(s => s.LastUpdatedBy)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Entity<SprintNote>()
            .HasOne(sn => sn.Sprint)
            .WithMany(s => s.SprintNotes)
            .HasForeignKey(sn => sn.SprintID);

        builder.Entity<SprintNote>()
            .HasOne(s => s.Teammate)
            .WithMany(t => t.SprintNotes)
            .HasForeignKey(s => s.TeammateGUID);

        builder.Entity<SprintNote>()
            .OwnsOne(sn => sn.Value, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            });

        builder.Entity<ActionHistory>()
            .HasOne<Teammate>()
            .WithMany()
            .HasForeignKey(ah => ah.UserGUID);
    } 
}
using DataAccess.Abstraction;
using DataAccess.Context;
using DataAccess.Models.Projects;
using Microsoft.EntityFrameworkCore;
using Task = DataAccess.Models.Projects.Task;

namespace DataAccess.Accessors;

public class ProjectItemAccessor(SimplifyAppDbContext dbContext): IProjectItemsAccessor
{
    public async Task<List<Project>> GetAllProjects()
    {
        return dbContext.Projects.ToList();
    }

    public async Task<List<Feature>> GetFeatureByProjectGUID(string projectGUID)
    {
        return dbContext.Features.Where(f => f.ProjectGUID == projectGUID).ToList();
    }

    public async Task<List<Task>> GetTasksByFeatureGUID(string featureGUID)
    {
        return dbContext.Tasks.Where(t => t.FeatureGUID == featureGUID).ToList();
    }

    public async Task<Project> GetProjectByGUID(string projectGUID)
    {
        return await dbContext.Projects.FindAsync(projectGUID);
    }

    public async Task<Feature> GetFeatureByGUID(string featureGUID)
    {
        return await dbContext.Features.FindAsync(featureGUID);
    }

    public async Task<Task> GetTaskByID(int taskID)
    {
        return await dbContext.Tasks.FindAsync(taskID);
    }

    public async Task<Project> AddProject(Project project)
    {
        if (string.IsNullOrEmpty(project.GUID))
            project.GUID = Guid.NewGuid().ToString();
        
        dbContext.Projects.Add(project);
      
        int result = dbContext.SaveChanges();
       
        var addedProject = await dbContext.Projects.FindAsync(project.GUID);
        return addedProject!;
    }

    public async Task<Feature> AddFeature(Feature feature)
    {
        if (string.IsNullOrEmpty(feature.GUID))
            feature.GUID = Guid.NewGuid().ToString();
        
        dbContext.Features.Add(feature);
        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        var addedFeature = await dbContext.Features.FindAsync(feature.GUID);
        return addedFeature!;
    }

    public async Task<Task> AddTask(Task task)
    {
        dbContext.Tasks.Add(task);
        await dbContext.SaveChangesAsync();
        var addedTask = await dbContext.Tasks.FindAsync(task.ID);
        return addedTask!;
    }

    public async Task<Project> UpdateProject(Project project)
    {
        dbContext.Projects.Update(project);
        await dbContext.SaveChangesAsync();
        return project;
    }

    public async Task<Feature> UpdateFeature(Feature feature)
    {
        dbContext.Features.Update(feature);
        await dbContext.SaveChangesAsync();
        return feature;
    }

    public async Task<Task> UpdateTask(Task task)
    {
        dbContext.Tasks.Update(task);
        await dbContext.SaveChangesAsync();
        return task;
    }

    public async Task<Project> DeleteProject(string projectGUID)
    {
        var project = await dbContext.Projects.FindAsync(projectGUID);
        dbContext.Projects.Remove(project!);
        await dbContext.SaveChangesAsync();
        return project!;
    }

    public async Task<Feature> DeleteFeature(string featureGUID)
    {
        var feature = await dbContext.Features.FindAsync(featureGUID);
        dbContext.Features.Remove(feature!);
        await dbContext.SaveChangesAsync();
        return feature!;
    }

    public async Task<Task> DeleteTask(int taskID)
    {
        var task = await dbContext.Tasks.FindAsync(taskID);
        dbContext.Tasks.Remove(task!);
        await dbContext.SaveChangesAsync();
        return task!;
    }
}
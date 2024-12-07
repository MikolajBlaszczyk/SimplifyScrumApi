namespace BacklogModule.Models;

public record PlanSprintRecord(SprintRecord Sprint, List<string> FeatureGUIDs);
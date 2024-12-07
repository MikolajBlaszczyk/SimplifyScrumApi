namespace SimplifyScrum.Utils.Messages;

public static class Messages
{
    #region Http Status Codes
    
    public const string GenericError500 = "Internal server error. Cannot process the request.";
    public const string Generic204 = "No content found.";
    public const string ActionFailed = "Action failed.";
 

    public static string MissingRequiredValue(string values) => $"Bad request. All of the parameters should be provided: {values}";

    #endregion

    #region Impl method messages

    public const string GetProjectParams = "Project GUID is required.";
    public const string DeleteProjectParams = "Project GUID is required.";
    public const string GetFeaturesByProjectGUIDParams = "Project GUID is required.";
    public const string GetFeatureParams = "Feature GUID is required.";
    public const string DeleteFeatureParams = "Feature GUID is required.";
    public const string GetTasksByFeatureGUIDParams = "Feature GUID is required.";
    
    public const string PlanSprintParams = "Plan requires at least one feature.";
    
    public const string RefinedFeatureParams = "To refine feature you need to provide feature GUID and points (1 - 8).";
    public const string ReadyForRefinementParams = "To set feature ready for refinement you need to provide feature GUID.";
    public const string SplitFeatureParams = "To split feature you need to provide feature GUID.";
    #endregion

    #region Generics

    public const string UserIsNotInAnyTeam = "User is not in any team.";

    #endregion
}
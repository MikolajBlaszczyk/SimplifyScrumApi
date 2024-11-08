namespace BacklogModule.Models;

public record SprintRecord(
    string GUID,
    string Name,
    string Goal,
    int IterationNumber,
    DateTime End, 
    string ProjectGUID,
    string Creator,
    string LastUpdate
    );
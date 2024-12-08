using DataAccess.Models.Projects;

namespace BacklogModule.Models;

public record RateSprintRecord(SprintRecord Sprint, List<SprintNoteRecord> Value);
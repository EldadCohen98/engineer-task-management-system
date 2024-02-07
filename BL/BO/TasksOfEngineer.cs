namespace BO;

/// <summary>
/// An entity for a list of tasks that belong to an engineer
/// </summary>
public class TasksOfEngineer
{
    public int EngineerId {  get; init; }
    public string? EngineerName { get; set; }
    public List<BOTask>? TasksList { get; init; } = null;
    public Status? status { get; set; }

}


namespace BO;
public class BOTask
{
    public int? NumOfTask {  get; init; }
    public string? ResultOfTask {  get; set; }
    public  int DurationOfExecution { get; set; }
    public string? Comment { get; set; }
    public string? DescriptionTask { get; set; }
    public int? EsponsibleEngineerId {  get; set; }
    public DifficultyLevelTask? DifficultyTasc {  get; set; }
    public EngineerLevels? EngineerLevel {  get; set; }
    public Status? status {  get; set; }


    //--- dates ---//
    public DateTime? TaskCreationDate {  get; set; }

    public DateTime? PlannedDateForStartingWork {  get; set; }
    public DateTime? DeadLine {  get; set; }
    
    public DateTime? StartWorkDate {  get; set; }
    public DateTime? EndOfActualWork{  get; set; }
    public string? Nickname {  get; set; }
    public List<BO.TaskInTheList> TasksListDependence { get; set; } = new();
}

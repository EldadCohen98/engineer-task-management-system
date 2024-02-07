
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
    DateTime? TaskCreationDate {  get; set; }

    DateTime? PlannedDateForStartingWork {  get; set; }
    DateTime? DeadLine {  get; set; }
    
    DateTime? StartWorkDate {  get; set; }
    DateTime? EndOfActualWork{  get; set; }
    string? Nickname {  get; set; } 
}

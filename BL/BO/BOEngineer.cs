namespace BO;

public class BOEngineer
{ 
    public int EngineerId {  get; init; }
    public string? EngineerName {  get; set; }
    public string? EngineerEmail {  get; set; }
    public EngineerLevels? LeverOfEngineer {  get; set; }
    public float? SalaryPerHour {  get; set; }
    public int? CurrentTaskId {  get; set; }    
    public string? Nickname {  get; set; }
}
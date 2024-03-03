namespace BO;


public class EngineerInTask
{
    public int EngineerId {  get; init; }
    public string? EngineerName { get; set; }
    public override string ToString()
    {
        return "ID Engineer: " + EngineerId+ "Name Engineer: "+ EngineerName;
    }
}

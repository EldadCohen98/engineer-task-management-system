namespace BlApi;

public interface IBl
{
    IBOEngineer BOEngineer { get;}
    IBOTask BOTask { get;}
    IEngineerInTask EngineerInTask { get;}
    ITaskInTheList TaskInTheList { get;}
     
}

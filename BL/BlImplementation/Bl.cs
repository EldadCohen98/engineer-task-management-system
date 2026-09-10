namespace BlImplementation;
using BlApi;


internal class Bl : IBl
{
    public IBOEngineer BOEngineer => new BOEngineerImplementation();

    public IBOTask BOTask => new BOTaskImplementation();

    public IEngineerInTask EngineerInTask => new EngineerInTaskImplementation();

    public ITaskInTheList TaskInTheList => new TaskInTheListImplementation();
}

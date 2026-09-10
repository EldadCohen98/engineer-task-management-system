namespace Dal;
using DalApi;


sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }


    public IEngineer Engineer =>  new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependence Dependency => new DependenceImplementation();

}

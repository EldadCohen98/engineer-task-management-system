using DalApi;
namespace Dal;

sealed internal class DalXml : IDal
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependence Dependency => new DependenceImplementation();

    public static IDal Instance { get; } = new DalXml();

    private DalXml() { }

}

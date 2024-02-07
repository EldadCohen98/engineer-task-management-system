namespace Dal;
using DalApi;

sealed internal class DalList : IDal
{
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependence Dependency => new DependenciesImplementation();

    public static IDal Instance { get; } = new DalList();


    private DalList() { }


}
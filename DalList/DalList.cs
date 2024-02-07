namespace Dal;
using DalApi;

sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }


    public IEngineer Engineer => new EngineerImplementation();

    public IDependence Dependency => new DependenciesImplementation();

    public static IDal Instance { get; } = new DalList();


    private DalList() { }


    public IDependence Dependence => new DependenciesImplementation(); 
}

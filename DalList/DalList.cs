namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }


    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependence Dependence => new DependenciesImplementation();

}

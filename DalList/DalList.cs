namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

sealed public class DalList : IDal
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependence Dependence => new DependenciesImplementation(); 
}

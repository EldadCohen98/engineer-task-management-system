using DO;

namespace DalApi;

public interface IDal
{
    IEngineer Engineer { get; } 
    ITask Task { get; }
    IDependence Dependence { get; }
}
    
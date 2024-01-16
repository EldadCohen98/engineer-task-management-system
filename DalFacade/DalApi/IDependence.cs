namespace DalApi;

using DO;

public interface IDependence
{
    int Create(Dependence item);
    Dependence? Read(int id);
    List<Dependence> ReadAll();
    void Update(Dependence item);
    void Delete(int id);
}
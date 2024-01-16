namespace DalApi;

using DO;

public interface IEngineer
{
    int Create(Engineer item);
    Engineer? Read(int id);
    List<Engineer> ReadAll();
    void Update(Engineer item);
    void Delete(int id);
}
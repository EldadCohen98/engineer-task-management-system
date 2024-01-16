namespace DalApi;

using DO;

public interface ITask
{
    int Create(Task item);
    Task? Read(int id);
    List<Task> ReadAll();
    void Update(Task item);
    void Delete(int id);
}
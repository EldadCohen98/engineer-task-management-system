

using BO;

namespace BlApi;
/// <summary>
/// An interface for a task logical entity
/// </summary>
public interface IBOTask
{
    //A task list request with or without conditions
    public IEnumerable<BO.BOTask> ReadList(Func<BO.BOTask, bool>? filter = null);

    //Requesting the details of a particular task
    public BO.BOTask? Read(int NumberOfTask);

    //Addition of a new task
    public void Add(BO.BOTask bOTask);

    //Update existing task details
    public void Update(BO.BOTask bOTask);

    //Deleting an task
    public void Remove(int NumberOfTask);

    //Update or add a task's scheduled start date
    public void UpdateStartDate(int NumberOfTask, DateTime plannedStart);
}

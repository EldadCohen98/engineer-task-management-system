using DalApi;
using DO;
using System.Data.Common;
using System.Threading.Tasks;

namespace Dal;

internal class TaskImplementation:ITask
{
    private readonly string s_task_xml = "tasks";

    public int Create(DO.Task task)
    {
        if (Read(task.NumOfTask)is not null)
        {
            throw new DalAlreadyExistsException($"Task with number = {task.NumOfTask} already exists");
        }

        List<DO.Task> tasks = new List<DO.Task>();
        tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);
        int nextIdNumber = Config.nextNumberOfTask;
        DO.Task newTask = task with { NumOfTask = nextIdNumber };
        tasks.Add(newTask);
        XMLTools.SaveListToXMLSerializer(tasks , s_task_xml);
        return task.NumOfTask;
    }

    public void Delete(int id)
    {
        List<DO.Task> tasks = new List<DO.Task>();
        tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);
        if (Read(id) is null)
        {
            throw new DalDoesNotExistException($"Task with number = {id} does not exist");
        }

        if ((Read(id) is not null) && Read(id).erasable == true)
        {
            throw new DalDeletionImpossibleException($"Task with number = {id} cannot be deleted");
        }

        tasks.Remove(Read(id));
        XMLTools.SaveListToXMLSerializer(tasks, s_task_xml);
    }

    public DO.Task? Read(int id)
    {
        List<DO.Task> tasks = new List<DO.Task>();
        tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);

        var reTask = from task in tasks
                     where task.NumOfTask == id
                     select task;
        XMLTools.SaveListToXMLSerializer(tasks, s_task_xml);
        return reTask.FirstOrDefault();
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        List<DO.Task> tasks = new List<DO.Task>();
        tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);

        var reTask = from task in tasks 
                     where filter==null|| filter(task)
                     select task;
        XMLTools.SaveListToXMLSerializer(tasks, s_task_xml);
        return reTask.FirstOrDefault();
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        List<DO.Task> tasks = new List<DO.Task>();
        tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(s_task_xml);

        if (filter == null)
        {
            //If a certain condition was not met, then we will simply return the entire list as it is
            XMLTools.SaveListToXMLSerializer(tasks, s_task_xml);
            return tasks.Select(item=>item).ToList();
        }
        else
        {
            //If a condition is accepted, we will select all the elements that receive "true" in this condition
            XMLTools.SaveListToXMLSerializer(tasks, s_task_xml);
            return tasks.Where(item=>filter(item)).ToList();
        }
    }

    public void Update(DO.Task task)
    {
        List<DO.Task> tasks = new List<DO.Task>();
        XMLTools.SaveListToXMLSerializer(tasks, s_task_xml);

        if (task == null)
        {
            throw new DalDoesNotExistException($"Task with number = {task.NumOfTask} was not found");
        }

        tasks.Remove(Read(task.NumOfTask));
        tasks.Add(task);
        XMLTools.SaveListToXMLSerializer(tasks, s_task_xml);

    }
}

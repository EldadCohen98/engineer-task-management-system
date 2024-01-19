namespace Dal;

using DalApi;                    
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

internal class TaskImplementation : ITask
{
    public int Create(DO.Task task)
    {
        //Checking if the object is in the list.
        //If so an exception is thrown
        if (Read(task.NumOfTask) is not null)
        {
            throw new DalAlreadyExistsException($"Task with number = {task.NumOfTask} already exists");
        }
        DO.Task newTask = task with {NumOfTask = DataSource.Config.NumOfNextTask };
        //Creating a new instance is exactly the same only the task number
        //has been updated to the runner number

        DataSource.Tasks.Add(newTask);// Adding a reference to the list of tasks
        return newTask.NumOfTask; //Returning the running number value
    }

    public DO.Task? Read(int id)
    {
        //Using a Linq query to select the required task
        var reTask = from task in DataSource.Tasks
                     where (task.NumOfTask) == id
                     select task;
        return reTask.FirstOrDefault();
    }

    public IEnumerable<DO.Task> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        if (filter == null)
        {
            //If a certain condition was not met, then we will simply return the entire list as it is
            return DataSource.Tasks.Select(item => item).ToList();
        }
        else
        {
            //If a condition is accepted, we will select all the elements that receive "true" in this condition
            return DataSource.Tasks.Where(item => filter(item)).ToList();
        }
    }

    public void Update(DO.Task task)
    {
        //Checking if there is an object that should be updated
        //If it does not exist in the list, a 'null' value will be returned, and throw an exception.
        if (Read(task.NumOfTask) is null)
        {
            throw new DalDoesNotExistException($"Task with number = {task.NumOfTask} does not exist");
        }

        int i = 0;
        for (; i < DataSource.Tasks.Count; i++)
        {
            if (DataSource.Tasks[i].NumOfTask == task.NumOfTask)
            {
                DataSource.Tasks.RemoveAt(i);
                DataSource.Tasks.Insert(i, task);
                //Deleting the old value and inserting a new value in its place
                //with updated values
                return;
            }
            //Checking if there is an element in the list
            //that contains a task number like the received number
        }
        if (DataSource.Tasks.Count == i)
        {
            throw new Exception("An object with such an ID does not exist");
        }
    }

    public void Delete(int id)
    //Deleting a task by its number.
    //In the missions, everything can be deleted.
    {

        //Checking if there is an object that should be updated
        //If it does not exist in the list, a 'null' value will be returned, and throw an exception.
        if (Read(id) is null)
        {
            throw new DalDoesNotExistException($"Task with number = {id} does not exist");
        }

        if ((Read(id) is not null) && Read(id).erasable == true)
        {
            throw new DalDeletionImpossibleException($"Task with number = {id} cannot be deleted");
        }

        int i = 0;
        for (; i < DataSource.Tasks.Count; i++)
        {
            if (DataSource.Tasks[i].NumOfTask == id)
            {
                DataSource.Tasks.RemoveAt(i);
            }
        }
        if (i==DataSource.Tasks.Count)
        {
            throw new Exception("An object with such an ID does not exist");
        }
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        //Check if the function of filtering the objects in the list
        //If the condition is null there is nothing to do the filtering and the method will return the first element in the list.
        //But, if the condition is not null then we will activate the filter on each object to check if it is met.
        //if it is met then we will return the first object that received the value 'true'
        var reTask = from task in DataSource.Tasks
                     where filter==null || filter(task)
                     select task;
        return reTask.FirstOrDefault();
    }
}

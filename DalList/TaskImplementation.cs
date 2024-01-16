namespace Dal;

using DalApi;                    
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task task)
    {
        Task newTask = task with {NumOfTask = DataSource.Config.NumOfNextTask };
        //Creating a new instance is exactly the same only the task number
        //has been updated to the runner number

        DataSource.Tasks.Add(newTask);// Adding a reference to the list of tasks
        return newTask.NumOfTask; //Returning the running number value
    }

    public Task? Read(int id)
    {
        for (int i = 0; i < DataSource.Tasks.Count; i++)
        {
            if (DataSource.Tasks[i].NumOfTask ==id)
            {
                return DataSource.Tasks[i];
            }
            //Checking if there is an element in the list
            //that contains a task number like the received number
        }
        return null;
    }

    public List<Task> ReadAll()
    {
        List<Task> newList = new(DataSource.Tasks);
        return newList;
        //Making a copy of the original list into a new list and returning the new list
    }

    public void Update(Task task)
    {
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
}

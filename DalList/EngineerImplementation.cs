namespace Dal;

using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer engineer)
    {
        //Checking if the object is in the list.
        //If so an exception is thrown
        if (Read(engineer.EngineerId) is not null)
        {
            throw new DalAlreadyExistsException($"Engineer with ID = {engineer.EngineerId} already exists");
        }
        DataSource.Engineers.Add(engineer);
        return engineer.EngineerId;

    }

    public Engineer? Read(int id)
    {
        //Using a Linq query to select the required engineer
        var reEngineer = from engineer in DataSource.Engineers
                         where (engineer.EngineerId) == id
                         select engineer;
        return reEngineer.FirstOrDefault();
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)

    {
        if (filter == null)
        {
            //If a certain condition was not met, then we will simply return the entire list as it is
            return DataSource.Engineers.Select(item => item).ToList();
        }
        else
        {
            //If a condition is accepted, we will select all the elements that receive "true" in this condition
            return DataSource.Engineers.Where(item => filter(item)).ToList();
        }
    }

    public void Update(Engineer engineer)
    {
        //Update engineer details
        //Delete his old details and re-add him to the database with updated details

        //Checking if there is an object that should be updated
        //If it does not exist in the list, a 'null' value will be returned, and throw an exception.
        if (Read(engineer.EngineerId) is null)
        {
            throw new DalDoesNotExistException($"Engineer with number = {engineer.EngineerId} does not exist");
        }

        int i = 0;
        for (; i < DataSource.Engineers.Count; i++)
        {
            if (engineer.EngineerId == DataSource.Engineers[i].EngineerId)
            {
                DataSource.Engineers.RemoveAt(i);
                DataSource.Engineers.Insert(i, engineer);
                return;
            }
        }
    }


    public void Delete(int id)
    //Deleting a engineer by his number.
    //everything can be deleted.
    {

        //Checking if there is an object that should be updated
        //If it does not exist in the list, a 'null' value will be returned, and throw an exception.
        if (Read(id) is null)
        {
            throw new DalDoesNotExistException($"Engineer with number = {id} does not exist");
        }

        if ((Read(id) is not null) && Read(id)!.erasable == true)
        {
            throw new DalDeletionImpossibleException($"Engineer with ID number = {id} cannot be deleted");
        }

        int i = 0;
        for (; i < DataSource.Engineers.Count; i++)
        {
            if (DataSource.Engineers[i].EngineerId == id)
            {
                DataSource.Engineers.RemoveAt(i);
                break;
            }
        }
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        //Check if the function of filtering the objects in the list
        //If the condition is null there is nothing to do the filtering and the method will return the first element in the list.
        //But, if the condition is not null then we will activate the filter on each object to check if it is met.
        //if it is met then we will return the first object that received the value 'true'
        var reEngineer = from engineer in DataSource.Engineers
                         where filter == null || filter(engineer)
                         select engineer;
        return reEngineer.FirstOrDefault();
    }
    public void Clear()
    {
        DataSource.Engineers.Clear();
    }
}


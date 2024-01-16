namespace Dal;

using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer engineer)
    {
        //Creating a new object of type 'engineer' and adding it to the database
        for (int i = 0; i < DataSource.Engineers.Count; i++)
        {
            if (engineer.EngineerId == DataSource.Engineers[i].EngineerId)
            {
                throw new Exception("An object of type 'engineer' with such an ID already exists");
            }
        }
        DataSource.Engineers.Add(engineer);
        return engineer.EngineerId;

    }

    public Engineer? Read(int id)
    {
        //Reading details of a certain engineer by returning his details
        for (int i = 0; i < DataSource.Engineers.Count; i++)
        {
            if (id == DataSource.Engineers[i].EngineerId)
            {
                return DataSource.Engineers[i];
            }
        }
        return null;
    }

    public List<Engineer> ReadAll()
    {
        //Copying the entire engineer database to a new list and returning it
        List<Engineer> newList = new(DataSource.Engineers);
        return newList;
    }

    public void Update(Engineer engineer)
    {
        //Update engineer details
        //Delete his old details and re-add him to the database with updated details
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
        if (i == DataSource.Engineers.Count)
        {
            throw new Exception("An object of type 'Engineer' with such an ID does not exist");
        }
    }


    public void Delete(int id)
    //Deleting a task by its number.
    //everything can be deleted.
    {
        int i = 0;
        for (; i < DataSource.Engineers.Count; i++)
        {
            if (DataSource.Engineers[i].EngineerId == id)
            {
                DataSource.Engineers.RemoveAt(i);
                break;
            }
        }
        if (i == DataSource.Engineers.Count)
        {
            throw new Exception("An object with such an ID does not exist");
        }
    }
}
    

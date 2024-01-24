namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class EngineerImplementation:IEngineer
{
    readonly string s_engineer_xml = "engineers";

    public int Create(Engineer engineer)
    {
        //Checking if the object is in the file.
        //If so an exception is thrown
        if (Read(engineer.EngineerId)is not null)
        {
            throw new DalAlreadyExistsException($"An engineer with an ID {engineer.EngineerId} already exists");
        }

        List<Engineer> engineers = new List<Engineer>();
        engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer_xml);
        
        engineers.Add(engineer);
        XMLTools.SaveListToXMLSerializer(engineers, s_engineer_xml);
        return engineer.EngineerId;
    }

    public void Delete(int id)
    {
        //Checking if there is a wanted engineer
        if (Read(id) is  null)
        {
            throw new DalDoesNotExistException($"An engineer with an ID {id} does not exists");
        }

        //Checking if the engineer can delete
        if ((Read(id) is not null) && Read(id).erasable == true)
        {
            throw new DalDeletionImpossibleException($"Engineer with ID number = {id} cannot be deleted");
        }

        //Deleting the engineer from the list and updating the file in the new list
        List<Engineer> engineers = new List<Engineer>();
        engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer_xml);

        engineers.Remove(Read(id));

        XMLTools.SaveListToXMLSerializer(engineers, s_engineer_xml);

    }

    public Engineer? Read(int id)
    {
        List<Engineer> engineers = new List<Engineer>();
        engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer_xml);

        //Using a Linq query to select the required engineer
        var reEngineer = from engineer in engineers
                         where engineer.EngineerId == id
                         select engineer;
        XMLTools.SaveListToXMLSerializer(engineers, s_engineer_xml);
        
        return reEngineer.FirstOrDefault();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        //Check if the function of filtering the objects in the list
        //If the condition is null there is nothing to do the filtering and the method will return the first element in the list or null.
        //But, if the condition is not null then we will activate the filter on each object to check if it is met.
        //if it is met then we will return the first object that received the value 'true'
        List<Engineer> engineers = new List<Engineer>();
        engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer_xml);

        var reEngineer = from engineer in engineers
                         where filter==null||filter(engineer)
                         select engineer;
        XMLTools.SaveListToXMLSerializer(engineers, s_engineer_xml);
        return reEngineer.FirstOrDefault();
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> engineers = new List<Engineer>();
        engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer_xml);

        if (filter == null)
        {
            XMLTools.SaveListToXMLSerializer(engineers, s_engineer_xml);
            return engineers.Select(item=>item).ToList();
        }
        else
        {
            XMLTools.SaveListToXMLSerializer(engineers, s_engineer_xml);
            return engineers.Where(item=>filter(item)).ToList();
        }
    }

    public void Update(Engineer engineer)
    {
        //Checking if there is a wanted engineer
        if (Read(engineer.EngineerId) is null)
        {
            throw new DalDoesNotExistException($"An engineer with an ID {engineer.EngineerId} does not exists");
        }

        //Deleting from the list an engineer with an equal ID number
        //Income of an up - to - date engineer
        List<Engineer> engineers = new List<Engineer>();
        engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(s_engineer_xml);
        engineers.Remove(Read(engineer.EngineerId));
        engineers.Add(engineer);

        XMLTools.SaveListToXMLSerializer(engineers, s_engineer_xml);
    }
}

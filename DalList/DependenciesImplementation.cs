namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DependenciesImplementation : IDependence
{
    public int Create(Dependence dependence)
    {
        Dependence newDependenceTask = dependence with { DependencyIdNumber = DataSource.Config.NumOfNextTask };
        DataSource.Dependences.Add(dependence);
        return dependence.DependencyIdNumber;
    }

    public Dependence? Read(int id)
    {
        //Reading details of a certain engineer by returning his details
        for (int i = 0; i < DataSource.Dependences.Count; i++)
        {
            if (id == DataSource.Dependences[i].TaskNumberDepends)
            {
                return DataSource.Dependences[i];
            }
        }
        return null;

    }

    public List<Dependence> ReadAll()
    {
        //Copying the entire dependence database to a new list and returning it
        List<Dependence> newList = new(DataSource.Dependences);
        return newList;
    }

    public void Update(Dependence dependence)
    {
        //Update Dependence details
        //Delete his old details and re-add him to the database with updated details
        int i = 0;
        for (; i < DataSource.Dependences.Count; i++)
        {
            if (dependence.TaskNumberDepends == DataSource.Dependences[i].TaskNumberDepends)
            {
                DataSource.Dependences.RemoveAt(i);
                DataSource.Dependences.Insert(i, dependence);
                return;
            }
        }
        if (i == DataSource.Dependences.Count)
        {
            throw new Exception("An object of type 'Dependence' with such an ID does not exist");
        }
    }

    public void Delete(int id)
    {
        //Deleting a task by its number.
        //everything can be deleted.
        {
            int i = 0;
            for (; i < DataSource.Dependences.Count; i++)
            {
                if (DataSource.Dependences[i].TaskNumberDepends == id)
                {
                    DataSource.Dependences.RemoveAt(i);
                    break;
                }
            }
            if (i == DataSource.Dependences.Count)
            {
                throw new Exception("An object with such an ID does not exist");
            }
        }
    }
}

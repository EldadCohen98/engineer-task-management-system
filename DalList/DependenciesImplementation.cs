namespace Dal;

using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

internal class DependenciesImplementation : IDependence
{
    public int Create(Dependence dependence)
    {

        //Checking if the object is in the list.
        //If so an exception is thrown
        if (Read(dependence.DependencyIdNumber) is not null)
        {
            throw new DalAlreadyExistsException($"Task depends with ID number = {dependence.DependencyIdNumber} already exists");
        }

        Dependence newDependenceTask = dependence with { DependencyIdNumber = DataSource.Config.NumOfNextTask };
        DataSource.Dependences.Add(newDependenceTask);
        return newDependenceTask.DependencyIdNumber;
    }

    public Dependence? Read(int id)
    {
        //Using a Linq query to select the required dependence task
        var reDependence = from dependence in DataSource.Dependences
                           where dependence.DependencyIdNumber == id
                           select dependence;
        return reDependence.FirstOrDefault();
    }

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    {
        if (filter == null)
        {
            //If a certain condition was not met, then we will simply return the entire list as it is
            return DataSource.Dependences.Select(item => item).ToList();
        }
        else
        {
            //If a condition is accepted, we will select all the elements that receive "true" in this condition
            return DataSource.Dependences.Where(item => filter(item)).ToList();
        }
    }

    public void Update(Dependence dependence)
    {

        //Checking if there is an object that should be updated
        //If it does not exist in the list, a 'null' value will be returned, and throw an exception.
        if (Read(dependence.TaskNumberDepends) is null)
        {
            throw new DalDoesNotExistException($"Task depends with number = {dependence.TaskNumberDepends} does not exist");
        }

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
    }

    public void Delete(int id)
    {

        //Checking if there is an object that should be updated
        //If it does not exist in the list, a 'null' value will be returned, and throw an exception.
        if (Read(id) is null)
        {
            throw new DalDoesNotExistException($"Task depends with number = {id} does not exist");
        }

        if ((Read(id) is not null)&& Read(id).erasable == true)
        { 
            throw new DalDeletionImpossibleException($"Task depends with number = {id} cannot be deleted");
        }

        //Deleting a task by its number.
        //everything can be deleted.
        
        for (int i = 0; i < DataSource.Dependences.Count; i++)
        {
            if (DataSource.Dependences[i].TaskNumberDepends == id)
            {
                DataSource.Dependences.RemoveAt(i);
                return;
            }
        }
    }

    public Dependence? Read(Func<Dependence, bool> filter)
    {
        //Check if the function of filtering the objects in the list
        //If the condition is null there is nothing to do the filtering and the method will return the first element in the list.
        //But, if the condition is not null then we will activate the filter on each object to check if it is met.
        //if it is met then we will return the first object that received the value 'true'
        var reDependence = from dependence in DataSource.Dependences
                     where filter == null || filter(dependence)
                     select dependence;
        return reDependence.FirstOrDefault();
    }

    public void Clear()
    {
        DataSource.Dependences.Clear();
    }
}

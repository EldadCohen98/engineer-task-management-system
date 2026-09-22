namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class DependenciesImplementation : IDependence
{
    public int Create(Dependency dependence)
    {
        Dependency newDependenceTask = dependence with
        {
            DependencyIdNumber = DataSource.Config.NumOfNextDependency
        };

        DataSource.Dependences.Add(newDependenceTask);
        return newDependenceTask.DependencyIdNumber;
    }

    public Dependency? Read(int id)
    {
        var reDependence = from dependence in DataSource.Dependences
                           where dependence.DependencyIdNumber == id
                           select dependence;

        return reDependence.FirstOrDefault();
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter == null)
        {
            return DataSource.Dependences.Select(item => item).ToList();
        }

        return DataSource.Dependences.Where(item => filter(item)).ToList();
    }

    public void Update(Dependency dependence)
    {
        int index = DataSource.Dependences.FindIndex(
            item => item.DependencyIdNumber == dependence.DependencyIdNumber);

        if (index < 0)
        {
            throw new DalDoesNotExistException(
                $"Task depends with number = {dependence.DependencyIdNumber} does not exist");
        }

        DataSource.Dependences[index] = dependence;
    }

    public void Delete(int id)
    {
        Dependency? dependence = Read(id);

        if (dependence is null)
        {
            throw new DalDoesNotExistException(
                $"Task depends with number = {id} does not exist");
        }

        if (dependence.erasable == true)
        {
            throw new DalDeletionImpossibleException(
                $"Task depends with number = {id} cannot be deleted");
        }

        int index = DataSource.Dependences.FindIndex(
            item => item.DependencyIdNumber == id);

        DataSource.Dependences.RemoveAt(index);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        var reDependence = from dependence in DataSource.Dependences
                           where filter(dependence)
                           select dependence;

        return reDependence.FirstOrDefault();
    }

    public void Clear()
    {
        DataSource.Dependences.Clear();
    }
}

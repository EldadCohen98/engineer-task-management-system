namespace BlImplementation;

using BlApi;
using BO;
using DalApi;
using DO;
using System.Collections.Generic;

internal class BOEngineerImplementation : IBOEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int? Add(BO.BOEngineer bOEngineer)
    {
        if(bOEngineer.EngineerId!>0)
            throw new BO.BlIncorrectInput($"An engineer ID number cannot be negative");

        if (bOEngineer.EngineerName == null || bOEngineer.EngineerName.Length == 0)
            throw new BO.BlIncorrectInput($"An engineer's name cannot be an empty string");

        if(bOEngineer.SalaryPerHour!>0)
            throw new BO.BlIncorrectInput($"An engineer's salary cannot be negative");
  

        DO.Engineer doEngineer = new DO.Engineer(bOEngineer.EngineerId,
                                                 bOEngineer.EngineerName,
                                                 bOEngineer.EngineerEmail,
                                                 null,
                                                 bOEngineer.SalaryPerHour);
        try
        {
            return _dal.Engineer.Create(doEngineer);
        }
        catch (DO.DalAlreadyExistsException)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with number = {bOEngineer.EngineerId} already exists");
        }

    }
    public BO.BOEngineer Read(int EngineerIdFromPl)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(EngineerIdFromPl);
        if (doEngineer == null)
            throw new BlDoesNotExistException($"Engineer with number = {EngineerIdFromPl} dose not exists");

        return new BO.BOEngineer() 
        {
            EngineerId = EngineerIdFromPl,
            EngineerName = doEngineer!.EngineerName,
            EngineerEmail = doEngineer.EngineerEmail,
            SalaryPerHour = doEngineer.SalaryPerHour,
            //Returning the ID number of the task that the engineer is responsible for
            CurrentTaskId = _dal.Task.Read(task => task.EsponsibleEngineerId == EngineerIdFromPl)!.NumOfTask,
            //Returning the engineer's mission description
            Nickname = _dal.Task.Read(task => task.EsponsibleEngineerId == EngineerIdFromPl)!.Nickname
        };
    }

    public IEnumerable<BO.BOEngineer> ReadList(Func<BO.BOEngineer, bool>? filter = null)
    {
        //Creating a collection of BO engineers
        List< BO.BOEngineer?> engineerList = new List<BO.BOEngineer?>();

        List<DO.Engineer?> doEngineerList = new List<DO.Engineer?>(_dal.Engineer.ReadAll()!);

        //Going over each DO engineer and copying the data to the BO engineer
        foreach (var doengineer in doEngineerList)
        {
            DO.Task? doCurrentTask = _dal.Task.Read(task => task.EsponsibleEngineerId == doengineer!.EngineerId)!;
            
            BO.BOEngineer? boengineer = new BO.BOEngineer
            {
                EngineerId = doengineer!.EngineerId,
                EngineerName = doengineer.EngineerName,
                EngineerEmail = doengineer.EngineerEmail,
                SalaryPerHour = doengineer.SalaryPerHour,
                LeverOfEngineer = (BO.EngineerLevels?)doengineer.LeverOfEngineer,
            };
            
            //Added a BO engineer to the collection
            engineerList.Add(boengineer);
        }

        //Returning the collection when the condition is empty
        if (filter == null)
        {
            return engineerList!;
        }

        //The return of the engineers who are true to the condition
        else
        {
            return engineerList.Where(item => filter(item!)!)! ;
        }
    }

    public void Remove(int EngineerId)
    {

        BO.BOEngineer bOEngineer = new BO.BOEngineer();
        bOEngineer = Read(EngineerId);

        if (bOEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with number = {EngineerId} dose not exists");

        if (bOEngineer.CurrentTaskId is not null)
        {
            DO.Engineer doEngineer = new DO.Engineer(bOEngineer.EngineerId, bOEngineer.EngineerName, bOEngineer.EngineerEmail, null, bOEngineer.SalaryPerHour);
            try
            {
                _dal.Engineer.Delete(EngineerId);
            }
            catch (DO.DalDoesNotExistException)
            {
                throw new BlDoesNotExistException($"Engineer with number = {EngineerId} dose not exists ");
            }
            catch (DO.DalDeletionImpossibleException)
            {
                throw new BlDeletionImpossibleException($"Engineer with ID number = {EngineerId} cannot be deleted");
            }

        }
    }
    public void Update(BO.BOEngineer bOEngineer)
    {
        if (bOEngineer.EngineerId! > 0)
            throw new BO.BlIncorrectInput($"An engineer ID number cannot be negative");

        if (bOEngineer.EngineerName == null || bOEngineer.EngineerName.Length == 0)
            throw new BO.BlIncorrectInput($"An engineer's name cannot be an empty string");

        if (bOEngineer.SalaryPerHour! > 0)
            throw new BO.BlIncorrectInput($"An engineer's salary cannot be negative");
        
        if(Read(bOEngineer.EngineerId)==null)
            throw new BlDoesNotExistException($"Engineer with number = {bOEngineer.EngineerId} dose not exists ");

        DO.Engineer dalEngineer= new DO.Engineer(bOEngineer.EngineerId, bOEngineer.EngineerName, bOEngineer.EngineerEmail, (DO.EngineerLevels?)bOEngineer.LeverOfEngineer, bOEngineer.SalaryPerHour,null);

        try
        {
            _dal.Engineer.Update(dalEngineer);
        }
        catch (DO.DalDoesNotExistException)
        {
            throw new BlDoesNotExistException($"Engineer with number = {bOEngineer.EngineerId} dose not exists "); 
        }
    }
}

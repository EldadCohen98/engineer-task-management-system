
using BlApi;
using BO;
using System.Data.SqlTypes;
using System.Reflection;
using System.Reflection.Metadata;

namespace BlImplementation;

internal class EngineerInTaskImplementation : IEngineerInTask 
{
    private DalApi.IDal _dal = DalApi.Factory.Get;


    //Returning the name and ID number of an engineer on a mission
    public EngineerInTask? EngineerInCharge(BO.BOTask boTask)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read((int)boTask.EsponsibleEngineerId!)!;

        BO.EngineerInTask? engineerCharge = new BO.EngineerInTask()
        {
            EngineerId = (int)doEngineer.EngineerId,
            EngineerName =(string)doEngineer.EngineerName!
        };
        return engineerCharge;
    }

    //The return of all the names and identifying numbers of the engineers
    public List<EngineerInTask> EngeneersList()
    {
        List <EngineerInTask> engineerInTasksList = new List <EngineerInTask>();
        List<DO.Engineer> DoengineerList= new List<DO.Engineer>(_dal.Engineer.ReadAll()!);
        foreach (var item in DoengineerList)
        {
            BO.EngineerInTask? engineerInTasks = new BO.EngineerInTask()
            {
                EngineerId = (int)item.EngineerId,
                EngineerName = (string)item.EngineerName!
            };
            engineerInTasksList.Add(engineerInTasks);
        }
        return engineerInTasksList;
    }
}

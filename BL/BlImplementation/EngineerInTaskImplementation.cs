
using BlApi;
using BO;

namespace BlImplementation;

internal class EngineerInTaskImplementation : IEngineerInTask 
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

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
}

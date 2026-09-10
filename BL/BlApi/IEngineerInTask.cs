

using BO;
using DO;

namespace BlApi;

public interface IEngineerInTask
{
    public EngineerInTask? EngineerInCharge(BO.BOTask boTask);
    public List<EngineerInTask> EngeneersList();

    

}

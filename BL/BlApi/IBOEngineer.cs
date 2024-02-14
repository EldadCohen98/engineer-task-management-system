

namespace BlApi;
/// <summary>
/// Interface for an engineer logical entity
/// </summary>
public interface IBOEngineer
{
    //Request a list of engineers with or without a filter
    public IEnumerable<BO.BOEngineer> ReadList(Func<BO.BOEngineer, bool>? filter = null);


    //Requesting the details of a particular engineer
    public BO.BOEngineer Read(int EngineerId);

    //Addition of a new engineer
    public int? Add(BO.BOEngineer bOEngineer);

    //Deleting an engineer
    public void Remove(int EngineerId);

    //Update existing engineer details
    public void Update(BO.BOEngineer bOEngineer);
}

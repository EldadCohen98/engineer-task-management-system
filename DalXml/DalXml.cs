using DalApi;
using System.Diagnostics;
namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }


    public IEngineer Engineer =>  new EngineerImplementation();

    private DalXml() { }

}


namespace DO
{
    /// <summary>
    /// This entity describes the relationship and dependencies between the existing tasks
    /// </summary>
    /// <param name="DependencyIdNumber"></param> Automatic running number of
    /// <param name="TaskNumberDepends"></param> Mission Identification Number
    /// <param name="PreviousTaskDepends"></param> Previous assignment identification number
    public record Dependence  
    (
       int DependencyIdNumber,
       int TaskNumberDepends,
       int PreviousTaskDepends,
       bool? erasable = null
    )
    {
        public Dependence() :this(0,0,0) { } 
    }
}

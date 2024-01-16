namespace Dal;
using System.Collections.Generic;
internal static class DataSource
{

    internal static class Config
    {
        internal const int numOfTask = 1; 
        private static int numOfNextTask = numOfTask;
        internal static int NumOfNextTask { get=> numOfNextTask++;}
    }

    internal static List<DO.Task> Tasks { get; } = new();
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Dependence> Dependences { get; } = new();
}

namespace Dal;
internal static class Config
{
    static string s_data_config_xml = "data-config";

    //A task runner number
    private static int numberOfTask = 1;
    //A depending task runner number
    private static int numberOfDependenceTask = 1;
    internal static int nextNumberOfTask { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "numberOfTask"); }
    internal static int nextNumberOfDependenceTask { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "numberOfDependenceTask"); }
}



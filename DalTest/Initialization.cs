namespace DalTest;


using DalApi;
using DO;
using System.Collections.Generic;
using System.Data.Common;

public static class Initialization
{
    private static IDal? s_dal;

    public static Random SRandom { get; } = new();

    public static void Do()
    {
        //s_dal = dal ?? throw new NullReferenceException("DAL can not be null");
        s_dal = DalApi.Factory.Get;
        clearLists();
        CreateEngineer();
        CreateTask();
        CreateDependence();
    }

    public static void clearLists()
    {
        s_dal!.Engineer.Clear();
        s_dal.Task.Clear();
        s_dal.Dependency.Clear();
    }

    private static void CreateTask()
    {

        EngineerLevels trainingEngineerLevel;
        DifficultyLevelTask difficultyLevelTask;
        int? idEngineer;

        //A size 5 array representing engineer levels
        EngineerLevels[] engineerLevels = new EngineerLevels[5];
        engineerLevels[0] = EngineerLevels.Beginner;
        engineerLevels[1] = EngineerLevels.AdvancedBeginner;
        engineerLevels[2] = EngineerLevels.Advanced;
        engineerLevels[3] = EngineerLevels.Intermediate;
        engineerLevels[4] = EngineerLevels.Expert;

        //An array representing the difficulty levels of a task
        DifficultyLevelTask[] levelTasks = new DifficultyLevelTask[5];
        levelTasks[0] = DifficultyLevelTask.Competent;
        levelTasks[1] = DifficultyLevelTask.Novice;
        levelTasks[2] = DifficultyLevelTask.Expert;

        List<Engineer> engineers = (List<Engineer>)s_dal!.Engineer.ReadAll();
        int totalEngineers = engineers!.Count;

        for (int i = 0; i < 50; i++)
        {
            string descriptionTsak = "Task number" + (i + 1).ToString();
            string nicknameTask = ("T") + (i + 1).ToString();
            trainingEngineerLevel = engineerLevels[SRandom.Next(0, 4)];
            idEngineer = engineers[SRandom.Next(0, totalEngineers)]!.EngineerId;
            difficultyLevelTask = levelTasks[SRandom.Next(0, 3)];
            Task task = new(0, "", 0, "", descriptionTsak, idEngineer, difficultyLevelTask, trainingEngineerLevel, DateTime.Today, new DateTime(2025, 01, 01), new DateTime(2028, 08, 01), null, null, false, nicknameTask);
            s_dal!.Task.Create(task);
        }
    }
    private static void CreateEngineer()
    {
        int maxId = 400000000, minId = 200000000;
        int _id;
        string nameEngineer, emailEnginrr;
        EngineerLevels trainingEngineerLevel;
        //יצירת מעkרך בגודל 5 המייצג את רמות המהנדסים
        EngineerLevels[] engineerLevels = new EngineerLevels[5];
        engineerLevels[0] = EngineerLevels.Beginner;
        engineerLevels[1] = EngineerLevels.AdvancedBeginner;
        engineerLevels[2] = EngineerLevels.Advanced;
        engineerLevels[3] = EngineerLevels.Intermediate;
        engineerLevels[4] = EngineerLevels.Expert;

        //Running on the 'engineers' list (size 4)
        //and checking which of those listed in Tafel is on the list of engineers
        //If someone exists, go to the next entry in Tafel, and if not, update their errors and add them to the list

        (string, string)[] engineerNameAndEmailTuple =
        {
            ("Avraham Cohen","avi1234@gmail.com"),
            ("Sara Cohen","SaraCo72@gmaol.com"),
            ("Netan Cohen","CoheNat@gamil.com"),
            ("Noga Cohen","NogaCoh@gamil.com"),
            ("Elyasaf Cohen","Elyasaf97@gamil.com"),
            ("Uriel Cohen","UriCo@gamil.com"),
            ("Eldad Cohen","EldCohen@gamil.com"),
            ("Oriya Cohen","OriCohen@gamil.com"),
            ("Achinoam Cohen","Achi92Co@gamil.com"),
            ("Noa Cohen","Noa2000@gamil.com")
        };
        //
        for (int i = 0; i < 4; i++)
        {
            foreach (var stringNameAndEmail in engineerNameAndEmailTuple)
            {
                do
                {
                    _id = SRandom.Next(minId, maxId);
                } while (s_dal!.Engineer.Read(_id) != null);
                nameEngineer = stringNameAndEmail.Item1;
                emailEnginrr = stringNameAndEmail.Item2;
                trainingEngineerLevel = engineerLevels[SRandom.Next(0, 4)];
                float perHour = SRandom.Next(5000, 20000);
                Engineer newEngineer = new(_id, nameEngineer, emailEnginrr, trainingEngineerLevel, perHour);
                s_dal!.Engineer.Create(newEngineer);
            }
        }
    }

    private static void CreateDependence()
    {
        List<Task> dependence = (List<Task>)s_dal!.Task.ReadAll();

        for (int i = 0; i < 50; i++)
        {
            int idTask1 = dependence[SRandom.Next(0, dependence.Count)]!.NumOfTask;
            Dependency newDependence = new(0, idTask1, dependence[SRandom.Next(0, dependence.Count)]!.NumOfTask);
            s_dal!.Dependency.Create(newDependence);

        }
    }
}

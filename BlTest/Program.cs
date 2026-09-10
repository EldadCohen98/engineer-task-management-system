
using BO;
using DalApi;
using DalTest;
using DO;
using System.Reflection;
using System.Threading.Channels;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace BlTest
{
    public class Program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public static void updateEngineerLevel(BOEngineer? updateEngineer)
        {
            Console.WriteLine("enter the new level 1.Beginner\r\n2. AdvancedBeginner\r\n3.Advanced\r\n4. Intermediate\r\n5.Expert");
            int choseUpdateLevel = int.Parse(Console.ReadLine()!);
            switch (choseUpdateLevel)
            {
                //Beginner
                case 1:
                    if (updateEngineer!.LeverOfEngineer == null)
                        updateEngineer.LeverOfEngineer = BO.EngineerLevels.Beginner;
                    else
                        throw new BO.BlIncorrectInput("It is not possible to update to a lower level");
                    break;

                //AdvancedBeginner
                case 2:
                    if (updateEngineer!.LeverOfEngineer < BO.EngineerLevels.AdvancedBeginner)
                        updateEngineer.LeverOfEngineer = BO.EngineerLevels.AdvancedBeginner;
                    else
                        throw new BO.BlIncorrectInput("It is not possible to update to a lower level");
                    break;

                //Advanced
                case 3:
                    if (updateEngineer!.LeverOfEngineer < BO.EngineerLevels.Advanced)
                        updateEngineer.LeverOfEngineer = BO.EngineerLevels.Advanced;
                    else
                        throw new BO.BlIncorrectInput("It is not possible to update to a lower level");
                    break;

                //Intermediate
                case 4:
                    if (updateEngineer!.LeverOfEngineer < BO.EngineerLevels.Intermediate)
                        updateEngineer.LeverOfEngineer = BO.EngineerLevels.Intermediate;
                    else
                        throw new BO.BlIncorrectInput("It is not possible to update to a lower level");
                    break;

                //Expert
                case 5:
                    if (updateEngineer!.LeverOfEngineer < BO.EngineerLevels.Expert)
                        updateEngineer.LeverOfEngineer = BO.EngineerLevels.Expert;
                    else
                        throw new BO.BlIncorrectInput("It is not possible to update to a lower level");
                    break;
            }
        }

        public static void BOEngineerEntity()
        {
            Console.WriteLine("What action would you like to take?\r\n1. Add a new engineer.\r\n2. Search for an engineer from the database.\r\n3. List of engineers.\r\n4. Delete an engineer.\r\n5. Update engineer details.");
            int chooseMethod = int.Parse(Console.ReadLine()!);

            switch (chooseMethod)
            {
                //Add new engineer
                case 1:
                    Console.Write("enter the ID of the engineer");
                    int ID = int.Parse(Console.ReadLine()!);

                    Console.Write("ebter the name of the engineer");
                    string name = Console.ReadLine()!;

                    Console.Write("enter the email of the engineer");
                    string email = Console.ReadLine()!;

                    Console.Write("ebter the level of the engineer");
                    BO.EngineerLevels level;
                    Enum.TryParse<BO.EngineerLevels>(Console.ReadLine(), out level);

                    Console.Write("enter the salary of the engineer");
                    float salary = float.Parse(Console.ReadLine()!);

                    List<BOTask> tasks = new List<BOTask>(s_bl.BOTask.ReadList());
                    var CurrentTaskSearch = from task in tasks
                                            where task.EsponsibleEngineerId == ID
                                            select task;
                    BOTask? newTask = CurrentTaskSearch.FirstOrDefault()!;

                    BOEngineer bOEngineer = new BOEngineer()
                    {
                        EngineerId = ID,
                        EngineerName = name,
                        EngineerEmail = email,
                        LeverOfEngineer = level,
                        SalaryPerHour = salary,
                        //CurrentTaskId = newTask.NumOfTask,
                        //Nickname = newTask.Nickname,
                    };

                    s_bl.BOEngineer.Add(bOEngineer);
                    break;

                //read wanted engineer
                case 2:
                    Console.WriteLine("Enter the ID number of a wanted engineer");
                    int IdEngineer = int.Parse(Console.ReadLine()!);
                    Console.WriteLine($"the Wanted engineer is: {s_bl.BOEngineer.Read(IdEngineer)}");
                    break;

                //read list of engineers
                case 3:
                    Console.WriteLine("enter the filter\n1. by training level\n2. Engineers without a mission\n3. without filter");
                    int filter = int.Parse(Console.ReadLine()!);
                    switch (filter)
                    {
                        //filtering by traning level
                        case 1:
                            Console.WriteLine("Enter the filter level 1-5");
                            int levelFilter = int.Parse(Console.ReadLine()!);
                            switch (levelFilter)
                            {
                                //beginner
                                case 1:
                                    List<BOEngineer> beginnersList = new List<BOEngineer>(s_bl.BOEngineer.ReadList(engineer => engineer.LeverOfEngineer == BO.EngineerLevels.Beginner));
                                    foreach (var engineer in beginnersList)
                                    {
                                        getPropertysEntity(engineer);
                                        Console.WriteLine();
                                    }
                                    break;

                                //advancedBeginner
                                case 2:
                                    List<BOEngineer> advancedBeginnersList = new List<BOEngineer>(s_bl.BOEngineer.ReadList(engineer => engineer.LeverOfEngineer == BO.EngineerLevels.AdvancedBeginner));
                                    foreach (var engineer in advancedBeginnersList)
                                    {
                                        getPropertysEntity(engineer);
                                        Console.WriteLine();
                                    }
                                    break;

                                //advanced
                                case 3:
                                    List<BOEngineer> advancedsList = new List<BOEngineer>(s_bl.BOEngineer.ReadList(engineer => engineer.LeverOfEngineer == BO.EngineerLevels.Advanced));
                                    foreach (var engineer in advancedsList)
                                    {
                                        getPropertysEntity(engineer);
                                        Console.WriteLine();
                                    }
                                    break;

                                //advanced
                                case 4:
                                    List<BOEngineer> intermediatesList = new List<BOEngineer>(s_bl.BOEngineer.ReadList(engineer => engineer.LeverOfEngineer == BO.EngineerLevels.Intermediate));
                                    foreach (var engineer in intermediatesList)
                                    {
                                        getPropertysEntity(engineer);
                                        Console.WriteLine();
                                    }
                                    break;

                                //expert
                                case 5:
                                    List<BOEngineer> expertsList = new List<BOEngineer>(s_bl.BOEngineer.ReadList(engineer => engineer.LeverOfEngineer == BO.EngineerLevels.Expert));
                                    foreach (var engineer in expertsList)
                                    {
                                        getPropertysEntity(engineer);
                                        Console.WriteLine();
                                    }
                                    break;
                            }
                            break;

                        //Engineers without a mission
                        case 2:
                            List<BOEngineer> withoutMission = new List<BOEngineer>(s_bl.BOEngineer.ReadList(engineer => engineer.CurrentTaskId == null));
                            foreach (var engineerMission in withoutMission)
                            {
                                getPropertysEntity(engineerMission);
                                Console.WriteLine();
                            }
                            break;

                        //without filter
                        case 3:
                            List<BOEngineer> withoutFilter= new (s_bl.BOEngineer.ReadList());
                            foreach (var engineerFilterless in withoutFilter)
                            {
                                getPropertysEntity(engineerFilterless);
                                Console.WriteLine();
                            }
                            break;
                    
                        default:
                            break;
                    }
                    break;

                // delete engineer
                case 4:
                    Console.WriteLine("Enter the ID number of a wanted engineer");
                    int IdEngineerForDelete = int.Parse(Console.ReadLine()!);
                    s_bl.BOEngineer.Remove(IdEngineerForDelete);
                    break;

                //update ditales engineer
                case 5:
                    Console.WriteLine("Enter an engineer's ID number to update");
                    int engineerId = int.Parse(Console.ReadLine()!);
                    BOEngineer? updateEngineer = s_bl.BOEngineer.Read(engineerId);

                    Console.WriteLine("Which field would you like to update?\r\n" +
                        "1. Name.\r\n" +
                        "2. Mail.\r\n" +
                        "3. Engineer level.\r\n" +
                        "4. Hourly wage.\r\n" +
                        "5. Task selection.");
                    int propertySelection = int.Parse(Console.ReadLine()!);
                    
                    switch (propertySelection)
                    {

                        //name
                        case 1:
                            Console.WriteLine("enter the new name");
                            string newName = Console.ReadLine()!;
                            updateEngineer.EngineerName = newName;
                            s_bl.BOEngineer.Update(updateEngineer);
                            break;

                        //mail
                        case 2:
                            Console.WriteLine("enter the new name");
                            string newMail = Console.ReadLine()!;
                            updateEngineer.EngineerName = newMail;
                            s_bl.BOEngineer.Update(updateEngineer);
                            break;
                        //engineer level
                        case 3:
                            Console.WriteLine("enter the new level 1.Beginner\r\n" +
                                "2. AdvancedBeginner\r\n" +
                                "3.Advanced\r\n" +
                                "4. Intermediate\r\n" +
                                "5.Expert");
                            int choseUpdateLevel = int.Parse(Console.ReadLine()!);
                            updateEngineerLevel(updateEngineer);
                            break;

                        //Hourly wage
                        case 4:
                            Console.WriteLine("enter the new name");
                            float newPay = float.Parse(Console.ReadLine()!);
                            updateEngineer.SalaryPerHour = newPay;
                            s_bl.BOEngineer.Update(updateEngineer);
                            break;

                        //Task selection
                        case 5:
                            BO.BOTask? bOTask = s_bl.BOTask.Read((int)updateEngineer.CurrentTaskId!);
                            if (bOTask!.status==BO.Status.Done)
                            {
                                //Searching for a new task
                                Console.WriteLine("Enter a new task number");
                                int? chooseNewTask = int.Parse(Console.ReadLine()!);
                                BO.BOTask? updateCurrentTask = s_bl.BOTask.Read((int)chooseNewTask);

                                //Checking the correctness of the input and the task
                                if (updateCurrentTask == null)
                                    throw new BO.BlDoesNotExistException($"No task found with this number {chooseNewTask}");
                                if (updateCurrentTask.status==BO.Status.Done)
                                    throw new BO.BlIncorrectInput($"The selected task {bOTask.NumOfTask} has already been completed");
                                if(updateCurrentTask.EsponsibleEngineerId != null)
                                    throw new BO.BlIncorrectInput($"The task {bOTask.NumOfTask} already has an engineer");

                                //Entry of the Mahdens task number
                                updateEngineer.CurrentTaskId = chooseNewTask;

                                //Update "engineer in charge" in the task
                                updateCurrentTask.EsponsibleEngineerId = updateEngineer.EngineerId;
                            }
                            else
                                throw new BO.BlIncorrectInput($"The task {bOTask.NumOfTask} is not over yet");

                            break;

                        default:
                            break;
                    }
                    break;
                
                //default
                default:
                    Console.WriteLine();
                    break;
            }
        }

        public static void getPropertysEntity<T>(T item)
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {            
                Console.Write($"{property.Name}: {property.GetValue(item)}");   
            }
        }

        public static void BOTaskEntity()
        {
            Console.WriteLine("what would you like to do?\r\n" +
                "1. Add a new task\r\n" +
                "2. Search for a task in the database\r\n" +
                "3. List of tasks\r\n" +
                "4. Delete a task\r\n" +
                "5. Update task details\r\n" +
                "6. Updating the start date for the task");
            int ChooseNumber = int.Parse(Console.ReadLine()!);

            switch (ChooseNumber)
            {   
                //Add 
                case 1:
                    //Console.WriteLine("Enter the following values in order:\r\n" + "1. Task number: ");
                    //int? taskNumber = int.Parse(Console.ReadLine()!);

                    Console.Write("\r\n2. The results of the mission: ");
                    string? missionResults = Console.ReadLine()!;

                    Console.Write("\r\n3. Duration of execution of the task: ");
                    int? Duration = int.Parse(Console.ReadLine()!);

                    Console.Write("\r\n4. Comments (if any): ");
                    string? comment = Console.ReadLine()!;

                    Console.Write("\r\n5. Description of the task: ");
                    string? Description = Console.ReadLine()!;

                    Console.Write("\r\n6. Engineer in charge: ");
                    int engineerChargeId = int.Parse(Console.ReadLine()!);

                    Console.Write("\r\n7. Difficulty level of the task");
                    BO.DifficultyLevelTask Difficulty;
                    Enum.TryParse<BO.DifficultyLevelTask>(Console.ReadLine(), out Difficulty);

                    Console.Write("\r\n8. Level of training of the engineer");
                    BO.EngineerLevels level;
                    Enum.TryParse<BO.EngineerLevels>(Console.ReadLine(), out level);

                    Console.Write("\r\n9. Status");
                    BO.Status statusTask;
                    Enum.TryParse<BO.Status>(Console.ReadLine(), out statusTask);

                    Console.Write("\r\n10. The date the task was created dd/MM/yyyy");
                    DateTime taskCreated = DateTime.Parse(Console.ReadLine()!);

                    Console.Write("\r\n11. Planned Start Date");
                    DateTime startDate = DateTime.Parse(Console.ReadLine()!);

                    Console.Write("\r\n12. Deadline");
                    DateTime Deadline = DateTime.Parse(Console.ReadLine()!);

                    Console.Write("\r\n13. Date of starting work on the task");
                    DateTime startingWork = DateTime.Parse(Console.ReadLine()!);

                    Console.Write("\r\n14. End date of work on the task");
                    DateTime endDate = DateTime.Parse(Console.ReadLine()!);

                    Console.Write("\r\n15. The nickname of the task");
                    string nickname = Console.ReadLine()!;

                    BO.BOTask newtask = new BO.BOTask()
                    {
                        NumOfTask = 0,
                        ResultOfTask = missionResults,
                        DurationOfExecution = (int)Duration,
                        Comment = comment,
                        DescriptionTask = Description,
                        EsponsibleEngineerId = engineerChargeId,
                        DifficultyTasc = Difficulty,
                        EngineerLevel = level,
                        status = statusTask,
                        TaskCreationDate = taskCreated,
                        PlannedDateForStartingWork = startDate,
                        DeadLine = Deadline,
                        StartWorkDate = startingWork,
                        EndOfActualWork = endDate,
                        Nickname = nickname,
                    };

                    s_bl.BOTask.Add(newtask);
                    break; 
                
                //read
                case 2:
                    Console.WriteLine("Enter the ID number of a wanted task");
                    int IdTask = int.Parse(Console.ReadLine()!);
                    Console.WriteLine($"the Wanted task is: {s_bl.BOEngineer.Read(IdTask)}");

                    break;
                
                //read list
                case 3:
                    Console.WriteLine("Insert desired filter\r\n1. Task difficulty levels\r\n2. Tasks that have not yet started\r\n3. Tasks that are not yet finished\r\n4. Without filter");
                    int chooseFilterTaskList = int.Parse(Console.ReadLine()!);
                    switch (chooseFilterTaskList)
                    {
                        //Task difficulty levels
                        case 1:
                            Console.WriteLine("Enter a difficulty level to filter\r\n1. Novice\r\n2. Competent\r\n3. Expert");
                            int filterLevel = int.Parse(Console.ReadLine()!);
                            switch (filterLevel)
                            {
                                //novise
                                case 1:
                                    List<BO.BOTask> noviceList = new List<BO.BOTask>(s_bl.BOTask.ReadList(task => task.DifficultyTasc == BO.DifficultyLevelTask.Novice ));
                                    foreach(var noviseTask in noviceList)
                                    {
                                        getPropertysEntity(noviseTask);
                                        Console.WriteLine();
                                    }
                                    break;

                                //Competent
                                case 2:
                                    List<BO.BOTask> competentList = new List<BO.BOTask>(s_bl.BOTask.ReadList(task => task.DifficultyTasc == BO.DifficultyLevelTask.Competent));
                                    foreach (var competentTask in competentList)
                                    {
                                        getPropertysEntity(competentTask);
                                        Console.WriteLine();
                                    }
                                    break;

                                //Expert
                                case 3:
                                    List<BO.BOTask> expertList = new List<BO.BOTask>(s_bl.BOTask.ReadList(task => task.DifficultyTasc == BO.DifficultyLevelTask.Expert));
                                    foreach (var experTask in expertList)
                                    {
                                        getPropertysEntity(experTask);
                                        Console.WriteLine();
                                    }
                                    break;

                                default:
                                    break;
                            }

                            break;

                        //Tasks that have not yet started
                        case 2:
                            List<BO.BOTask> notStartList = new List<BO.BOTask>(s_bl.BOTask.ReadList(task => task.StartWorkDate < DateTime.Today || task.StartWorkDate == null));
                            foreach(var notStartTask in notStartList)
                            {
                                getPropertysEntity(notStartTask);
                                Console.WriteLine();
                            }
                            break;

                        //Tasks that are not yet finished
                        case 3:
                            List<BO.BOTask> notEndList = new List<BO.BOTask>(s_bl.BOTask.ReadList(task => task.EndOfActualWork < DateTime.Today || task.EndOfActualWork==null));
                            foreach (var notEndTask in notEndList)
                            {
                                getPropertysEntity(notEndTask);
                                Console.WriteLine();
                            }
                            break;

                        //Without filter
                        case 4:
                            List<BO.BOTask> notFilterList = new List<BO.BOTask>(s_bl.BOTask.ReadList());
                            foreach (var notFilterTask in notFilterList)
                            {
                                getPropertysEntity(notFilterTask);
                                Console.WriteLine();
                            }
                            break;
                        
                        default:
                            break;
                    }
                    break;
                
                //delete
                case 4:
                    Console.WriteLine("Enter a task ID number to delete");
                    int taskDeleteId = int.Parse(Console.ReadLine()!);
                    s_bl.BOTask.Remove(taskDeleteId);
                    break; 
                
                //update
                case 5:
                    Console.WriteLine("Enter a task ID number to update");
                    int updateTaskId = int.Parse(Console.ReadLine()!);
                    BO.BOTask updateTask = s_bl.BOTask.Read(updateTaskId)!;
                    if (updateTask == null) 
                        throw new BO.BlDoesNotExistException($"There is no assignment with the number {updateTaskId}");

                    Console.WriteLine("Which feature would you like to update?\r\n" +
                        "1. Nikname\r\n" +
                        "2. Description\r\n" +
                        "3. Status\r\n" +
                        "4. Results");
                    int chooseTaskPropertyToUpdate = int.Parse(Console.ReadLine()!);
                    switch (chooseTaskPropertyToUpdate)
                    {
                        //Nikname
                        case 1:
                            Console.WriteLine("Enter the new nickname for the task");
                            string newNikname = Console.ReadLine()!;
                            updateTask.Nickname = newNikname;
                            s_bl.BOTask.Update(updateTask);                                                  
                            break;

                        //Description
                        case 2:
                            Console.WriteLine("Enter the new description for the task");
                            string newDescription = Console.ReadLine()!;
                            updateTask.DescriptionTask = newDescription;
                            s_bl.BOTask.Update(updateTask);

                            break;

                        //Status
                        case 3:
                            if (updateTask.status == BO.Status.Done)
                                throw new BO.BlIncorrectInput($"The selected task {updateTask.NumOfTask} has already been completed");
                            
                            Console.WriteLine("1. Unscheduled,\r\n" +
                                "2. Scheduled,\r\n" +
                                "3. Started,\r\n" +
                                "4. Done");
                            int statusChooseUpdate = int.Parse(Console.ReadLine()!);
                            switch (statusChooseUpdate)
                            {
                                //Scheduled
                                case 1:
                                    if(updateTask.status <= Status.Unscheduled || updateTask.status==null)
                                        updateTask.status = Status.Unscheduled;
                                    break;

                                //Scheduled
                                case 2:
                                    if(updateTask.status <= Status.Scheduled)
                                        updateTask.status = Status.Scheduled;
                                    break;

                                //Started
                                case 3:
                                    if(updateTask.status <= Status.Started)
                                        updateTask.status = Status.Started; 
                                    break;

                                //Done
                                case 4:
                                    if(updateTask.status <= Status.Done)
                                        updateTask.status = Status.Done;
                                    break;

                                default:
                                    break;
                            }

                            s_bl.BOTask.Update(updateTask);
                            break;

                        //Results
                        case 4:
                            Console.WriteLine("Enter the new results for the task");
                            string newResults = Console.ReadLine()!;
                            updateTask.ResultOfTask = newResults;
                            s_bl.BOTask.Update(updateTask);

                            break;

                        default:
                            break;
                    }
                    break;
                
                //update start date
                case 6:
                    Console.WriteLine("Enter a task ID");
                    int taskId = int.Parse(Console.ReadLine()!);

                    Console.WriteLine("Enter a start date dd/MM/yyyy");
                    DateTime sartDateUpdate = DateTime.Parse(Console.ReadLine()!);

                    s_bl.BOTask.UpdateStartDate(taskId, sartDateUpdate);
                    break;

                default:
                    break;
            }
        }


        public static void Main(string[] args)
        {
            try
            {
                Console.Write("Would you like to create Initial data? (Y/N)");
                string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
                if (ans == "Y")
                    DalTest.Initialization.Do();

                posting:
                Console.WriteLine("Enter a number to select an entity\n1.'Engineer' entity\n2. 'Task' entity\n0. to exit");
            
                string? Choose = Console.ReadLine();
                int intChoose = int.Parse(Choose!);

                if (!(int.TryParse(Choose, out intChoose)))
                {
                    Console.WriteLine("You entered a wrong number!\nEnter again");
                    goto posting;
                }


                while (intChoose >=0||intChoose<=2)
                {
                    switch (intChoose)
                    {
                        case 0:
                            Console.WriteLine("Good bye\a ");
                            break;

                        //engineer entity
                        case 1:
                            BOEngineerEntity();
                            break;

                        //task entity
                        case 2:
                            BOTaskEntity();
                            break;
                    }

                    if (intChoose == 0)
                        return;
                
                    Console.WriteLine("Enter a number to select an entity\n1.'Engineer' entity\n2. 'Task' entity\n0. to exit");
                    Choose = Console.ReadLine();
                    intChoose = int.Parse(Choose!);
                }
            }

            catch (BO.BlDoesNotExistException BoExeption)
            {
                Console.WriteLine("\a" + BoExeption);
            }
            catch (BO.BlAlreadyExistsException BoExeption)
            {
                Console.WriteLine("\a" + BoExeption);
            }
            catch (BO.BlDeletionImpossibleException BoExeption)
            {
                Console.WriteLine("\a" + BoExeption);
            }
            catch (BO.BlIncorrectInput BoExeption)
            {
                Console.WriteLine("\a" + BoExeption);
            }
            catch(BO.BlThereIsNoStartDateForPreviousTasks BoExeption)
            {
                Console.WriteLine("\a" + BoExeption);
            }
            catch (BO.BlTheTaskStartDateDoesNotMatch BoExeption)
            {
                Console.WriteLine("\a"+BoExeption);
            }
        }
    }
}


using DalApi;
using DO;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace DalTest
{
    public class Program
    {
        //static readonly IDal s_dal = new DalList();
        static readonly IDal s_dal = new DalXml();


        // A private method for all entity CRUD methods
        private static void Crud<T>(T item) where T : new()
        {
            //Initialization.Do(s_dalTask, s_engineer, s_dependence);

            Console.WriteLine("Enter a number to select a method,\r\n0. Exit to main menu.\r\n1. Adding an object.\r\n2. Object display.\r\n3. List view.\r\n4. Update an existing object.\r\n5. Object deletion.");
        ChooseMethod:
            string? stringChooseMethod = Console.ReadLine();
            int chooseMethod = int.Parse(stringChooseMethod);
            if (!(int.TryParse(stringChooseMethod, out chooseMethod)))
            {
                Console.WriteLine("You entered a wrong number!\nEnter again");
                goto ChooseMethod;
            }

            switch (chooseMethod)
            {
                //creat
                case 1:
                    if (item is Engineer)
                    {
                        Console.WriteLine("Enter an engineer's ID number");
                        int idEngineer = int.Parse(Console.ReadLine());

                        Console.Write("Enter the name of the engineer: ");
                        string? nameOfEngineer = Console.ReadLine();

                        Console.Write("Enter the email of the engineer: ");
                        string? emailOfDependence = Console.ReadLine();

                        EngineerLevels engineerLevels;
                        Console.WriteLine("Enter the engineer's training level");
                        Enum.TryParse<EngineerLevels>(Console.ReadLine(), out engineerLevels);

                        Console.WriteLine("Enter 'true' or 'false' if the engineer can be deleted or not");
                        bool deleteTask = bool.Parse(Console.ReadLine());

                        Engineer engineer = new(idEngineer, nameOfEngineer, emailOfDependence, engineerLevels, null, deleteTask);
                        Console.WriteLine("The identity number of the new engineer is:" + s_dal.Engineer.Create(engineer));
                    }

                    if (item is DO.Task)
                    {
                        Console.WriteLine("Enter the mission objectives");
                        string? missionObjective = Console.ReadLine();

                        Console.WriteLine("Enter the duration of work on the task in days");
                        int DurationOfWork = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter the task description");
                        string taskDescription = Console.ReadLine();

                        Console.WriteLine("Enter the ID number of the engineer in charge");
                        int engineerInCharge = int.Parse(Console.ReadLine());

                        DifficultyLevelTask difficultyLevelTask;
                        Enum.TryParse<DifficultyLevelTask>(Console.ReadLine(), out difficultyLevelTask);
                        EngineerLevels engineerLevels;
                        Enum.TryParse<EngineerLevels>(Console.ReadLine(), out engineerLevels);

                        Console.WriteLine("Enter the task creation date");
                        DateTime dateCreatTask;
                        DateTime.TryParse(Console.ReadLine(), out dateCreatTask);

                        Console.WriteLine("Enter an estimated start date of work on the task");
                        DateTime startDateWork;
                        DateTime.TryParse(Console.ReadLine(), out startDateWork);

                        Console.WriteLine("Enter the end date of the task");
                        DateTime.TryParse(Console.ReadLine(), out DateTime deadLine);

                        Console.WriteLine("Enter the actual start date on the task");
                        DateTime.TryParse(Console.ReadLine(), out DateTime startDateTask);

                        Console.WriteLine("Enter the actual task end date");
                        DateTime.TryParse(Console.ReadLine(), out DateTime actualTaskEndDate);

                        Console.WriteLine("Enter the task alias");
                        string alias = Console.ReadLine();

                        Console.WriteLine("Enter 'true' or 'false' if the task can be deleted or not");
                        bool deleteTask = bool.Parse(Console.ReadLine());

                        DO.Task task = new(0, missionObjective, DurationOfWork, "", taskDescription, engineerInCharge,
                            difficultyLevelTask, engineerLevels, dateCreatTask, startDateWork, deadLine, startDateTask,
                            actualTaskEndDate, false, alias, deleteTask);
                        Console.WriteLine("The new task's ID is: " + s_dal.Task.Create(task));
                    }

                    if (item is Dependency)
                    {
                        Console.WriteLine("Enter a task number dependent");
                        int numberDependent = int.Parse(Console.ReadLine());

                        Console.WriteLine("Insert a number of previous task dependent");
                        int previousTaskDependent = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter 'true' or 'false' if the task can be deleted or not");
                        bool deleteTask = bool.Parse(Console.ReadLine());

                        Dependency task = new(0, int.Parse(Console.ReadLine()), previousTaskDependent, deleteTask);
                        Console.WriteLine("The new dependence's ID is: " + s_dal.Dependency.Create(task));
                    }

                    Console.WriteLine("Select an action to continue.\r\nPress 0 for the methods menu on the same entity");
                    int? selction = int.Parse(Console.ReadLine());

                    if (selction == 0)
                    {
                        Console.WriteLine("Enter a number to select a method,\r\n0. Exit to main menu.\r\n1. Adding an object.\r\n2. Object display.\r\n3. List view.\r\n4. Update an existing object.\r\n5. Object deletion.");
                        goto ChooseMethod;
                    }
                    break;

                //Read
                case 2:
                    if (item is Engineer)
                    {
                        Console.WriteLine("Enter the ID number of the requested engineer");
                        int ID = int.Parse(Console.ReadLine());
                        Engineer? flag = s_dal.Engineer.Read(ID);
                        if (flag == null)
                            Console.WriteLine("No wanted engineer found");
                        else
                            Console.WriteLine(flag);
                    }

                    if (item is DO.Task)
                    {
                        Console.WriteLine("Enter the ID number of the requested task");
                        int ID = int.Parse(Console.ReadLine());
                        DO.Task? flag = s_dal.Task.Read(ID);
                        if (flag == null)
                            Console.WriteLine("No wanted task found");
                        else
                            Console.WriteLine(flag);
                    }

                    if (item is Dependency)
                    {
                        Console.WriteLine("Enter the ID number of the requested pending task");
                        int ID = int.Parse(Console.ReadLine());
                        Dependency? flag = s_dal.Dependency.Read(ID);
                        if (flag == null)
                            Console.WriteLine("No wanted pending task found");
                        else
                            Console.WriteLine(flag);
                    }
                    break;

                //ReadAll
                case 3:
                    if (item is Engineer)
                    {
                        List<Engineer> engineerList = new(s_dal.Engineer.ReadAll());
                        foreach (var engineer in engineerList)
                        {
                            Console.Write(engineer + " ");
                        }
                    }

                    if (item is DO.Task)
                    {
                        List<DO.Task> taskList = new(s_dal.Task.ReadAll());
                        foreach (var task in taskList)
                        {
                            Console.Write(task + " ");
                        }
                    }

                    if (item is Dependency)
                    {
                        List<Dependency> dependenceList = new(s_dal.Dependency.ReadAll());
                        foreach (var dependence in dependenceList)
                        {
                            Console.Write(dependence + " ");
                        }
                    }
                    break;

                //Update
                case 4:
                    GenericUpdate(item);
                    break;

                //Delete
                case 5:
                    Console.WriteLine("Enter an ID number to delete");
                    int id = int.Parse(Console.ReadLine());
                    GenericDelete(item, id);
                    break;

                default:
                    break;
            }
        }

        private static void GenericDelete<T>(T item, int id) where T : new()
        {
            if (item is Engineer)
            {
                s_dal.Engineer.Delete(id);
            }

            if (item is DO.Task)
            {
                s_dal.Task.Delete(id);
            }

            if (item is Dependency)
            {
                s_dal.Dependency.Delete(id);
            }
        }

        private static void GenericUpdate<T>(T item) where T : new()
        {
            if (item is Engineer)
            {
                Console.WriteLine("Enter an engineer ID number");
                int ID = int.Parse(Console.ReadLine());
                Engineer? engineer = s_dal.Engineer.Read(ID);
                if (engineer == null)
                    throw new DalDoesNotExistException("engineer not found");

                Console.WriteLine("The wanted engineer is: " + engineer);
                Console.WriteLine("What do you want to change?");
                Console.WriteLine("1. Updating the identity number.\r\n2. Update engineer name.\r\n3. Update engineer email.\r\n4. Updating the level of the engineer's training.\r\n0. Return to the entity update menu");

            SelectingUpdateEngineer:
                if (!(int.TryParse(Console.ReadLine(), out int selectingUpdate)))
                {
                    Console.WriteLine("You entered a wrong number!\nEnter again");
                    goto SelectingUpdateEngineer;
                }
                Engineer newEngineer;
                switch (selectingUpdate)
                {
                    case 0:
                        goto SelectingUpdateEngineer;
                        break;
                    // Update ID number
                    case 1:
                        Console.WriteLine("Enter a new ID number");
                        int newID = int.Parse(Console.ReadLine());
                        newEngineer = engineer with { EngineerId = newID };
                        s_dal.Engineer.Update(newEngineer);
                        break;
                    // Update engineer name
                    case 2:
                        Console.WriteLine("Insert a new engineer there");
                        string newName = Console.ReadLine();
                        newEngineer = engineer with { EngineerName = newName };
                        s_dal.Engineer.Update(newEngineer);
                        break;
                    // Update engineer email
                    case 3:
                        Console.WriteLine("Enter a new email of the engineer");
                        string newEmail = Console.ReadLine();
                        newEngineer = engineer with { EngineerEmail = newEmail };
                        s_dal.Engineer.Update(newEngineer);
                        break;
                    //Update the engineer's training level
                    case 4:
                        EngineerLevels engineerLevels;
                        Console.WriteLine("Enter a new training level");
                        Enum.TryParse<EngineerLevels>(Console.ReadLine(), out engineerLevels);
                        newEngineer = engineer with { LeverOfEngineer = engineerLevels };
                        s_dal.Engineer.Update(newEngineer);
                        break;
                }
            }

            if (item is DO.Task)
            {
                Console.WriteLine("Enter an Task ID number");
                int ID = int.Parse(Console.ReadLine());
                DO.Task task = s_dal.Task.Read(ID);
                Console.WriteLine("The wanted Task is: " + task);
                Console.WriteLine("What do you want to change?");
                Console.WriteLine("1. Updating mission objectives.\r\n2. Updating the duration of the task\r\n3. Update notes on the task\r\n4. Update task description.\r\n5. Update responsible engineer.\r\n6. Updating the difficulty level of the task.\r\n7. Updating the level of the engineer's training.\r\n8. Updating the creation date of the task.\r\n9. Updating the planned date for the start of execution.\r\n10. Deadlien update.\r\n11. Updating the date of the beginning of the execution of the task.\r\n12. Updating the actual end date of the task.\r\n13. Updating the task alias.\r\n0. Return to the entity update menu");

            SelectingUpdateTask:
                if (!(int.TryParse(Console.ReadLine(), out int selectingUpdateTask)))
                {
                    Console.WriteLine("You entered a wrong number!\nEnter again");
                    goto SelectingUpdateTask;
                }
                DO.Task newTask;
                switch (selectingUpdateTask)
                {
                    case 0:
                        goto SelectingUpdateTask;
                        break;

                    //Updating mission objectives
                    case 1:
                        Console.WriteLine("Insert new mission objectives");
                        string missionObjectives = Console.ReadLine();
                        newTask = task with { ResultOfTask = missionObjectives };
                        s_dal.Task.Update(newTask);
                        break;

                    //Updating the duration of the task
                    case 2:
                        Console.WriteLine("Enter the new execution duration in days");
                        int durationTask = int.Parse(Console.ReadLine());
                        newTask = task with { DurationOfExecution = durationTask };
                        s_dal.Task.Update(newTask);
                        break;

                    //Update notes on the task
                    case 3:
                        Console.WriteLine("Enter new notes on the task");
                        string newNotes = Console.ReadLine();
                        newTask = task with { Comment = newNotes };
                        s_dal.Task.Update(newTask);
                        break;


                    //Update task description
                    case 4:
                        Console.WriteLine("Enter new task description");
                        string newDescription = Console.ReadLine();
                        newTask = task with { DescriptionTask = newDescription };
                        s_dal.Task.Update(newTask);
                        break;

                    //Update responsible engineer
                    case 5:
                        Console.WriteLine("Enter new ID of the responsible engineer");
                        int responsibleEngineer = int.Parse(Console.ReadLine());
                        newTask = task with { EsponsibleEngineerId = responsibleEngineer };
                        s_dal.Task.Update(newTask);
                        break;

                    //Updating the difficulty level of the task
                    case 6:
                        Console.WriteLine("Enter new difficulty level of the task");
                        Enum.TryParse<DifficultyLevelTask>(Console.ReadLine(), out DifficultyLevelTask difficultyLevelTask);
                        newTask = task with { DifficultyTasc = difficultyLevelTask };
                        s_dal.Task.Update(newTask);
                        break;

                    //Updating the level of the engineer's training
                    case 7:
                        Console.WriteLine("Enter new level of the engineer's training");
                        Enum.TryParse<EngineerLevels>(Console.ReadLine(), out EngineerLevels engineerLevels);
                        newTask = task with { EngineerLevel = engineerLevels };
                        s_dal.Task.Update(newTask);
                        break;

                    //Updating the creation date of the task
                    case 8:
                        Console.WriteLine("Enter new creation date of the task");
                        DateTime.TryParse(Console.ReadLine(), out DateTime creationDateTask);
                        newTask = task with { TaskCreationDate = creationDateTask };
                        s_dal.Task.Update(newTask);
                        break;

                    //Updating the planned date for the start of execution
                    case 9:
                        Console.WriteLine("Enter new planned date for the start of execution");
                        DateTime.TryParse(Console.ReadLine(), out DateTime dateStartExecution);
                        newTask = task with { PlannedDateForStartingWork = dateStartExecution };
                        s_dal.Task.Update(newTask);
                        break;

                    //Deadlien update
                    case 10:
                        Console.WriteLine("Enter new Deadlien");
                        DateTime.TryParse(Console.ReadLine(), out DateTime Deadlien);
                        newTask = task with { DeadLine = Deadlien };
                        s_dal.Task.Update(newTask);
                        break;

                    // Updating the date of the beginning of the execution of the task
                    case 11:
                        Console.WriteLine("Enter new date of the beginning of the execution of the task");
                        DateTime.TryParse(Console.ReadLine(), out DateTime dateBeginningExecution);
                        newTask = task with { StartWorkDate = dateBeginningExecution };
                        s_dal.Task.Update(newTask);
                        break;

                    // Updating the actual end date of the task
                    case 12:
                        Console.WriteLine("Enter new actual end date of the task");
                        DateTime.TryParse(Console.ReadLine(), out DateTime actualEndDate);
                        newTask = task with { EndOfActualWork = actualEndDate };
                        s_dal.Task.Update(newTask);
                        break;

                    // Updating the task alias
                    case 13:
                        Console.WriteLine("Enter new task alias");
                        string alias = Console.ReadLine();
                        newTask = task with { Nickname = alias };
                        s_dal.Task.Update(newTask);
                        break;
                }
            }

            if (item is Dependency)
            {
                Console.WriteLine("Enter an ID number of a task dependent");
                int idTaskDependence = int.Parse(Console.ReadLine());
                Dependency dependence = s_dal.Dependency.Read(idTaskDependence);
                Console.WriteLine("The wanted Task is: " + dependence);
                Console.WriteLine("What do you want to change?");
                Console.WriteLine("1. Update the number of a task that depends\r\n2. Previous dependent task.\r\n0. Return to the entity update menu");

            SelectingUpdateTaskDependence:
                if (!(int.TryParse(Console.ReadLine(), out int selectingUpdateTaskDependence)))
                {
                    Console.WriteLine("You entered a wrong number!\nEnter again");
                    goto SelectingUpdateTaskDependence;
                }
                Dependency newTaskDependence;
                switch (selectingUpdateTaskDependence)
                {
                    case 0:
                        goto SelectingUpdateTaskDependence;
                        break;
                    // Update the number of a task that depends
                    case 1:
                        Console.WriteLine("Enter a dependent task number");
                        int newTaskNumber = int.Parse(Console.ReadLine());
                        newTaskDependence = dependence with { TaskNumberDepends = newTaskNumber };
                        s_dal.Dependency.Update(newTaskDependence);
                        break;

                    // Previous dependent task
                    case 2:
                        Console.WriteLine("Enter the number of previous dependent task");
                        int previousDependent = int.Parse(Console.ReadLine());
                        newTaskDependence = dependence with { PreviousTaskDepends = previousDependent };
                        s_dal.Dependency.Update(newTaskDependence);
                        break;
                }
            }
        }

        public static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

        Start:
            try
            {
                int EntitySelection;

            MainMenu:
                Console.WriteLine("Enter a number to select an entity\n1.'Engineer' entity\n2. 'Task' entity\n3. 'Dependence' entity\n4.Initialization data\n0. to exit");

            posting:
                string? stringChooseNumber = Console.ReadLine();

                EntitySelection = int.Parse(stringChooseNumber);
                if (!(int.TryParse(stringChooseNumber, out EntitySelection)))
                {
                    Console.WriteLine("You entered a wrong number!\nEnter again");
                    goto posting;
                }


                switch (EntitySelection)
                {
                    //Exit
                    case 0:
                        Console.WriteLine("Goodbye\a");
                        break;

                    // Engineer
                    case 1:
                        Engineer engineer = new();
                        Crud(engineer);
                        break;

                    //Task
                    case 2:
                        DO.Task task = new();
                        Crud(task);
                        break;

                    // Dependency
                    case 3:
                        Dependency dependence = new();
                        Crud(dependence);
                        break;

                    //Initialization
                    case 4:
                        Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
                        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
                        if (ans == "Y") //stage 3
                        {
                            
                            try {Initialization.Do(s_dal); }
                            catch (Exception ex)
                            { Console.WriteLine(ex); }
                        }

                        break;
                    // defolte
                    default:
                        Console.WriteLine("You entered an incorrect value");
                        break;
                }

                if (EntitySelection == 0)
                    return;

                goto MainMenu;
            }
            catch (DalDoesNotExistException ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Start again");
            }

            catch (DalAlreadyExistsException ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Start again");
            }

            catch (DalDeletionImpossibleException ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Start again");
            }
            finally
            {
            }
            goto Start;
        }
    }
}
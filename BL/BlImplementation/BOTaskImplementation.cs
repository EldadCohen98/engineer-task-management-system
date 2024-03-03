using BlApi;
using BO;
using DalApi;
using DO;
using System.Collections.Generic;
using System;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Channels;

namespace BlImplementation;

internal class BOTaskImplementation : IBOTask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    
    public static Status? getStatus(DO.Task doTask)
    {
        if (doTask.PlannedDateForStartingWork == null)
            //bOTask.status =
            return Status.Unscheduled;
        else
        {
            if (doTask.PlannedDateForStartingWork != null && DateTime.Now > doTask.StartWorkDate)
                //bOTask.status = 
                    return Status.Scheduled;
            else
            {
                if (doTask.StartWorkDate != null && doTask.EndOfActualWork == null)
                    //bOTask.status =
                    return Status.Started;
                else
                {
                    if (doTask.EndOfActualWork != null && doTask.EndOfActualWork < DateTime.Now)
                        //bOTask.status =
                        return Status.Done;
                }
            }
        }
        return null;

    }

    public void Add(BOTask bOTask)
    {
        if (bOTask.NumOfTask < 0)
            throw new BlIncorrectInput($"A task ID number cannot be negative");
        if (bOTask.Nickname == null || bOTask.Nickname.Length == 0)
            throw new BlIncorrectInput($"The task does not have an alias");

        DO.Task? doTask = new DO.Task(0,
                                   bOTask.ResultOfTask!,
                                   bOTask.DurationOfExecution,
                                   bOTask.Comment!,
                                   bOTask.DescriptionTask!,
                                   bOTask.EsponsibleEngineerId,
                                  (DO.DifficultyLevelTask?)bOTask.DifficultyTasc,
                                  (DO.EngineerLevels?)bOTask.EngineerLevel,
                                   bOTask.TaskCreationDate,
                                   bOTask.PlannedDateForStartingWork,
                                   bOTask.DeadLine,
                                   bOTask.StartWorkDate,
                                   bOTask.EndOfActualWork,
                                   false,
                                   bOTask.Nickname,
                                   null);

        List<DO.Dependency?> DoListDependency = new List<DO.Dependency?>(_dal.Dependency.ReadAll());

        //Deleting all the tasks that the task depends on
        foreach (var DoDependency in DoListDependency)
        {
            if (DoDependency!.TaskNumberDepends==bOTask.NumOfTask)
            {
                DoListDependency.Remove(DoDependency);
            }
        }

        //Reception of new pending tasks
        Console.WriteLine("Enter preliminary tasks\r\n0 - end of reception");
        int check = int.Parse(Console.ReadLine()!);

        while (check>0)
        {
            Console.WriteLine("Enter the number, description, status and nickname of a preliminary task");
            int dependenceTAskId = int.Parse(Console.ReadLine()!);
            string DescriptionTask = Console.ReadLine()!;
            BO.Status statusDependencyTask;
            Enum.TryParse<BO.Status>(Console.ReadLine()!, out statusDependencyTask);
            string nicknameDependencyTask = Console.ReadLine()!;

            TaskInTheList dependencytask = new TaskInTheList()
            {
                NumOfTask = dependenceTAskId,
                DescriptionTask = DescriptionTask,
                status = statusDependencyTask,
                Nickname = nicknameDependencyTask,
            };
            bOTask.TasksListDependence!.Add(dependencytask);
            Console.WriteLine("Enter preliminary tasks\r\n0 - end of reception");
            check = int.Parse(Console.ReadLine()!);
        }

        //Update of data layer dependencies
        foreach (var BoDependency in bOTask.TasksListDependence!)
        {
            DO.Dependency dOdependency = new DO.Dependency(BoDependency.NumOfTask, (int)bOTask.NumOfTask!, 0,null);
            _dal.Dependency.Create(dOdependency);
        }

        try
        {
            _dal.Task.Create(doTask);
        }
        catch(DO.DalAlreadyExistsException)
        {
            throw new BlAlreadyExistsException($"");
        }
    }

    public BOTask? Read(int NumberOfTask)
    {
        DO.Task? doTask = new DO.Task();
        doTask = _dal.Task.Read(NumberOfTask);

        if (doTask == null) 
            if (doTask == null)
                throw new BlDoesNotExistException($"Task with number = {NumberOfTask} dose not exists");

        BO.BOTask bOTask = new BOTask()
        {
            NumOfTask = doTask.NumOfTask,
            ResultOfTask = doTask.ResultOfTask,
            DurationOfExecution = doTask.DurationOfExecution,
            Comment = doTask.Comment,
            DescriptionTask = doTask.DescriptionTask,
            EsponsibleEngineerId = doTask.EsponsibleEngineerId,
            DifficultyTasc = (BO.DifficultyLevelTask?)doTask.DifficultyTasc,
            EngineerLevel = (BO.EngineerLevels?)doTask.EngineerLevel,
            status = getStatus(doTask),
            TaskCreationDate = doTask.TaskCreationDate,
            PlannedDateForStartingWork = doTask.PlannedDateForStartingWork,
            DeadLine = doTask.DeadLine,
            StartWorkDate = doTask.StartWorkDate,
            EndOfActualWork = doTask.EndOfActualWork,
            Nickname = doTask.Nickname,
        };

        ///From the DAL dependencies we will look for a task whose number is equal to the current task
        //If I found the same dependency, go to the field "depends on.."
        //With the number of "depends on.." we will go to the tasks and from there we will copy the attributes to the list of dependencies of a logical task
        List<DO.Dependency?> DoListDependency = new List<DO.Dependency?>(_dal.Dependency.ReadAll());

        foreach (var DoDependency in DoListDependency)
        {
            if (DoDependency!.DependencyIdNumber == bOTask.NumOfTask)
            {
                DO.Task? dependenceOn = _dal.Task.Read(DoDependency.TaskNumberDepends);
                BO.TaskInTheList taskInTheList = new BO.TaskInTheList()
                {
                    NumOfTask = dependenceOn!.NumOfTask,
                    DescriptionTask = dependenceOn.DescriptionTask,
                    status = getStatus(dependenceOn),
                    Nickname = dependenceOn.Nickname,
                };
                bOTask.TasksListDependence!.Add(taskInTheList);
            }
        }

        return bOTask;
    }

    public IEnumerable<BO.BOTask> ReadList(Func<BO.BOTask, bool>? filter = null)
    {
        //Creating a collection of BO task
        IEnumerable<BO.BOTask> taskList = new List<BO.BOTask>();

        //Going over each DO engineer and copying the data to the BO task
        foreach (var dotask in _dal.Task.ReadAll())
        {
            BO.BOTask boTask = new BO.BOTask
            {
                NumOfTask = dotask!.NumOfTask,
                ResultOfTask= dotask.ResultOfTask,
                DurationOfExecution = dotask.DurationOfExecution,
                Comment = dotask.Comment,
                DescriptionTask = dotask.DescriptionTask,
                EsponsibleEngineerId = dotask.EsponsibleEngineerId,
                DifficultyTasc = (BO.DifficultyLevelTask?)dotask.DifficultyTasc,
                EngineerLevel = (BO.EngineerLevels?)dotask.EngineerLevel,
                TaskCreationDate = dotask.TaskCreationDate,
                PlannedDateForStartingWork = dotask.PlannedDateForStartingWork,
                DeadLine = dotask.DeadLine,
                StartWorkDate = dotask.StartWorkDate,
                EndOfActualWork = dotask.EndOfActualWork,
                Nickname = dotask.Nickname
            };

            //Added a BO engineer to the collection
            taskList.Append(boTask);
        }

        //Returning the collection when the condition is empty
        if (filter == null)
        {
            return taskList;
        }

        //The return of the engineers who are true to the condition
        else
        {
            return taskList.Where(item => filter(item)); ;
        }
    }

    public void Remove(int NumberOfTask)
    {
        DO.Task? doTask = new DO.Task();
        doTask = _dal.Task.Read(NumberOfTask);

        if (doTask is not null)
        {
            BO.BOTask boTask = new BO.BOTask
            {
                NumOfTask = doTask.NumOfTask,
                ResultOfTask = doTask.ResultOfTask,
                DurationOfExecution = doTask.DurationOfExecution,
                Comment = doTask.Comment,
                DescriptionTask = doTask.DescriptionTask,
                EsponsibleEngineerId = doTask.EsponsibleEngineerId,
                DifficultyTasc = (BO.DifficultyLevelTask?)doTask.DifficultyTasc,
                EngineerLevel = (BO.EngineerLevels?)doTask.EngineerLevel,
                status = getStatus(doTask),
                TaskCreationDate = doTask.TaskCreationDate,
                PlannedDateForStartingWork = doTask.PlannedDateForStartingWork,
                DeadLine = doTask.DeadLine,
                StartWorkDate = doTask.StartWorkDate,
                EndOfActualWork = doTask.EndOfActualWork,
                Nickname = doTask.Nickname
            };

            if (boTask.TasksListDependence!.Count != 0)
            {
                try
                {
                    _dal.Task.Delete(NumberOfTask);
                }
                catch (DO.DalDoesNotExistException)
                {
                    throw new BO.BlDoesNotExistException($"Task with number = {NumberOfTask} does not exist");
                }
                catch (DO.DalDeletionImpossibleException)
                {
                    throw new BO.BlDeletionImpossibleException($"Task with number = {NumberOfTask} cannot be deleted");
                }
            }

            else
                throw new BO.BlDeletionImpossibleException($"Task with number = {NumberOfTask} cannot be deleted");
        }
        else
            throw new BO.BlDoesNotExistException($"Task with number = {NumberOfTask} does not exist");
    }

    public void Update(BOTask bOTask)
    {
        if (bOTask.NumOfTask < 0)
            throw new BlIncorrectInput($"A task ID number cannot be negative");
        if (bOTask.Nickname == null || bOTask.Nickname.Length == 0)
            throw new BlIncorrectInput($"The task does not have an alias");

        DO.Task? doTask = new DO.Task((int)bOTask.NumOfTask!,
                                      bOTask.ResultOfTask!,
                                      bOTask.DurationOfExecution,
                                      bOTask.Comment!,
                                      DescriptionTask: bOTask.DescriptionTask!,
                                      bOTask.EsponsibleEngineerId,
                                     (DO.DifficultyLevelTask?)bOTask.DifficultyTasc,
                                     (DO.EngineerLevels?)bOTask.EngineerLevel,
                                      bOTask.TaskCreationDate,
                                      bOTask.PlannedDateForStartingWork,
                                      bOTask.DeadLine,
                                      bOTask.StartWorkDate,
                                      bOTask.EndOfActualWork,
                                      false,
                                      bOTask.Nickname,
                                      null);
        try
        {
            _dal.Task.Update(doTask);
        }
        catch (DO.DalDoesNotExistException)
        {
            throw new BO.BlDoesNotExistException($"Task with number = {doTask.NumOfTask} does not exist");
        }
    }

    public void UpdateStartDate(int NumberOfTask, DateTime plannedStart)
    {
        BO.BOTask bOTask = new BO.BOTask();
        bOTask = Read(NumberOfTask)!;
        foreach (var task in bOTask.TasksListDependence!)
        {
            BO.BOTask BOdepndencyTask = new BO.BOTask();
            BOdepndencyTask = Read(task.NumOfTask)!;

            if (BOdepndencyTask.PlannedDateForStartingWork == null)
                throw new BO.BlThereIsNoStartDateForPreviousTasks($"There is no start date for all the tasks prior to task number {task.NumOfTask} ");
            if (plannedStart < BOdepndencyTask.EndOfActualWork)
                throw new BO.BlTheTaskStartDateDoesNotMatch("The task's start date is earlier than the end dates of all its predecessor tasks");

            try
            {
                bOTask.PlannedDateForStartingWork = plannedStart;
                Update(bOTask);
            }
            catch (DO.DalDoesNotExistException)
            {
                throw new BO.BlDoesNotExistException($"Task with number = {bOTask.NumOfTask} does not exist");
            }
        } 
    }   
}

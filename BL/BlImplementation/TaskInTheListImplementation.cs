using BlApi;
using BO;

namespace BlImplementation;

internal class TaskInTheListImplementation : ITaskInTheList
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
    public List<BO.TaskInTheList> TasksListDependence(BO.BOTask task)
    {
        List<DO.Dependency?> DoListDependency = new List<DO.Dependency?>(_dal.Dependency.ReadAll());

        List<TaskInTheList?> tasksLists = new List<TaskInTheList?>();

        foreach (var DoDependency in DoListDependency)
        {
            if (DoDependency!.DependencyIdNumber == task.NumOfTask)
            {
                DO.Task? dependenceOn = _dal.Task.Read(DoDependency.TaskNumberDepends);

                BO.TaskInTheList? inTheList = new BO.TaskInTheList()
                {
                    NumOfTask = dependenceOn!.NumOfTask!,
                    DescriptionTask = dependenceOn.DescriptionTask,
                    status = getStatus(dependenceOn),
                    Nickname = dependenceOn.Nickname
                };

                tasksLists.Add(inTheList);
            }
        }
        return tasksLists!;
    }
}

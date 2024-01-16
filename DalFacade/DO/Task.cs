
using System;
using static DO.DifficultyLevelTask;

namespace DO;

/// <summary>
/// The Task entity is an entity that represents a task with all its attributes
/// </summary>
/// <param name="NumOfTask"></param> number of the task
/// <param name="ResultOfTask"></param> Description of mission results
/// <param name="DurationOfExecution"></param> The duration of the task
/// <param name="EngineerLevel"></param> level of an engineer who can work on the task
/// <param name="Comment"></param> Notes on the task for us
/// <param name="DescriptionTask"></param> Mission description
/// <param name="EndOfActualWork"></param> End of actual work
/// <param name="DeadLine"></param> Final date for completing the task
/// <param name="TaskCreationDate"></param> The date the task was created by the administrator
/// <param name="PlannedDateForStartingWork"></param> Planned date of commencement of work on the assignment
/// <param name="StartWorkDate"></param> Actual start of work
/// <param name="Milestone"></param> A milestone in the mission
/// <param name="name"></param> The task's nickname
public record Task
(
    int NumOfTask,
    string ResultOfTask,     
    int DurationOfExecution,
    string Comment,
    string DescriptionTask, 
    int? EsponsibleEngineerId=null,
    DifficultyLevelTask? DifficultyTasc= null,
    EngineerLevels? EngineerLevel = null,


    //--- dates ---//
    DateTime? TaskCreationDate = null,

    DateTime? PlannedDateForStartingWork = null,
    DateTime? DeadLine= null,
    
    DateTime? StartWorkDate = null,
    DateTime? EndOfActualWork = null,
    bool Milestone = false,               
    string? Nickname = null                  
)
{
    public Task() : this(0, "", 0, "", "") { }
} 
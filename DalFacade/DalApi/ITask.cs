namespace DalApi;

using DO;
using System;
using System.Collections.Generic;

public interface ITask : ICrud<Task>
{
    IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null);
}
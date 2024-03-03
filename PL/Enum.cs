using System;
using System.Collections;
using System.Collections.Generic;
using BO;

namespace PL;

internal class EngineerCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerLevels> s_enums = (Enum.GetValues(typeof(BO.EngineerLevels)) as IEnumerable<BO.EngineerLevels>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}


internal class TaskCollection : IEnumerable
{
    static readonly IEnumerable<BO.DifficultyLevelTask> s_enums = (Enum.GetValues(typeof(BO.DifficultyLevelTask)) as IEnumerable<BO.DifficultyLevelTask>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}


internal class StatusCollection : IEnumerable
{
    static readonly IEnumerable<BO.Status> s_enums = (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

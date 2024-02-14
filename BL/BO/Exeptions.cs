

using DO;

namespace BO;

[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
}


[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
}


[Serializable]
public class BlDeletionImpossibleException : Exception
{
    public BlDeletionImpossibleException(string? message) : base(message) { }
}


[Serializable]
public class BlIncorrectInput: Exception
{
    public BlIncorrectInput(string? message) : base(message) { }
}


[Serializable]
public class BlThereIsNoStartDateForPreviousTasks
 : Exception
{
    public BlThereIsNoStartDateForPreviousTasks(string? message) : base(message) { }
}

[Serializable]
public class BlTheTaskStartDateDoesNotMatch
 : Exception
{
    public BlTheTaskStartDateDoesNotMatch(string? message) : base(message) { }
}



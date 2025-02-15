using System.Runtime.Serialization;

namespace BO;



[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}


[Serializable]
public class BlWrongInput : Exception
{
    public BlWrongInput(string? message) : base(message) { }
}

[Serializable]
public class BlWrongProjectLevel : Exception
{
    public BlWrongProjectLevel(string? message) : base(message) { }
}

[Serializable]
public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message) : base(message) { }
    public BlDeletionImpossible(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
public class BlCannotUpdateException : Exception
{
    public BlCannotUpdateException(string? message) : base(message) { }
    public BlCannotUpdateException(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
internal class ProjectStatusIsNotScheduled : Exception
{
    public ProjectStatusIsNotScheduled()
    {
    }

    public ProjectStatusIsNotScheduled(string? message) : base(message)
    {
    }

    public ProjectStatusIsNotScheduled(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ProjectStatusIsNotScheduled(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

[Serializable]
internal class StartTimeOfDependenceTaskNotExist : Exception
{
    public StartTimeOfDependenceTaskNotExist()
    {
    }

    public StartTimeOfDependenceTaskNotExist(string? message) : base(message)
    {
    }

    public StartTimeOfDependenceTaskNotExist(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected StartTimeOfDependenceTaskNotExist(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

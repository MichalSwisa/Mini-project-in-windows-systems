namespace DO;
/// <summary>
/// A worker entity represents a worker with all of its prop.
/// </summary>
/// <param name="Rank">the rank of the worker</param>
/// <param name="Role">what he actually does</param>
/// <param name="Name">the worker's name</param>
/// <param name="Gmail">the worker's gmail</param>

public record Worker
(
    int Id,
    string? Name = null,
    string? Gmail = null,
    double? Cost = null,
    Roles? Role = null
)
{
    public Worker() : this(0) { }//constructor with no parameters
}



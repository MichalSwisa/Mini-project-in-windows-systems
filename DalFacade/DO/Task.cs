namespace DO;


/// <summary>
/// A task entity represents a task with all of its prop.
/// </summary>
/// <param name="Id">id num of task</param>
/// <param name="Alias">task's name</param>
/// <param name="Description">task's description</param>
/// <param name="IsMilestone">is comleted</param>
/// <param name="CreatedDate">the day we got the task </param>
/// <param name="StartTime">the actual time we started the task</param>
/// <param name="ScheduledDate">the date when we planed to start</param>
/// <param name="RequiredEffortTime">how long it takes</param>
/// <param name="DeadlineDate">the last date to end task successfully</param>
/// <param name="CompleteDate">the actual time we ended the task </param>
/// <param name="Deliverables">the product</param>
/// <param name="Remarks">notices about the task</param>
/// <param name="WorkerId">worker's id</param>
/// <param name="Complexity"> the required role</param>

public record Task
(
    int Id,
    int? WorkerId=null,
    bool? IsMilestone=false,
    string? Alias = null,
    string? Description = null,

    DateTime? CreatedDate = null,
    DateTime? StartTime = null,
    DateTime? ScheduledDate = null,
    TimeSpan? RequiredEffortTime = null,
    DateTime? DeadlineDate = null,
    DateTime? CompleteDate = null,
    string? Deliverables = null,
    string? Remarks = null,
    Roles? Complexity = null
)
{
    public Task() : this(0) { }
    ////ctor without parameters

}

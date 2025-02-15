namespace BO;
/// <summary>
/// A task entity represents a task with all of its prop.
/// </summary>
public class Task
{
    public int Id { get; set; }
    public string? Alias { get; set; }
    public string? Description { get; set; }
    public BO.TaskStatus Status { get; set; } //?
    public List<TaskInList>? Dependecies { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public TimeSpan? RequiredEffortTime { get; set; }
    public DateTime? DeadlineDate { get; set; }
    //public bool? IsMilestone { get; set; } 
    public DateTime? CompleteDate { get; set; }
    public string? Deliverables { get; set; }
    public string? Remarks { get; set; }
    public WorkerInTask? Worker { get; set; } //   public int WorkerId { get; set; }
    public Roles? Complexity { get; set; }

    public override string ToString() => this.ToStringProperty();

}

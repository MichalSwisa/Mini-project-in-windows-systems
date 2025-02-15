namespace BO;
/// <summary>
/// Helper (LOGIC) entity of task-in-list- for the list of task dependencies and for the task list screen:
/// </summary>
public class TaskInList
{
    public int Id { get; init; }
    public int? WorkerId { get; set; }
    public string? Alias { get; set; }
    public TaskStatus Status { get; set; }

    public Roles Complexity { get; set; }
    public string? Description { get; set; }
    //public string? Deliverables { get; set; }
    public override string ToString() => this.ToStringProperty();

}

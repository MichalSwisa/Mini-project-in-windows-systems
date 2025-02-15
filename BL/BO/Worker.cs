namespace BO;

/// <summary>
///A worker LOGIG entity represents a worker with all of its prop.
/// </summary>

public class Worker
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Gmail { get; set; }
    public double? Cost { get; set; }
    public Roles? Role { get; set; }
    public TaskInWorker? CurrentTask { get; set; }
    public override string ToString() => this.ToStringProperty();
}

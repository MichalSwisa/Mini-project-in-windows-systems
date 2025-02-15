namespace BO;

public class TaskInWorker
{
    public int TaskID { get; set; }
    public string TaskAlias { get; set; } = null!;
    public override string ToString() => this.ToStringProperty();
}

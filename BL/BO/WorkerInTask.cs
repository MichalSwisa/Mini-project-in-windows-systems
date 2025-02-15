
namespace BO;

public class WorkerInTask
{
    public int WorkerID { get; set; }
    public string WorkerName { get; set; } = null!;
    public override string ToString() => this.ToStringProperty();
}


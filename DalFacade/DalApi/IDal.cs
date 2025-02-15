namespace DalApi;

public interface IDal
{
    ITask Task { get; }
    IDependence Dependence { get; }
    IWorker Worker { get; }

    public DateTime? StartProjectDate { get; set; }
    public DateTime Clock { get; set; }

   
}

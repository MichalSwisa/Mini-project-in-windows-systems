
namespace BlApi;

/// <summary>
/// logic interface for task
/// </summary>
public interface ITask
{

    public int Create(BO.Task t);
    public void Update(BO.Task t);
    public void Delete(int id);
    public BO.Task Read(int id);
    bool IsTaskCanBeAssigntToWorker(BO.Worker worker, BO.Task task);
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList, bool>? filter = null);
    IEnumerable<BO.Task> GetAllTasksThatCanBeAssignToWorker(BO.Worker worker);
    BO.Task AssignToWorker(BO.Task task, BO.Worker worker);
    //void AddStartDates();   זה נמצא בממשק של Project כבר
    void AddOrUpdateDependecies(BO.Task task);
    public void StartTask(int taskId, int wrkId);
    public void FinishTask(int id);
}

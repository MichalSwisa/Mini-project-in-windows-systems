
using BO;

namespace BlApi;

/// <summary>
/// logic interface for task in list
/// </summary>
public interface ITaskInList
{
    // public void Create(TaskInList t);
    // public void Update(TaskInList t);
    //  public void Delete(int id) ;
    public TaskInList Read(int id);
    public IEnumerable<TaskInList> ReadAll(Func<TaskInList, bool>? filter = null);
    public TaskInList GetDetailedTaskForWorker(int WorkerId, int TaskId);
}

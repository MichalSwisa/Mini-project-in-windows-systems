namespace BlApi;

using BO;
//using System.Data.Common;


/// <summary>
/// logic interface for worker
/// </summary>
public interface IWorker
{
    int Create(Worker worker);
    void Update(Worker worker);
    void Delete(int id);
    Worker Read(int id);

    IEnumerable<Worker> ReadAll(Func<Worker, bool>? filter = null);
}

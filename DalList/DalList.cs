namespace Dal;
using DalApi;
using System.Runtime.InteropServices;

sealed internal class DalList : IDal
{
    /// <summary>
    /// The public Instance property to use
    /// </summary>
    public static IDal Instance { get; } = new DalList();
    private DalList() { }


    public ITask Task => new TaskImplementation();

    public IDependence Dependence => new DependenceImplementation();

    public IWorker Worker => new WorkerImplementation();

    public DateTime? StartProjectDate
    {
         
        get { return DataSource.Config.StartProjectDate;  }

        set { DataSource.Config.StartProjectDate = value; }

    }
    public DateTime Clock
    {

        get { return DataSource.Config.Clock; }

        set { DataSource.Config.Clock = value; }

    }
    //public void SaveClock(DateTime? time)
    //{
    //    Clock = time;
    //}

    //public DateTime LoadClock() => Clock ?? DateTime.Now;

}

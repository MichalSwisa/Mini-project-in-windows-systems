using DalApi;
using System.Data.Common;
using System.Xml.Linq;
namespace Dal;

//stage 3
sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IWorker Worker => new WorkerImplementation();
    public ITask Task => new TaskImplementation();
    public IDependence Dependence => new DependenceImplementation();

    public DateTime? StartProjectDate
    {

        get { return Config.StartProjectDate; }

        set { Config.StartProjectDate = value; }

    }

    public DateTime Clock
    {
        get { return Config.Clock; }
        set { Config.Clock = value; }
    }
    
}
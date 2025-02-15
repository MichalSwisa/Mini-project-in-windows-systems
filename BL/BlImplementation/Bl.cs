namespace BlImplementation;
using BlApi;
using BO;

internal class Bl : IBl
{
   

    public ITask Task => new TaskImplementation(this);

    //public ITask Task1 => new TaskImplementation(Bl.);
    //public ITask TaskInList => new TaskImplementation(this);
    public IProject Project => new ProjectImplementation();
    public IWorker Worker => new WorkerImplementation(Project, Task);

    public void InitializeDB() => DalTest.Initialization.Do();

    public DateTime? StartProjectDate
    {
        get { return /*DataSource.Config.*/StartProjectDate; }

        set { /*DataSource.Config.*/StartProjectDate = value; }
    }


    public void Reset()
    {
        BlApi.Factory.Get();
        DalApi.IDal s_dal = DalApi.Factory.Get;

        //clear data
        s_dal.StartProjectDate = null;
        s_dal!.Worker.Clear();
        s_dal!.Task.Clear();
        s_dal!.Dependence.Clear();
    }


    public static DateTime s_Clock = DateTime.Now;//.Date;
    public DateTime Clock { get { return DalApi.Factory.Get.Clock; } private set { DalApi.Factory.Get.Clock = value; } }

    public void PromoteTime(ClockTime addTime)
    {
        switch (addTime) {
            case ClockTime.Hours:
                Clock=Clock.AddHours(1);
                break;
            case ClockTime.Days:
                Clock=Clock.AddDays(1); 
                break;
            case ClockTime.Years:
                Clock=Clock.AddYears(1);
                break;
            default:
                break;

        }
        //SaveClock();
    }

    public void ResetTime()
    {
        Clock = DateTime.Now;
    }



    //public void SaveClock()
    //{
    //    DalApi.IDal s_dal = DalApi.Factory.Get;
    //    s_dal.SaveClock(Clock/*CurrentTime*/);
    //}

    //public void LoadClock()
    //{
    //    DalApi.IDal s_dal = DalApi.Factory.Get;
    //    /* CurrentTime*/
    //    Clock = s_dal.LoadClock();
    //}



}

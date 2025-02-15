namespace BlApi;

using BlImplementation;
using BO;


public interface IBl
{
    public IWorker Worker { get; }
    public ITask Task { get; }
    public IProject Project { get; }
    public void Reset();
    public void InitializeDB();
 
    #region Clock
    public DateTime Clock { get ;}
    public void PromoteTime(ClockTime addTime);
    public void ResetTime() ;
    //public void SaveClock();
    //public void LoadClock();
    #endregion
}


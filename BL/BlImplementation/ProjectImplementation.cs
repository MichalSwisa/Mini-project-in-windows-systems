using BlApi;
using BO;
using DalApi;
using DO;
using System.Data;

namespace BlImplementation;

public class ProjectImplementation : IProject
{
    private static readonly DalApi.IDal _dal = DalApi.Factory.Get;
    
    public Status GetProjectStatus()
    {
        if (_dal.StartProjectDate is null) return Status.Unscheduled;
        else if (_dal.Task.ReadAll().All(t => t.ScheduledDate is not null)) return Status.Scheduled;
        return Status.OnTrack;
    }


    public void AddStartDates()
    {
        if (_dal.StartProjectDate is not null)
        {
            var tasks = _dal.Task.ReadAll().ToDictionary(t => t.Id);
            var dependencies = _dal.Dependence.ReadAll();
           
            foreach (var task in tasks.Values)
            {
               var  dependencies1 = dependencies.Where(d => d.DependentTask == task.Id);

                var dependenciesTasks = dependencies1.Select(t =>_dal.Task.Read(t.DependsOnTask!)).ToList();

                var maxDate = dependenciesTasks.Max(t => getTaskEnd(t!));

                var scheduledDate = maxDate is null ? _dal.StartProjectDate : maxDate;

                _dal.Task.Update(task with { ScheduledDate = scheduledDate });
            }
        }
    }
    private DateTime? getTaskEnd(DO.Task task) => task.ScheduledDate + task.RequiredEffortTime;
    
    public void SetDate(DateTime? date)
    {
        _dal.StartProjectDate = date;
    }

    public DateTime? GetStartDate()
    {
        return _dal.StartProjectDate;
    }
}

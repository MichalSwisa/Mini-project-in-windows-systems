
namespace BlImplementation;
using BlApi;
using BO;
using DO;
using System.Linq;
using System.Security.Cryptography;

//using BO;
//using DO;


/// <summary>
/// logic Task implementation 
/// </summary>
internal class TaskImplementation : ITask
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;

    private readonly Bl _bl;
    internal TaskImplementation(Bl bl) => _bl = bl;

    private readonly IProject? _project = new ProjectImplementation();

    //public TaskImplementation(IProject project) => _project = project;





    /// <summary>
    /// we check wich tasks we can assingt to each worker
    /// </summary>
    /// <param name="worker"></param>
    /// <param name="task"></param>
    /// <returns></returns>
    public bool IsTaskCanBeAssigntToWorker(BO.Worker worker, BO.Task task)
    {
        //ask for all the tasks that 'this task'  depends on them, and then take only the numbers of these tasks
        var dependensTasksIds = _dal.Dependence.ReadAll(t => /*4 dependent on this*/  t.DependentTask == task.Id).Select(t => t!.DependsOnTask)
        .ToHashSet();//in the hashset i have all the numbers of dependend tasks

        //we check if all the tasks that 'task' dependents on them are done
        var dependensTasks = _dal.Task.ReadAll(task => dependensTasksIds.Contains(task.Id) && task!.CompleteDate is null/*task.status==Done*/);
        // var dependensTasks = _dal.Task.ReadAll(task => dependensTasksIds.Contains(task.Id));

        // return worker?.Role==task.Complexity&&!dependensTasks.Any();
        return task.Worker is null && dependensTasks.All(task => task.CompleteDate is null) &&
            worker?.Role == task.Complexity && task.Status==BO.TaskStatus.Scheduled;
    }

    public IEnumerable<BO.Task> GetAllTasksThatCanBeAssignToWorker(BO.Worker worker) =>
         readAllTasks(task => IsTaskCanBeAssigntToWorker(worker, task));

    /// <summary>
    /// Create a new task to DO
    /// </summary>
    /// <param name="boTsk"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlAlreadyExistsException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public int Create(BO.Task boTsk)
    {
        try
        {
            //if (_project.GetProjectStatus() is not BO.blabla.Scheduled) throw new ProjectStatusIsNotScheduled();

            if (_project?.GetProjectStatus() is not BO.Status.Unscheduled) throw new BO.ProjectStatusIsNotScheduled("Project status is not scheduled");

            //check needed inputs
            if (string.IsNullOrEmpty(boTsk.Alias)) throw new BO.BlWrongInput("Wrong input");

            if ((int?)boTsk.Complexity < 1 || (int?)boTsk.Complexity > 6) throw new BO.BlWrongInput("Wrong Coplexity");


            DO.Task doTask = new DO.Task(Id: boTsk.Id, Alias: boTsk.Alias, Description: boTsk.Description,
            Remarks: boTsk.Remarks, Complexity: (DO.Roles?)boTsk.Complexity);//a new task from do type


            boTsk.Id = _dal.Task.Create(doTask);//send it to create in DO

            AddOrUpdateDependecies(boTsk);

            return boTsk.Id;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTsk.Id} already exists", ex);
        }
    }

    /// <summary>
    /// delete this task
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Delete(int id)
    {
        try
        {
            if (_project?.GetProjectStatus() is BO.Status.OnTrack)
            {
                if (id <= 0) throw new BO.BlWrongInput("Wrong Id");//input check
                if (_dal.Dependence.ReadAll(d => d.DependentTask == id).Any())
                {
                    throw new DependenceTaskExist();
                }
                DO.Task? doTsk = _dal.Task.Read(id);//try to read this from do
                if (doTsk == null)
                    throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
                _dal.Task.Delete(id);
            }
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} already exists", ex);
        }
    }

    /// <summary>
    /// Update this task
    /// </summary>
    /// <param name="task"></param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Update(BO.Task task)
    {
        try
        {
            DO.Task? doTsk = _dal.Task.Read(task.Id);//try to read this from do

            if (doTsk == null)
                throw new BO.BlDoesNotExistException($"Task with ID={task.Id} does Not exist");

            if (_project?.GetProjectStatus() is not BO.Status.Unscheduled)
                task.RequiredEffortTime = doTsk.RequiredEffortTime;

            DO.Task doTask = new DO.Task(Id: task.Id, Alias: task.Alias, Description: task.Description, 
            CreatedDate: task.CreatedDate, StartTime: task.StartTime, ScheduledDate: task.ScheduledDate,
            RequiredEffortTime: task.RequiredEffortTime,CompleteDate:task.CompleteDate,Remarks: task.Remarks, 
            Complexity: (DO.Roles?)task.Complexity);//a new task from do type

            if (_project?.GetProjectStatus() is BO.Status.Scheduled && task.Worker is not null)
            {
                doTask = doTask with { WorkerId = task.Worker.WorkerID };
                BO.TaskInWorker? taskInWorker = new BO.TaskInWorker()
                {
                    TaskAlias = doTask.Alias!,
                    TaskID = doTask.Id
                };  
                BO.Worker worker = _bl.Worker.Read((int)doTask.WorkerId);
                worker.CurrentTask = taskInWorker;//CurrentTask.TaskId\\ = doTask.Id, CurrentTask.TaskAlias = doTask.Alias };
            }
            _dal.Task.Update(doTask);//send it to update

          
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={task.Id} already exists", ex);
        }
    }

    public void AddOrUpdateDependecies(BO.Task task)
    {
        if (task.Dependecies is not null && task.Dependecies!.Any())
        {
            _dal.Dependence.RemoveAllTaskDependenceis(task.Id);
            foreach (var dependenceTask in task.Dependecies)
            {
               // if (hasCircle(task.Id, dependenceTask.Id, dependecies))
                 //   throw new CircleException("There is a  Circle between the dependecies");
                _dal.Dependence.Create(new DO.Dependence(0, dependenceTask.Id, task.Id));
            }

          
        }
    }

    public BO.Task AssignToWorker(BO.Task task, BO.Worker worker)
    {
        try
        {
            if (_project?.GetProjectStatus() is BO.Status.OnTrack && IsTaskCanBeAssigntToWorker(worker, task))
            {
                DO.Task? doTsk = _dal.Task.Read(task.Id);//try to read this from do
                _dal.Task.Update(doTsk! with { WorkerId = worker.Id });
                task.Worker = new BO.WorkerInTask
                {
                    WorkerID = worker.Id,
                    WorkerName = worker.Name!
                };
            }
            return task;
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={task.Id} already exists", ex);
        }
    }

    /// <summary>
    /// read the required task
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Task Read(int id)
    {
        if (id <= 0) throw new BO.BlWrongInput("Wrong Id");//input check

        DO.Task? doTsk = _dal.Task.Read(id);//read it from do (task)

        if (doTsk == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        return doToBoTask(doTsk);
    }

    private IEnumerable<BO.Task> readAllTasks(Func<BO.Task, bool>? filter = null) =>
        from task in _dal.Task.ReadAll()
        let boTask = doToBoTask(task)
        where filter is null ? true : filter(boTask)//if the filter in null we return the whole list if not we return the filtered tasks
        select boTask;

    private bool hasCircle(int dependentTask, int dependOnTask, IEnumerable<DO.Dependence> dependecies)
    {
        if (dependentTask == dependOnTask) return true;


        var hasCircleRes = false;
      
        if (dependecies.Any())
        {
            foreach (var dependence in dependecies)
            {
                hasCircleRes = hasCircle(dependOnTask, dependence.DependsOnTask, dependecies);
                if (hasCircleRes) return hasCircleRes;
            }
        }

        return hasCircleRes;
    }

    private BO.Task doToBoTask(DO.Task doTsk)
    {
        var work = _dal.Worker.Read(t => t.Id == doTsk.WorkerId);
        //var dependence = getAllTaskInListDependecies(doTsk.Id).ToList();
        return new BO.Task
        {
            Id = doTsk.Id,
            Alias = doTsk.Alias,
            Description = doTsk.Description,
            CreatedDate = doTsk.CreatedDate,
            StartTime = doTsk.StartTime,
            ScheduledDate = doTsk.ScheduledDate,
            RequiredEffortTime = doTsk.RequiredEffortTime,
            CompleteDate = doTsk.CompleteDate,
            Deliverables = doTsk.Deliverables,
            Remarks = doTsk.Remarks,
            Complexity = (BO.Roles?)doTsk.Complexity,
            Dependecies = getAllTaskInListDependecies(doTsk.Id).ToList(),//dependence is null? null: dependence,
            Worker = work is null ? null : new BO.WorkerInTask
            {
                WorkerID = work!.Id,
                WorkerName = work.Name!
            },
            Status = getStatus(doTsk)
        };
    }

    private TaskStatus getStatus(DO.Task task) =>
        task switch
        {
            { StartTime: not null,  CompleteDate: null } => BO.TaskStatus.OnTrack,
            { ScheduledDate: not null, CompleteDate: null } => BO.TaskStatus.Scheduled,
            { CompleteDate: not null } => BO.TaskStatus.Done,
            _ => BO.TaskStatus.Unscheduled
        };

    private IEnumerable<BO.TaskInList> getAllTaskInListDependecies(int taskId)
    {
        var dependecies = _dal.Dependence.ReadAll(d => d.DependentTask == taskId)
            .Select(d => d.DependsOnTask).ToHashSet();
        return ReadAll(t => dependecies.Contains(t.Id));//.ToList();
    }

    /// <summary>
    /// read all filtered tasksinlist
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    ///     public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null);
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList, bool>? filter = null)
    {
        return from DO.Task doTsk in _dal.Task.ReadAll()//resd from DO
               let task = new BO.TaskInList//put the information we read from DO in BO
               {
                   Id = doTsk.Id,
                   WorkerId = doTsk.WorkerId,
                   Alias = doTsk.Alias,
                   Description = doTsk.Description,
                   Complexity= (BO.Roles)doTsk.Complexity!,
                   Status = getStatus(doTsk)
               }
               where filter is null ? true : filter(task)//if the filter in null we return the whole list if not we return the filtered tasks
               select task;
    }

   
    public void StartTask(int taskId, int wrkId)
    {
        if (_project?.GetProjectStatus() == BO.Status.Unscheduled) // DO NOT START ANY TASK IF WE DO NOT SCHEDULED OUR PROJECT!
            throw new BlCannotUpdateException("You can't start task before scheduled project");

        BO.Task task = Read(taskId);

        if (task.Worker != null)
            throw new BlCannotUpdateException("another worker already assign to this task");

        // check if engineer is available
        if (_bl!.Worker.Read(wrkId).CurrentTask is not null)
            throw new BlCannotUpdateException("Can't register to more then one task at the same time");

        // check dependencies if we can start
        if (task.Dependecies is not null)
        {
            foreach (var t in task.Dependecies)
            {
                if (t.Status != TaskStatus.Done)
                    throw new BlCannotUpdateException("Has to finish task's dependencies first.");
            }
        }

        task.StartTime = _bl.Clock;  
        BO.Worker wrk = _bl.Worker.Read(wrkId);
        task.Worker = new WorkerInTask() { WorkerID = wrkId, WorkerName = wrk.Name! };
        Update(task);
    }

    public void FinishTask(int id)
    {
        BO.Task task = Read(id);

        if (task.StartTime == null)
            throw new BlCannotUpdateException("you need start task to finish it");

        if (task.Worker == null)
            throw new BlCannotUpdateException("you need assign worker to task to finish it");

        task.CompleteDate = _bl?.Clock;   
        Update(task);
    }
}
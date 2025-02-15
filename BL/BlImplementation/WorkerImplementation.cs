namespace BlImplementation;
using BlApi;
using BO;
using System.ComponentModel.DataAnnotations;



/// <summary>
/// logic worker implementation 
/// </summary>
internal class WorkerImplementation : IWorker
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private readonly ITask _task;
    private readonly IProject _project;

    public WorkerImplementation(IProject project, ITask task) =>
        (_project, _task) = (project, task);

    private void inputCheck(BO.Worker boWrk)
    {
        if (boWrk.Id <= 0) throw new BO.BlWrongInput("Wrong Id");
        if (string.IsNullOrEmpty(boWrk.Name)) throw new BO.BlWrongInput("Wrong Name");
        if (boWrk.Cost <= 0) throw new BO.BlWrongInput("Wrong Cost");
        if (!new EmailAddressAttribute().IsValid(boWrk.Gmail)) throw new BO.BlWrongInput("Wrong Gmail");
    }

    /// <summary>
    /// create new worker
    /// </summary>
    /// <param name="boWrk">the BO parameterthat the function get</param>
    /// <exception cref="BO.BlAlreadyExistsException"> throw exception if the worker already exist in system</exception>
    public int Create(BO.Worker boWrk)
    {
        try
        {
            inputCheck(boWrk);//check the unput
            DO.Worker doWorker = new DO.Worker(boWrk.Id, boWrk.Name, boWrk.Gmail, boWrk.Cost, (DO.Roles?)boWrk.Role);
            return _dal.Worker.Create(doWorker);//create
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"WorkerId with ID={boWrk.Id} already exists", ex);
        }
    }



    /// <summary>
    /// Delete the worker with this id
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Delete(int id)
    {
        try
        {
            //check id only
            if (id <= 0) throw new BO.BlWrongInput("Wrong Id");

            //try to read the worker with this id
            DO.Worker? doWrk = _dal.Worker.Read(id);
            if (doWrk == null) throw new BO.BlDoesNotExistException($"WorkerId with ID={id} does Not exist");

            //check if this worker does a task right now
            DO.Task? doT = _dal.Task.Read(w => (w.Id == doWrk.Id && w.StartTime != null));
            if (doT is not null) throw new BO.BlDeletionImpossible($"WorkerId with ID={id} is immposible to delete");

            _dal.Worker.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"WorkerId with ID={id} doesnt exists", ex);
        }
    }


    /// <summary>
    /// Update the worker
    /// </summary>
    /// <param name="worker"></param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Update(BO.Worker worker)
    {
        try
        {
            inputCheck(worker);
     
            DO.Worker doWrk = _dal.Worker.Read(worker.Id)!;
            if ((int?)worker.Role > (int?)doWrk.Role || (int?)worker.Role < 1) throw new BO.BlWrongInput("Wrong Role");
            
            DO.Worker workerToUpdate = new DO.Worker(worker.Id, worker.Name, worker.Gmail, worker.Cost, (DO.Roles?)worker.Role);
            
            _dal.Worker.Update(workerToUpdate);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"WorkerId with ID={worker.Id}  doesnt exists", ex);
        }
    } 
    /// <summary>
    /// convert from do worker to bo worker
    /// </summary>
    /// <param name="doWork"></param>
    /// <returns></returns>
    private Worker doToBoWorker(DO.Worker doWork)
    {
        var task = _dal.Task.Read(t => t.WorkerId == doWork.Id && t.StartTime is not null && t.CompleteDate is null);

        return new BO.Worker
        {
            Id = doWork.Id,
            Name = doWork.Name,
            Gmail = doWork.Gmail,
            Cost = doWork.Cost,
            Role = (BO.Roles?)doWork.Role,
            CurrentTask = task is null ? null : new TaskInWorker
            {
                TaskID = task.Id,
                TaskAlias = task.Alias!
            }
        };
    }


    /// <summary>
    /// read the wanted worker 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public BO.Worker Read(int id)
    {
        if (id <= 0) throw new BO.BlWrongInput("Wrong Id");//input check

        DO.Worker? doWork = _dal.Worker.Read(id);//read it from do (worker)

        if (doWork == null)
            throw new BO.BlDoesNotExistException($"WorkerId with ID={id} does Not exist");

        return doToBoWorker(doWork);
    }

    /// <summary>
    /// read all filtered worker
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<BO.Worker> ReadAll(Func<BO.Worker, bool>? filter = null)
    {
        return from DO.Worker doWork in _dal.Worker.ReadAll()//read all workers from do
               let worker = doToBoWorker(doWork)//convert them to bo type
               where filter is null ? true : filter(worker)//if the filter in null we return the whole list, if not we return the filtered workers
               select worker;
    } 
}

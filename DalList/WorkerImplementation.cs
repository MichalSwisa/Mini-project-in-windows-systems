namespace Dal;
using DalApi;
using DO;


internal class WorkerImplementation : IWorker
{
    public IEnumerable<Worker> worker = new List<Worker>();

    public int Create(Worker item)
    {

        foreach (Worker tempWorker in DataSource.Workers)
        {
            if (tempWorker.Id == item.Id)// if it already exist
                throw new DalAlreadyExistsException($"An object of worker type with id={item.Id} already exist");
        }
        DataSource.Workers.Add(item);//if its a new item- add it to the list

        return item.Id;//return the id of the new added item
    }

    public void Delete(int id)
    {
        Worker? w = Read(id);
        if (w != null)
        { //if we found object with the request id
            DataSource.Workers.Remove(w);//if you find it - delete
            return;
        }
        //if doesnt exist -
        throw new DalDoesNotExistException($"An object of type worker with ID={id} does not exist");
    }
    public Worker? Read(Func<Worker, bool> filter)
    {
        return DataSource.Workers.FirstOrDefault(filter);
    }

    public Worker? Read(int id)
    {
        return DataSource.Workers.FirstOrDefault(d => d.Id == id);
    }

    public IEnumerable<Worker> ReadAll(Func<Worker, bool> filter) =>//stage 2
   from item in DataSource.Workers
   where filter is null ? true : filter(item)
   select item;

    public void Update(Worker item)
    {
        if (Read(item.Id) == null)
            throw new DalDoesNotExistException($"Worker with ID={item.Id} does not exist");

        DataSource.Workers.Remove(Read(item.Id)!); //remove from list
        DataSource.Workers.Add(item); //adding update item
    }

    public void Clear()
    {
        DataSource.Workers.Clear();
    }
}

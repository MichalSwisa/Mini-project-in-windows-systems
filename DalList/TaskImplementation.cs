namespace Dal;
using DalApi;
using DO;

internal class TaskImplementation : ITask
{
    public IEnumerable<Task> task = new List<Task>();

    public int Create(Task item)
    {
        int newNum = DataSource.Config.NextTaskId;
        Task newTask = item with { Id = newNum };  //creat new item
        DataSource.Tasks.Add(newTask);  //adding the new item to list
        return newNum;
    }

    public void Delete(int id)
    {
        Task? tas = Read(id);
        if (tas != null)
        {//if we found object with the request id
            DataSource.Tasks.Remove(tas);
            return;
        }
        throw new DalDoesNotExistException($"Aa object of task type with id={id} doesnt exist");
    }

    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(filter);
    }


    public Task? Read(int id)
    {
        return DataSource.Tasks.FirstOrDefault(d => d.Id == id);
    }

    public IEnumerable<Task> ReadAll(Func<Task, bool> filter) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Tasks
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Tasks
               select item;
    }

    public void Update(Task item)
    {
        if (Read(item.Id) == null)
            throw new DalDoesNotExistException($"Task with ID={item.Id} does not exist");

        DataSource.Tasks.Remove(Read(item.Id)!);//remove from list
        DataSource.Tasks.Add(item);//adding update item
    }

    public void Clear()
    {
        DataSource.Tasks.Clear();
    }
}

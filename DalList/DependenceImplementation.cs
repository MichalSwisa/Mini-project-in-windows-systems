namespace Dal;
using DalApi;
using DO;

internal class DependenceImplementation : IDependence
{

    public IEnumerable<Dependence> dep = new List<Dependence>();

    public int Create(Dependence item)
    {
        int newNum = DataSource.Config.NextDependenceId;
        Dependence newDependence = item with { Id = newNum };  //creat new item
        DataSource.Dependences.Add(newDependence);   //adding the new item to list
        return newNum;
    }

    public void Delete(int id)
    {
        Dependence? dep = Read(id);
        if (dep != null)
        {//if we found object with the request id
            DataSource.Dependences.Remove(dep);
            return;
        }
        throw new DalDoesNotExistException($"An object of type Dependence with ID={id} does not exist");
    }
    public Dependence? Read(Func<Dependence, bool> filter)
    {
        return DataSource.Dependences.FirstOrDefault(filter);
    }
    public Dependence? Read(int id)
    {
        Dependence? result = DataSource.Dependences.FirstOrDefault(d => d.Id == id);
        return result;
    }

    public IEnumerable<Dependence> ReadAll(Func<Dependence, bool> filter) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Dependences
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependences
               select item;
    }

    public void Update(Dependence item)
    {
        if (Read(item.Id) == null)
            throw new DalDoesNotExistException($"Dependence with ID={item.Id} does not exist");

        DataSource.Dependences.Remove(Read(item.Id)!);//remove from list
        DataSource.Dependences.Add(item);//adding update item
    }

    public void Clear()
    {
        DataSource.Dependences.Clear();
    }

    public void RemoveAllTaskDependenceis(int taskId)
    {
        DataSource.Dependences.RemoveAll(dependency => dependency.DependentTask == taskId);
    }
}


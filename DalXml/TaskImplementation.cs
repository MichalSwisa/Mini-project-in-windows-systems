namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{
    readonly string s_tasks_xml = "tasks";

    /// <summary>
    /// create new task
    /// </summary>
    /// <param name="item"> the paramter we need to add</param>
    public int Create(Task item)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);

        //for entities with auto id
        int nextId = Config.NextTaskId;
        Task t = item with { Id = nextId };
        //adding the object
        tasks.Add(t);

        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
        return item.Id;
    }

    public void Delete(int id)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        Task? item = Read(id);
        if (tasks.RemoveAll(w => w.Id == item!.Id) == 0)
            throw new DalDoesNotExistException($"Worker with ID={item!.Id} does not exist");
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);
    }

    public Task? Read(int id)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        return tasks.FirstOrDefault(d => d.Id == id);
    }

    public Task? Read(Func<Task, bool> filter)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        return tasks.FirstOrDefault(filter);
    }

    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);
        if (filter != null)
        {
            return from item in tasks
                   where filter(item)
                   select item;
        }
        return from item in tasks
               select item;
    }


    /// <summary>
    /// updating item
    /// </summary>
    /// <param name="item">parameter of the update item</param>
    public void Update(Task item)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);

        //delete all worker type items with the Id
        if (tasks.RemoveAll(w => w.Id == item.Id) == 0)
            throw new DalDoesNotExistException($"Task with ID={item.Id} does not exist");

        //adding update object
        tasks.Add(item);
        XMLTools.SaveListToXMLSerializer(tasks, s_tasks_xml);

    }

    /// <summary>
    /// clear file from every thing
    /// </summary>
    public void Clear()
    {
        XElement? tsk = XMLTools.LoadListFromXMLElement(s_tasks_xml);// get the information
        tsk.RemoveAll();
        XMLTools.SaveListToXMLElement(tsk, s_tasks_xml);//load the updated informayion
        Config.NextTaskId = 0;
       
    }
}

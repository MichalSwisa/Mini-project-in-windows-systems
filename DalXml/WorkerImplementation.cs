namespace Dal;
using DalApi;
using DO;
using System.Data.Common;
using System.Xml.Linq;


internal class WorkerImplementation : IWorker
{
    readonly string s_workers_xml = "workers";

    /// <summary>
    /// create new worker
    /// </summary>
    /// <param name="item"> the paramter we need to add</param>
    public int Create(Worker item)
    {
        List<Worker> workers = XMLTools.LoadListFromXMLSerializer<Worker>(s_workers_xml);

        foreach (var tempWorker in workers)
        {
            if (tempWorker.Id == item.Id)// if it already exist
                throw new DalAlreadyExistsException($"An object of worker type with id={item.Id} already exist");
        }

        Worker w = item with { Id = item.Id };
        //adding the object
        workers.Add(w);

        XMLTools.SaveListToXMLSerializer(workers, s_workers_xml);
        return item.Id;
    }

    public void Delete(int id)
    {
        List<Worker> workers = XMLTools.LoadListFromXMLSerializer<Worker>(s_workers_xml);
        Worker? item = Read(id);
        if (workers.RemoveAll(w => w.Id == item!.Id) == 0)
            throw new DalDoesNotExistException($"Worker with ID={item!.Id} does not exist");
        XMLTools.SaveListToXMLSerializer(workers, s_workers_xml);
    }

    public Worker? Read(int id)
    {
        List<Worker> workers = XMLTools.LoadListFromXMLSerializer<Worker>(s_workers_xml);
        return workers.FirstOrDefault(d => d.Id == id);
    }

    public Worker? Read(Func<Worker, bool> filter)
    {
        List<Worker> workers = XMLTools.LoadListFromXMLSerializer<Worker>(s_workers_xml);
        return workers.FirstOrDefault(filter);
    }

    public IEnumerable<Worker> ReadAll(Func<Worker, bool>? filter)
    {
        List<Worker> workers = XMLTools.LoadListFromXMLSerializer<Worker>(s_workers_xml);
        if (filter != null)
        {
            return from item in workers
                   where filter(item)
                   select item;
        }
        return from item in workers
               select item;
    }


    /// <summary>
    /// updating item
    /// </summary>
    /// <param name="item">parameter of the update item</param>
    public void Update(Worker item)
    {
        List<Worker> workers = XMLTools.LoadListFromXMLSerializer<Worker>(s_workers_xml);

        //delete all worker type items with the Id
        if (workers.RemoveAll(w => w.Id == item.Id) == 0)
            throw new DalDoesNotExistException($"Worker with ID={item.Id} does not exist");

        //adding update object
        workers.Add(item);
        XMLTools.SaveListToXMLSerializer(workers, s_workers_xml);
    }

    /// <summary>
    /// convert work to xelement
    /// </summary>
    /// <param name="work"></param>
    /// <returns></returns>
    static XElement getXElement(Worker work)
    {
        return new XElement("Worker",
            new XElement("Id", work.Id),
            new XElement("Name", work.Name),
            new XElement("cost", work.Cost),
            new XElement("Gmail", work.Gmail),
            new XElement("Role", work.Role));
    }

    /// <summary>
    /// clear file from every thing
    /// </summary>
    public void Clear()
    {
        XElement? work = XMLTools.LoadListFromXMLElement(s_workers_xml);// get the information
        work.RemoveAll();
        XMLTools.SaveListToXMLElement(work, s_workers_xml);//load the updated informayion
        
    }
}


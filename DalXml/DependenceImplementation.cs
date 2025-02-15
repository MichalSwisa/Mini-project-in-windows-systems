namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;
/// <summary>
/// we use xml files to manage dependencies with CRRUD meyhods
/// </summary>
internal class DependenceImplementation : IDependence
{
    static string entity = "dependences";

    readonly string s_dependences_xml = "dependences";
    /// <summary>
    /// convert xelement to depe..
    /// </summary>
    /// <param name="dep"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    static Dependence getDependence(XElement elem)
    {
        return new Dependence()
        {//!!!
            Id = int.TryParse((string?)elem.Element("Id"), out var id) ? id : throw new FormatException("can't convert id"),
            DependentTask = elem.ToIntNullable("DependentTask") ?? throw new FormatException("can't convert id"),
            DependsOnTask = elem.ToIntNullable("DependsOnTask") ?? throw new FormatException("can't convert id"),
        };
    }
    /// <summary>
    /// convert dep to xelement
    /// </summary>
    /// <param name="dep"></param>
    /// <returns></returns>
    static XElement getXElement(Dependence dep)
    {
        return new XElement("Dependence",
            new XElement("Id", dep.Id),
            new XElement("DependentTask", dep.DependentTask),
            new XElement("DependsOnTask", dep.DependsOnTask));
    }

    /// <summary>
    /// create new dependence
    /// </summary>
    /// <param name="item"> the paramter we need to add</param>
    public int Create(Dependence item)
    {
        XElement? depRoot = XMLTools.LoadListFromXMLElement(s_dependences_xml);//get the information
        int nextDepId = Config.NextDependencyId;
        Dependence newOne = item with { Id = nextDepId };
        depRoot.Add(getXElement(newOne));//add the new item
        XMLTools.SaveListToXMLElement(depRoot, s_dependences_xml);//load the updated informayion
        return nextDepId;
    }
    /// <summary>
    /// delete the requested dependece if it exist
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)
    {
        XElement? depRoot = XMLTools.LoadListFromXMLElement(s_dependences_xml);//get the information
        XElement? elemToDelete = depRoot.Elements().FirstOrDefault(dep => (int?)dep.Element("Id") == id);//try to find the elemnt
        if (elemToDelete != null)//if you find it
        {
            elemToDelete.Remove();//remove this varieble
            XMLTools.SaveListToXMLElement(depRoot, s_dependences_xml);//load the updated informayion
        }
        else//if you didnt find it trow exeption
            throw new DalDoesNotExistException($"dependence with ID={id} does not exist");
    }
    /// <summary>
    /// find and return the wanted dep if exist
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Dependence? Read(int id)
    {
        XElement? depRoot = XMLTools.LoadListFromXMLElement(s_dependences_xml);// get the information
        XElement? depElem = depRoot.Elements().FirstOrDefault(dep => (int?)dep.Element("Id") == id);//try to find the elemnt
        if (depElem != null)//if you find it
        {
            return getDependence(depElem);//go to get dependence
        }
        else//doent exist
            return null;
    }
    /// <summary>
    /// find and return the wanted dep if exist and fits the filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Dependence? Read(Func<Dependence, bool> filter)
    {
        XElement? depRoot = XMLTools.LoadListFromXMLElement(s_dependences_xml);// get the information
        return depRoot.Elements().Select(dep => getDependence(dep)).FirstOrDefault(filter);
    }
    /// <summary>
    /// return all dependencies that exist and filtered
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Dependence> ReadAll(Func<Dependence, bool> filter)
    {
        XElement? depRoot = XMLTools.LoadListFromXMLElement(s_dependences_xml);// get the information
        if (filter != null)
            return depRoot.Elements().Select(dep => getDependence(dep)).Where(filter!);
        else
            return depRoot.Elements().Select(dep => getDependence(dep));
    }


    /// <summary>
    /// updating item
    /// </summary>
    /// <param name="item">parameter of the update item</param>
    public void Update(Dependence item)
    {
        XElement? depRoot = XMLTools.LoadListFromXMLElement(s_dependences_xml);// get the information
        XElement? depElem = depRoot.Elements().FirstOrDefault(dep => (int?)dep.Element("Id") == item.Id);
        if (depElem == null)
            throw new DalDoesNotExistException($"Dependence with ID={item.Id} does not exist");
        else
        {
            depElem.Remove();
            depRoot.Add(item);
            XMLTools.SaveListToXMLElement(depRoot, s_dependences_xml);//load the updated informayion
        }
    }
    /// <summary>
    /// clear file from every thing
    /// </summary>
    public void Clear()
    {
        XElement? dep = XMLTools.LoadListFromXMLElement(s_dependences_xml);// get the information
        dep.RemoveAll();
        XMLTools.SaveListToXMLElement(dep, s_dependences_xml);//load the updated informayion
        Config.NextDependencyId = 1;
    }

    public void RemoveAllTaskDependenceis(int taskId)
    {
        XElement? depRoot = XMLTools.LoadListFromXMLElement(s_dependences_xml);//get the information
        IEnumerable<XElement?> elemToDelete = depRoot.Elements().Where(dep => (int?)dep.Element("DependentTask") == taskId);//try to find the elemnt
        foreach (var element in elemToDelete) element?.Remove();
        XMLTools.SaveListToXMLElement(depRoot, s_dependences_xml);//load the updated informayion
    }
}


namespace DalApi;
using DO;
public interface IDependence : ICrud<Dependence> 
{
    void RemoveAllTaskDependenceis(int taskId);

}

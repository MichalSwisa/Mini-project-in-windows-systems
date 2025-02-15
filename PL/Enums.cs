using System.Collections;
namespace PL;


internal class WorkerCollection : IEnumerable
{
    static readonly IEnumerable<BO.Roles> s_enums =
    (Enum.GetValues(typeof(BO.Roles)) as IEnumerable<BO.Roles>)!;

 
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
internal class TaskCollection : IEnumerable
{
    static readonly IEnumerable<BO.TaskStatus> s_enums2 =
    (Enum.GetValues(typeof(BO.TaskStatus)) as IEnumerable<BO.TaskStatus>)!;
    public IEnumerator GetEnumerator() => s_enums2.GetEnumerator();

}
internal class TaskComplexityCollection : IEnumerable
{
    static readonly IEnumerable<BO.Roles> s_enums3 =
    (Enum.GetValues(typeof(BO.Roles)) as IEnumerable<BO.Roles>)!;
    public IEnumerator GetEnumerator() => s_enums3.GetEnumerator();

}
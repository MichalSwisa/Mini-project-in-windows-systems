//namespace DO;

///// <summary>
///// Dependence Entity represents a dependence with all its props
///// </summary>
///// <param name="cmn">ID number of current task (created otomaticlly)</param>
///// <param name="fm">ID number of a previous task (on which the current one depended)</param>
///// <param name="nm">ID number of the next task (which depends on the current one)</param>
//public record Dependence
//(
//    int Id,
//    int? DependsOnTask, //this depends on task XYZ    (my last mission)
//    int? DependentTask  //task that depends on this  (my next mission)
//)
//{
//    public Dependence() : this(0, 0 ,0 ) { }//ctor with out parameters
//}

namespace DO;

/// <summary>
/// Dependence Entity represents a dependence with all its props
/// </summary>
/// <param name="cmn">ID number of current task (created otomaticlly)</param>
/// <param name="fm">ID number of a previous task (on which the current one depended)</param>
/// <param name="nm">ID number of the next task (which depends on the current one)</param>
public record Dependence
(
    int Id,
    int DependsOnTask, //this depends on task XYZ    (my last mission)
    int DependentTask  //task that depends on this  (my next mission)
)
{
    public Dependence() : this(0, 0, 0) { }//ctor with out parameters
}

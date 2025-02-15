namespace DalTest;
using DalApi;
using DO;

//we used TryParse

internal class Program
{

    static readonly IDal s_dal = Factory.Get;

    static void Main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
        if (ans == "Y")
            Initialization.Do();

        Console.WriteLine("\nCheck entity\n");
        Console.WriteLine("Choose one of the following:");
        Console.WriteLine("0: exit");
        Console.WriteLine("1: Worker");
        Console.WriteLine("2: Task");
        Console.WriteLine("3: Dependence");
        int choice = int.Parse(Console.ReadLine()!);

        do
        {
            try
            {
                switch (choice)
                {
                    case 0:
                        return;
                    case 1://calling to worker options
                        Worker();
                        break;
                    case 2://calling to task options
                        Task();
                        break;
                    case 3://calling to dependence options
                        Dependence();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("\nCheck entity\n");
                Console.WriteLine("Choose one of the following:");
                Console.WriteLine("0: exit");
                Console.WriteLine("1: Worker");
                Console.WriteLine("2: Task");
                Console.WriteLine("3: Dependence");
                choice = int.Parse(Console.ReadLine()!);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (choice != 0);
    }



    static int menu(string s)
    {
        Console.WriteLine("choose operation");
        Console.WriteLine("Choose one of the following:");
        Console.WriteLine("0 exit");
        Console.WriteLine("1 Create new ", s);
        Console.WriteLine("2 Read ", s);
        Console.WriteLine("3 ReadAll ", s);
        Console.WriteLine("4 Update ", s);
        Console.WriteLine("5 Delete ", s);
        int i = int.Parse(Console.ReadLine()!);
        return i;
    }
    static void Worker()
    {

        int i = menu("Worker");
        do
        {
            try
            {
                switch (i)
                {
                    case 0: break;

                    case 1:
                        //create - Worker
                        CreateWorker();
                        break;

                    case 2:
                        //Read- Worker
                        ReadWorker();
                        break;

                    case 3:
                        //ReadAll- Worker
                        ReadAllWorker();
                        break;

                    case 4:
                        //Uptade- Worker
                        UptadeWorker();
                        break;

                    case 5:
                        //Delete- Worker
                        DeleteWorker();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            i = menu("Worker");
        } while (i != 0);
    }
    static void Task()
    {

        int i = menu("Task");
        do
        {
            try
            {
                switch (i)
                {
                    case 0: break;

                    case 1:
                        //create - Task
                        CreateTask();
                        break;

                    case 2:
                        //Read- Task
                        ReadTask();
                        break;

                    case 3:
                        //ReadAll- Task
                        ReadAllTask();
                        break;

                    case 4://Uptade- Task
                        UptadeTask();
                        break;
                    case 5://Delete- Task
                        DeleteTask();
                        break;
                    default:
                        break;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            i = menu("Task");
        } while (i != 0);

    }
    static void Dependence()
    {

        int i = menu("Dependence");
        do
        {
            try
            {
                switch (i)
                {
                    case 0: break;

                    case 1:
                        //create - dependence
                        CreateDependence();
                        break;

                    case 2:
                        //Read- Dependence
                        ReadDependence();
                        break;

                    case 3:
                        //ReadAll- Dependence
                        ReadAllDependence();
                        break;

                    case 4://Uptade- Dependence
                        UptadeDependence();
                        break;
                    case 5://Delete- Dependence
                        DeleteDependence();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            i = menu("Dependence");
        } while (i != 0);
    }





    /// <summary>
    /// Create Worker
    /// </summary>
    private static void CreateWorker()
    {
        int id;

        Console.WriteLine("Enter id please");
        if (!int.TryParse(Console.ReadLine(), out id))
            throw new FormatException("Wrong input");
        Console.WriteLine("Enter name please");
        string name = Console.ReadLine() ?? throw new FormatException("Wrong input");
        Console.WriteLine("Enter Email please");
        string? email = Console.ReadLine() ?? throw new FormatException("Wrong input");
        Console.WriteLine("Enter cost please");
        if (!double.TryParse(Console.ReadLine(), out double cost))
            throw new FormatException("Wrong input");
        Console.WriteLine("Enter role please (number)");
        if (!int.TryParse(Console.ReadLine(), out int roleNum))
            throw new FormatException("Wrong input");

        Worker w = new Worker(id, name, email, cost, (Roles)roleNum);//create a new object of worker type

        s_dal!.Worker.Create(w);//sending to create
    }

    /// <summary>
    /// Read Worker
    /// </summary>
    private static void ReadWorker()
    {
        Console.WriteLine("Enter id:");
        int id = int.Parse(Console.ReadLine()!);
        Worker w = s_dal!.Worker.Read(id) ?? throw new NullReferenceException("Worker can not be null!");
        Console.WriteLine(w);
    }

    /// <summary>
    /// Read All Workers
    /// </summary>
    private static void ReadAllWorker()//print every element in the nwe list
    {
        IEnumerable<Worker?> workers = s_dal!.Worker.ReadAll();
        foreach (Worker? worker in workers)
        {
            Console.WriteLine(worker);
        }
    }

    /// <summary>
    /// Uptade Worker
    /// </summary>
    private static void UptadeWorker()
    {
        Console.WriteLine("Enter id please");
        int id = int.Parse(Console.ReadLine()!);
        Worker? t = s_dal!.Worker.Read(id); //print worker details

        Console.WriteLine(t);
        Console.WriteLine("Enter name please");
        string? name = Console.ReadLine();
        Console.WriteLine("Enter Email please");
        string? email = Console.ReadLine();
        Console.WriteLine("Enter cost please");
        double? cost = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter role please (number)");
        Roles? roleNum = (Roles)Int16.Parse(Console.ReadLine()!);

        //checks
        if (name == "") name = t!.Name;
        if (email == "") email = t!.Gmail;
        if (cost == '\t') cost = t!.Cost;
        if (((int)roleNum > 6) || ((int)roleNum < 0)) roleNum = t!.Role;

        Worker w = new(id, name, email, cost, (Roles)roleNum!);//create a new object of worker type
        s_dal!.Worker.Update(w);//sending to update
    }

    /// <summary>
    /// Delete Worker
    /// </summary>
    private static void DeleteWorker()
    {
        Console.WriteLine("Enter id to delete:");
        int id = int.Parse(Console.ReadLine()!);
        s_dal!.Worker.Delete(id);
    }





    /// <summary>
    /// create task
    /// </summary>
    private static void CreateTask()
    {
        Console.WriteLine("Enter worker id");
        int workerId = int.Parse(Console.ReadLine()!);  //the id of the worker who got the task
        Console.WriteLine("Enter task alias");
        string? alias = Console.ReadLine();
        Console.WriteLine("Enter task description");
        string? description = Console.ReadLine();

        Console.WriteLine("Enter created date");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime createdDate))
            throw new FormatException("Wrong input");
        Console.WriteLine("Enter start time");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
            throw new FormatException("Wrong input");
        Console.WriteLine("Enter schedual date");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime scheduledDate))
            throw new FormatException("Wrong input");
        Console.WriteLine("Enter required effort time");
        if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan requiredEffortTime))
            throw new FormatException("Wrong input");
        Console.WriteLine("Enter deadline date");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime deadlineDate))
            throw new FormatException("Wrong input");
        Console.WriteLine("Enter complete date");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime completeDate))
            throw new FormatException("Wrong input");

        Console.WriteLine("Enter deliverable");
        string? deliverable = Console.ReadLine();
        Console.WriteLine("Enter remark");
        string? remark = Console.ReadLine();
        Console.WriteLine("Enter complexity");
        int comlexity = int.Parse(Console.ReadLine()!);

        Task t = new Task(0, workerId, false, alias, description, createdDate, startTime, scheduledDate, requiredEffortTime,
            deadlineDate, completeDate, deliverable, remark, (Roles)comlexity);//create a new object of Task type

        s_dal!.Task.Create(t);//sending to create
    }

    /// <summary>
    /// Read Task
    /// </summary>
    private static void ReadTask()
    {
        Console.WriteLine("Enter id:");
        int id = int.Parse(Console.ReadLine()!);
        Task t = s_dal!.Task.Read(id) ?? throw new NullReferenceException("Task can not be null!");
        Console.WriteLine(t);
    }

    /// <summary>
    /// Read All Task
    /// </summary>
    private static void ReadAllTask()
    {
        IEnumerable<Task?> tasks = s_dal!.Task.ReadAll();

        foreach (Task? task in tasks)//print every element in the nwe list
        {
            Console.WriteLine(task);
        }
    }

    /// <summary>
    /// Uptade Task
    /// </summary>
    private static void UptadeTask()
    {
        Console.WriteLine("Enter id please");
        int id = int.Parse(Console.ReadLine()!);
        Task? doTsk = s_dal!.Task.Read(id);

        Console.WriteLine(doTsk);  //print worker details

        Console.WriteLine("Enter worker id");
        int? workerId = int.Parse(Console.ReadLine()!);
        //Console.WriteLine("Enter mile stone");
        //bool isMileStone = bool.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter task alias");
        string? alias = Console.ReadLine();
        Console.WriteLine("Enter task description");
        string? description = Console.ReadLine();

        Console.WriteLine("Enter created date");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime createdDate))
            throw new FormatException("Wrong input");
        Console.WriteLine("Enter start time");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
            throw new FormatException("Wrong input");
        Console.WriteLine("Enter schedual date");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime scheduledDate))
            throw new FormatException("Wrong input");
        Console.WriteLine("Enter required effort time");
        if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan requiredEffortTime))
            throw new FormatException("Wrong input");
        Console.WriteLine("Enter deadline date");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime deadlineDate))
            throw new FormatException("Wrong input");
        Console.WriteLine("Enter complete date");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime completeDate))
            throw new FormatException("Wrong input");

        Console.WriteLine("Enter deliverable");
        string? deliverable = Console.ReadLine();
        Console.WriteLine("Enter remark");
        string? remark = Console.ReadLine();
        Console.WriteLine("Enter complexity");
        Roles? complexity = (Roles)Int16.Parse(Console.ReadLine()!);

        //checks
        if (workerId == '\t') workerId = doTsk!.WorkerId;
        if (alias == "") alias = doTsk!.Alias;
        if (description == "") description = doTsk!.Description;
        if (createdDate == new DateTime()) createdDate = ((DateTime)doTsk!.CreatedDate!);
        if (startTime == new DateTime()) startTime = ((DateTime)doTsk!.StartTime!);
        if (scheduledDate == new DateTime()) scheduledDate = ((DateTime)doTsk!.ScheduledDate!);
        if (requiredEffortTime == new TimeSpan()) requiredEffortTime = ((TimeSpan)doTsk!.RequiredEffortTime!);
        if (deadlineDate == new DateTime()) deadlineDate = ((DateTime)doTsk!.DeadlineDate!);
        if (completeDate == new DateTime()) completeDate = ((DateTime)doTsk!.CompleteDate!);
        if (deliverable == "") deliverable = doTsk!.Deliverables;
        if (remark == "") remark = doTsk!.Remarks;
        if (((int)complexity < 1) || ((int)complexity > 6)) complexity = doTsk!.Complexity;

        Task tsk = new Task(id, workerId, false, alias, description, createdDate, startTime, scheduledDate, requiredEffortTime,
            deadlineDate, completeDate, deliverable, remark, (Roles)complexity!);//create a new object of Task type

        s_dal!.Task.Create(tsk);//sending to create
    }

    /// <summary>
    /// Delete Task
    /// </summary>
    private static void DeleteTask()
    {
        Console.WriteLine("Enter i to delete:");
        int id = int.Parse(Console.ReadLine()!);
        s_dal!.Task.Delete(id);
    }





    /// <summary>
    /// create dependency
    /// </summary>
    private static void CreateDependence()
    {
        Console.WriteLine("Enter current mission number,former mission and next mission please");
        int currentMissionNum = int.Parse(Console.ReadLine()!);
        int formerMission = int.Parse(Console.ReadLine()!);
        int nextMission = int.Parse(Console.ReadLine()!);
        Dependence d = new Dependence(currentMissionNum, formerMission, nextMission);//create a new object of Dependence type
        s_dal!.Dependence.Create(d);//sending to create
    }

    /// <summary>
    /// Read Dependence
    /// </summary>
    private static void ReadDependence()
    {
        Console.WriteLine("Enter id of current mission:");
        int id = int.Parse(Console.ReadLine()!);
        Dependence d = s_dal!.Dependence.Read(id) ?? throw new NullReferenceException("Dependence can not be null!");
        Console.WriteLine(d);
    }

    /// <summary>
    /// Read All Dependence
    /// </summary>
    private static void ReadAllDependence()
    {
        IEnumerable<Dependence?> dependences;
        dependences = s_dal!.Dependence.ReadAll();
        foreach (Dependence? dependence in dependences)//print every element in the nwe list
        {
            Console.WriteLine(dependence);
        }
    }

    /// <summary>
    /// Uptade Dependence
    /// </summary>
    private static void UptadeDependence()
    {
        Console.WriteLine("Enter current mission number (id) please");
        int Id = int.Parse(Console.ReadLine()!);
        Dependence? d = s_dal!.Dependence.Read(Id);
        Console.WriteLine(d); //print

        Console.WriteLine("Enter former mission");
        int DependsOnTask = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter next mission");
        int DependentTask = int.Parse(Console.ReadLine()!);

        //checks
        if (DependsOnTask == '\t') DependsOnTask = ((int)d!.DependsOnTask!);
        if (DependentTask == '\t') DependentTask = ((int)d!.DependentTask!);

        Dependence dep = new Dependence(Id, DependsOnTask, DependentTask);//create a new object of Dependence type
        s_dal!.Dependence.Update(dep);//sending to uptade

    }

    /// <summary>
    /// Delete Dependence
    /// </summary>
    private static void DeleteDependence()
    {
        Console.WriteLine("Enter i to delete:");
        int id = int.Parse(Console.ReadLine()!);
        s_dal!.Dependence.Delete(id);
    }

}


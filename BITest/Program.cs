using BO;

namespace BlTest;

internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    static void Main(string[] args)
    {
        s_bl.Project.SetDate(null);
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y") {
            s_bl.Reset();//
            DalTest.Initialization.Do(); }
        Console.WriteLine("\nCheck entity\n");
        Console.WriteLine("Choose one of the following:");
        Console.WriteLine("0: exit");
        Console.WriteLine("1: Worker"/*WorkerId"*/);
        Console.WriteLine("2: Task");
        Console.WriteLine("3: ProjectDate");
        Console.WriteLine("4: Start dates");
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
                    case 3://calling to set date
                        Console.WriteLine("Enter project start date:");
                        DateTime date = DateTime.Parse(Console.ReadLine()!);
                        s_bl.Project.SetDate(date);
                        break;//AddStartDates()
                    case 4://calling to add start date
                       
                        s_bl.Project.AddStartDates(); 
                        break;
                    default:
                        break;
                }
                Console.WriteLine("\nCheck entity\n");
                Console.WriteLine("Choose one of the following:");
                Console.WriteLine("0: exit");
                Console.WriteLine("1: Worker");
                Console.WriteLine("2: Task");
                Console.WriteLine("3: ProjectDate");
                Console.WriteLine("4: Start dates");
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
        int i = menu("WorkerId");
        do
        {
            try
            {
                switch (i)
                {
                    case 0: break;

                    case 1:
                        //create - WorkerId
                        CreateWorker();
                        break;

                    case 2:
                        //ReadAll- WorkerId
                        ReadWorker();
                        break;

                    case 3:
                        //ReadAll- WorkerId
                        ReadAllWorker();
                        break;

                    case 4:
                        //Uptade- WorkerId
                        UptadeWorker();
                        break;

                    case 5:
                        //Delete- WorkerId
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
            i = menu("WorkerId");
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
                        //ReadAll- Task
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


    /// <summary>
    /// Create WorkerId
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
     
        BO.Worker newWorker = new BO.Worker();//create a new object of worker type
        newWorker.Id = id;
        newWorker.Name = name;
        newWorker.Cost = cost;
        newWorker.Gmail = email;
        newWorker.Role = (BO.Roles)roleNum;
        s_bl!.Worker.Create(newWorker);
    }

    /// <summary>
    /// ReadAll WorkerId
    /// </summary>
    private static void ReadWorker()
    {
        Console.WriteLine("Enter id:");
        int id = int.Parse(Console.ReadLine()!);
        BO.Worker w = s_bl!.Worker.Read(id) ?? throw new NullReferenceException("WorkerId can not be null!");
        Console.WriteLine(w);
    }

    /// <summary>
    /// ReadAll All Workers
    /// </summary>
    private static void ReadAllWorker()//print every element in the nwe list
    {
        IEnumerable<BO.Worker?> workers = s_bl!.Worker.ReadAll();
        foreach (BO.Worker? worker in workers)
        {
            Console.WriteLine(worker);
        }
    }

    /// <summary>
    /// Uptade WorkerId
    /// </summary>
    private static void UptadeWorker()
    {
        Console.WriteLine("Enter id please");
        int id = int.Parse(Console.ReadLine()!);
        BO.Worker? t = s_bl!.Worker.Read(id); //print worker details
        Console.WriteLine(t);

        string? name = t.Name;
        string? email = t.Gmail;
        double? cost = t.Cost;
        BO.Roles? roleNum = t.Role;
        int choice;
        
        do
        {
            Console.WriteLine("\nUpdate worker\n");
            Console.WriteLine("Choose one of the following:");
            Console.WriteLine("0: No more details to update");
            Console.WriteLine("1: Update name");
            Console.WriteLine("2: Update email");
            Console.WriteLine("3: Update cost");
            Console.WriteLine("4: Update role");

            /*int*/ choice = int.Parse(Console.ReadLine()!);
            switch (choice)
            {
                case 0: break;

                case 1:
                    Console.WriteLine("Enter name please");
                    /*string?*/ name = Console.ReadLine();
                    break;

                case 2:
                    Console.WriteLine("Enter Email please");
                    /*string?*/ email = Console.ReadLine();
                    break;

                case 3:
                    Console.WriteLine("Enter cost please");
                    /*double?*/ cost = int.Parse(Console.ReadLine()!);
                    break;

                case 4:
                    Console.WriteLine("Enter role please (number)");
                    /*BO.Roles?*/ roleNum = (BO.Roles)Int16.Parse(Console.ReadLine()!);
                    break;

                default:
                    break;
            }
        } while (choice != 0);


        //checks
        if (name == "") name = t!.Name;
        if (email == "") email = t!.Gmail;
        if (cost == '\t') cost = t!.Cost;
        if (((int)roleNum! > 6) || ((int)roleNum < 0)) roleNum = t!.Role;

        BO.Worker workerToUpdate = new BO.Worker();//create a new object of worker type
        workerToUpdate.Id = id;
        workerToUpdate.Name = name;
        workerToUpdate.Cost = cost;
        workerToUpdate.Gmail = email;
        workerToUpdate.Role = roleNum;

        s_bl!.Worker.Update(workerToUpdate);//sending to Update
    }

    /// <summary>
    /// Delete WorkerId
    /// </summary>
    private static void DeleteWorker()
    {
        Console.WriteLine("Enter id to delete:");
        int id = int.Parse(Console.ReadLine()!);
        s_bl!.Worker.Delete(id);
    }





    /// <summary>
    /// create task
    /// </summary>
    private static void CreateTask()
    {
        Console.WriteLine("Enter task alias");
        string? alias = Console.ReadLine();
        Console.WriteLine("Enter task description");
        string? description = Console.ReadLine();
        Console.WriteLine("Enter required effort time");
        if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan requiredEffortTime))
            throw new FormatException("Wrong input");
        Console.WriteLine("Enter deliverable");
        string? deliverable = Console.ReadLine();
        Console.WriteLine("Enter remark");
        string? remark = Console.ReadLine();
        Console.WriteLine("Enter complexity");
        int complexity = int.Parse(Console.ReadLine()!);

        BO.Task t = new BO.Task();
        Console.WriteLine("Do you want to insert dependent tasks write yes or no");
        var res = Console.ReadLine();

        if (res?.ToLower() is "yes")
        {
            Console.WriteLine("insert dependent tasks ids with , between the numbers: ");
            t.Dependecies = Console.ReadLine()?.Trim().Split(',')
                .Select(num => new TaskInList { Id = int.Parse(num) }).ToList(); 
        }
      
        t.Alias = alias;
        t.Description = description;
        t.CreatedDate = DateTime.Now;
        t.RequiredEffortTime = requiredEffortTime;
        t.Deliverables = deliverable;
        t.Remarks = remark;
        t.Complexity = (BO.Roles)complexity;

        s_bl!.Task.Create(t);//sending to create
    }

    /// <summary>
    /// ReadAll Task
    /// </summary>
    private static void ReadTask()
    {
        Console.WriteLine("Enter id:");
        int id = int.Parse(Console.ReadLine()!);
        BO.Task t = s_bl!.Task.Read(id) ?? throw new NullReferenceException("Task can not be null!");
        Console.WriteLine(t);
    }

    /// <summary>
    /// ReadAll All Task
    /// </summary>
    private static void ReadAllTask()
    {
        IEnumerable<BO.TaskInList?> tasks = s_bl!.Task.ReadAll();
        foreach (BO.TaskInList? task in tasks)//print every element in the nwe list
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
        BO.Task? t = s_bl!.Task.Read(id);

        Console.WriteLine(t);  //print worker details

        Console.WriteLine("Enter worker id");
        int workerId = int.Parse(Console.ReadLine()!);
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
        BO.Roles? complexity = (BO.Roles)Int16.Parse(Console.ReadLine()!);

        //checks
       
        if (alias == "") alias = t!.Alias;
        if (description == "") description = t!.Description;
        if (createdDate == new DateTime()) createdDate = ((DateTime)t!.CreatedDate!);
        if (startTime == new DateTime()) startTime = ((DateTime)t!.StartTime!);
        if (scheduledDate == new DateTime()) scheduledDate = ((DateTime)t!.ScheduledDate!);
        if (requiredEffortTime == new TimeSpan()) requiredEffortTime = ((TimeSpan)t!.RequiredEffortTime!);
        //if (deadlineDate == new DateTime()) deadlineDate = ((DateTime)t!.DeadlineDate!);
        if (completeDate == new DateTime()) completeDate = ((DateTime)t!.CompleteDate!);
        if (deliverable == "") deliverable = t!.Deliverables;
        if (remark == "") remark = t!.Remarks;
        if (((int)complexity < 1) || ((int)complexity > 6)) complexity = t!.Complexity;

        BO.Task tsk = new BO.Task();//create a new object of Task type

       // tsk.Worker = workerId;
        tsk.Alias = alias;
        tsk.Description = description;
        tsk.CreatedDate = createdDate;
        tsk.StartTime = startTime;
        tsk.ScheduledDate = scheduledDate;
        tsk.RequiredEffortTime = requiredEffortTime;
        tsk.CompleteDate = completeDate;
        tsk.Deliverables = deliverable;
        tsk.Remarks = remark;
        tsk.Complexity = (BO.Roles?)complexity;
        s_bl!.Task.Update(tsk);//sending to update
        s_bl!.Task.AssignToWorker(tsk, s_bl.Worker.Read(workerId));//sending to update

    }

    /// <summary>
    /// Delete Task
    /// </summary>
    private static void DeleteTask()
    {
        Console.WriteLine("Enter i to delete:");
        int id = int.Parse(Console.ReadLine()!);
        s_bl!.Task.Delete(id);
    }






















}





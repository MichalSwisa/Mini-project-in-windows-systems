using BO;
using PL.Task;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Worker;

/// <summary>
/// Interaction logic for Worker_sViewWindow.xaml
/// </summary>
/// 
public partial class Worker_sViewWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public BO.Worker? Worker
    {
        get { return (BO.Worker?)GetValue(WorkerListProperty); }
        set { SetValue(WorkerListProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Worker.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty WorkerListProperty =
        DependencyProperty.Register("Worker", typeof(BO.Worker), typeof(Worker_sViewWindow), new PropertyMetadata(null));



    public IEnumerable<BO.Task> TasksOfWorker
    {
        get { return (IEnumerable<BO.Task>)GetValue(TaskOfWorkerProperty); }
        set { SetValue(TaskOfWorkerProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TasksOfWorker.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TaskOfWorkerProperty =
        DependencyProperty.Register("TasksOfWorker", typeof(IEnumerable<BO.Task>), typeof(Worker_sViewWindow), new PropertyMetadata(null));

    public BO.Task CurrentTaskOfWorker
    {
        get { return (BO.Task)GetValue(CurrentTaskOfWorkerProperty); }
        set { SetValue(CurrentTaskOfWorkerProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CurrentTaskOfWorker.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentTaskOfWorkerProperty =
        DependencyProperty.Register("CurrentTaskOfWorker", typeof(BO.Task), typeof(Worker_sViewWindow), new PropertyMetadata(null));


    //public BO.Task Task
    //{
    //    get { return (BO.Task)GetValue(TaskProperty); }
    //    set { SetValue(TaskProperty, value); }
    //}

    //// Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
    //public static readonly DependencyProperty TaskProperty =
    //    DependencyProperty.Register("Task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));

    public IEnumerable<BO.TaskInList> TasksA
    {
        get { return (IEnumerable<BO.TaskInList>)GetValue(TasksProperty); }
        set { SetValue(TasksProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TasksProperty =
        DependencyProperty.Register("Tasks", typeof(IEnumerable<BO.TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));

    //private HashSet<int> _selectedTasks
    //{
    //    get { return (HashSet<int>)GetValue(SelectedTasksProperty); }
    //    set { SetValue(SelectedTasksProperty, value); }
    //}

    //// Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
    //public static readonly DependencyProperty SelectedTasksProperty =
    //    DependencyProperty.Register(nameof(_selectedTasks), typeof(HashSet<int>), typeof(TaskWindow), new PropertyMetadata(null));


    public Worker_sViewWindow(int id)
    {
        try {
            InitializeComponent();
            Worker = s_bl.Worker.Read(id);

            TasksOfWorker = s_bl.Task.GetAllTasksThatCanBeAssignToWorker(Worker);
          
            if(Worker.CurrentTask is not null)
            {
                CurrentTaskOfWorker = s_bl.Task.Read(Worker!.CurrentTask!.TaskID);
            }
            
        }
        catch (BO.BlDoesNotExistException ex)
        {
            Worker = null;
            MessageBox.Show(ex.Message, "No Worker", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            this.Close();
        }
    }

   

    //private void TaskSelected(object sender, RoutedEventArgs e)
    //{
    //    var value = (sender as CheckBox)!.IsChecked;
    //    var task = ((sender as FrameworkElement)!.DataContext) as BO.TaskInList;
        
        
    //    if (task is not null)
    //    {
    //        if (value is bool isTaskSelected) _selectedTasks.Add(task.Id);
    //        else _selectedTasks.Remove(task.Id);
    //    }
    //}

    //private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    s_bl.Task.ReadAll();
    //}

   

   
    private void UpdateFields()
    {
        Worker = s_bl.Worker.Read(Worker!.Id);
        TasksA = s_bl.Task.ReadAll(t => t.WorkerId == null && t.Complexity <= Worker.Role);

    }
 
    private void SelectTask(object sender, MouseButtonEventArgs e)
    {
        if ((sender as ListView)!.SelectedItem is BO.Task task)
        {
            try
            {
                s_bl.Task.StartTask(task.Id, (int)Worker!.Id);  //??????????? int to int?
                MessageBox.Show("Task added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateFields();
        }
    }

    private void FinishTaskBtn(object sender, RoutedEventArgs e)
    {
        if (Worker?.CurrentTask == null)
        {
            MessageBox.Show("First you need to get a new task");
            return;
        }

        var ans = MessageBox.Show("Are you sure you finished your task?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (ans == MessageBoxResult.No)
            return;

        try
        {
            s_bl.Task.FinishTask(Worker.CurrentTask.TaskID);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        UpdateFields();
    }

    private void returnButton(object sender, RoutedEventArgs e)
    {
        new LoginWindow().Show();
        this.Close();
    }
}

using BO;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
namespace PL.Task;

/// <summary>
/// Interaction logic for TaskWindow.xaml
/// </summary>
/// 
public partial class TaskWindow : Window
{

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public BO.Task Task
    {
        get { return (BO.Task)GetValue(TaskProperty); }
        set { SetValue(TaskProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TaskProperty =
        DependencyProperty.Register("Task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));


    public ObservableCollection<BO.TaskInList> Tasks
    {
        get { return (ObservableCollection<BO.TaskInList>)GetValue(TasksProperty); }
        set { SetValue(TasksProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TasksProperty =
        DependencyProperty.Register("Tasks", typeof(ObservableCollection<BO.TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));

    private HashSet<int> _selectedTasks
    {
        get { return (HashSet<int>)GetValue(SelectedTasksProperty); }
        set { SetValue(SelectedTasksProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SelectedTasksProperty =
        DependencyProperty.Register(nameof(_selectedTasks), typeof(HashSet<int>), typeof(TaskWindow), new PropertyMetadata(null));


    public IEnumerable<BO.Worker> Workers
    {
        get { return (IEnumerable<BO.Worker>)GetValue(WorkerInTProperty); }
        set { SetValue(WorkerInTProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Workers.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty WorkerInTProperty =
        DependencyProperty.Register("Workers", typeof(IEnumerable<BO.Worker>), typeof(TaskWindow), new PropertyMetadata(null));

    public TaskWindow(int id = 0)
    {

        try
        {
            _selectedTasks = new HashSet<int>();
            var isUpdate = id != 0;

            Task = isUpdate ? s_bl!.Task.Read(id)! : new BO.Task() { Dependecies = new() };

            _selectedTasks = Task.Dependecies!.Select(task => task.Id).ToHashSet();

            Tasks = new ObservableCollection<TaskInList>(s_bl?.Task.ReadAll(t => t.Id != id)!);

            _selectedTasks.Clear();
            //Workers = s_bl?.Worker.ReadAll(worker => s_bl.Task.IsTaskCanBeAssigntToWorker(worker, Task))!;
        }
        catch (BO.BlDoesNotExistException ex)
        {
            MessageBox.Show(ex.Message, "Operation Faild", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Operation Faild", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            this.Close();
        }
        InitializeComponent();
    }

    private void btn_Add_Update_Task(object sender, RoutedEventArgs e)
    {
        updateTaskDependencies();

        if ((sender as Button)!.Content.ToString() == "Add")
        {
            try
            {
                int? id = s_bl.Task.Create(Task!);
                MessageBox.Show($"Task {id} was successfully added!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            catch (BO.BlAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message, "Operation Faild", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Operation Faild", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
        else//update
        {
            try
            {

                s_bl.Task.Update(Task!);
                MessageBox.Show($"Task {Task?.Id} was successfully updated!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            catch (BO.BlDoesNotExistException ex)
            {
                MessageBox.Show(ex.Message, "Operation Faild", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Operation Faild", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }

    private void TaskListView(object sender, SelectionChangedEventArgs e)
    {
        s_bl.Task.ReadAll();
    }

   
    private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        s_bl.Task.ReadAll();
    }



    private void TaskSelected(object sender, RoutedEventArgs e)
    {
        var value = (sender as CheckBox)!.IsChecked;
        var task = ((sender as FrameworkElement)!.DataContext) as BO.TaskInList;

        if (task is not null)
        {
            if (value is bool isTaskSelected) _selectedTasks.Add(task.Id);
            else _selectedTasks.Remove(task.Id);
        }
    }



    private void DependenciesToRemove(object sender, RoutedEventArgs e)
    {
        var value = (sender as CheckBox)!.IsChecked;
        var task = ((sender as FrameworkElement)!.DataContext) as BO.TaskInList;

        if (task is not null)
        {
            if (value is bool isTaskSelected) _selectedTasks.Add(task.Id);
            else _selectedTasks.Remove(task.Id);
        }
    }
    private void SaveDependencies(object sender, RoutedEventArgs e)
    {
        updateTaskDependencies();
    }

    private void updateTaskDependencies()
    {
        try
        {
        
            Task.Dependecies = Tasks.Where(task => _selectedTasks.Contains(task.Id)).ToList();  
            s_bl.Task.AddOrUpdateDependecies(Task!);

            //Tasks = new ObservableCollection<TaskInList>(Tasks.Where(task => !_selectedTasks.Contains(task.Id)).ToList());
            _selectedTasks.Clear();

            MessageBox.Show($"Dependecies was saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        catch (BO.BlDoesNotExistException ex)
        {
            MessageBox.Show(ex.Message, "Operation Faild", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Operation Faild", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

    }

    private void DependsOnList(object sender, SelectionChangedEventArgs e)
    {

    }

    //private void DependsOnList(object sender, SelectionChangedEventArgs e)
    //{


    //}








    //public IEnumerable<BO.TaskInList> TaskList
    //{
    //    get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
    //    set { SetValue(TaskListProperty, value); }
    //}

    //public static readonly DependencyProperty TaskListProperty =
    //    DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));
}

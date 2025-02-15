using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Task;

/// <summary>
/// Interaction logic for TasksListWindow.xaml
/// </summary>

public partial class TasksListWindow : Window     
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public BO.Status StatusOfProject
    {
        get { return (Status)GetValue(MyPropertyProperty); }
        set { SetValue(MyPropertyProperty, value); }
    }

    // Using a DependencyProperty as the backing store for StatusOfProject.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyPropertyProperty =
        DependencyProperty.Register("StatusOfProject", typeof(Status), typeof(TasksListWindow), new PropertyMetadata(null));



    public IEnumerable<BO.TaskInList> TaskList
    {
        get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }

    public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(TasksListWindow), new PropertyMetadata(null));

    public BO.TaskStatus StatusFilter { get; set; } = BO.TaskStatus.None;

 
    private void CmbWorkerFilterByStatus(object sender, SelectionChangedEventArgs e)
    {
        TaskList = (StatusFilter == BO.TaskStatus.None) ?
        s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Status == StatusFilter)!;
    }

    public TasksListWindow()
    {
        InitializeComponent();
        TaskList = s_bl?.Task.ReadAll()!;
       // StatusOfProject = s_bl!.Project.GetProjectStatus();//Kבדוק אם עובד בלי זה
    }


    private void AddTask(object sender, RoutedEventArgs e)
    {
        //if (s_bl.Project.GetProjectStatus() == BO.blabla.Unscheduled)
        //{
            
        //    new TaskWindow().Show();
        //}
        //else
        //{

        new TaskWindow().Show();
        //}
    }

    private void DoubleClickUpdateTask(object sender, MouseButtonEventArgs e)
    {
        BO.TaskInList? Task = (sender as ListView)?.SelectedItem as BO.TaskInList;

        new TaskWindow(Task!.Id).Show();
    }

    private void Window_Activated(object sender, EventArgs e)
    {
        TaskList = (StatusFilter == BO.TaskStatus.None) ?
        s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Status == StatusFilter)!;
    }

    private void returnButton(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }
}

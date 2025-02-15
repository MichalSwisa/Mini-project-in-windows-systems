using System.Text;
using PL.Task;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using BO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.Worker;
using BlApi;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public BO.Status ProjectStatus
    {
        get { return (BO.Status)GetValue(ProjectStatusProperty); }
        set { SetValue(ProjectStatusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ProjectStatus.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ProjectStatusProperty =
        DependencyProperty.Register("ProjectStatus", typeof(BO.Status),
            typeof(MainWindow), new PropertyMetadata(BO.Status.Unscheduled));

    public DateTime? ProjectStartDate
    {
        get { return (DateTime?)GetValue(ProjectStartDateProperty); }
        set { SetValue(ProjectStartDateProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ProjectStartDate.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ProjectStartDateProperty =
        DependencyProperty.Register("ProjectStartDate", typeof(DateTime?), typeof(MainWindow), new PropertyMetadata(null));

    public MainWindow()
    {
        InitializeComponent();
    }

    private void btnTasks_Click(object sender, RoutedEventArgs e)
    {

        new TasksListWindow().Show();
    }

    private void btnInitialization_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result =
        MessageBox.Show("Are you sure you want to initializate?", "Initialization", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            DalTest.Initialization.Do();
        }
    }

    private void btnReset_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result =
        MessageBox.Show("Are you sure you want to Reset?", "Reset", MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            BlApi.Factory.Get().Reset();
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        new WorkersListWindow1().Show();
        Hide();
    }

    private void GantButton(object sender, RoutedEventArgs e)
    {
        new Gant().Show();
        Hide();
    }

    private void SchedualButton(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result =
        MessageBox.Show("Are you sure you want to schedual all dates?", "schedual", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        
        {
            s_bl.Project.SetDate(ProjectStartDate);

            s_bl.Project.AddStartDates();
            ProjectStatus = s_bl.Project.GetProjectStatus();
        }
       
    }

    private void returnButton(object sender, RoutedEventArgs e)
    {
        new LoginWindow().Show();
        Hide();
    }
}
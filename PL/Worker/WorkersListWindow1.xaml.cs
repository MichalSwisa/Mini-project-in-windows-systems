using System.Windows;
using System.Windows.Controls;


namespace PL.Worker;

/// <summary>
/// Interaction logic for WorkersListWindow1.xaml
/// </summary>
public partial class WorkersListWindow1 : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public WorkersListWindow1()
    {
        InitializeComponent();
        WorkerList = s_bl?.Worker.ReadAll()!;

    }

    public IEnumerable<BO.Worker> WorkerList
    {
        get { return (IEnumerable<BO.Worker>)GetValue(WorkerListProperty); }
        set { SetValue(WorkerListProperty, value); }
    }

    public static readonly DependencyProperty WorkerListProperty =
        DependencyProperty.Register("WorkerList", typeof(IEnumerable<BO.Worker>), typeof(WorkersListWindow1), new PropertyMetadata(null));

    public BO.Roles RoleFilter { get; set; } = BO.Roles.None;

    private void CmbWorkerFilterByRole(object sender, SelectionChangedEventArgs e)
    {
        WorkerList = (RoleFilter == BO.Roles.None) ?
        s_bl?.Worker.ReadAll()! : s_bl?.Worker.ReadAll(item => item.Role == RoleFilter)!;

    }
    private void btnAdd_Worker(object sender, RoutedEventArgs e)
    {
        new WorkerWindow().ShowDialog();

    }

    private void Window_Activated(object sender, EventArgs e)
    {
        WorkerList = (RoleFilter == BO.Roles.None) ?
        s_bl?.Worker.ReadAll()! : s_bl?.Worker.ReadAll(item => item.Role == RoleFilter)!;

    }

    private void duableClick_Update_Worker(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        BO.Worker? worker = (sender as ListView)?.SelectedItem as BO.Worker;
        if (worker is not null) new WorkerWindow(worker!.Id).ShowDialog();

       
        //this.Hide();
    }

    private void ReturnButton(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }
}

//<!--IsEnabled="{Binding Converter={StaticResource ConvertAddToIsEnableKey},Mode=OneTime}"-->
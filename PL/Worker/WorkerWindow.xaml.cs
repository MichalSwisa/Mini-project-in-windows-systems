using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Worker;

/// <summary>
/// Interaction logic for WorkerWindow.xaml
/// </summary>
public partial class WorkerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public BO.Worker? WorkerList
    {
        get { return (BO.Worker?)GetValue(WorkerListProperty); }
        set { SetValue(WorkerListProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Worker.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty WorkerListProperty =
        DependencyProperty.Register("WorkerList", typeof(BO.Worker), typeof(WorkerWindow), new PropertyMetadata(null));


    public WorkerWindow(int id = 0)
    {
        InitializeComponent();
        try
        {
            WorkerList = (id != 0) ? s_bl.Worker.Read(id)! : new BO.Worker() { Id = 0, Name = "", Cost = 0, Gmail = "", Role = BO.Roles.None, CurrentTask = null };

        }
        catch (BO.BlDoesNotExistException ex)
        {
            WorkerList = null;
            MessageBox.Show(ex.Message, "Operation Faild", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Operation Faild", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            this .Close();
        }


    }

    private void btn_Add_Update_Worker(object sender, RoutedEventArgs e)
    {
        if ((sender as Button)!.Content.ToString() == "Add")
        {
            try
            {
                int? id = s_bl.Worker.Create(WorkerList!);
                MessageBox.Show($"Worker {id} was successfully added!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                
                s_bl.Worker.Update(WorkerList!);
                MessageBox.Show($"Worker {WorkerList?.Id} was successfully updated!", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

    private void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(((TextBox)sender).Text, out int result))
        {
            MessageBox.Show("Numbers only!");
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        new WorkersListWindow1().Show();
        this.Close();
    }
}

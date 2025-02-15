using System.Windows;
using System.Windows.Controls;

namespace PL.Worker;

/// <summary>
/// Interaction logic for IdWorkerLIWindow.xaml
/// </summary>
/// 
public partial class IdWorkerLIWindow : Window
{

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


    public int Id
    {
        get { return (int)GetValue(IdProperty); }
        set { SetValue(IdProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IdProperty =
        DependencyProperty.Register("Id", typeof(int), typeof(IdWorkerLIWindow), new PropertyMetadata(null));




    public IdWorkerLIWindow()
    {
        InitializeComponent();
    }

    private void goToCurrentWorker(object sender, RoutedEventArgs e)
    {
        try
        {
            if (s_bl.Worker.Read(Id) != null)
            {
                new Worker_sViewWindow(Id).Show();
                this.Close();
            }
        }
        catch (BO.BlWrongInput ex)
        {
            MessageBox.Show(ex.Message, "Wrong Id", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        catch (BO.BlDoesNotExistException ex)
        {
            MessageBox.Show(ex.Message, "No Worker", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }

    private void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(((TextBox)sender).Text, out int result))
        {
            MessageBox.Show("Numbers only!");
        }
    }

}

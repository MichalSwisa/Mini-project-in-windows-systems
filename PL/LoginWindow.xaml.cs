using PL.Worker;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


        public DateTime CurrentTime
        {
            get { return (DateTime)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(LoginWindow), new PropertyMetadata(null));



        public LoginWindow()
        {
            InitializeComponent();
            CurrentTime = s_bl.Clock;


           // DispatcherTimer timer = new DispatcherTimer();  
        }

        private void MannagerLogin(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void WorkerLogin(object sender, RoutedEventArgs e)
        {
            new IdWorkerLIWindow().Show();
            this.Close();
        }

        private void btnAddHour_click(object sender, RoutedEventArgs e)
        {
            s_bl.PromoteTime(BO.ClockTime.Hours);
            CurrentTime = s_bl.Clock;
           
        }

        private void btnAddDay_click(object sender, RoutedEventArgs e)
        {
            s_bl.PromoteTime(BO.ClockTime.Days);
            CurrentTime = s_bl.Clock;
        }

        private void btnAddYear_click(object sender, RoutedEventArgs e)
        {
            s_bl.PromoteTime(BO.ClockTime.Years);
            CurrentTime = s_bl.Clock;
        }

        private void btn_reset_clock(object sender, RoutedEventArgs e)
        {
            s_bl.ResetTime();
            CurrentTime = s_bl.Clock;
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            s_bl.PromoteTime(BO.ClockTime.Hours);
            CurrentTime = s_bl.Clock;
        }

     
    }
}

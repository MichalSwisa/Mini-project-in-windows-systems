﻿using BO;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class Gant : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public IEnumerable<BO.TaskInList> TasksList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TasksListProperty); }
            set { SetValue(TasksListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TasksList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TasksListProperty =
            DependencyProperty.Register("TasksList", typeof(IEnumerable<BO.TaskInList>), typeof(Gant), new PropertyMetadata(null));

        public Gant()
        {
            InitializeComponent();

            if (s_bl.Project.GetProjectStatus()== BO.Status.Unscheduled)
                MessageBox.Show("The project is not scheduled", "Note", MessageBoxButton.OK, MessageBoxImage.Information);

            // update chart when back to window:
            this.Activated += (s, e) => TasksList = s_bl.Task.ReadAll().OrderBy(t => s_bl.Task.Read(t.Id).ScheduledDate);
        }
    }
}

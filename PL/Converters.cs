
using BlApi;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PL;

class ConvertIdToContent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? "Add" : "Update";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
class ConvertIdToIsEnable : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? true : false;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class IsCheckedToDependencies : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        return values[0] is BO.Task currentTask && values[1] is BO.TaskInList dependOnTask &&
            currentTask!.Dependecies!.Exists(task => task.Id == dependOnTask?.Id);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class ConvertAddToIsEnable : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BlApi.IBl s_bl = BlApi.Factory.Get();
        return s_bl.Project.GetProjectStatus() == BO.Status.Unscheduled ? true : false;
        //return (int)value == 0 ? true : false;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class ConvertVisibiltyToProjectStatus : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var projectStatus = (BO.Status)value;
        return projectStatus is BO.Status.Unscheduled ? Visibility.Visible : Visibility.Collapsed;
        //return (int)value == 0 ? true : false;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class ConvertIsEnabledToProjectStatus : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var projectStatus = (BO.Status)value;
        return projectStatus is BO.Status.Unscheduled ? true : false;
        //return (int)value == 0 ? true : false;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary> convert the date to x position in the gant chart </summary>
public class StatusToColorConverter : IValueConverter
{
    static SolidColorBrush GetBrushFromHex(string hexValue)
    {
        SolidColorBrush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom(hexValue))!;
        return brush;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is BO.TaskStatus status)
        {
            return status switch
            {
                BO.TaskStatus.Unscheduled => GetBrushFromHex("#C8A2C8"),
                BO.TaskStatus.Scheduled => GetBrushFromHex("#90EE90"),
                BO.TaskStatus.OnTrack => GetBrushFromHex("#87CEEB"),
                BO.TaskStatus.Done => GetBrushFromHex("#B0E0E6"),
                _ => GetBrushFromHex("#ADD8E6"),
            };
        }
        return GetBrushFromHex("#ADD8E6");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}
/// <summary> convert task id to width of the rectangle in the gant chart </summary>
public class TaskIdToWidth : IValueConverter
{
    static readonly IBl s_bl = BlApi.Factory.Get();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Task task = s_bl.Task.Read((int)value);
        return (task.RequiredEffortTime ?? new TimeSpan(30)).TotalDays * 2;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary> convert task id to margin of the rectangle in the gantt chart </summary>
public class TaskIdToMargin : IValueConverter
{
    static readonly IBl s_bl = BlApi.Factory.Get();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DateTime? scheduledDate = s_bl.Task.Read((int)value).ScheduledDate;
        int left = 0;
        if (scheduledDate is not null)
        {
            DateTime? startProj = s_bl.Project.GetStartDate();
            left = (int)((DateTime)scheduledDate - (DateTime)startProj!).TotalDays * 2;
        }
        return $"{left},0,0,0";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary> convert task id to full data of the task </summary>
public class TaskIdToString : IValueConverter
{
    static readonly IBl _bl = BlApi.Factory.Get();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        BO.Task task = _bl.Task.Read((int)value);
        string text = task.Description!;
        if (task.ScheduledDate is not null && task.DeadlineDate is not null)
            text += $"\nScheduled Date: {task.ScheduledDate}\nForecast Date: {task.DeadlineDate}\n";
        return text;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}




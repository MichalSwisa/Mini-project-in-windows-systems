using BO;

namespace BlApi;

public interface IProject
{
    Status GetProjectStatus();
    void AddStartDates();
    void SetDate(DateTime? date);
    DateTime? GetStartDate();
}

using Budget_Tracker.Models;
using CommunityToolkit.Mvvm.Input;

namespace Budget_Tracker.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}
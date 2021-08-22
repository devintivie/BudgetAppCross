namespace BudgetAppCross.Core.ViewModels
{
    public interface INavigationResult
    {
    }

    public class NavigationResult : INavigationResult
    {
        public bool Success { get; set; }

        public NavigationResult(bool success)
        {
            Success = success;
        }
    }
}
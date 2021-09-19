using BudgetAppCross.Models;

namespace BudgetAppCross.Core.ViewModels
{
    public interface IBillInfoViewModel
    {
        Bill Bill { get; }
        string Payee { get; }
    }
}
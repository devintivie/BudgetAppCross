//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("BudgetAppCross.Views.Budget.BudgetListPage.xaml", "Views/Budget/BudgetListPage.xaml", typeof(global::BudgetAppCross.Views.BudgetListPage))]

namespace BudgetAppCross.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\Budget\\BudgetListPage.xaml")]
    public partial class BudgetListPage : global::MvvmCross.Forms.Views.MvxContentPage<global::BudgetAppCross.Core.ViewModels.BudgetListViewModel> {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView BudgetCompanyList;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(BudgetListPage));
            BudgetCompanyList = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "BudgetCompanyList");
        }
    }
}

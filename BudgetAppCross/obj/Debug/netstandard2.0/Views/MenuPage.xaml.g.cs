//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("BudgetAppCross.Views.MenuPage.xaml", "Views/MenuPage.xaml", typeof(global::BudgetAppCross.Views.MenuPage))]

namespace BudgetAppCross.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\MenuPage.xaml")]
    public partial class MenuPage : global::MvvmCross.Forms.Views.MvxContentPage<global::BudgetAppCross.Core.ViewModels.MenuViewModel> {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::MvvmCross.Forms.Views.MvxContentPage<global::BudgetAppCross.Core.ViewModels.MenuViewModel> MainContent;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView MenuList;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(MenuPage));
            MainContent = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::MvvmCross.Forms.Views.MvxContentPage<global::BudgetAppCross.Core.ViewModels.MenuViewModel>>(this, "MainContent");
            MenuList = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "MenuList");
        }
    }
}

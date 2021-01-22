using BudgetAppCross.Models;
using BudgetAppCross.StateManagers;
using BudgetAppCross.Core.Services;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using MvvmCross;
using System.Windows.Input;
using MvvmCross.Navigation;
using BaseClasses;

namespace BudgetAppCross.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {

        #region Fields

        #endregion

        #region Properties
        public StateManager StateManager => StateManager.Instance;
        public IDataManager BudgetDatabase = Mvx.IoCProvider.Resolve<IDataManager>();
        public Messenger Messenger => Messenger.Instance;


        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Commands
        #endregion

        #region Constructors
        #endregion

        #region Methods
        
        #endregion






    }

    public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter>, IMvxViewModel
    {
        public abstract void Prepare(TParameter parameter);
    }
}

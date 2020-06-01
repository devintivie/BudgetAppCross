﻿using BudgetAppCross.Models;
using MvvmCross.Forms.Presenters.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetAppCross.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true, Title = "Agenda", WrapInNavigationPage =false)]
    public partial class SelectBudgetPage
    {
        public SelectBudgetPage()
        {
            InitializeComponent();
            
        }
    }
}
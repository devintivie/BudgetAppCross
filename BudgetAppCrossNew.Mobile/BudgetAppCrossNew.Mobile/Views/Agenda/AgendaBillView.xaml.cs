using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetAppCrossNew.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaBillView : MvxContentView
    {
        //public event EventHandler<SwipedEventArgs> Swipe;

        //public bool TestVisible { get; set; } = false;

        public AgendaBillView()
        {
            InitializeComponent();
            //GestureRecognizers.Add(GetSwipeGestureRecognizer(SwipeDirection.Left));
            //GestureRecognizers.Add(GetSwipeGestureRecognizer(SwipeDirection.Right));
            //GestureRecognizers.Add(GetSwipeGestureRecognizer(SwipeDirection.Up));
            //GestureRecognizers.Add(GetSwipeGestureRecognizer(SwipeDirection.Down));
        }

        //SwipeGestureRecognizer GetSwipeGestureRecognizer(SwipeDirection direction)
        //{
        //    var swipe = new SwipeGestureRecognizer { Direction = direction };
        //    swipe.Swiped += (sender, e) => Swipe?.Invoke(this, e);
        //    return swipe;
        //}
    }
}
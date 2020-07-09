//using Android.Content;
//using BudgetAppCross.Droid;
//using MvvmCross.Forms.Views;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(MvxContentPage), typeof(CustomRenderer))]
//namespace BudgetAppCross.Droid
//{
   
//    public class CustomRenderer : PageRenderer
//    {
//        public CustomRenderer(Context context) : base(context)
//        {
            
//        }

//        private Android.Support.V7.Widget.Toolbar toolbar;

//        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
//        {
//            base.OnElementChanged(e);

//        }

//        public override void OnViewAdded(Android.Views.View child)
//        {
//            base.OnViewAdded(child);
//            if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
//            {
//                toolbar = child as Android.Support.V7.Widget.Toolbar;
//                toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
//                var a = toolbar.ChildCount;
//            }
//        }

//        void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
//        {
//            var view = e.Child.GetType();
//            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.AppCompatTextView))
//            {
//                var textView = e.Child as Android.Support.V7.Widget.AppCompatTextView;
//                textView.TextSize = 3;
//                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
//            }
//        }
//    }
//}
using BudgetAppCross.iOS.CustomControls;
using BudgetAppCross.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace BudgetAppCross.iOS.CustomControls
{
    
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.BackgroundColor = UIColor.FromRGB(204, 153, 255);
                Control.BorderStyle = UITextBorderStyle.Line;

                Control.AllTouchEvents += Control_AllTouchEvents;
                Control.AllEditingEvents += Control_AllEditingEvents;
            }
        }

        private void Control_AllEditingEvents(object sender, System.EventArgs e)
        {
            var entry = sender as UITextField;
            var position = entry.EndOfDocument;
            entry.SelectedTextRange = entry.GetTextRange(position, position);
        }

        private void Control_AllTouchEvents(object sender, System.EventArgs e)
        {
            var entry = sender as UITextField;
            var postion = entry.EndOfDocument;
        }
    }


}
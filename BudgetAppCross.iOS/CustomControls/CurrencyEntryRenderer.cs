using BudgetAppCross.iOS.CustomControls;
using BudgetAppCross.Views;
using Foundation;
using System;
using System.Drawing;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(CurrencyEntry), typeof(CurrencyEntryRenderer))]
namespace BudgetAppCross.iOS.CustomControls
{
    
    public class CurrencyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                var view = (CurrencyEntry)Element;
                Control.Started += Control_Started;
                Control.ValueChanged += Control_ValueChanged;
                Control.EditingChanged += Control_EditingChanged;
                Control.Ended += Control_Ended;
                Control.BackgroundColor = UIColor.White;
                Control.TextColor = UIColor.Black;
                view.Placeholder = Control.Placeholder;
                //Control.TintColor = UIColor.Black;
                //Control.hint
                //SetPlaceholderTextColor(view);


                //Control.RightViewMode = UITextFieldViewMode.WhileEditing;
                //Control.RightView


                var text = Control.Text;
                if (text.Equals("0.00") || text.Equals("$0.00") || text.Equals("0"))
                {
                    Control.Text = "";
                }else 
                if (decimal.TryParse(text, out var result))
                {
                    var temp = Math.Truncate(100 * result) / 100;
                    Control.Text = temp.ToString("C");
                }
                if (Control.Placeholder == null)
                {
                    Control.Placeholder = $"$0.00";
                }
                //view.PlaceholderColor = Color.Green;
                Control.KeyboardType = UIKeyboardType.DecimalPad;
                AddDoneButton();
                
                Control.ClearButtonMode = UITextFieldViewMode.WhileEditing;
                
                //Control.AllEvents += Control_AllEvents;

                //Control.AllTouchEvents += Control_AllTouchEvents;
                //Control.AllEditingEvents += Control_AllEditingEvents;
            }
        }

        private void Control_EditingChanged(object sender, EventArgs e)
        {
            var entry = sender as UITextField;
            var text = entry.Text;
            if (text.Equals("0.00") || text.Equals("$0.00") || text.Equals("0"))
            {
                entry.Text = "";
            }
            var array = text.ToCharArray();
            var dotCount = array.Count(c => c == '.');
            var lastIndex = text.LastIndexOf('.');
            if (dotCount > 1)
            {

                entry.Text = text.Remove(lastIndex, text.Length - lastIndex);
            }
            else if (dotCount == 1)
            {
                Console.WriteLine($"text.length = {text.Length}");
                Console.WriteLine($"lastIndex = {lastIndex}");
                Console.WriteLine();
                if ((text.Length - lastIndex) > 3)
                {
                    entry.Text = text.Substring(0, lastIndex + 3);
                }
            }
        }

        private void Control_Ended(object sender, EventArgs e)
        {
            var entry = sender as UITextField;
            var text = entry.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                entry.Text = "0";
            }
            else if (decimal.TryParse(text, out var result))
            {
                var temp = Math.Truncate(100 * result) / 100;
                entry.Text = temp.ToString("C");
            }
        }

        //private void Control_AllEvents(object sender, EventArgs e)
        //{
        //    var entry = sender as UITextField;

        //    var text = entry.Text;
        //    if (text.Equals("0.00") || text.Equals("$0.00") || text.Equals("0"))
        //    {
        //        entry.Text = "";
        //    }
        //}

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            var entry = sender as UITextField;
            var text = entry.Text;
            if (text.Equals("0.00") || text.Equals("$0.00") || text.Equals("0"))
            {
                entry.Text = "";
            }
            var array = text.ToCharArray();
            var dotCount = array.Count(c => c == '.');
            var lastIndex = text.LastIndexOf('.');
            if (dotCount > 1)
            {

                entry.Text = text.Remove(lastIndex, text.Length - lastIndex);
            }
            else if (dotCount == 1)
            {
                Console.WriteLine($"text.length = {text.Length}");
                Console.WriteLine($"lastIndex = {lastIndex}");
                Console.WriteLine();
                if ((text.Length - lastIndex) > 3)
                {
                    entry.Text = text.Substring(0, lastIndex + 3);
                }
            }
        }

        private void Control_Started(object sender, EventArgs e)
        {
            var entry = sender as UITextField;

            if (entry.Text.StartsWith("$"))
            {
                entry.Text = entry.Text.Replace("$", "");
            }
        }

        //private void Control_AllEditingEvents(object sender, System.EventArgs e)
        //{
        //    var entry = sender as UITextField;
        //}

        //private void Control_AllTouchEvents(object sender, System.EventArgs e)
        //{
        //    var entry = sender as UITextField;
        //}

   //     void SetPlaceholderTextColor(CurrencyEntry view)
   //     {
   //         /* UIColor *color = [UIColor lightTextColor];
   //          * YOURTEXTFIELD.attributedPlaceholder = [[NSAttributedString alloc] initWithString:@"PlaceHolder Text" attributes:@{NSForegroundColorAttributeName: color}];
			//*/
   //         if (string.IsNullOrEmpty(view.Placeholder) == false && view.PlaceholderColor != Color.Default)
   //         {
   //             NSAttributedString placeholderString = new NSAttributedString(view.Placeholder, new UIStringAttributes() { ForegroundColor = UIColor.Green });
   //             Control.AttributedPlaceholder = placeholderString;
   //         }
   //     }

        void AddDoneButton()
        {
            var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50f, 44f));

            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
            {
                Control.ResignFirstResponder();
                var baseEntry = Element.GetType();
                ((IEntryController)Element).SendCompleted();
            });
            toolbar.Items = new UIBarButtonItem[]
            {
                new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
                doneButton
            };
            this.Control.InputAccessoryView = toolbar;
        }
    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetAppCross.iOS.CustomControls;
using BudgetAppCross.Views;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PurchaseButton), typeof(PurchaseButtonRenderer))]
namespace BudgetAppCross.iOS.CustomControls
{
    public class PurchaseButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

        }
    }
}
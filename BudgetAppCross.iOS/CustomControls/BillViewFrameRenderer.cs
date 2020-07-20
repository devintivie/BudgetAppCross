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

[assembly: ExportRenderer(typeof(BillViewFrame), typeof(BillViewFrameRenderer))]
namespace BudgetAppCross.iOS.CustomControls
{
    public class BillViewFrameRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
        }
    }
}
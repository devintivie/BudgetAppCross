using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomXamarinFormsBehaviors
{
    public class OnItemTappedBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty CommandProperty = 
            BindableProperty.Create("Command", typeof(ICommand), typeof(OnItemTappedBehavior), null);

        #region Properties
        public ListView AssociatedObject { get; private set; }
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        #endregion

        #region Overrides
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.ItemTapped += Bindable_ItemTapped;
        }
        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.ItemTapped -= Bindable_ItemTapped;
            AssociatedObject = null;
        }

        protected override void OnBindingContextChanged()
        {
            BindingContext = AssociatedObject.BindingContext;
        }
        #endregion

        #region Events
        private void Bindable_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (Command == null) { return; }
            Command.Execute(null);
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        #endregion

    }
}

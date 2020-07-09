using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomXamarinFormsBehaviors
{
    public class OnUnfocusedBehavior : Behavior<VisualElement>
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(OnUnfocusedBehavior), null);
        
        public VisualElement AssociatedObject { get; private set; }
        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.Unfocused += Bindable_Unfocused;
            
        }

        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            if(Command == null) { return; }


            Command.Execute(null);
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.Unfocused -= Bindable_Unfocused;
            AssociatedObject = null;
        }


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }

    }
}

using System;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls.Compatibility;
using MauiTunes.View;
using Microsoft.Maui.Controls;



namespace MauiTunes.Behaviors
{
    public class FocusBehavior : Behavior<Microsoft.Maui.Controls.View>
    {
        private Microsoft.Maui.Controls.View currentView;

        public static BindableProperty IsFocusedProperty = BindableProperty.Create(nameof(IsFocused), typeof(bool), typeof(FocusBehavior));

        public bool IsFocused
        {
            get => (bool)GetValue(IsFocusedProperty);
            set => SetValue(IsFocusedProperty, value);
        }

        public FocusBehavior()
        {
        }

        protected override void OnAttachedTo(Microsoft.Maui.Controls.View bindable)
        {
            base.OnAttachedTo(bindable);

            currentView = bindable;
            currentView.Unfocused += CurrentView_Unfocused;
        }

        private void CurrentView_Unfocused(object sender, FocusEventArgs e)
        {
            IsFocused = false;
        }

        protected override void OnDetachingFrom(Microsoft.Maui.Controls.View bindable)
        {
            base.OnDetachingFrom(bindable);

            currentView.Unfocused -= CurrentView_Unfocused;
            currentView = null;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsFocused) && IsFocused && currentView != null)
            {
                currentView.Focus();
            }
        }
    }
}
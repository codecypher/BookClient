
using System;
using Xamarin.Forms;
using BookClient.Extensions;
using System.ComponentModel;
using System.Linq;

namespace BookClient.Controls
{
    // Realtime Validation in Xamarin Forms with FluentValidation
    // https://chaseflorell.github.io/xamarin/2017/10/04/realtime-validation-in-xamarin-forms-with-fluentvalidation/
    // The problem with XamarinForms as it stands right now is that there is not error messaging built into any of the controls.
    // To enable this, we need to build a custom control to handle it.
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class ExtendedEntry
    {
        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
            propertyName: "ErrorText",
            returnType: typeof(string),
            declaringType: typeof(ExtendedEntry),
            defaultValue: default(string),
            BindingMode.TwoWay);

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            propertyName: "IsPassword",
            returnType: typeof(string),
            declaringType: typeof(ExtendedEntry),
            defaultValue: default(string),
            BindingMode.TwoWay);

        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(
            propertyName: "HorizontalTextAlignment",
            returnType: typeof(TextAlignment),
            declaringType: typeof(ExtendedEntry),
            defaultValue: TextAlignment.Start);

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
            propertyName: "Keyboard",
            returnType: typeof(Keyboard),
            declaringType: typeof(ExtendedEntry),
            defaultValue: Keyboard.Default);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: "Text",
            returnType: typeof(string),
            declaringType: typeof(ExtendedEntry),
            defaultValue: default(string),
            BindingMode.TwoWay);

        public static readonly BindableProperty ValidatesOnDataErrorsProperty = BindableProperty.Create(
            propertyName: "ValidatesOnDataErrors",
            returnType: typeof(bool),
            declaringType: typeof(ExtendedEntry),
            defaultValue: default(bool),
            propertyChanged: OnValidatesOnDataErrorsPropertyChanged);

        private Binding _binding;

        public ExtendedEntry()
        {
            InitializeComponent();
        }

        public string ErrorText
        {
            get => (string)GetValue(ErrorTextProperty);
            set => SetValue(ErrorTextProperty, value);
        }

        public string IsPassword
        {
            get => (string)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }

        /// <summary>
        ///     Keyboard summary. This is a bindable property.
        /// </summary>
        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool ValidatesOnDataErrors
        {
            get => (bool)GetValue(ValidatesOnDataErrorsProperty);
            set => SetValue(ValidatesOnDataErrorsProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            OnValidatesOnDataErrorsPropertyChanged(this, null, ValidatesOnDataErrors);
        }

        private static void OnValidatesOnDataErrorsPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var self = (ExtendedEntry)bindable;
            if (oldvalue == newvalue) return;
            if (!(self.BindingContext is INotifyDataErrorInfo vm)) return;
            vm.ErrorsChanged -= self.VmOnErrorsChanged;
            if (bool.TryParse(newvalue.ToString(), out var validatesOnDataErrors) && validatesOnDataErrors) vm.ErrorsChanged += self.VmOnErrorsChanged;
        }

        private void VmOnErrorsChanged(object sender, DataErrorsChangedEventArgs args)
        {
            var model = (INotifyDataErrorInfo)sender;
            _binding = _binding ?? this.GetBinding(TextProperty);
            if (string.IsNullOrWhiteSpace(_binding.Path)) throw new ArgumentNullException($"{nameof(_binding.Path)} cannot be null");
            if (args.PropertyName != _binding.Path) return;
            var error = model.GetErrors(args.PropertyName)?.Cast<string>().First();
            ErrorText = error;
        }
    }
}

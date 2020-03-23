using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BookClient.Validators
{
    /// <summary>
    /// Highlights the value entered by the user in an Entry control in red, if it's not a double
    /// </summary>
    // Xamarin.Forms Behaviors
    // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/creating
    // Input Validation In Xamarin Forms Behaviors
    // https://www.c-sharpcorner.com/article/input-validation-in-xamarin-forms-behaviors/
    // Xamarin Forms Triggers vs Behaviors vs Effects
    // https://xamarinhelp.com/xamarin-forms-triggers-behaviors-effects/
    public class NumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            double result;
            bool isValid = double.TryParse(args.NewTextValue, out result);
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}

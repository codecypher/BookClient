using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BookClient.Validators
{
    /// <summary>
    /// A DatePicker is restricted to a max date of 100 years from the current day 
    /// and min date to be one year from current day.
    /// </summary>
    // Xamarin.Forms Behaviors
    // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/creating
    // Input Validation In Xamarin Forms Behaviors
    // https://www.c-sharpcorner.com/article/input-validation-in-xamarin-forms-behaviors/
    // Xamarin Forms Triggers vs Behaviors vs Effects
    // https://xamarinhelp.com/xamarin-forms-triggers-behaviors-effects/
    public class DateValidationBehavior : Behavior<DatePicker>
    {
        protected override void OnAttachedTo(DatePicker datepicker)
        {
            datepicker.DateSelected += Datepicker_DateSelected;
            base.OnAttachedTo(datepicker);
        }

        private void Datepicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            DateTime value = e.NewDate;
            int year = DateTime.Now.Year;
            int selyear = value.Year;
            int result = selyear - year;
            bool isValid = false;
            if (result <= 100 && result > 0)
            {
                isValid = true;
            }
           ((DatePicker)sender).BackgroundColor = isValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(DatePicker datepicker)
        {
            datepicker.DateSelected -= Datepicker_DateSelected;
            base.OnDetachingFrom(datepicker);
        }


    }
}

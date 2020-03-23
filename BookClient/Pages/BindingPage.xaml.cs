using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookClient.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookClient.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BindingPage : ContentPage
	{
        private Flag CurrentFlag = new Flag("US", new DateTime(1970, 1, 1), false, "United States Flag", new Uri("https://www.usflag.gov"));

		public BindingPage ()
		{
			InitializeComponent();
            this.BindingContext = CurrentFlag;
        }

        // Add support for property change notifications
        // We want to modify the Flag object in some way that is independent of the UI.
        private async void OnShow(object sender, EventArgs e)
        {
            CurrentFlag.DateAdopted = CurrentFlag.DateAdopted.AddYears(1);

            await DisplayAlert(CurrentFlag.Country,
                $"{CurrentFlag.DateAdopted:D} - {CurrentFlag.IncludesShield}: {CurrentFlag.InfoUrl}",
                "OK");
        }
    }
}
using BookClient.PageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookClient.Pages
{
    // Simplifying Events with Commanding
    // https://blog.xamarin.com/simplifying-events-with-commanding/
    // Xamarin.Forms Behaviors
    // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/creating
    // Input Validation In Xamarin Forms Behaviors
    // https://www.c-sharpcorner.com/article/input-validation-in-xamarin-forms-behaviors/
    // Xamarin Forms Triggers vs Behaviors vs Effects
    // https://xamarinhelp.com/xamarin-forms-triggers-behaviors-effects/
    //[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SquareRootPage : ContentPage
	{
        //readonly SquareRootPageModel _viewModel;

        public SquareRootPage ()
		{
            //_viewModel = new SquareRootViewModel();
            //BindingContext = _viewModel;
            InitializeComponent();
            //finishButton.Clicked += async (s, e) => await Navigation.PopModalAsync();
        }
    }
}
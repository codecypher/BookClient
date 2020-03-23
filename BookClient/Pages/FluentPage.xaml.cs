using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookClient.Pages
{
    // Fluent Validation With MVVM In Xamarin Forms Application
    // https://www.c-sharpcorner.com/article/fluent-validation-with-mvvm-in-xamarin-forms-application/
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FluentPage : ContentPage
	{
        //SignUpPageModel _viewModel;

        public FluentPage ()
		{
			InitializeComponent ();
            //_viewModel = new SignUpPageModel();
            //BindingContext = _viewModel;
        }
    }
}
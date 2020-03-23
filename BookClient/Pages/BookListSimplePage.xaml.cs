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
    // The view is responsible for defining the structure, layout, and appearance 
    // of what the user sees on screen. Ideally, each view is defined in XAML, 
    // with a limited code-behind that does not contain business logic. 
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookListSimplePage : ContentPage
	{
        private BookCollectionPageModel _viewModel;

		public BookListSimplePage()
		{
			InitializeComponent();
            _viewModel = new BookCollectionPageModel();
            BindingContext = _viewModel;
            finishButton.Clicked += async (s, e) => await Navigation.PopModalAsync();
        }
    }
}
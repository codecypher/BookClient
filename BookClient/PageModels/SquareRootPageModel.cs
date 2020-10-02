using FreshMvvm;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookClient.PageModels
{
    // FreshMvvm for Xamarin.Forms - Samples
    // https://github.com/rid00z/FreshMvvm
    // FreshMvvm Quick Start Guide
    // http://michaelridland.com/xamarin/freshmvvm-quick-start-guide/
    // Getting Started With Xamarin Forms FreshMVVM Framework (C# - Xaml)
    // http://bsubramanyamraju.blogspot.com/2018/03/getting-started-with-xamarin-forms.html
    // Simplifying Events with Commanding
    // https://blog.xamarin.com/simplifying-events-with-commanding/
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    class SquareRootPageModel : FreshBasePageModel
    {
        // ICommand implementations
        //private double _result;
        public double SquareRootWithParameterResult { get; private set; }
        public ICommand SquareRootWithParameterCommand { get; private set; }

        public SquareRootPageModel()
        {
            SquareRootWithParameterCommand = new Command<string>(CalculateSquareRoot);
        }

        private void CalculateSquareRoot(string value)
        {
            double num = Convert.ToDouble(value);
            SquareRootWithParameterResult = Math.Sqrt(num);
        }

        //public double SquareRootWithParameterResult
        //{
        //    get => _result;
        //    set => _result = value;
        //}
    }
}

using BookClient.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookClient.PageModels
{
    // Calling an Asynchronous Method
    // The following code example shows a sample application command implementation
    // that simulates a file downloading through an asynchronous method
    // https://blog.xamarin.com/simplifying-events-with-commanding/
    class DemoPageModel : ObservableObject
    {
        bool canDownload = true;
        string simulatedDownloadResult;

        public string SimulatedDownloadResult
        {
            get { return simulatedDownloadResult; }
            private set
            {
                if (simulatedDownloadResult != value)
                {
                    simulatedDownloadResult = value;
                    OnPropertyChanged("SimulatedDownloadResult");
                }
            }
        }

        public ICommand SimulateDownloadCommand { get; private set; }

        public DemoPageModel()
        {
            SimulateDownloadCommand = new Command(async () => await SimulateDownloadAsync(), () => canDownload);
        }

        async Task SimulateDownloadAsync()
        {
            CanInitiateNewDownload(false);
            SimulatedDownloadResult = string.Empty;
            await Task.Run(() => SimulateDownload());
            SimulatedDownloadResult = "Simulated download complete";
            CanInitiateNewDownload(true);
        }

        void CanInitiateNewDownload(bool value)
        {
            canDownload = value;
            ((Command)SimulateDownloadCommand).ChangeCanExecute();
        }

        void SimulateDownload()
        {
            // Simulate a 5 second pause
            var endTime = DateTime.Now.AddSeconds(5);
            while (true)
            {
                if (DateTime.Now >= endTime)
                {
                    break;
                }
            }
        }
    }
}

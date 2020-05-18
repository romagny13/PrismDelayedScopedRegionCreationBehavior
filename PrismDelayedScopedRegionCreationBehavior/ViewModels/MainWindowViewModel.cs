using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismDelayedScopedRegionCreationBehavior.Views;

namespace PrismDelayedScopedRegionCreationBehavior.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private DelegateCommand showShellCommand;

        public MainWindowViewModel()
        {

        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }


        public DelegateCommand ShowShellCommand
        {
            get
            {
                if (showShellCommand == null)
                    showShellCommand = new DelegateCommand(ExecuteShowShellCommand);
                return showShellCommand;
            }
        }

        private void ExecuteShowShellCommand()
        {
            var window = new MainWindow();
            RegionManager.SetRegionManager(window, new RegionManager());
            window.Show();
        }

    }
}

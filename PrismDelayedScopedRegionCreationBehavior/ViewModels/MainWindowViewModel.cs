using Prism.Commands;
using Prism.Regions;
using PrismDelayedScopedRegionCreationBehavior.Views;
using System;

namespace PrismDelayedScopedRegionCreationBehavior.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IRegionManagerAware
    {
        private IShellService shellService;
        private DelegateCommand showShellCommand;

        public MainWindowViewModel(IShellService shellService)
        {
            if (shellService is null)
                throw new ArgumentNullException(nameof(shellService));

            this.shellService = shellService;
            Title = "Prism Application";
        }

        public IRegionManager RegionManager { get; set; }

        public DelegateCommand ShowShellCommand
        {
            get
            {
                if (showShellCommand == null)
                    showShellCommand = new DelegateCommand(ExecuteShowShellCommand);
                return showShellCommand;
            }
        }

        internal void OnLoaded()
        {
            IRegion region = RegionManager.Regions["ContentRegion"]; // must be not null (DelayedScopedRegionCreationBehavior UpdatingRegions + Loaded)
        }


        private void ExecuteShowShellCommand()
        {
            shellService.ShowShell<MainWindow>();
        }
    }
}

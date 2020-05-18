using Prism.Commands;
using Prism.Regions;
using Prism.Regions.Behaviors;
using PrismDelayedScopedRegionCreationBehavior.Views;
using System;

namespace PrismDelayedScopedRegionCreationBehavior.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IRegionManagerAware
    {
        private IShellService shellService;
        private IScopedRegionCollection scopedRegionCollection;
        private DelegateCommand showShellCommand;
        private DelegateCommand<string> navigateAllCommand;

        public MainWindowViewModel(IShellService shellService, IScopedRegionCollection scopedRegionCollection)
        {
            if (shellService is null)
                throw new ArgumentNullException(nameof(shellService));
            if (scopedRegionCollection is null)
                throw new ArgumentNullException(nameof(scopedRegionCollection));

            this.shellService = shellService;
            this.scopedRegionCollection = scopedRegionCollection;
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

        public DelegateCommand<string> NavigateAllCommand
        {
            get
            {
                if (navigateAllCommand == null)
                    navigateAllCommand = new DelegateCommand<string>(ExecuteNavigateAllCommand);
                return navigateAllCommand;
            }
        }

        internal void OnLoaded()
        {
            IRegion region = RegionManager.Regions["ContentRegion"]; // must be not null (DelayedScopedRegionCreationBehavior UpdatingRegions + Loaded)

            var regions = scopedRegionCollection["ContentRegion"];
        }


        private void ExecuteShowShellCommand()
        {
            shellService.ShowShell<MainWindow>();
        }

        private void ExecuteNavigateAllCommand(string target)
        {
            var regions = scopedRegionCollection["ContentRegion"];
            foreach (var region in regions)
                region.RequestNavigate(target);
        }

    }
}

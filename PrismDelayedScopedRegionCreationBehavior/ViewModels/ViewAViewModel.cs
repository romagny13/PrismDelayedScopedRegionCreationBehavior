using Prism.Commands;
using Prism.Regions;
using System;
using System.Diagnostics;

namespace PrismDelayedScopedRegionCreationBehavior.ViewModels
{

    public class ViewAViewModel : ViewModelBase, IRegionManagerAware
    {
        private DelegateCommand<string> childNavigationCommand;

        public ViewAViewModel()
        {
            Title = "View A";
        }

        internal void OnLoaded()
        {
            IRegion region = RegionManager.Regions["ChildRegion"]; // must be not null (RegionManagerAwareBehavior)

        }

        public IRegionManager RegionManager { get; set; }

        public DelegateCommand<string> ChildNavigationCommand
        {
            get
            {
                if (childNavigationCommand == null)
                    childNavigationCommand = new DelegateCommand<string>(ExecuteChildNavigationCommand);
                return childNavigationCommand;
            }
        }

        private void ExecuteChildNavigationCommand(string target)
        {
            // use RegionManager injected
            RegionManager.RequestNavigate("ChildRegion", target);
        }
    }
}

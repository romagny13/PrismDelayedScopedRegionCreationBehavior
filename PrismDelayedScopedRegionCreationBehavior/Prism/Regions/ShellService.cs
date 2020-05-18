using CommonServiceLocator;
using Prism.Common;
using System.Windows;

namespace Prism.Regions
{
    public class ShellService : IShellService
    {
        public void ShowShell<T>() where T : Window
        {
            IServiceLocator locator = ServiceLocator.Current;
            IRegionManager regionManager = new RegionManager();

            var window = locator.GetInstance<T>();

            RegionManager.SetRegionManager(window, regionManager);

            // try to Set IRegionManagerAware for Shell ViewModel
            MvvmHelpers.ViewAndViewModelAction<IRegionManagerAware>(window, v => v.RegionManager = regionManager);

            window.Show();
        }
    }
}

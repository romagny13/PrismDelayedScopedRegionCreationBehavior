using CommonServiceLocator;
using Prism.Common;
using System;
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

            //EventHandler updatingRegionsEventHandler = null;
            //RoutedEventHandler loadedEventHandler = null;

            //updatingRegionsEventHandler = (s, e) =>
            //{
            //    RegionManager.UpdatingRegions -= updatingRegionsEventHandler;
            //    window.Loaded -= loadedEventHandler;

            //};
            //loadedEventHandler = (s, e) =>
            //{
            //    RegionManager.UpdatingRegions -= updatingRegionsEventHandler;
            //    window.Loaded -= loadedEventHandler;

            //    MvvmHelpers.ViewAndViewModelAction<IRegionManagerAware>(window, v => v.RegionManager = regionManager);
            //};

            //RegionManager.UpdatingRegions += updatingRegionsEventHandler;
            //window.Loaded += loadedEventHandler;

            // try to Set IRegionManagerAware for Shell ViewModel
            MvvmHelpers.ViewAndViewModelAction<IRegionManagerAware>(window, v => v.RegionManager = regionManager);

            window.Show();
        }

        private static void SetRegionManagerAwareFor<T>(T window, IRegionManager regionManager) where T : Window
        {
            MvvmHelpers.ViewAndViewModelAction<IRegionManagerAware>(window, v => v.RegionManager = regionManager);
        }
    }
}

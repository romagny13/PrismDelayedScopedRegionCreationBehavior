using CommonServiceLocator;
using Prism.Common;
using System;
using System.Windows;

namespace Prism.Regions
{
    public class ShellService : IShellService
    {
        private IServiceLocator locator;

        public ShellService(IServiceLocator locator)
        {
            if (locator is null)
                throw new ArgumentNullException(nameof(locator));

            this.locator = locator;
        }

        public virtual void ShowShell<T>() where T : Window
        {
            IRegionManager regionManager = new RegionManager();

            var window = locator.GetInstance<T>();

            RegionManager.SetRegionManager(window, regionManager);

            // try to Set IRegionManagerAware for Shell ViewModel
            MvvmHelpers.ViewAndViewModelAction<IRegionManagerAware>(window, v => v.RegionManager = regionManager);

            InitializeShell(window);

            window.Show();
        }

        protected virtual void InitializeShell(Window shell)
        {

        }
    }
}

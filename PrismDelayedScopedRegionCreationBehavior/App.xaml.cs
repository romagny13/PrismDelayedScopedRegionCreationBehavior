using Prism.Ioc;
using Prism.Regions;
using Prism.Regions.Behaviors;
using PrismDelayedScopedRegionCreationBehavior.Views;
using System.Windows;

namespace PrismDelayedScopedRegionCreationBehavior
{

    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<DelayedRegionCreationBehavior, DelayedScopedRegionCreationBehavior>();
            base.RegisterRequiredTypes(containerRegistry);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            IRegionManager regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
        }
    }
}

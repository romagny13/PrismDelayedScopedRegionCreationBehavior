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
            // replace DelayedRegionCreationBehavior (Prism) by DelayedScopedRegionCreationBehavior> for ServiceLocator/ ContainerLocator
            containerRegistry.Register<DelayedRegionCreationBehavior, DelayedScopedRegionCreationBehavior>();
            // Shell Service
            containerRegistry.RegisterSingleton<IShellService, ShellService>();

            base.RegisterRequiredTypes(containerRegistry);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
            // add RegionManagerAwareBehavior to resolve IRegionManagerAware for Views
            regionBehaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            IRegionManager regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
        }
    }

}

using Prism.Common;
using System;
using System.Windows;

namespace Prism.Regions.Behaviors
{
    public class DelayedScopedRegionCreationBehavior : DelayedRegionCreationBehavior
    {

        public DelayedScopedRegionCreationBehavior(RegionAdapterMappings regionAdapterMappings)
            : base(regionAdapterMappings)
        {
        }

        protected override IRegion CreateRegion(DependencyObject targetElement, string regionName)
        {
            if (targetElement == null)
                throw new ArgumentNullException(nameof(targetElement));

            IRegionManager regionManager = FindRegionManagerForScope(targetElement);
            if (ContainsRegionWithName(regionManager, regionName))
                regionManager = CreateRegionManagerForScope(targetElement);

            MvvmHelpers.ViewAndViewModelAction<IRegionManagerAware>(targetElement, v => v.RegionManager = regionManager);

            return base.CreateRegion(targetElement, regionName);
        }

        protected IRegionManager FindRegionManagerForScope(DependencyObject dependencyObject)
        {
            IRegionManager regionManager = RegionManager.GetRegionManager(dependencyObject);
            if (regionManager != null)
                return regionManager;

            DependencyObject parent = LogicalTreeHelper.GetParent(dependencyObject);
            if (parent != null)
                return FindRegionManagerForScope(parent);

            return null;
        }

        protected bool ContainsRegionWithName(IRegionManager regionManager, string regionName)
        {
            if (regionManager is null)
                throw new ArgumentNullException(nameof(regionManager));
            if (regionName is null)
                throw new ArgumentNullException(nameof(regionName));

            return regionManager.Regions.ContainsRegionWithName(regionName);
        }

        protected virtual IRegionManager CreateRegionManagerForScope(DependencyObject dependencyObject)
        {
            if (dependencyObject is null)
                throw new ArgumentNullException(nameof(dependencyObject));

            IRegionManager regionManager = new RegionManager();
            RegionManager.SetRegionManager(dependencyObject, regionManager);
            return regionManager;
        }
    }
}

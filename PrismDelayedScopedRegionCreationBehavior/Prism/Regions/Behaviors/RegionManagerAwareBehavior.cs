using Prism.Common;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace Prism.Regions.Behaviors
{
    public class RegionManagerAwareBehavior : RegionBehavior
    {
        public const string BehaviorKey = "RegionManagerAwareBehavior";

        protected override void OnAttach()
        {
            if (Region.ActiveViews.Count() > 0)
                Reset();

            Region.ActiveViews.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    SetRegionManagerAwareFor(e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    ClearRegionManager(e.OldItems[0]);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Reset();
                    break;
            }
        }

        protected virtual void ClearRegionManager(object item)
        {
            MvvmHelpers.ViewAndViewModelAction<IRegionManagerAware>(item, p => p.RegionManager = null);
        }

        protected virtual void Reset()
        {
            foreach (var item in Region.ActiveViews)
                SetRegionManagerAwareFor(item);
        }

        protected virtual void SetRegionManagerAwareFor(object item)
        {
            IRegionManager regionManager = ResolveRegionManager(item);
            MvvmHelpers.ViewAndViewModelAction<IRegionManagerAware>(item, p => p.RegionManager = regionManager);
        }

        protected virtual IRegionManager ResolveRegionManager(object item)
        {
            IRegionManager regionManager = Region.RegionManager;
            FrameworkElement element = item as FrameworkElement;
            if (element != null)
            {
                IRegionManager scopedRegionManager = element.GetValue(RegionManager.RegionManagerProperty) as IRegionManager;
                if (scopedRegionManager != null)
                    regionManager = scopedRegionManager;
            }
            return regionManager;
        }
    }
}

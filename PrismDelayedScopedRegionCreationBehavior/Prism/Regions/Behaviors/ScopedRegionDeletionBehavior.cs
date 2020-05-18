using System;
using System.Windows;

namespace Prism.Regions.Behaviors
{
    public class ScopedRegionDeletionBehavior
    {
        private WeakReference elementWeakReference;
        private IRegion region;
        private IScopedRegionCollection scopedRegionCollection;

        public ScopedRegionDeletionBehavior(IScopedRegionCollection scopedRegionCollection)
        {
            if (scopedRegionCollection is null)
                throw new ArgumentNullException(nameof(scopedRegionCollection));

            this.scopedRegionCollection = scopedRegionCollection;
        }

        public IRegion Region
        {
            get { return region; }
            set { region = value; }
        }

        public DependencyObject TargetElement
        {
            get { return elementWeakReference != null ? elementWeakReference.Target as DependencyObject : null; }
            set { elementWeakReference = new WeakReference(value); }
        }

        public void Attach()
        {
            WireUpTargetElement();
        }

        public void WireUpTargetElement()
        {
            FrameworkElement frameworkElement = TargetElement as FrameworkElement;
            if (frameworkElement != null)
            {
                frameworkElement.Unloaded += Element_Unloaded;
                return;
            }

            FrameworkContentElement frameworkContentElement = TargetElement as FrameworkContentElement;
            if (frameworkContentElement != null)
            {
                frameworkContentElement.Unloaded += Element_Unloaded;
                return;
            }
        }

        public void UnWireTargetElement()
        {
            FrameworkElement frameworkElement = TargetElement as FrameworkElement;
            if (frameworkElement != null)
            {
                frameworkElement.Unloaded -= Element_Unloaded;
                return;
            }

            FrameworkContentElement frameworkContentElement = TargetElement as FrameworkContentElement;
            if (frameworkContentElement != null)
            {
                frameworkContentElement.Unloaded -= Element_Unloaded;
                return;
            }
        }

        private void Element_Unloaded(object sender, RoutedEventArgs e)
        {
            UnWireTargetElement();
            RemoveRegionFromScopedRegionManager();
        }

        protected virtual void RemoveRegionFromScopedRegionManager()
        {
            DependencyObject targetElement = this.TargetElement;
            if (targetElement == null)
                return;

            scopedRegionCollection.Remove(region.Name, region);
        }
    }

}

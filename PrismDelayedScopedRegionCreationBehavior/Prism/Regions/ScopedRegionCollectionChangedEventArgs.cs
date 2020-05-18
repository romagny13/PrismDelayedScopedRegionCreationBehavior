using System;
using System.Collections.Specialized;

namespace Prism.Regions
{
    public class ScopedRegionCollectionChangedEventArgs
    {
        private NotifyCollectionChangedAction action;
        private string regionName;
        private IRegion addedItem;
        private IRegion removedItem;

        public ScopedRegionCollectionChangedEventArgs(NotifyCollectionChangedAction action)
        {
            this.action = action;
        }

        public ScopedRegionCollectionChangedEventArgs(NotifyCollectionChangedAction action, string regionName)
        {
            if (regionName is null)
                throw new ArgumentNullException(nameof(regionName));

            this.action = action;
            this.regionName = regionName;
        }

        public ScopedRegionCollectionChangedEventArgs(NotifyCollectionChangedAction action, string regionName, IRegion changedItem)
        {
            if (regionName is null)
                throw new ArgumentNullException(nameof(regionName));

            this.action = action;
            this.regionName = regionName;

            if (action == NotifyCollectionChangedAction.Add)
                this.addedItem = changedItem;
            else if (action == NotifyCollectionChangedAction.Remove)
                this.removedItem = changedItem;
        }

        public NotifyCollectionChangedAction Action
        {
            get { return action; }
        }

        public string RegionName
        {
            get { return regionName; }
        }

        public IRegion AddedItem
        {
            get { return addedItem; }
        }

        public IRegion RemovedItem
        {
            get { return removedItem; }
        }
    }

}

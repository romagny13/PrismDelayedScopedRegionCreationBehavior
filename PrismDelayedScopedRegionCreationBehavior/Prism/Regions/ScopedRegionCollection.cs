using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Prism.Regions
{

    public class ScopedRegionCollection : IScopedRegionCollection
    {
        private Dictionary<string, List<IRegion>> items;

        public ScopedRegionCollection()
        {
            items = new Dictionary<string, List<IRegion>>();
        }

        public List<IRegion> this[string regionName]
        {
            get
            {
                List<IRegion> regions = null;
                TryGetValue(regionName, out regions);
                return regions;
            }
        }

        public bool Contains(string regionName)
        {
            if (regionName is null)
                throw new ArgumentNullException(nameof(regionName));

            return items.ContainsKey(regionName);
        }

        public bool TryGetValue(string regionName, out List<IRegion> regions)
        {
            if (regionName is null)
                throw new ArgumentNullException(nameof(regionName));

            return items.TryGetValue(regionName, out regions);
        }

        public void Add(string regionName, IRegion region)
        {
            if (regionName is null)
                throw new ArgumentNullException(nameof(regionName));
            if (region is null)
                throw new ArgumentNullException(nameof(region));

            List<IRegion> regions = null;
            if (!items.TryGetValue(regionName, out regions))
            {
                regions = new List<IRegion>();
                items.Add(regionName, regions);
            }
            regions.Add(region);
            OnPropertyChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Add, regionName, region);
        }

        public bool Remove(string regionName, IRegion region)
        {
            if (regionName is null)
                throw new ArgumentNullException(nameof(regionName));
            if (region is null)
                throw new ArgumentNullException(nameof(region));

            if (items.TryGetValue(regionName, out List<IRegion> regions))
            {
                bool removed = regions.Remove(region);
                if (removed)
                {
                    OnPropertyChanged();
                    OnCollectionChanged(NotifyCollectionChangedAction.Remove, regionName, region);
                }
                return removed;
            }
            return false;
        }

        public void RemoveAll(string regionName)
        {
            if (regionName is null)
                throw new ArgumentNullException(nameof(regionName));

            items.Remove(regionName);
            OnPropertyChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Reset, regionName);
        }

        public void Clear()
        {
            items.Clear();
            OnPropertyChanged();
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
        }

        public IEnumerator<KeyValuePair<string, List<IRegion>>> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new ScopedRegionCollectionChangedEventArgs(action));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, string regionName)
        {
            CollectionChanged?.Invoke(this, new ScopedRegionCollectionChangedEventArgs(action, regionName));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, string regionName, IRegion changedItem)
        {
            CollectionChanged?.Invoke(this, new ScopedRegionCollectionChangedEventArgs(action, regionName, changedItem));
        }

        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<ScopedRegionCollectionChangedEventArgs> CollectionChanged;
    }

}

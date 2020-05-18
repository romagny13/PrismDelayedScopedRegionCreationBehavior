using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Prism.Regions
{
    public interface IScopedRegionCollection : IEnumerable<KeyValuePair<string, List<IRegion>>>, INotifyPropertyChanged
    {
        List<IRegion> this[string regionName] { get; }

        event EventHandler<ScopedRegionCollectionChangedEventArgs> CollectionChanged;

        void Add(string regionName, IRegion region);
        void Clear();
        bool Contains(string regionName);
        bool Remove(string regionName, IRegion region);
        void RemoveAll(string regionName);
        bool TryGetValue(string regionName, out List<IRegion> regions);
    }

}

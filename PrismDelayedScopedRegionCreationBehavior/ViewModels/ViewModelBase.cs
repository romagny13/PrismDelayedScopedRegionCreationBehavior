using Prism.Mvvm;
using Prism.Regions;

namespace PrismDelayedScopedRegionCreationBehavior.ViewModels
{
    public class ViewModelBase: BindableBase
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}

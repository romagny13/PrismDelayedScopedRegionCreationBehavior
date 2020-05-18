using Prism.Mvvm;
using Prism.Regions;

namespace PrismDelayedScopedRegionCreationBehavior.ViewModels
{
    public class ViewModelBase: BindableBase, INavigationAware
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
          
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
           
        }
    }
}

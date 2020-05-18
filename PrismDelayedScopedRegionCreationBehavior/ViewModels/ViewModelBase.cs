using Prism.Mvvm;

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

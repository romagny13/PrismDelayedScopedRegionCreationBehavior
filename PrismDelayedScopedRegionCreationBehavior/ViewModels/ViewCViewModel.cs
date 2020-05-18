using Prism.Regions;

namespace PrismDelayedScopedRegionCreationBehavior.ViewModels
{
    public class ViewCViewModel : ViewModelBase
    {
        private string message;

        public ViewCViewModel()
        {
            Title = "View C";
        }

        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("message"))
                Message = navigationContext.Parameters.GetValue<string>("message");
        }
    }
}

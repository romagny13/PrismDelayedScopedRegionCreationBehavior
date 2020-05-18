using Prism.Regions;

namespace PrismDelayedScopedRegionCreationBehavior.ViewModels
{
    public class ViewBViewModel : ViewModelBase, IRegionMemberLifetime
    {
        public ViewBViewModel()
        {
            Title = "View B";
        }

        public bool KeepAlive => false;
    }
}

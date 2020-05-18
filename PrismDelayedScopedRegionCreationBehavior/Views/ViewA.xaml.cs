using PrismDelayedScopedRegionCreationBehavior.ViewModels;
using System.Windows.Controls;

namespace PrismDelayedScopedRegionCreationBehavior.Views
{
    /// <summary>
    /// Interaction logic for ViewA
    /// </summary>
    public partial class ViewA : UserControl
    {
        public ViewA()
        {
            InitializeComponent();

            this.Loaded += ViewA_Loaded;
        }

        private void ViewA_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            (DataContext as ViewAViewModel).OnLoaded();
        }
    }
}

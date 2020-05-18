using PrismDelayedScopedRegionCreationBehavior.ViewModels;
using System;
using System.Windows;

namespace PrismDelayedScopedRegionCreationBehavior.Views
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).OnLoaded();
        }
    }
}

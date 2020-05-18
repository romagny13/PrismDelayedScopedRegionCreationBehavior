using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismDelayedScopedRegionCreationBehavior.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string title;

        public ViewAViewModel()
        {
            title = "View A";
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}

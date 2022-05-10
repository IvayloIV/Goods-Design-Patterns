using Goods.ViewModels;
using System;

namespace Goods.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;

        private BaseViewModel currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set { currentViewModel = value; OnCurrentViewModelChanged(); }
        }

        private void OnCurrentViewModelChanged()
        {
            if (CurrentViewModelChanged != null)
            {
                CurrentViewModelChanged.Invoke();
            }
        }
    }
}

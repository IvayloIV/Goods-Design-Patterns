using Goods.Commands;
using Goods.Dao;
using Goods.Stores;
using System.Windows;
using System.Windows.Input;

namespace Goods.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ICommand NavigationFormInputCommand { get; }
        public ICommand NavigationReportsCommand { get; }
        public ICommand NavigationExitCommand { get; }

        public HomeViewModel(NavigationStore navigationStore)
        {
            NavigationFormInputCommand = new NavigateCommand<FormHomeViewModel>(navigationStore, (n) => new FormHomeViewModel(n));
            NavigationReportsCommand = new NavigateCommand<ReportHomeViewModel>(navigationStore, (n) => new ReportHomeViewModel(n));
            NavigationExitCommand = new RelayCommand(CloseMainWindow);
        }

        private void CloseMainWindow()
        {
            GoodsContextSingleton.DestroyContext();
            Application.Current.MainWindow.Close();
        }
    }
}

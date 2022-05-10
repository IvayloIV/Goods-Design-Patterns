using Goods.Dao;
using Goods.Stores;
using Goods.ViewModels;
using System.Windows;

namespace Goods
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            GoodsContextSingleton.GetContext();
            NavigationStore navigationStore = new NavigationStore();
            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}

using Goods.Commands;
using Goods.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Goods.ViewModels
{
    public class FormHomeViewModel : BaseViewModel
    {
        public ICommand NavigationStocksCommand { get; }
        public ICommand NavigationProvidersCommand { get; }
        public ICommand NavigationBackCommand { get; }

        public FormHomeViewModel(NavigationStore navigationStore)
        {
            NavigationStocksCommand = new NavigateCommand<StockFormViewModel>(navigationStore, (n) => new StockFormViewModel(n));
            NavigationProvidersCommand = new NavigateCommand<ProviderFormViewModel>(navigationStore, (n) => new ProviderFormViewModel(n));
            NavigationBackCommand = new NavigateCommand<HomeViewModel>(navigationStore, (n) => new HomeViewModel(n));
        }
    }
}

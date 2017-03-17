using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace UWPComunityToolkit.PullToRefresh.POC.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<string> _items = new ObservableCollection<string>();

        public ObservableCollection<string> Items
        {
            get { return _items; }
            set { Set(ref _items, value); }
        }

        public async Task GetMoreItems()
        {
            Debug.WriteLine("Fetching more items");

            await Task.Delay(1500);

            var count = _items.Count;

            for (int i = 0; i < 5; i++)
            {
                Items.Insert(0, $"Item {count + i}");
            }
        }
    }
}


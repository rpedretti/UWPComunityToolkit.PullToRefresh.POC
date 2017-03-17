using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using UWPComunityToolkit.PullToRefresh.POC.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace UWPComunityToolkit.PullToRefresh.POC.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        MainPageViewModel ViewModel => DataContext as MainPageViewModel;

        private void List_PullProgressChanged(object sender, Microsoft.Toolkit.Uwp.UI.Controls.RefreshProgressEventArgs e)
        {
            if (e.PullProgress == 1)
            {
                if (progressAnimation.GetCurrentState() == ClockState.Active)
                {
                    progressAnimation.Stop();
                }

                progressAnimation.Begin();
            }
            else
            {
                renderTransform.Angle = e.PullProgress * 720;
            }
        }

        private async void MyList_RefreshRequested(object sender, EventArgs e)
        {
            await ViewModel.GetMoreItems();
            if (progressAnimation.GetCurrentState() == ClockState.Active)
            {
                progressAnimation.Stop();
            }
        }
    }
}

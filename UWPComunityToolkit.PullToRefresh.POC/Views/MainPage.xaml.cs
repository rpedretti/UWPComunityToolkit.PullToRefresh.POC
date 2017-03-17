using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Diagnostics;
using UWPComunityToolkit.PullToRefresh.POC.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace UWPComunityToolkit.PullToRefresh.POC.Views
{
    public sealed partial class MainPage : Page
    {
        private bool _animating;

        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        MainPageViewModel ViewModel => DataContext as MainPageViewModel;

        private void List_PullProgressChanged(object sender, Microsoft.Toolkit.Uwp.UI.Controls.RefreshProgressEventArgs e)
        {
            if (e.PullProgress == 1 && !_animating)
            {
                _animating = true;
                if (progressAnimation.GetCurrentState() == ClockState.Active)
                {
                    progressAnimation.Stop();
                }

                Debug.WriteLine("Animating");
                progressAnimation.Begin();
            }
            else if (e.PullProgress < 1)
            {
                if (progressAnimation.GetCurrentState() == ClockState.Active)
                {
                    progressAnimation.Stop();
                    _animating = false;
                }

                renderTransform.Angle = e.PullProgress * 360;
            }
        }

        private async void MyList_RefreshRequested(object sender, EventArgs e)
        {
            Debug.WriteLine("Fetchig");
            await ViewModel.GetMoreItems();
            if (progressAnimation.GetCurrentState() == ClockState.Active)
            {
                Debug.WriteLine("stopping animation");
                progressAnimation.Stop();
                _animating = false;
            }
        }
    }
}

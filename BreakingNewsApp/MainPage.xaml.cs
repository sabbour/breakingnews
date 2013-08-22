using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.PushNotifications;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace BreakingNewsApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : BreakingNewsApp.Common.LayoutAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // When the page is initially loaded, try to load preferences from the LocalSettings
            var tagsJoined = ApplicationData.Current.LocalSettings.Values["tags"] as string;

            // If we have preferences stored, update the interface toggle switches with them
            if (tagsJoined != null)
            {
                // Split them back into a list
                var tags = tagsJoined.Split(',');
                if (tags.Contains("worldnews"))
                    worldNews.IsOn = true;
                if (tags.Contains("technews"))
                    techNews.IsOn = true;
                if (tags.Contains("sportsnews"))
                    sportsNews.IsOn = true;
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Save topic preferences event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the current push notification channel
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            // Get selected tags. Not the best way but hey, it works for the demo!
            var tags = new List<string>();
            if(worldNews.IsOn)
                tags.Add("worldnews");
            else tags.Remove("worldnews");

            if(techNews.IsOn)
                tags.Add("technews");
            else tags.Remove("technews");

            if(sportsNews.IsOn)
                tags.Add("sportsnews");
            else tags.Remove("sportsnews");

            // Important! You also need to store these preferences somewhere, either in LocalSettings or on your service, MobileService, etc.
            // For this demo, I'll store them in LocalSettings as a comma separated list
            ApplicationData.Current.LocalSettings.Values["tags"] = string.Join(",",tags);

            // Register with the notification hub, and pass the tags you are interested in
            await App.Hub.RegisterNativeAsync(channel.Uri, tags);
        }
    }
}

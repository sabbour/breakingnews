using Microsoft.ServiceBus.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BreakingNewsBackend
{
    public partial class _Default : Page
    {
        // Create a hub client using the DefaultFullSharedAccessSignature
        NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString(
            "Endpoint=sb://sabbour.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=qqlk0PPmc0RAv/HgjMOAED3zAWQ+UqAGgupsn3JFHXM=",
            "myhub"
        );

        // Since we are using native notifications, we have to construct the payload in the format
        // the service is expecting. The example below is for sending a Toast notification on Windows 8
        string toastTemplate = @"<toast><visual><binding template=""ToastText01""><text id=""1"">{0}</text></binding></visual></toast>";
            
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // This call essentialy broadcasts a push notification to ALL devices that are registered with the service
            // and registered to that tag
            var payload = string.Format(toastTemplate, "Breaking World News!");
            hub.SendWindowsNativeNotificationAsync(payload,"worldnews");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // This call essentialy broadcasts a push notification to ALL devices that are registered with the service
            // and registered to that tag
            var payload = string.Format(toastTemplate, "Breaking Tech News!");
            hub.SendWindowsNativeNotificationAsync(payload, "technews");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            // This call essentialy broadcasts a push notification to ALL devices that are registered with the service
            // and registered to that tag
            var payload = string.Format(toastTemplate, "Breaking Sports News!");
            hub.SendWindowsNativeNotificationAsync(payload, "sportsnews");
        }
    }
}
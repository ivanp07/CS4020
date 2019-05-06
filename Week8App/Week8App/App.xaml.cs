using Windows.UI.Xaml;
using System.Threading.Tasks;
using Week8App.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;

namespace Week8App
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : BootStrapper
    {
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);
            Template10.Services.LoggingService.LoggingService.Enabled = true;

            #region app settings

            // some settings must be set in app.constructor
            var settings = SettingsService.Instance;
            RequestedTheme = settings.AppTheme;
            CacheMaxDuration = settings.CacheMaxDuration;
            ShowShellBackButton = settings.UseShellBackButton;

            #endregion
        }

        public override UIElement CreateRootElement(IActivatedEventArgs e)
        {
            var service = NavigationServiceFactory(BackButton.Attach, ExistingContent.Exclude);
            return new ModalDialog
            {
                DisableBackButtonWhenModal = true,
                Content = new Views.Shell(service),
                ModalContent = new Views.Busy(),
            };
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            await Task.Delay(3500);

            var param = string.Empty;
            var protocolArgs = args as ProtocolActivatedEventArgs;
            if (protocolArgs != null)
            {
                var uri = protocolArgs.Uri;
                var decoder = new Windows.Foundation.WwwFormUrlDecoder(uri.Query);
                param = decoder.GetFirstValueByName("filter");
            }
            NavigationService.Navigate(typeof(Views.MainPage), param);
        }
    }
}

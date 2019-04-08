using System;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using WindowsApp22Minimal.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Mvvm;
using Template10.Common;
using System.Linq;
using Windows.UI.Xaml.Data;
using Template10.Services.LoggingService;
using System.Diagnostics;

namespace WindowsApp22Minimal
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : BootStrapper
    {
        public object LogginService { get; private set; }

        public App()
        {
            InitializeComponent();
            //SplashFactory = (e) => new Views.Splash(e);
            LogginService.WriteLine = new DebugWriteDelegate(Log);

            #region app settings

            // some settings must be set in app.constructor
            var settings = SettingsService.Instance;
            RequestedTheme = settings.AppTheme;
            CacheMaxDuration = settings.CacheMaxDuration;
            ShowShellBackButton = settings.UseShellBackButton;

            #endregion
        }

        private void Log(string text, Severities severity, AttributeTargets targets, string caller)
        {
            Debug.WriteLine($"{DateTime.Now.TimeOfDay.ToString()} {severity} {caller} {text} ");

        }

        public override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            return Task.CompletedTask;
        }
        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // TODO: add your long-running task here
            await NavigationService.NavigateAsync(typeof(Views.MainPage));
        }
    }
}

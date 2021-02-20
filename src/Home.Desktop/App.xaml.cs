namespace Home.Desktop
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow AppWindow { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            AppWindow = new MainWindow();
            //uwp root app needs window to show.. is it possible to skip?
            //relaunch existing instance is using wndproc so that also requires this.
            AppWindow.Show();

            base.OnStartup(e);
        }
    }
}

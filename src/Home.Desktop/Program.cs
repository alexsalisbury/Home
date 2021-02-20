namespace Home.Desktop
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using System.Windows;
    using System.Windows.Threading;

    class Program
    {
        private static Systray sysTray;

        [STAThreadAttribute()]
        public static void Main()
        {
            App app = new App();
            app.InitializeComponent();
            app.Startup += App_Startup;
            app.SessionEnding += App_SessionEnding;
            app.Run();
        }

        public static void ShowMainWindow()
        {
            App.AppWindow.Show();
            App.AppWindow.WindowState = App.AppWindow.WindowState != WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        public static void ExitApplication()
        {
            sysTray?.Dispose();
            Application.Current.Shutdown();
        }

        private static void App_Startup(object sender, StartupEventArgs e)
        {
            sysTray = new Systray(true);
        }

        private static void App_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {
            if (e.ReasonSessionEnding == ReasonSessionEnding.Shutdown || e.ReasonSessionEnding == ReasonSessionEnding.Logoff)
            {
                //delay shutdown till we close properly.
                e.Cancel = true;
                ExitApplication();
            }
        }
    }
}
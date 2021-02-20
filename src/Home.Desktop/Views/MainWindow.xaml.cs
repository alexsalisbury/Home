namespace Home.Desktop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Microsoft.Toolkit.Wpf.UI.XamlHost;
    using System.Windows.Forms.Design.Behavior;
    using System.Windows.Interop;
    using System.Windows.Threading;
    using Windows.Networking.NetworkOperators;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool IsExit { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Makes content frame null and GC call.<br>
        /// Xaml Island gif image playback not pausing fix.</br>
        /// </summary>
        public void HideWindow()
        {
            ContentFrame.Content = null;
            this.Hide();
            GC.Collect();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsExit)
            {
                e.Cancel = true;
                HideWindow();
            }
            else
            {
                //todo
            }
        }

        public void NavViewNavigate(string tag)
        {
        }


        private void MyNavView_ChildChanged(object sender, EventArgs e)
        {
        }

        private void ContentFrame_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            // fix: https://github.com/rocksdanister/lively/issues/114
            // When backspace is pressed while focused on frame hosting uwp usercontrol textbox, the key is passed to frame also.
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Back)
            {
                e.Cancel = true;
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }

        private void statusBtn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
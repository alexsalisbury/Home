using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.System;
using MahApps.Metro.IconPacks;
using Home.App.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Home.App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public LogViewModel ViewModel = new LogViewModel();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void CommandTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = CommandTb.Text;
            var prefixCandidate = CommandTemplate.GetCommandDefaultTemplate($"{text}");
            if (prefixCandidate.IsPrefixValid)
            {
                switch (prefixCandidate.IconType)
                {
                    case IconType.FontAwesome:
                        ChangeIcon(prefixCandidate.FontAwesomeIcon);
                        break;
                    case IconType.Material:
                        ChangeIcon(prefixCandidate.MaterialIcon);
                        break;
                    case IconType.ModernKind:
                        ChangeIcon(prefixCandidate.ModernIcon);
                        break;
                    default:

                        break;
                }
            }
            else
            {
                ChangeIcon(CommandTemplate.DefaultUnknownTemplate.ModernIcon);
            }
        }

        private void CommandTb_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            var text = CommandTb.Text;
            var commandCandidate = Command.GenerateCandidate(text, e.Key);

            if (e.Key == VirtualKey.Enter) // Enter the command, run it
            {
                if (commandCandidate.IsValid)
                {
                    //One of:
                    // a) RunCommand(CommandTb.Text);
                    // b) commandCandidate.Run();
                }
            }
            else if (e.Key == VirtualKey.Tab)
            { // Tab to autocomplete word if possible
                //Autocomplete();
            }
            else if (e.Key == VirtualKey.Space)
            {
            }

        }

        private void ChangeIcon(PackIconModernKind icon)
        {
            
            this.ModernIcon.Kind = icon;
            this.ResetIcons();
            this.ModernIcon.Visibility = Visibility.Visible;
        }


        private void ChangeIcon(PackIconFontAwesomeKind icon)
        {
            this.FAIcon.Kind = icon;
            this.ResetIcons();
            this.FAIcon.Visibility = Visibility.Visible;
        }


        private void ChangeIcon(PackIconMaterialKind icon)
        {
            this.MaterialIcon.Kind = icon;
            this.ResetIcons();
            this.MaterialIcon.Visibility = Visibility.Visible;
        }

        private void ResetIcons()
        {
            this.FAIcon.Visibility = Visibility.Collapsed;
            this.MaterialIcon.Visibility = Visibility.Collapsed;
            this.ModernIcon.Visibility = Visibility.Collapsed;
        }
    }
}

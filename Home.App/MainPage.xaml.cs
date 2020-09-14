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
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MahApps.Metro.IconPacks;
using Windows.System;
using System.Collections.Concurrent;
using System.Data;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Home.App
{
    public class CommandTemplate
    {
        static ConcurrentDictionary<char, CommandTemplate> DefaultTemplates = new ConcurrentDictionary<char, CommandTemplate>();
        static ConcurrentDictionary<string, CommandTemplate> CommandTemplates = new ConcurrentDictionary<string, CommandTemplate>();
        public static CommandTemplate DefaultUnknownTemplate = new CommandTemplate();
        public static CommandTemplate DefaultDiscordTemplate = new CommandTemplate() { IconType = IconType.FontAwesome, IsPrefixValid = true, FontAwesomeIcon = PackIconFontAwesomeKind.DiscordBrands };
        public static CommandTemplate DefaultWindowsTemplate = new CommandTemplate() { IconType = IconType.Material, IsPrefixValid = true, MaterialIcon = PackIconMaterialKind.MicrosoftWindows};

        public CommandType CommandType { get; private set; } = CommandType.Unknown;
        public bool IsPrefixValid { get; private set; } = false;
        public bool IsValid { get; private set; } = false;
        public PackIconModernKind ModernIcon { get; private set; } = PackIconModernKind.Home;
        public PackIconFontAwesomeKind FontAwesomeIcon { get; private set; } = PackIconFontAwesomeKind.None;
        public PackIconMaterialKind MaterialIcon { get; private set; } = PackIconMaterialKind.None;
        public IconType IconType { get; private set; } = IconType.ModernKind;

        static CommandTemplate()
        {
            DefaultTemplates.TryAdd('d', DefaultDiscordTemplate);
            DefaultTemplates.TryAdd('w', DefaultWindowsTemplate);
        }

        public CommandTemplate()
        {

        }

        public static CommandTemplate GetCommandDefaultTemplate(string text)
        {

            if (string.IsNullOrWhiteSpace(text))
            {
                return DefaultUnknownTemplate;
            }

            var spaceindex = text.IndexOf(' ');
            if (spaceindex == 1)
            {
                var pref = text[0];
                var isPrefixValid = CommandTemplate.IsPrefixKnown(pref);

                if (isPrefixValid)
                {
                    return DefaultTemplates[pref];
                }
            }

            return DefaultUnknownTemplate;
        }

        public static CommandTemplate GetCommandTemplate(string text, VirtualKey key)
        {
            if (key == VirtualKey.Space)
            {
                text = text + " ";
            }
            
            if (string.IsNullOrWhiteSpace(text))
            {
                return DefaultUnknownTemplate;
            }

            var spaceindex = text.IndexOf(' ');
            if (spaceindex == 1)
            {
                var end = text.IndexOf(' ', spaceindex);
                if (end > spaceindex)
                {
                    var command = text.Substring(0, end);
                    return CommandTemplates[command];
                }
            }

            return DefaultUnknownTemplate;
        }

        private static bool IsPrefixKnown(char prefixCandidate)
        {
            return DefaultTemplates.ContainsKey(prefixCandidate);
        }
    }

    public enum IconType
    {
        Unknown = 0,
        FontAwesome = 250,
        Material = 520,
        ModernKind = 550
    }

    public enum CommandType
    {
        Unknown = 0,
        Discord = 150,
        Windows = 900
    }

    public class Command
    {
        public CommandTemplate Template = CommandTemplate.DefaultUnknownTemplate;

        public bool IsPrefixValid => Template?.IsValid ?? false;
        public bool IsValid => Template?.IsValid ?? false;
        public CommandType CommandType => Template?.CommandType ?? CommandType.Unknown;
        public PackIconModernKind ModernIcon => Template?.ModernIcon ?? PackIconModernKind.Home;
        public PackIconFontAwesomeKind FontAwesomeIcon => Template?.FontAwesomeIcon ?? PackIconFontAwesomeKind.None;
        public IconType IconType => Template?.IconType ?? IconType.Unknown;

        public Command(string text, VirtualKey key)
        {
            var candidate = CommandTemplate.GetCommandTemplate(text, key);
            Template = candidate;
        }

        internal static Command GenerateCandidate(string text, VirtualKey key)
        {
            try
            {
                var command = new Command(text, key);

                return command;
            }
            // catch and log
            catch
            {
            }

            return null;
        }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
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

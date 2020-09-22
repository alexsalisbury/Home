using MahApps.Metro.IconPacks;
using Windows.System;
using System.Collections.Concurrent;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Home.App.Models
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
}

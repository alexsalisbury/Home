using MahApps.Metro.IconPacks;
using Windows.System;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Home.App.Models
{

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
}

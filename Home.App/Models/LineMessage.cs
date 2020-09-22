// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Home.App.Models
{
    public enum Icon
    {
        Unknown,
        Home,
        Windows = 10,
        Azure = 100,
        Discord = 1000,

    }

    public enum Level
    {
        Unknown = 0,
        Information = 1,
        Warning = 2,
        Error = 3
    } 

    public class LineMessage
    {
        public string Text { get; set; }
        public Icon Icon { get; set; }
        public Level Level { get; set; }
    }
}

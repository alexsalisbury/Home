// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Home.App.Models
{
    public enum Icon
    {
        Unknown,
        Home = 1,
        Windows = 10,
        Azure = 100,
        Discord = 200,
        WeatherCloudy = 920,
        WeatherSnowy = 970,
    }

    public enum Level
    {
        Unknown = 0,
        Information = 1,
        Warning = 2,
        Error = 3
    } 

    public enum LineActivity
    {
        Unknown = 0,
        NeverActive = 1,
        Running = 50,
        Complete = 80,
        SelfRemovalQueued = 97,
        RemovalQueued = 98,
        Removed = 99,
    }

    public class LineMessage
    {
        public bool IsWeather => Icon == Icon.WeatherCloudy || Icon == Icon.WeatherSnowy;
        public string Text { get; set; }
        public Icon Icon { get; set; }
        public Level Level { get; set; }
        public LineActivity Status { get; set; }
    }
}

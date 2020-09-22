using Windows.UI.Xaml.Media;
using Windows.UI;
using System.Collections.ObjectModel;
using Home.App.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Home.App.Models
{
    public class LogViewModel
    {
        public int HeaderIconHeight = 8;
        public int CommandIconHeight = 8;
        public int LineIconHeight = 8;

        public int CommandText = 8;
        public int LineText = 8;

        public SolidColorBrush Error = new SolidColorBrush(Colors.Red);
        public SolidColorBrush Warning = new SolidColorBrush(Colors.Yellow);
        public SolidColorBrush Online = new SolidColorBrush(Colors.YellowGreen);
        public SolidColorBrush Discord = new SolidColorBrush(Colors.MediumSlateBlue);
        public SolidColorBrush Home = new SolidColorBrush(Colors.Orange);
        public SolidColorBrush Azure = new SolidColorBrush(Colors.CornflowerBlue);
        public SolidColorBrush Windows = new SolidColorBrush(Colors.CadetBlue);
        public SolidColorBrush Weather = new SolidColorBrush(Colors.WhiteSmoke);

        public ObservableCollection<LineMessage> Message { get; set; } = new ObservableCollection<LineMessage>();
    }
}

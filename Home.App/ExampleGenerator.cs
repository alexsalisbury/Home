using Home.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.App
{
    public class ExampleGenerator
    {
        public static readonly LineMessage CommandQueued = new LineMessage()
        {
            Icon = Icon.Home,
            Level = Level.Information,
            Status = LineActivity.NeverActive, // Running?
            Text = "Command Queued."
        };

        public static IEnumerable<LineMessage> GenerateExampleStartup()
        {
            yield return new LineMessage()
            {
                Icon = Icon.Home,
                Level = Level.Information,
                Status = LineActivity.Running,
                Text = "Starting"
            };
            yield return new LineMessage()
            {
                Icon = Icon.Discord,
                Level = Level.Information,
                Status = LineActivity.Running,
                Text = "Connecting"
            };
            yield return new LineMessage()
            {
                Icon = Icon.Discord,
                Level = Level.Information,
                Status = LineActivity.NeverActive,
                Text = "Connected!"
            };
            yield return CommandQueued;
            yield return new LineMessage()
            {
                Icon = Icon.Home,
                Level = Level.Warning,
                Status = LineActivity.NeverActive,
                Text = "Nothing is currently monitoring the queue."
            };
            yield return new LineMessage()
            {
                Icon = Icon.Azure,
                Level = Level.Information,
                Status = LineActivity.NeverActive,
                Text = "Storage drives are connected."
            };
            yield return new LineMessage()
            {
                Icon = Icon.WeatherCloudy,
                Level = Level.Information,
                Status = LineActivity.Running,
                Text = "Cloudy. High 83 Low 56"
            };
            yield return new LineMessage()
            {
                Icon = Icon.WeatherSnowy,
                Level = Level.Warning,
                Status = LineActivity.NeverActive,
                Text = "Snow is expected in the next ten days"
            };
            yield return new LineMessage()
            {
                Icon = Icon.Home,
                Level = Level.Error,
                Status = LineActivity.Running,
                Text = "Error! API is offline."
            };
        }
    }
}

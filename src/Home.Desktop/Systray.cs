namespace Home.Desktop
{
    using System;
    using System.Windows.Forms;

    public partial class Systray : IDisposable
    {
        public ToolStripMenuItem UpdateTrayBtn { get; private set; }

        private readonly NotifyIcon notifyIcon = new NotifyIcon();

        public Systray(bool visibility = true)
        {
            notifyIcon.Visible = visibility;
        }
    }
}
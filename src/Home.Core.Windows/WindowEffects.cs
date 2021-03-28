namespace Home.Core.Windows
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>http://www.ocbj.net/blog/code/recreating-the-msn-window-nudge/</remarks>
    public static class WindowEffects
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <example>WindowEffects.Nudge("Notepad");</example>
        public static void Nudge(string name)
        {
            var hWnd = NativeMethods.FindWindow(name, null);
            if (hWnd == IntPtr.Zero)
            {
                return;
            }

            NativeMethods.RECT rect = new NativeMethods.RECT();
            if (NativeMethods.GetWindowRect(hWnd, ref rect))
            {
                int x = rect.Left;
                int y = rect.Top;
                Random random = new Random();
                for (int i = 0; i <= 20; i++)
                {
                    rect.Left = random.Next(x + 1, x + 15);
                    rect.Top = random.Next(y + 1, y + 15);
                    NativeMethods.SetWindowPos(hWnd, IntPtr.Zero, rect.Left, rect.Top, 0, 0, NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOZORDER);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientModSendCommand
{
    /// <summary>
    /// Class for working with sending commands to ClientMod
    /// </summary>
    public static class ClientMod
    {
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// win32 function for finding custom window
        /// </summary>
        /// <param name="lpClassName">Window Class Name</param>
        /// <param name="lpWindowName">Window Name</param>
        /// <returns>Window handle</returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private const int WM_COPYDATA = 0x4A;

        [StructLayout(LayoutKind.Sequential)]
        private struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            public string lpData;
        }

        private static IntPtr IntPtrAlloc<T>(T param)
        {
            IntPtr retValue = Marshal.AllocHGlobal(Marshal.SizeOf(param));
            Marshal.StructureToPtr(param, retValue, false);
            return (retValue);
        }

        /// <summary>
        /// Send custom command to the game
        /// </summary>
        /// <param name="szCommand">Command (quit, disconnect, etc...)</param>
        /// <returns>0 - fail, else - success</returns>
        public static int SendCommandToWindow(string szCommand)
        {
            IntPtr hWnd = FindWindow("ClientModListener", null);
            if (hWnd != null)
            {
                COPYDATASTRUCT CopyData;
                CopyData.cbData = szCommand.Length + 1;
                CopyData.dwData = IntPtr.Zero;
                CopyData.lpData = szCommand;
                IntPtr copyDataBuff = IntPtrAlloc(CopyData);
                return SendMessage(hWnd, WM_COPYDATA, IntPtr.Zero, copyDataBuff);
            }
            return 0;
        }
    }
}

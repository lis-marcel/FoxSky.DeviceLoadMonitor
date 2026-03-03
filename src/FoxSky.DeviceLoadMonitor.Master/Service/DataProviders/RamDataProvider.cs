using System.Diagnostics;
using System.Runtime.InteropServices;

#pragma warning disable CA1416 // Validate platform compatibility

namespace FoxSky.DeviceLoadMonitor.Master.Service.DataProviders
{
    public class RamDataProvider
    {
        // Definicja struktury wymaganej przez Windows API
        [StructLayout(LayoutKind.Sequential)]
        public class MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;

            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            }
        }

        // Import funkcji z biblioteki systemowej Windows
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        public string GetRamUsage()
        {
            MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();

            if (GlobalMemoryStatusEx(memStatus))
            {
                // dwMemoryLoad zwraca wartość od 0 do 100 oznaczającą % zajęcia RAM-u fizycznego
                return memStatus.dwMemoryLoad.ToString();
            }

            return "0"; // W razie błędu odczytu
        }
    }
}

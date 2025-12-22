using System.Diagnostics;

#pragma warning disable CA1416 // Validate platform compatibility

namespace FoxSky.DeviceLoadMonitor.Master.Service
{
    public class CpuDataProvider
    {
        private readonly PerformanceCounter _cpuCounter;

        public CpuDataProvider()
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public void GetCpuUsage()
        {
            _cpuCounter.NextValue().ToString();

            // Wait a second to get a valid reading
            System.Threading.Thread.Sleep(1000);

            float cpuUsage = MathF.Round(_cpuCounter.NextValue(), 0);

            Console.WriteLine($"Current CPU Usage: {cpuUsage}%");
        }
    }
}

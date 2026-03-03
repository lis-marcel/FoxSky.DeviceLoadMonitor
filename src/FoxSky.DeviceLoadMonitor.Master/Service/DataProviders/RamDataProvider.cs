using System.Diagnostics;

#pragma warning disable CA1416 // Validate platform compatibility

namespace FoxSky.DeviceLoadMonitor.Master.Service.DataProviders
{
    public class RamDataProvider
    {
        private readonly PerformanceCounter _ramCounter;

        public RamDataProvider() 
        {
            _ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        }

        public string GetRamUsage()
        {
            return MathF.Round(_ramCounter.NextValue(), 0).ToString();
        }
    }
}

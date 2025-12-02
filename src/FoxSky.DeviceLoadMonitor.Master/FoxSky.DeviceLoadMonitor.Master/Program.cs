using FoxSky.DeviceLoadMonitor.Master.Service;

namespace FoxSky.DeviceLoadMonitor.Master
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cpuService = new CpuDataProvider();

            cpuService.GetCpuUsage();
        }
    }
}

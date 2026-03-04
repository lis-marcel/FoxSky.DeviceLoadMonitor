using FoxSky.DeviceLoadMonitor.Master.Services;
using System.Threading.Tasks;

namespace FoxSky.DeviceLoadMonitor.Master
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MasterService masterService = new();

            await masterService.Run();
        }
    }
}

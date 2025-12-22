using System.Threading;
using System.Threading.Tasks;

namespace FoxSky.DeviceLoadMonitor.Master.Service
{
    public class MasterService
    {
        private readonly PeriodicTimer _timer;
        private readonly CpuDataProvider _cpuDataProvider;


        public MasterService() 
        {
            _timer = new(TimeSpan.FromSeconds(5));
            _cpuDataProvider = new CpuDataProvider();
        }

        public async Task Run()
        {
            while (await _timer.WaitForNextTickAsync())
            {
                _cpuDataProvider.GetCpuUsage();
            }
        }
    }
}

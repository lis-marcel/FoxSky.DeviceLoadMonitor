using System.Threading;
using System.Threading.Tasks;

namespace FoxSky.DeviceLoadMonitor.Master.Service
{
    public class MasterService
    {
        private readonly PeriodicTimer _timer;
        private readonly CpuDataProvider _cpuDataProvider;
        private readonly SerialWriter _serialWriter;

        public MasterService() 
        {
            _timer = new(TimeSpan.FromSeconds(5));
            _cpuDataProvider = new CpuDataProvider();
            _serialWriter = new SerialWriter();
        }

        public async Task Run()
        {
            while (await _timer.WaitForNextTickAsync())
            {
                string value = _cpuDataProvider.GetCpuUsage();

                _serialWriter.WriteData(value);
            }
        }
    }
}

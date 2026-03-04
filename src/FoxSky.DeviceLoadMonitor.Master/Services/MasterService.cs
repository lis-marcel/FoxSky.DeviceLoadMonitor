using FoxSky.DeviceLoadMonitor.Master.Service.DataProviders;
using FoxSky.DeviceLoadMonitor.Master.Services.DataProviders;

namespace FoxSky.DeviceLoadMonitor.Master.Services
{
    public class MasterService
    {
        private readonly PeriodicTimer _timer;
        private readonly CpuDataProvider _cpuDataProvider;
        private readonly RamDataProvider _ramDataProvider;
        private readonly GpuDataProvider _gpuDataProvider;
        private readonly SerialWriter _serialWriter;

        public MasterService() 
        {
            _timer = new(TimeSpan.FromSeconds(2));

            _cpuDataProvider = new CpuDataProvider();
            _ramDataProvider = new RamDataProvider();
            _gpuDataProvider = new GpuDataProvider();

            _serialWriter = new SerialWriter();
        }

        public async Task Run()
        {
            while (await _timer.WaitForNextTickAsync())
            {
                string cpuValue = _cpuDataProvider.GetCpuUsage();
                string ramValue = _ramDataProvider.GetRamUsage();
                string gpuValue = _gpuDataProvider.GetGpuUsage();

                string value = $"{cpuValue},{ramValue},{gpuValue}";

                _serialWriter.WriteData(value);
            }
        }
    }
}

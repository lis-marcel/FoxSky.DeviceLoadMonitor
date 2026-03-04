using LibreHardwareMonitor.Hardware;

namespace FoxSky.DeviceLoadMonitor.Master.Services.DataProviders
{
    public class GpuDataProvider
    {
        private readonly Computer _computer;
        private IHardware _gpu;

        public GpuDataProvider()
        {
            _computer = new Computer
            {
                IsGpuEnabled = true // Włączamy tylko odczyt GPU, żeby oszczędzać zasoby
            };

            _computer.Open();

            // Szukamy pierwszej dostępnej karty graficznej (Nvidia, AMD lub Intel)
            _gpu = _computer.Hardware.FirstOrDefault(h =>
                h.HardwareType == HardwareType.GpuNvidia ||
                h.HardwareType == HardwareType.GpuAmd ||
                h.HardwareType == HardwareType.GpuIntel);
        }

        public string GetGpuUsage()
        {
            if (_gpu == null) return "0";

            _gpu.Update(); // Pobranie najświeższych danych z czujników

            // Szukamy czujnika typu "Load" (Obciążenie) o nazwie "GPU Core"
            var coreLoadSensor = _gpu.Sensors.FirstOrDefault(s =>
                s.SensorType == SensorType.Load && s.Name == "GPU Core");

            if (coreLoadSensor != null && coreLoadSensor.Value.HasValue)
            {
                return MathF.Round(coreLoadSensor.Value.Value, 0).ToString();
            }

            return "0";
        }

        // Dobrą praktyką jest zamknięcie instancji przy zamykaniu aplikacji
        public void Close()
        {
            _computer.Close();
        }
    }
}

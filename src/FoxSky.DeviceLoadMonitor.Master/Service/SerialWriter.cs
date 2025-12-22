using System.IO.Ports;

namespace FoxSky.DeviceLoadMonitor.Master.Service
{
    internal class SerialWriter
    {
        private readonly SerialPort _serialPort;

        public SerialWriter()
        {
            _serialPort = new SerialPort("COM7", 9600, Parity.None, 8, StopBits.One);
            _serialPort.Open();
        }

        public void WriteData(string data)
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Write(data);
                }
                else
                {
                    throw new InvalidOperationException("Serial port is not open.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
}
